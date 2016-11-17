using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafePointSecurity.Infrastructure.Interfaces;
using SafePointSecurity.Common.Exceptions;
using System.Data.SqlClient;
using SafePointSecurity.Model;
using System.Data;

namespace SafePointSecurity.Infrastructure.Repository.SqlServer
{
    public class SiteRepository : ISiteRepository
    {
        private enum ResultLine
        {
            Id = 0,
            ParentWebId = 1,
            Description = 2,
            Title = 3,
            FullUrl = 4
        }

        private const string query = @"
            SELECT
            W.Id,
            W.parentWebId,
            W.Description, 
            W.title,
            W.FullUrl
            FROM Webs W
            WHERE W.DeleteTransactionId <> '0x'";

        public List<Model.Site> FindAll(string connectionString = null)
        {
            if (String.IsNullOrEmpty(connectionString))
                throw new SafePointStringConnectionException();

            //var dictionary = new Dictionary<object, Site>();

            SqlConnection conexao = null;

            try
            {
                conexao = new SqlConnection(connectionString);
                var command = new SqlCommand(query, conexao);
                conexao.Open();

                var reader = command.ExecuteReader();
                var list = new List<Site>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var site = new Site()
                        {
                            Id = (Guid)reader.GetValue((int)ResultLine.Id),
                            Url = (String.IsNullOrWhiteSpace(reader.GetString((int)ResultLine.FullUrl)) ? @"": String.Concat(@"/",reader.GetString((int)ResultLine.FullUrl))),
                            Nome = reader.GetString((int)ResultLine.Title),
                            Descricao = reader.GetString((int)ResultLine.Description),
                        };
                        if (!reader.IsDBNull((int)ResultLine.ParentWebId))
                            site.ParentId = (Guid)reader.GetValue((int)ResultLine.ParentWebId);

                        //if(site.ParentId.HasValue)
                        //    dictionary.Add(site.ParentId.Value, site);
                        //else
                        //    dictionary.Add(null, site);

                        list.Add(site);
                    }
                    //list = list.GroupBy(x=>x.ParentId).ToList();
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conexao.State == ConnectionState.Open)
                    conexao.Close();
            }
        }

        public bool Save(Model.Site entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Model.Site entity)
        {
            throw new NotImplementedException();
        }
    }
}
