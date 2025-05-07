using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDS.Library.Model
{
    public class ParameterPending
    {
            public int ParameterPendingId { get; set; }
            public string Nama { get; set; }
            public string Value1 { get; set; }
            public string Value2 { get; set; }
            public string Value3 { get; set; }
            public bool active { get; set; }
            public string Status { get; set; }
            public string ApprovalNama { get; set; }

    }
}
