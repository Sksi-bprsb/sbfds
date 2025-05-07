using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDS.Library.Model
{
   public class JumlahTransaksiTidakSesuaiPola
    {
        public string customer_name { get; set; }
        public string cif_number { get; set; }
        public string customer_username { get; set; }
        public string transaction_type { get; set; }
        public string description { get; set; }
        public string count { get; set; }
        public string sum { get; set; }
        public string min { get; set; }
        public string max { get; set; }
        public string avg { get; set; }
        public string avg_count { get; set; }
        public string date { get; set; }
        public string last_transaction_amount { get; set; }
        public string avg_transaction_per_day { get; set; }
        public string min_daily_transaction_count { get; set; }
        public string max_daily_transaction_count { get; set; }
        public string total_transaction_count { get; set; }
        public string transaction_count_on_specified_date { get; set; }
        public string transaction_date { get; set; }
        

        


    }
}
