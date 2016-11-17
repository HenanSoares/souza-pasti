using SafePointSecurity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafePointSecurity.Common.Util;

namespace SafePointSecurity.Application.Interfaces
{
    public interface IApplicationFacade
    {
        List<Permissao> GetPermissoes(Connection connection);

        List<Model.Site> GetSites(Connection connection, bool onlyParents = false);
    }
}
