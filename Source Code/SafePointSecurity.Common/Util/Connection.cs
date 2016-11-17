using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePointSecurity.Common.Util
{
    public sealed class Connection
    {
        private static readonly Connection instance = new Connection();

        public string ConnectionString { get; set; }

        public string User { get; set; }
        public string Password { get; set; }

        private Connection() { }

        public static Connection Instance
        {
            get { return instance; }
        }
    }
}
