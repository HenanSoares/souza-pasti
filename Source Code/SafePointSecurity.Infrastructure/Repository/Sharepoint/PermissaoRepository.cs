using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafePointSecurity.Infrastructure.Interfaces;

namespace SafePointSecurity.Infrastructure.Repository.Sharepoint
{
    public class PermissaoRepository : IPermissaoRepository
    {
        public List<Model.Permissao> FindAll()
        {
            //TODO: Wilhas - Codigo trazer dados do banco aqui.
            throw new NotImplementedException();
        }

        public bool Save(Model.Permissao entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Model.Permissao entity)
        {
            throw new NotImplementedException();
        }

        public List<Model.Permissao> FindAll(string connectionString = null)
        {
            throw new NotImplementedException();
        }
    }
}
