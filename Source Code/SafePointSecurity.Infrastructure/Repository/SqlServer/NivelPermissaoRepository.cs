using SafePointSecurity.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafePointSecurity.Infrastructure.Repository.SqlServer
{
    public class NivelPermissaoRepository : INivelPermissaoRepository
    {
        public List<Model.NivelPermissao> FindAll()
        {
            throw new NotImplementedException();
        }

        public bool Save(Model.NivelPermissao entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Model.NivelPermissao entity)
        {
            throw new NotImplementedException();
        }

        public List<Model.NivelPermissao> FindAll(string connectionString = null)
        {
            throw new NotImplementedException();
        }
    }
}
