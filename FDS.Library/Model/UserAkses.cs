using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDS.Library.Model
{
   public class UserAkses
    {
        public string id { get; set; }
        public string m_role_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public bool Active { get; set; }
    }
}
