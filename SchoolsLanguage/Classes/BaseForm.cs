using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolsLanguage.Classes
{
    public class BaseForm : Form
    {
        public BaseForm() : base()
        {
            Font = Properties.Settings.Default.MainFont;
            Icon = Properties.Resources.school_logo;
            this.Location = Screen.AllScreens.Length > 1 ? Screen.AllScreens[1].WorkingArea.Location : Screen.AllScreens[0].WorkingArea.Location;
        }
    }
}
