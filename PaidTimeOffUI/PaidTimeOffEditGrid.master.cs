using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using V2.PaidTimeOffBLL.Framework;

namespace PaidTimeOffUI
{
    public partial class PaidTimeOffEditGrid : System.Web.UI.MasterPage
    {
        public delegate void ButtonClickedHandler(object sender, EventArgs e);
        public event ButtonClickedHandler AddButton_Click;
        public event ButtonClickedHandler PrintButton_Click;

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (((BasePage)this.Page).ReadOnly)
            {
                // Hide the Add Button
                btnAddNew.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            if (AddButton_Click != null)
            {
                AddButton_Click(sender, e);
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (PrintButton_Click != null)
            {
                PrintButton_Click(sender, e);
            }
        }
    }
}