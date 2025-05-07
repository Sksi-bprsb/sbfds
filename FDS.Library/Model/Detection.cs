using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDS.Library.Model
{
   public class Detection
    {
        public int m_customer_id { get; set; }
        public string transaction_type { get; set; }
        public int transaction_amount { get; set; }
        public DateTime transaction_date { get; set; }
        public string biller_name { get; set; }
        public string Status { get; set; }



    }
}
