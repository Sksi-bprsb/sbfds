using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDS.Library.Model
{
    public class MasterUser
    {
        public int MasterUserId { get; set; }
        public bool Active { get; set; }
        public string Nik { get; set; } = "";
        public string NamaKaryawan { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
        public string NoHp { get; set; } = "";
        public string Departemen { get; set; } = "";
        public string Cabang { get; set; } = "";
        public int UserGroupId { get; set; }
        public string NamaUserGroup { get; set; } 
    }
}
