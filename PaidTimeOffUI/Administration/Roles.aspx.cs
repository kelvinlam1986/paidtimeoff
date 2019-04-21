using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using V2.PaidTimeOffBLL.Framework;

namespace PaidTimeOffUI.Administration
{
    public partial class Roles : BasePage
    {
        private const int COL_INDEX_ACTION = 0;
        private const int COL_INDEX_ROLENAME = 1;

        public override string[] CapabilityNames()
        {
            return new string[] { "Roles" };
        }

        public override string MenuItemName()
        {
            return "Roles";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.AddButton_Click += new PaidTimeOffEditGrid.ButtonClickedHandler(Master_AddButton_Click);
            if (!IsPostBack)
            {
                // Tell the control what class to create and what method
                // to call to load the class
                cgvRoles.ListClassName = typeof(ENTRoleEOList).AssemblyQualifiedName;
                cgvRoles.LoadMethodName = "Load";

                // Actions column contains the edit and delete links
                cgvRoles.AddBoundField("", "Actions", "");

                // Name
                cgvRoles.AddBoundField("DisplayText", "Name", "DisplayText");

                cgvRoles.DataBind();
            }
            else
            {
                string eventTarget = Page.Request.Form["__EVENTTARGET"].ToString();
                if (eventTarget.IndexOf("lbtnDelete") > -1)
                {
                    // Rebind the grid so the delete event is captured
                    cgvRoles.DataBind();
                }
            }
        }

        void Master_AddButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Role.aspx" + EncryptQueryString("id=0"));
        }

        protected void cgvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Add the edit link to action column
                HyperLink editLink = new HyperLink();
                if (ReadOnly)
                {
                    editLink.Text = "View";
                }
                else
                {
                    editLink.Text = "Edit";
                }

                editLink.NavigateUrl = "Roles.aspx" + EncryptQueryString("id=" +
                    ((ENTRoleEO)e.Row.DataItem).ID.ToString());
                e.Row.Cells[COL_INDEX_ACTION].Controls.Add(editLink);
                if (ReadOnly == false)
                {
                    LiteralControl lc = new LiteralControl(" | ");
                    e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lc);

                    // Add the Delete link
                    LinkButton lbtnDelete = new LinkButton();
                    lbtnDelete.ID = "lbtnDelete" + ((ENTRoleEO)e.Row.DataItem).ID.ToString();
                    lbtnDelete.Text = "Delete";
                    lbtnDelete.CommandArgument = ((ENTRoleEO)e.Row.DataItem).ID.ToString();
                    lbtnDelete.OnClientClick = "return ConfirmDelete()";
                    lbtnDelete.Command += new CommandEventHandler(lbtnDelete_Command);
                    e.Row.Cells[COL_INDEX_ACTION].Controls.Add(lbtnDelete);
                }
            }
        }

        private void lbtnDelete_Command(object sender, CommandEventArgs e)
        {
            ENTValidationErrors validationErrors = new ENTValidationErrors();
            ENTRoleEO role = new ENTRoleEO();
            role.DbAction = ENTBaseEO.DBActionEnum.Delete;

            role.ID = Convert.ToInt32(e.CommandArgument);
            role.Delete(ref validationErrors, CurrentUser.ID);

            Master.ValidationErrors = validationErrors;
            cgvRoles.DataBind();
        }
    }
}