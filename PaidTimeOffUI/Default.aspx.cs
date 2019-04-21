using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PaidTimeOffUI
{
    public partial class Default : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            IgnoreCapabilityCheck = true;
            base.OnInit(e);
        }

        public override string[] CapabilityNames()
        {
            throw new NotImplementedException();
        }

        public override string MenuItemName()
        {
            return "Home";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}