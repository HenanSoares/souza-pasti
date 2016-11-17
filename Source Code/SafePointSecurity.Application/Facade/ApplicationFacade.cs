using SafePointSecurity.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafePointSecurity.Infrastructure.Interfaces;
using SafePointSecurity.Infrastructure.Repository.SqlServer;
using SafePointSecurity.Common.Util;

namespace SafePointSecurity.Application.Facade
{
    public class ApplicationFacade : IApplicationFacade
    {
        private IPermissaoRepository permissaoRepository = new PermissaoRepository();
        private ISiteRepository siteRepository = new SiteRepository();

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
    }
}
