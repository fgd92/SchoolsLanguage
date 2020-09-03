using SchoolsLanguage.ModelDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolsLanguage.Forms
{
    public partial class VisitsInfoOfClient : Form
    {
        public VisitsInfoOfClient(int ClientID)
        {
            InitializeComponent();

            using (DataBaseEntities db = new DataBaseEntities())
            {
                foreach (var item in db.ClientService.Where(c => c.ClientID == ClientID))
                {
                    SchoolsLanguage.UserControls.VisitInfo visitInfo = new UserControls.VisitInfo(item.ID);
                    container.Controls.Add(visitInfo);
                }
            }
        }
    }
}
