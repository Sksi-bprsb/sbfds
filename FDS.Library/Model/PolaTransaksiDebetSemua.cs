using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDS.Library.Model
{
   public class PolaTransaksiDebetSemua
    {
        public string customer_name { get; set; }
        public string cif_number { get; set; }
        public string customer_username { get; set; }
        public string transaction_type { get; set; }
        public string description { get; set; }
        public string from_account_number { get; set; }
        public string to_account_number { get; set; }
        public string count { get; set; }
        public string sum { get; set; }
        public string min { get; set; }
        public string max { get; set; }
        public string avg { get; set; }
        public string date { get; set; }
        public string min_daily { get; set; }
        public string max_daily { get; set; }
        public string avg_daily { get; set; }
        public string last_transaction_amount { get; set; }
        public string total_last_transaction_amount { get; set; }
        public string transaction_count { get; set; }  



    }
}
