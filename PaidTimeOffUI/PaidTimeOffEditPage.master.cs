using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using V2.PaidTimeOffBLL.Framework;

namespace PaidTimeOffUI
{
    public partial class PaidTimeOffEditPage : System.Web.UI.MasterPage
    {
        public delegate void ButtonClickedHandler(object sender, EventArgs e);
        public event ButtonClickedHandler SaveButton_Click;
        public event ButtonClickedHandler CancelButton_Click;

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (((BasePage)Page).ReadOnly)
            {
                // Hide the save button
                btnSave.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveButton_Click != null)
            {
                SaveButton_Click(sender, e);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (CancelButton_Click != null)
            {
                CancelButton_Click(sender, e);
            }
        }

        public ENTValidationErrors ValidationErrors
        {
            get
            {
                return ValidationErrorMessages1.ValidationErrors;
            }

            set
            {
                ValidationErrorMessages1.ValidationErrors = value;
            }
        }
    }
}