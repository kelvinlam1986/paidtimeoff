using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace V2.FrameworkControls
{
    [ToolboxData("<{0}:CustomGridView runat=server></{0}:CustomGridView>")]
    public class CustomGridView : GridView
    {
        private ArrayList _methodParameters = new ArrayList();

        public CustomGridView()
        {
            AutoGenerateColumns = false;
            AllowPaging = true;
            AllowSorting = true;
            PageSize = 25;
            GridLines = GridLines.Both;
            HeaderStyle.CssClass = "gridViewHeader";
            RowStyle.CssClass = "gridViewRow";
            AlternatingRowStyle.CssClass = "gridViewAlternatingRow";
            PagerStyle.CssClass = "gridViewPager";
            CellPadding = 3;
        }

        public string ListClassName
        {
            get { return ViewState["ListClassName"].ToString(); }
            set { ViewState["ListClassName"] = value; }
        }

        public string LoadMethodName
        {
            get { return ViewState["LoadMethodName"].ToString(); }
            set { ViewState["LoadMethodName"] = value; }
        }

        public string SortExpressionLast
        {
            get
            {
                if (ViewState["SortExpressionLast"] == null)
                    //Default to displaytext
                    return "DisplayText";

                else
                    return ViewState["SortExpressionLast"].ToString();
            }

            set { ViewState["SortExpressionLast"] = value; }
        }

        public SortDirection SortDirectionLast
        {
            get
            {
                if (ViewState["SortDirectionLast"] == null)
                    //Default to ascending
                    return SortDirection.Ascending;
                else
                    return (SortDirection)ViewState["SortDirectionLast"];
            }

            set { ViewState["SortDirectionLast"] = value; }
        }

        public ArrayList LoadMethodParameters
        {
            get
            {
                return _methodParameters;
            }
        }

        public new object DataSource
        {
            set { throw new NotImplementedException(); }
        }

        protected override void OnPageIndexChanging(GridViewPageEventArgs e)
        {
            PageIndex = e.NewPageIndex;
            BindGridView(SortExpressionLast, SortDirectionLast);
        }

        protected override void OnSorting(GridViewSortEventArgs e)
        {
            PageIndex = 0;
            if (e.SortExpression == SortExpressionLast)
            {
                if (SortDirectionLast == SortDirection.Ascending)
                {
                    BindGridView(e.SortExpression, SortDirection.Descending);
                }
                else
                {
                    BindGridView(e.SortExpression, SortDirection.Ascending);
                }
            }
            else
            {
                BindGridView(e.SortExpression, SortDirection.Ascending);
            }
        }

        public new void DataBind()
        {
            BindGridView(SortExpressionLast, SortDirectionLast);
        }

        private void BindGridView(string sortExpression, SortDirection sortDirection)
        {
            Type objectType = Type.GetType(ListClassName);
            object listObject = Activator.CreateInstance(objectType);
            objectType.InvokeMember(LoadMethodName, System.Reflection.BindingFlags.InvokeMethod, null,
                listObject, _methodParameters.ToArray());

            base.DataSource = objectType.InvokeMember("SortByPropertyName", System.Reflection.BindingFlags.InvokeMethod,
                null, listObject, new object[] { sortExpression, sortDirection == SortDirection.Ascending });
            base.DataBind();

            SortExpressionLast = sortExpression;
            SortDirectionLast = sortDirection;

            objectType = null;
            listObject = null;
        }

        public void AddBoundField(string dataField, string headerText, string sortExpression)
        {
            BoundField bf = new BoundField();
            if (dataField != "")
            {
                bf.DataField = dataField;
            }

            if (headerText != "")
            {
                bf.HeaderText = headerText;
            }

            if (sortExpression != "")
            {
                bf.SortExpression = sortExpression;
            }

            Columns.Add(bf);
        }
    }
}
