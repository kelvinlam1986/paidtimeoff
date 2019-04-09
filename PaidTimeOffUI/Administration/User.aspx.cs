using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using V2.PaidTimeOffBLL.Framework;

namespace PaidTimeOffUI.Administration
{
    public partial class User : BaseEditPage<ENTUserAccountEO>
    {
        private const string VIEW_STATE_KEY_USER = "User";

        public override string MenuItemName()
        {
            return "Users";
        }

        protected override void GoToGridPage()
        {
            Response.Redirect("Users.aspx");
        }

        protected override void LoadControls()
        {
        }

        protected override void LoadObjectFromScreen(ENTUserAccountEO baseEO)
        {
            baseEO.WindowAccountName = txtWindowAccountName.Text;
            baseEO.FirstName = txtFirstName.Text;
            baseEO.LastName = txtLastName.Text;
            baseEO.Email = txtEmail.Text;
            baseEO.IsActive = chkActive.Checked;
        }

        protected override void LoadScreenFromObject(ENTUserAccountEO baseEO)
        {
            txtWindowAccountName.Text = baseEO.WindowAccountName;
            txtFirstName.Text = baseEO.FirstName;
            txtLastName.Text = baseEO.LastName;
            txtEmail.Text = baseEO.Email;
            chkActive.Checked = baseEO.IsActive;
            ViewState[VIEW_STATE_KEY_USER] = baseEO;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SaveButton_Click += new
                PaidTimeOffEditPage.ButtonClickedHandler(Master_SaveButton_Click);
            Master.CancelButton_Click += new PaidTimeOffEditPage.ButtonClickedHandler(Master_CancelButton_Click);
        }

        private void Master_CancelButton_Click(object sender, EventArgs e)
        {
            GoToGridPage();
        }

        private void Master_SaveButton_Click(object sender, EventArgs e)
        {
            var validationErrors = new ENTValidationErrors();
            var userAccount = (ENTUserAccountEO)ViewState[VIEW_STATE_KEY_USER];
            LoadObjectFromScreen(userAccount);
            if (!userAccount.Save(ref validationErrors, 1))
            {
                Master.ValidationErrors = validationErrors;
            }
            else
            {
                GoToGridPage();
            }
        }
    }
}