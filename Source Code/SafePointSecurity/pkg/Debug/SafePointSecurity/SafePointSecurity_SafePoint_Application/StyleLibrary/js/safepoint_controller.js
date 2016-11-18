var myApp = angular.module('safepointApp', ['angular.filter', 'angularUtils.directives.dirPagination', 'multiselect-searchtree', 'angular-loading-bar', 'ngTagsInput']);

//Condfigurando loanding bar:
myApp.config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.parentSelector = '#loading-bar-container';
    cfpLoadingBarProvider.includeSpinner = false;
}]);


myApp.factory('Excel', function ($window) {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8"><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    return {
        tableToExcel: function (tableId, worksheetName) {
            var table = $(tableId),
                ctx = { worksheet: worksheetName, table: table.html() },
                href = uri + base64(format(template, ctx));
            return href;
        }
    };
})

myApp.controller('safepointController', function ($scope, $http, $filter, Excel, $timeout) {
    //Pra dar sorte. ;)
    $scope.helloworld = "Hello World!!";

    $scope.exportToExcel = function (tableId) { // ex: '#my-table'
        $scope.exportHref = Excel.tableToExcel(tableId, 'SafePoint');
        $timeout(function () { location.href = $scope.exportHref; }, 100); // trigger download
    }


    $scope.currentPage = 1;
    $scope.pageSize = 20;

    //Amazena os filtros do usuario:
    $scope._filtros = [];//global

    //ResetFiltros:
    $scope.LimparFiltros = function () {
        $scope._filtros = [];
    };

    //Carregando dados externos:
    $scope.TestConnection = function () {
        $http.post("/SitePages/SafePointSecurity/SPS_Service.aspx/TesteConnection", '')
            .error(function () {
                alert('WebPart não conseguiu se comunicar com o webservice do sistema. Tente novamente mais tarde.');
            });
    };

    $scope.GetPermissoes = function () {
        return $http.post("/SitePages/SafePointSecurity/SPS_Service.aspx/GetPermissoes", '');
    };

    $scope.TestConnection();
    $scope.GetPermissoes().success(function (data) {
        $scope.permissoes = $.parseJSON(data.d);
        $scope.permissoes = _.filter($scope.permissoes, function (permissao) {
            return permissao.nivelPermissao.nome.substring(0, 11) != '$Resources:'
        });
    })
        .error(function () {
            alert('Não foi possível carregar dados do sistema. Tente novamente mais tarde. Dados fictícios serão aplicados no momento.');
            $scope.permissoes = [
                {
                    usuario: { nome: "Wilhas Mendes de Souza", id: 100 },
                    grupo: { nome: "Grupo @ 01", id: 1 },
                    site: { nome: "Administrativo", url: "/administrativo/default.aspx", id: 1000, heranca: true },
                    nivelPermissao: { nome: "Colaboracao" }
                },
                {
                    usuario: { nome: "Luiz Henrique Nunes Santos", id: 200 },
                    grupo: { nome: "Grupo @ 01", id: 1 },
                    site: { nome: "Administrativo", url: "/administrativo/default.aspx", id: 1000, heranca: true },
                    nivelPermissao: { nome: "Colaboracao" }
                },
                {
                    usuario: { nome: "Heloisa Gomes Lima", id: 300 },
                    grupo: { nome: "Grupo @ 02", id: 2 },
                    site: { nome: "Administrativo", url: "/administrativo/default.aspx", id: 1000, heranca: true },
                    nivelPermissao: { nome: "Leitura" }
                },
                {
                    usuario: { nome: "Zaire Pianzola Pasti", id: 666 },
                    grupo: { nome: "Grupo @ 02", id: 2 },
                    site: { nome: "Administrativo", url: "/administrativo/default.aspx", id: 1000, heranca: true },
                    nivelPermissao: { nome: "Controle Total" }
                },
                {
                    usuario: { nome: "Wilhas Mendes de Souza", id: 100 },
                    grupo: { nome: "Grupo @ 03", id: 3 },
                    site: { nome: "Tecnologia da Informação", url: "/ti/default.aspx", id: 4000, heranca: true },
                    nivelPermissao: { nome: "Acesso Limitado" }
                },
                {
                    usuario: { nome: "Guilherme Alvres Ribeiro", id: 500 },
                    grupo: null,
                    site: { nome: "Tecnologia da Informação", url: "/ti/default.aspx", id: 4000, heranca: true },
                    nivelPermissao: { nome: "Acesso Limitado" }
                },
            ];
        })
        .finally(function () {
            $scope.InitApplication();
        });

    $scope.InitApplication = function () {
        //preenche tabela exibida:
        $scope.tableResult = $scope.permissoes;

        //Coleta Filtros:
        $scope.Grupos = $scope.ColetarFiltros('grupo');
        $scope.Usuarios = $scope.ColetarFiltros('usuario');
        $scope.NiveisPermissao = $scope.ColetarFiltros('nivelPermissao');
        $scope.Sites = $scope.ColetarFiltros('site');
    }

    //Coletando itens unicos do array de permissoes:
    $scope.ColetarFiltros = function (key) {
        return $scope._DistinctArray($scope.permissoes, key);
    };

    $scope._DistinctArray = function (array, key) {
        return _.without($filter('unique')(
            _.mapObject(
                array,
                function (p) {
                    return _.clone(p[key]);
                }),
            'nome'), null);
    }


    //Metodo de seleção de itens:
    $scope.Selecionar = function (item, key) {
        item.selecionado = !item.selecionado;
        if (item.selecionado) {
            $scope._filtros.push(_.extend(item, { type: key }));
        }
        else {
            var index = _.findIndex($scope._filtros, function (filtro) { return filtro.nome == item.nome });
            if (index > -1) {
                $scope._filtros.splice(index, 1);
            }
        }

        $scope.FiltrarItens();
    };

    //Metodo para ordenar colunas na tabela
    $scope.sortKey = 'site';
    $scope.Ordenar = function (keyname) {
        $scope.sortKey = keyname;
        $scope.reverse = ($scope.sortKey === keyname) ? !$scope.reverse : false;
    };

    //Metodo de filtragem:
    $scope.FiltrarItens = function () {
        $scope.tableResult = _.filter($scope.permissoes, function (permissao) {
            var groupsFiltros = _.groupBy($scope._filtros, 'type');
            return _.every(groupsFiltros, function (group) {
                return _.some(group, function (filtro) {
                    if (!permissao[filtro.type]) return false;
                    return permissao[filtro.type]['nome'] == filtro.nome;
                })
            });
        });
    };


    //Preenche Membros do Grupo:
    $scope.MembrosGrupo = [];
    $scope.grupoSelecionadoAdmin = [];
    $scope.PrencheMembros = function (grupo) {
        $scope.grupoSelecionadoAdmin = grupo;
        $scope.MembrosGrupo = [];

        $scope.MembrosGrupo = $scope._DistinctArray(
            _.filter($scope.permissoes, function (permissao) {
                return ((permissao.grupo) && permissao.grupo.nome == grupo.nome);
            }),
            'usuario');

    };

    $scope.OpenUrlAddUser = function (grupo) {
        if (grupo) {
            var options =
             {
                 url: window.location.origin + '/_layouts/aclinv.aspx?GroupId=' + grupo.id + '&IsDlg=1',
                 dialogReturnValueCallback: Function.createDelegate(null, function (result, target) {
                     $scope.GetPermissoes().success(function (data) {
                         $scope.permissoes = $.parseJSON(data.d);
                         $scope.permissoes = _.filter($scope.permissoes, function (permissao) {
                             return permissao.nivelPermissao.nome.substring(0, 11) != '$Resources:'
                         });

                         $scope.Grupos = $scope.ColetarFiltros('grupo');
                         $scope.Usuarios = $scope.ColetarFiltros('usuario');
                         $scope.FiltrarItens();

                         $scope.PrencheMembros(grupo);
                     });
                 })
             };
            SP.UI.ModalDialog.showModalDialog(options);
        }
    };

    $scope.OpenUrlNovoGrupo = function () {

        var options =
         {
             url: window.location.origin + '/_layouts/15/newgrp.aspx?&IsDlg=1',
             dialogReturnValueCallback: Function.createDelegate(null, function (result, target) {
                 $scope.GetPermissoes().success(function (data) {
                     $scope.permissoes = $.parseJSON(data.d);
                     $scope.permissoes = _.filter($scope.permissoes, function (permissao) {
                         return permissao.nivelPermissao.nome.substring(0, 11) != '$Resources:'
                     });

                     $scope.Grupos = $scope.ColetarFiltros('grupo');
                     $scope.Usuarios = $scope.ColetarFiltros('usuario');
                     $scope.FiltrarItens();
                 });
             })
         };
        SP.UI.ModalDialog.showModalDialog(options);
    };


    $scope.RemoverUsuario = function (usuario, grupo) {
        if (window.confirm('Tem certeza que deseja remover o usuário ' + usuario.nome + ' do grupo ' + grupo.nome + '?')) {
            var ctx = SP.ClientContext.get_current()
            var web = ctx.get_web();
            var group = web.get_siteGroups().getById(grupo.id);
            group.get_users().removeByLoginName(usuario.login);
            ctx.executeQueryAsync(
               function () {
                   $scope.GetPermissoes().success(function (data) {
                       $scope.permissoes = $.parseJSON(data.d);
                       $scope.permissoes = _.filter($scope.permissoes, function (permissao) {
                           return permissao.nivelPermissao.nome.substring(0, 11) != '$Resources:'
                       });

                       $scope.InitApplication();
                       $scope.PrencheMembros(grupo);
                   }, function (error) { alert(error) });
               });
        }
    };


    //Montagem do do treeview de sites:
    $scope.treeview = [];
    $scope.CarregaSiteHierarquia = function () {
        $http.post("/SitePages/SafePointSecurity/SPS_Service.aspx/GetSitesHierarquia", '')
        .success(function (data) {
            $scope.allSites = $.parseJSON(data.d);

            $scope.treeview = _.filter($scope.allSites, function (site) {
                return !site.parentId;
            });

            $scope.treeview = _.map($scope.treeview, function (site) {
                site.selected = false;
                site.children = $scope.GetSitesFilhos(site, $scope.allSites);
                site.isExpanded = site.length > 0;
                site.name = site.nome;
                return site;
            });
        });
    };

    $scope.GetSitesFilhos = function (pai, arrayData) {

        //recupera os filhos:
        var filhos = _.filter(arrayData, function (filho) {
            return filho.parentId == pai.id;
        });

        //condição de saida:
        if (filhos.length == 0) return [];

        _.map(filhos, function (filho) {
            filho.selected = false;
            filho.children = $scope.GetSitesFilhos(filho, arrayData);
            filho.isExpanded = filho.length > 0;
            filho.name = filho.nome;
            return filho;
        });
        return filhos;
    };

    $scope.CarregaSiteHierarquia();


    //Metodo de seleção de site
    $scope.siteSelecionadoAdmin = [];
    $scope.SelecionarSite = function (item, selectedItems) {
        $scope.siteSelecionadoAdmin = item;

        $scope.permissao.site = item.url;
        $scope.permissao.tipo = 1;
        $scope.users = [];

        $scope.SitePermissoes = $scope._CarregarSitePermissoes(item, $scope.permissoes);

    };

    $scope._CarregarSitePermissoes = function (site, array) {
        return _.unique(_.map(_.filter(array, function (permissao) {
            return permissao.site.id == site.id;
        }), function (mapItem) {
            var principalName = (!mapItem.grupo ? mapItem.usuario.nome : mapItem.grupo.nome);
            return { nome: principalName, permissao: mapItem.nivelPermissao.nome };
        }), 'nome');
    };

    //Metodo de adição de permissao
    $scope.permissao = new PermissaoViewModel();
    $scope.AdicionarPermissao = function () {        
        $http.post('/SitePages/SafePointSecurity/SPS_Service.aspx/AddPermissao', { 'viewModel': $scope.permissao })
            .success(function (data) {
                $scope.GetPermissoes().success(function (data) {
                    $scope.permissoes = $.parseJSON(data.d);
                    $scope.permissoes = _.filter($scope.permissoes, function (permissao) {
                        return permissao.nivelPermissao.nome.substring(0, 11) != '$Resources:'
                    });

                    $scope.SitePermissoes = $scope._CarregarSitePermissoes($scope.siteSelecionadoAdmin, $scope.permissoes);

                    //$scope.users = [];
                    $scope.permissao = new PermissaoViewModel();

                    $scope.Grupos = $scope.ColetarFiltros('grupo');
                    $scope.FiltrarItens();
                });
            })
            .error(function (erro) {
                alert('Não foi possivel adicionar a permissão ao site ' + $scope.permissao.site + '. Entre em contato com o administrador do sistema.')
            });
    };

    $scope.RemoverPermissao = function (item, site) {
        if (window.confirm('Tem certeza que deseja remover ' + item.nome + ' do site ' + site.nome + '?')) {
            $scope.permissao.nome = item.nome;
            $scope.permissao.tipo = 0;
            $scope.permissao.nivelPermissao = item.permissao;
            $scope.permissao.site = site.url;

            $http.post('/SitePages/SafePointSecurity/SPS_Service.aspx/RemoverPermissao', { 'viewModel': $scope.permissao })
            .success(function (data) {
                $scope.GetPermissoes().success(function (data) {
                    $scope.permissoes = $.parseJSON(data.d);
                    $scope.permissoes = _.filter($scope.permissoes, function (permissao) {
                        return permissao.nivelPermissao.nome.substring(0, 11) != '$Resources:'
                    });

                    $scope.SitePermissoes = $scope._CarregarSitePermissoes($scope.siteSelecionadoAdmin, $scope.permissoes);
                    //$scope.users = [];
                    $scope.permissao = new PermissaoViewModel();

                    $scope.Grupos = $scope.ColetarFiltros('grupo');
                    $scope.FiltrarItens();
                });
            })
            .error(function (erro) {
                alert('Não foi possivel remover a permissão ao site ' + $scope.permissao.site + '. Entre em contato com o administrador do sistema.')
            });
        }
    }

    //Modal
    $scope.users = [];
    $scope.GetPeoplePickerSuggestion = function (searchString, site) {

        return $scope._getPeoplePickerSuggestion(searchString, site)
            .then(function (response) {
                if (!response.data.d.results) return [];
                var results = response.data.d.results;
                return results.filter(function (user) {
                    return user.Name.toLowerCase().indexOf(searchString.toLowerCase()) != -1;
                });
            });
    };

    $scope._getPeoplePickerSuggestion = function (searchKey, site) {
        var userSearchSuggestionEndpoint = site.url + "/_vti_bin/ListData.svc/UserInformationList?$select=Id,Name&$filter=substringof('" + searchKey + "',Name)%20and%20(ContentType%20eq%20'SharePointGroup')";
        return $http.get(userSearchSuggestionEndpoint);
    };

    $scope.NiveisPermissaoAdm = [
        { nome: 'Controle Total', value: "TotalControl" },
        { nome: 'Contribuição', value: "Contribute" },
        { nome: 'Leitura', value: "Read" },
        { nome: 'Edição', value: "Edit" },
        { nome: 'Designer', value: "WebDesigner" }]
});
