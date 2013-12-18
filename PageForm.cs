using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Frikz.Web.Core
{
    public abstract class PageForm : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ChangeMode(PageMode.PageRead, sender);
        }
        
        public abstract void ChangeMode(PageMode mode, object sender);

        public abstract void RefreshGrid();
    }
}
