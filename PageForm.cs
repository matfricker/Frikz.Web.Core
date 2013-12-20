using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Frikz.Web.Core
{
    public abstract class PageForm : Page
    {        
        public abstract void ChangeMode(PageMode mode, object sender);

        public abstract void RefreshGrid();
    }
}
