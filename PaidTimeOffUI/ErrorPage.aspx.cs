using System;

namespace PaidTimeOffUI
{
    public partial class ErrorPage : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
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