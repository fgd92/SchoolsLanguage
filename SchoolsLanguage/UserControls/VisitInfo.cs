using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SchoolsLanguage.ModelDB;

namespace SchoolsLanguage.UserControls
{
    public partial class VisitInfo : UserControl
    {
        public VisitInfo(int ID)
        {
            InitializeComponent();

            using (DataBaseEntities db = new DataBaseEntities())
            {
                ClientService clientService = db.ClientService.FirstOrDefault(s => s.ID == ID);
                lbl_nameService.Text = clientService.Service.Title;
                lbl_date.Text = clientService.StartTime.ToString("yyyy.mm.dd hh:mm");
                lbl_countFile.Text = "Всего файлов: " + clientService.DocumentByService.Count();
            }
        }

        private void btn_showFiles_Click(object sender, EventArgs e)
        {

        }

        private void lbl_date_Click(object sender, EventArgs e)
        {

        }
    }
}
