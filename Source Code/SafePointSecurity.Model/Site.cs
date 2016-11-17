using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafePointSecurity.Model
{
    public class Site
    {
        private Guid id;
        private Guid? parentId;
        
        private string nome;
        private string descricao;
        private string url;
        private List<Site> subSites;


        public List<Site> SubSites
        {
            get { return subSites; }
            set { subSites = value; }
        }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public Guid? ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

    }
}
