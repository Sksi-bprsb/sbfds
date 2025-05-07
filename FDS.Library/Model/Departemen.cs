using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDS.Library.Model
{
    public class Departemen
    {
        public int DepartemenId { get; set; }
        public bool Active { get; set; }
        public string KodeDepartemen { get; set; }
        public string NamaDepartemen { get; set; }
    }
}
