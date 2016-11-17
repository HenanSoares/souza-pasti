using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafePointSecurity.Model
{
    public class Usuario
    {
        private string nome;
        private string login;
        private int id;
        
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        
    }
}
