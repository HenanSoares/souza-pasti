<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SafePointSecurity.ascx.cs" Inherits="SafePointSecurity.VisualWebPart1" %>

<SharePoint:ScriptLink runat="server" ID="ScriptLinkSafePoint1" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/jquery.min.js" />
<SharePoint:ScriptLink runat="server" ID="ScriptLinkSafePoint2" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/bootstrap.min.js" />
<SharePoint:ScriptLink runat="server" ID="ScriptLinkSafePoint3" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/pageRelatorios.js" />
<SharePoint:ScriptLink runat="server" ID="ScriptLinkSafePoint4" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/angular.min.js" />
<SharePoint:ScriptLink runat="server" ID="ScriptLinkSafePoint5" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/angular-filter.min.js" />
<SharePoint:ScriptLink runat="server" ID="ScriptLinkSafePoint7" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/dirPagination.js" />
<SharePoint:ScriptLink runat="server" ID="ScriptLinkSafePoint8" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/underscore-min.js" />
<%--<SharePoint:ScriptLink runat="server" ID="ScriptLinkSafePoint9" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/treeDirective.js" />--%>
<SharePoint:ScriptLink runat="server" ID="ScriptLink2" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/kjvelarde-multiselect-searchtree-0.9.5.min.js" />
<SharePoint:ScriptLink runat="server" ID="ScriptLink1" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/kjvelarde-multiselect-searchtree-0.9.5.tpl.js" />
<SharePoint:ScriptLink runat="server" ID="ScriptLink3" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/loading-bar.min.js" />
<SharePoint:ScriptLink runat="server" ID="ScriptLink5" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/ng-tags-input.min.js" />
<SharePoint:ScriptLink runat="server" ID="ScriptLink4" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/ViewModel/PermissaoViewModel.js" />
<SharePoint:ScriptLink runat="server" ID="ScriptLinkSafePoint10" Language="javascript" Name="~sitecollection/Style Library/SafePointSecurity/js/safepoint_controller.js" />



<link href="/Style%20Library/SafePointSecurity/css/bootstrap.min.css" rel="stylesheet" />
<link href="/Style%20Library/SafePointSecurity/css/icons/simple-line-icons.min.css" rel="stylesheet" />
<link href="/Style%20Library/SafePointSecurity/css/kjvelarde-multiselect-searchtree-0.9.5.min.css" rel="stylesheet" />
<link href="/Style%20Library/SafePointSecurity/css/loading-bar.min.css" rel="stylesheet" />
<link href="/Style%20Library/SafePointSecurity/css/ng-tags-input.min.css" rel="stylesheet" />
<link href="/Style%20Library/SafePointSecurity/css/default.css" rel="stylesheet" />

