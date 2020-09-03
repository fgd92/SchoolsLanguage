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
    public partial class addTag : Form
    {
        public addTag()
        {
            InitializeComponent();
            BackColor = Properties.Settings.Default.SecondColor;
        }

        private void btn_colorPick_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btn_colorPick.BackColor = colorDialog.Color;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Tag tag = new Tag();
            tag.Color = btn_colorPick.BackColor.R.ToString("X2") +
                btn_colorPick.BackColor.G.ToString("X2") +
                btn_colorPick.BackColor.B.ToString("X2");
            tag.Title = txt_title.Text;
            Tag = tag;
        }
    }
}
