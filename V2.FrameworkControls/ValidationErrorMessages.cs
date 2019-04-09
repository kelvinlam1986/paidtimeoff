using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using V2.PaidTimeOffBLL.Framework;

namespace V2.FrameworkControls
{
    [ToolboxData("<{0}:ValidationErrorMessages runat=server></{0}:ValidationErrorMessages>")]
    public class ValidationErrorMessages : WebControl
    {
        [Bindable(false),
         Browsable(false)]
        public ENTValidationErrors ValidationErrors { get; set; }

        public ValidationErrorMessages()
        {
            ValidationErrors = new ENTValidationErrors();
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            // Show all the messages in ENTValidationErrors

            if (ValidationErrors.Count != 0)
            {
                var table = new HtmlTable();
                var trHeader = new HtmlTableRow();
                var tcHeader = new HtmlTableCell();

                tcHeader.InnerText = "Please review the following issues:";
                tcHeader.Attributes.Add("class", "validatioErrorMessageHeader");
                trHeader.Cells.Add(tcHeader);
                table.Rows.Add(trHeader);

                foreach (var ve in ValidationErrors)
                {
                    var tr = new HtmlTableRow();
                    var tc = new HtmlTableCell();
                    tc.InnerText = ve.ErrorMessage;
                    tc.Attributes.Add("class", "validatioErrorMessageHeader");
                    tr.Cells.Add(tc);
                    table.Rows.Add(tr);
                    tc = null;
                    tr = null;
                }

                table.RenderControl(output);
                tcHeader = null;
                trHeader = null;
                table = null;
            }
            else
            {
                output.Write("");
            }
        }
    }
}
