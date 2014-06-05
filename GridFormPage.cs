using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Frikz.Web.Core
{
    public abstract class GridFormPage : Page
    {        
        public abstract void ChangeMode(PageMode mode, object sender);

        public abstract void RefreshGrid();

        protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
        {
            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                if (GridViewSortExpression != string.Empty)
                {
                    if (isPageIndexChanging)
                    {
                        dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                    }
                    else
                    {
                        dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                    }
                }
                return dataView;
            }
            else
            {
                return new DataView();
            }
        }

        protected string GridViewSortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected string GridViewSortExpression
        {
            get { return ViewState["SortExpression"] as string ?? string.Empty; }
            set { ViewState["SortExpression"] = value; }
        }

        protected string GetSortDirection()
        {
            switch (GridViewSortDirection)
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;
                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }

            return GridViewSortDirection;
        }

        protected void HideButtonSection(object sender)
        {
            if (sender is Button)
            {
                string CommandArgument = ((Button)(sender)).CommandArgument;
                Panel createButtonPanel = this.Master.FindControl("maincontent").FindControl("createButtonSection") as Panel;
                Panel updateButtonPanel = this.Master.FindControl("maincontent").FindControl("updateButtonSection") as Panel;    
                 
                if (CommandArgument == "CREATE")
                     createButtonPanel.Visible = false;
                else if (CommandArgument == "UPDATE" | CommandArgument == "REMOVE" | CommandArgument == "ARCHIVE")
                    updateButtonPanel.Visible = false;
            }
        }
    }
}
