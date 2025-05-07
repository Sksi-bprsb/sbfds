using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDS.Library.Model
{
    public class UserGroup
    {
        public int UserGroupId { get; set; }
        public bool Active { get; set; }
        public string NamaUserGroup { get; set; }
        public string Master { get; set; }
        public string RestoreDB { get; set; }
        public string Cabang { get; set; }
        public string Departemen { get; set; }
        public string User { get; set; }
        public string UserGroupSetting { get; set; }
        
        
        public string Laporan { get; set; }

        public string ConvertToString(bool tampil, bool simpan, bool hapus, bool print, bool approve)
        {
            return tampil.ToString() + "," + simpan.ToString() + "," + hapus.ToString()
                + "," + print.ToString() + "," + approve.ToString();
        }
        public bool GetBoolValue(string NamaForm, string akses)
        {
            string values = "";
            switch (NamaForm.ToLower())
            {
                case "master":
                    values = Master;
                    break;
                case "user":
                    values = User;
                    break;
                case "usergroupsetting":
                    values = UserGroupSetting;
                    break;
                case "restoredb":
                    values = RestoreDB;
                    break;
                    //case "laporan":
                    //    values = Laporan;
                    //    break;
            }
            if (values == "")
                return false;
            string[] vals = values.Split(',');
            switch (akses.ToLower())
            {
                case "tampil":
                    return Convert.ToBoolean(vals[0]);
                case "simpan":
                    return Convert.ToBoolean(vals[1]);
                case "hapus":
                    return Convert.ToBoolean(vals[2]);
                case "print":
                    return Convert.ToBoolean(vals[3]);
                case "approve":
                    return Convert.ToBoolean(vals[4]);
            }
            return false;
        }

    }
}
