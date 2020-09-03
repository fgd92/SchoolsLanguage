using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolsLanguage.UserControls
{
    public partial class Buttons : UserControl
    {
        public Buttons()
        {
            InitializeComponent();
        }

        private void btn_showClients_Click(object sender, EventArgs e)
        {
            Clients clients = new Clients();
            Menu menu = (Menu)Application.OpenForms["Menu"];
            clients.Dock = DockStyle.Fill;
            menu.Controls.Add(clients);
            menu.Controls.SetChildIndex(clients, 0);
            clients.Show();
        }
    }
}
