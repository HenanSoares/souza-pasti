using SafePointSecurity.Common.Util;
using SafePointSecurity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePointSecurity.Infrastructure.Interfaces
{
    public interface ISiteRepository : IBaseRepository<Site>
    {
        bool AdicionarPermissao(string urlSite, string nomeGrupo, string nomePermissao);

        bool RemoverPermissao(string urlSite, string nomeGrupo);
    }
}