<div ng-app="safepointApp">
    <div class="safepoint" ng-controller="safepointController">
        <div class="container-fluid">
            <div id="loading-bar-container"></div>
            <div class="navbar-header">
                <a href="#">
                    <img src="../Style%20Library/SafePointSecurity/img/logo.png" alt="logo" style="width: 102px; margin-right: 40px" />
                </a>
            </div>
            <ul class="nav nav-tabs">
                <li class="active">
                    <a data-toggle="tab" href="#relatorio">Relatórios<span></span></a></li>
                <li>
                    <a data-toggle="tab" href="#cadastro">Administração</a></li>
            </ul>
        </div>

        <div class="tab-content">
            <div id="relatorio" class="tab-pane fade in active">
                <div class="filtros-bordered">
                    <div class="title">
                        <div class="caption">
                            <i class=" icon-layers font-green"></i>
                            <span class="caption-subject font-green bold uppercase">Relatório</span>
                            <div class="caption-desc font-grey-cascade">Filtre os itens para o relatório.</div>
                        </div>
                    </div>
                    <div class="body">
                        <div class="row">
                            <div class="content-item col-md-3 col-sm-6">
                                <div class="content-item-head list-simple font-white bg-green">
                                    <div class="list-head-title-container">
                                        <div class="list-info">Total: {{Grupos.length}} itens</div>
                                        <h3 class="list-title">Grupos</h3>
                                    </div>
                                </div>
                                <div class="content-item-container">
                                    <ul class="scrollable">
                                        <li class="mt-list-item" ng-repeat="grupo in Grupos">
                                            <div class="list-icon-container" ng-class="{'done': grupo.selecionado}">
                                                <i class="icon-check" ng-class="{'icon-check': grupo.selecionado, 'icon-close': !grupo.selecionado }"></i>
                                            </div>
                                            <div class="list-item-content">
                                                <h3>
                                                    <a href="javascript:;" ng-click="Selecionar(grupo, 'grupo')">{{grupo.nome}}</a>
                                                </h3>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="content-item col-md-3 col-sm-6">
                                <div class="content-item-head list-simple font-white bg-blue">
                                    <div class="list-head-title-container">
                                        <div class="list-info">Total: {{Usuarios.length}} itens</div>
                                        <h3 class="list-title">Usuários</h3>
                                    </div>
                                </div>
                                <div class="content-item-container">
                                    <ul class="scrollable">
                                        <li class="mt-list-item" ng-repeat="usuario in Usuarios">
                                            <div class="list-icon-container" ng-class="{'done': usuario.selecionado}">
                                                <i class="icon-check" ng-class="{'icon-check': usuario.selecionado, 'icon-close': !usuario.selecionado }"></i>
                                            </div>
                                            <div class="list-item-content">
                                                <h3>
                                                    <a href="javascript:;" ng-click="Selecionar(usuario, 'usuario')">{{usuario.nome}}</a>
                                                </h3>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="content-item col-md-3 col-sm-6">
                                <div class="content-item-head list-simple font-white bg-purple">
                                    <div class="list-head-title-container">
                                        <div class="list-info">Total: {{NiveisPermissao.length}} itens</div>
                                        <h3 class="list-title">Permissões</h3>
                                    </div>
                                </div>
                                <div class="content-item-container">
                                    <ul class="scrollable">
                                        <li class="mt-list-item" ng-repeat="nivelPermissao in NiveisPermissao">
                                            <div class="list-icon-container" ng-class="{'done': nivelPermissao.selecionado}">
                                                <i class="icon-check" ng-class="{'icon-check': nivelPermissao.selecionado, 'icon-close': !nivelPermissao.selecionado }"></i>
                                            </div>
                                            <div class="list-item-content">
                                                <h3>
                                                    <a href="javascript:;" ng-click="Selecionar(nivelPermissao, 'nivelPermissao')">{{nivelPermissao.nome}}</a>
                                                </h3>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="content-item col-md-3 col-sm-6">
                                <div class="content-item-head list-simple font-white bg-red">
                                    <div class="list-head-title-container">
                                        <div class="list-info">Total: {{Sites.length}} itens</div>
                                        <h3 class="list-title">Sites</h3>
                                    </div>
                                </div>
                                <div class="content-item-container">
                                    <ul class="scrollable">
                                        <li class="mt-list-item" ng-repeat="site in Sites">
                                            <div class="list-icon-container" ng-class="{'done': site.selecionado}">
                                                <i class="icon-check" ng-class="{'icon-check': site.selecionado, 'icon-close': !site.selecionado }"></i>
                                            </div>
                                            <div class="list-item-content">
                                                <h3>
                                                    <a href="javascript:;" ng-click="Selecionar(site, 'site')">{{site.nome}}</a>
                                                </h3>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="table-scrollable report-table-result">
                                <dir-pagination-controls max-size="1000" boundary-links="true"></dir-pagination-controls>
                                <button class="btn btn-link" ng-click="exportToExcel('#table1')" style="float:right; border: 1px solid; border-radius: 5px; text-decoration: none;">
                                    <span class="icon-cloud-download"></span> Exportar para Excel
                                </button>
                                <table id="table1" class="table table-striped table-hover">
                                    <thead>
                                        <tr>
                                            <th># </th>
                                            <th ng-click="Ordenar('grupo.nome')">Grupo </th>
                                            <th ng-click="Ordenar('usuario.nome')">Usuário </th>
                                            <th ng-click="Ordenar('permissao.nome')">Permissão </th>
                                            <th ng-click="Ordenar('site.nome')">Site </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr dir-paginate="item in tableResult | itemsPerPage: pageSize" current-page="currentPage">
                                            <td>{{($index + 1) + (currentPage - 1) * pageSize }}</td>
                                            <td>{{item.grupo.nome}}</td>
                                            <td>{{item.usuario.nome}}</td>
                                            <td>{{item.nivelPermissao.nome}}</td>
                                            <td><a target="_blank" href="{{'/' + item.site.url}}">{{item.site.nome}}</a></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dir-pagination-controls max-size="10" boundary-links="true"></dir-pagination-controls>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="cadastro" class="tab-pane fade">
                <div class="filtros-bordered">
                    <div class="title">
                        <div class="caption">
                            <i class=" icon-layers font-green"></i>
                            <span class="caption-subject font-green bold uppercase">Cadastros</span>
                            <div class="caption-desc font-grey-cascade">Selecione o cadastro que deseja gerenciar.</div>
                        </div>
                    </div>
                    <div class="body">
                        <div class="row">
                            <div class="panel-group" id="accordion">
                                <div class="panel panel-default">
                                    <div class="panel-heading bg-green">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">
                                            <h4 class="panel-title">Grupos</h4>
                                        </a>
                                    </div>
                                    <div id="collapse1" class="panel-collapse collapse">
                                        <div class="panel-body">                                            
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label style="width:100%">Grupos:
                                                        
                                                    </label>
                                                    <ul class="list-group" style="max-height: 300px; overflow: auto">
                                                        <li class="list-group-item" ng-repeat="grupo in Grupos">
                                                            <a href="javascript:;" ng-click="PrencheMembros(grupo)">{{grupo.nome}} </a>
                                                            <span class="badge" ng-show="grupo.usuarios && grupo.usuarios.length > 0">{{grupo.usuarios.length}} 
                                                                <i class=" icon-users"></i>
                                                            </span>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-md-8">
                                                    <label style="width:100%">Membros{{' de ' + grupoSelecionadoAdmin.nome}}:
                                                        <a href="#" ng-click="OpenUrlAddUser(grupoSelecionadoAdmin)" ng-show="grupoSelecionadoAdmin != false">
                                                            <span class="badge pull-right bg-greenligth">
                                                                <i class=" icon-plus" style="font-size: 20px"></i>
                                                                Adicionar
                                                            </span>
                                                        </a>
                                                    </label>
                                                    <ul class="list-group" style="max-height: 300px; overflow: auto">
                                                        <li class="list-group-item" ng-repeat="usuario in MembrosGrupo">
                                                            <a href="javascript:;">{{usuario.nome}} </a>
                                                            <span class="badge bg-red" ng-click="$parent.RemoverUsuario(usuario, $parent.grupoSelecionadoAdmin)">
                                                                <i class=" icon-user-unfollow" style="font-size: 16px"></i>
                                                            </span>

                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-heading bg-blue">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">
                                                <h4 class="panel-title">Permissões</h4>
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="collapse2" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label style="width:100%">Sites:</label>
                                                    <multiselect-searchtree
                                                        multi-select="false"
                                                        data-input-model="treeview"
                                                        data-output-model="siteSelecionadoAdmin"
                                                        data-callback="SelecionarSite(item, selectedItems)"
                                                        data-select-only-leafs="false">
                                                    </multiselect-searchtree>
                                                </div>
                                                <div class="col-md-8">
                                                    <label style="width:100%">Permissões{{' de ' + siteSelecionadoAdmin.nome}}:
                                                        <a href="#" ng-show="siteSelecionadoAdmin != false" data-toggle="modal" data-target="#myModal" class="pull-right">
                                                            <span class="badge pull-right bg-greenligth">
                                                                <i class=" icon-plus" style="font-size: 20px"></i>
                                                                Adicionar
                                                            </span>
                                                        </a>
                                                    </label>
                                                    <ul class="list-group" style="max-height: 300px; overflow: auto">
                                                        <li class="list-group-item" ng-repeat="item in SitePermissoes">
                                                            <a href="javascript:;">{{item.nome}} - {{item.permissao}} </a>
                                                            <span class="badge bg-red" ng-click="$parent.RemoverPermissao(item, $parent.siteSelecionadoAdmin)">
                                                                <i class=" icon-user-unfollow" style="font-size: 16px"></i>
                                                            </span>

                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal -->
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Adicionar Permissão</h4>
                    </div>
                    <div class="modal-body">
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <label for="novoValor">Grupo:</label>
                                    <%--<tags-input ng-model="users"
                                        display-property="Name"
                                        placeholder=" "
                                        add-from-autocomplete-only="true"
                                        replace-spaces-with-dashes="false"
                                        max-tags="1">
                                                <auto-complete source="GetPeoplePickerSuggestion($query, siteSelecionadoAdmin)" min-length="3" debounce-delay="1000"></auto-complete>
                                    </tags-input>--%>
                                    <input type="text" ng-model="permissao.nome" class="form-control" placeholder="Informe o nome do grupo"/>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <label for="novoValor2">Permissão:</label>
                                    <div class="radio" ng-repeat="item in NiveisPermissaoAdm">
                                        <label><input type="radio" name="optradio" ng-model="$parent.permissao.nivelPermissao" ng-value="item.nome"/>{{item.nome}}</label>
                                    </div>                                    
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                        <button id="btAtualizarValor" type="button" class="btn btn-primary" data-dismiss="modal" ng-click="AdicionarPermissao()">Salvar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
