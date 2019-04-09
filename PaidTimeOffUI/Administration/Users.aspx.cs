using System;
using System.Web.UI.WebControls;
using V2.PaidTimeOffBLL.Framework;

namespace PaidTimeOffUI.Administration
{
    public partial class Users : BasePage
    {
        private const int COL_INDEX_ACTION = 0;
        private const int COL_INDEX_NAME = 1;
        private const int COL_INDEX_WINDOWSNAME = 2;
        private const int COL_INDEX_EMAIL = 3;
        private const int COL_INDEX_ACTIVE = 4;

        public override string MenuItemName()
        {
            return "Users";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.AddButton_Click += new PaidTimeOffEditGrid.ButtonClickedHandler(Master_AddButton_Click);
            if (!IsPostBack)
            {
                // Tell the control what class to create and what method to call to load the class.
                cgvUsers.ListClassName = typeof(ENTUserAccountEOList).AssemblyQualifiedName;
                cgvUsers.LoadMethodName = "Load";

                //Action column-Contains the Edit link
                cgvUsers.AddBoundField("", "Actions", "");

                //Name
                cgvUsers.AddBoundField("DisplayText", "Name", "DisplayText");

                //Windows Account Name
                cgvUsers.AddBoundField("WindowAccountName", "Windows Account", "WindowAccountName");

                //Email
                cgvUsers.AddBoundField("Email", "Email", "Email");

                //Is Active-This will be a checkbox column
                cgvUsers.AddBoundField("", "Active", "IsActive");

                cgvUsers.DataBind();
            }
        }

        void Master_AddButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("User.aspx" + EncryptQueryString("id=0"));
        }

        protected void cgvUsers_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                var editLink = new HyperLink();
                editLink.Text = "Edit";
                editLink.NavigateUrl = "User.aspx" + EncryptQueryString("id=" + ((ENTUserAccountEO)e.Row.DataItem).ID.ToString());
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);

                var chkActive = new CheckBox();
                chkActive.Checked = ((ENTUserAccountEO)e.Row.DataItem).IsActive;
                chkActive.Enabled = false;
                e.Row.Cells[COL_INDEX_ACTIVE].Controls.Add(chkActive);
            }
        }
    }
}