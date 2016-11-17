using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafePointSecurity.Model
{
    public class NivelPermissao
    {
        private string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
