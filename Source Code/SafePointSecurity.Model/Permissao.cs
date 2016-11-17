using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafePointSecurity.Model
{
    public class Permissao
    {
        private Site site;
        private Grupo grupo;
        private Usuario usuario;
        private NivelPermissao nivelPermissao;

        public Grupo Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }

        public Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public Site Site
        {
            get { return site; }
            set { site = value; }
        }

        public NivelPermissao NivelPermissao
        {
            get { return nivelPermissao; }
            set { nivelPermissao = value; }
        }

    }
}
