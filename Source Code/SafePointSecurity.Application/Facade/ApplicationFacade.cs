using SafePointSecurity.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafePointSecurity.Infrastructure.Interfaces;
using SafePointSecurity.Infrastructure.Repository.SqlServer;
using SP = SafePointSecurity.Infrastructure.Repository.Sharepoint;
using SafePointSecurity.Common.Util;

namespace SafePointSecurity.Application.Facade
{
    public class ApplicationFacade : IApplicationFacade
    {
        private IPermissaoRepository permissaoRepository = new PermissaoRepository();
        private ISiteRepository siteRepository = new SiteRepository();
        private ISiteRepository siteSPRepository = new SP.SiteRepository();

        public List<Model.Permissao> GetPermissoes(Connection connection)
        {
            return permissaoRepository.FindAll(connection.ConnectionString);
        }

        public List<Model.Site> GetSites(Connection connection, bool onlyParents = false)
        {
            var sites = siteRepository.FindAll(connection.ConnectionString);
                        
            if (onlyParents)
                sites = sites.Where(x => x.ParentId != null).ToList();

            return sites;
        }

        public bool AdicionarPermissaoSite(string urlSite, string nomeGrupo, string nomePermissao)
        {
            return siteSPRepository.AdicionarPermissao(urlSite, nomeGrupo, nomePermissao);
        }

        public bool RemoverPermissaoSite(string urlSite, string nomeGrupo)
        {
            return siteSPRepository.RemoverPermissao(urlSite, nomeGrupo);
        }
    }
}
