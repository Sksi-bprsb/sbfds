using BPRSB.Kuesioner.Repository;
using FDS.Library.Model;
using System;
using SBFDSys;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SBFDSys
{
    public partial class SideBarAdmin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUser"] != null)
                loadUserAccess();
        }
        private void loadUserAccess()
        {
            MasterUser user = (MasterUser)Session["LoginUser"];
            UserGroup group = MySqlRepository.GetById<UserGroup>(Convert.ToInt32(user.UserGroupId));

            LabelParameter.Visible = group.GetBoolValue("master", "tampil");
            LabelUser.Visible = group.GetBoolValue("user", "tampil");
            LabelUserGroup.Visible = group.GetBoolValue("UserGroupSetting", "tampil");
            ApprovalPar.Visible = group.GetBoolValue("restoredb", "approve");

        }
    }
}