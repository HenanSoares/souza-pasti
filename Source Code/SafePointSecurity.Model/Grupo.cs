using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafePointSecurity.Model
{
    public class Grupo
    {
        private string nome;
        private string descricao;
        private int id;
        private List<Usuario> usuarios;

        public List<Usuario> Usuarios
        {
            get { return usuarios; }
            set { usuarios = value; }
        }
        
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
    }
}
