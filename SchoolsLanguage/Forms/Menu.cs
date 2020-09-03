using SchoolsLanguage.Classes;
using SchoolsLanguage.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolsLanguage
{
    public partial class Menu : BaseForm
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            Buttons buttons = new Buttons();
            buttons.Show();
            container.Controls.Add(buttons);
        }
    }
}
