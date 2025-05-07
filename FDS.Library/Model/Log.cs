using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDS.Library.Model
{
    public class Log
    {
            public int LogId { get; set; }
            public string Nik { get; set; }
            public int MasterUserId { get; set; }
            public string Username { get; set; }
            public string NamaKaryawan { get; set; }
            public DateTime Tanggal { get; set; }
            public string Keterangan { get; set; }
            public bool active { get; set; }

    }
}
