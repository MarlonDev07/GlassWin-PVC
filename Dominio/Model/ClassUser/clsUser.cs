using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ClassUser
{
    public class clsUser
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Roll { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string State { get; set; }
        public string TypeSystem { get; set; }
        public DateTime Expiration { get; set; }
    }
}
