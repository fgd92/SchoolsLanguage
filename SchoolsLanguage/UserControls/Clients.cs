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
using System.Diagnostics;
using System.Windows.Threading;
using SchoolsLanguage.Classes;
using System.Collections;

namespace SchoolsLanguage.UserControls
{
    
    public partial class Clients : UserControl
    {
        Func<Client, bool> filter;
        AutoCompleteStringCollection nameSource;
        AutoCompleteStringCollection emailSource;
        AutoCompleteStringCollection phoneNumberSource;
        ClientsModel clientsModel;

        public Clients()
        {
            InitializeComponent();

            filter = c => c.ID != null;
            dgv_client.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            status.BackColor = Properties.Settings.Default.SecondColor;

            ArrayList arrayList = new ArrayList(Controls.Cast<Control>().OrderBy(c => c.Top).ToArray());

            for (int i = 0; i < arrayList.Count; i++)
            {
                ((Control)arrayList[i]).TabIndex = i;
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            Update_data_clients();
        }

        private void Update_data_clients()
        {
            UpdateData();
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            num_countRowsOnPage.Value = Math.Round((decimal)dgv_client.Size.Height / dgv_client.RowTemplate.Height, 0, MidpointRounding.AwayFromZero) - 2;
            clientsModel = new ClientsModel((int)num_countRowsOnPage.Value);
            num_countRowsOnPage.Maximum = clientsModel.MaxRows;

            Fill_autoCompleteString();

            cmb_countVisit.SelectedIndex = 0;
            cmb_lastName.SelectedIndex = 0;
            cmb_dateVisit.SelectedIndex = 0;
            cmb_gender.SelectedIndex = 0;
        }

        private void Fill_autoCompleteString()
        {
            using (DataBaseEntities db = new DataBaseEntities())
            {
                var listClient = db.Client.ToList();
                nameSource = new AutoCompleteStringCollection();
                emailSource = new AutoCompleteStringCollection();
                phoneNumberSource = new AutoCompleteStringCollection();

                listClient.ForEach(c =>
                {
                    nameSource.Add(c.LastName + " " + c.FirstName + " " + c.Patronymic);
                    emailSource.Add(c.Email);
                    phoneNumberSource.Add(c.Phone);
                });

                txt_name.AutoCompleteCustomSource = nameSource;
                txt_email.AutoCompleteCustomSource = emailSource;
                txt_phone.AutoCompleteCustomSource = phoneNumberSource;
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            clientsModel.NextPage();
            UpdateData();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            clientsModel.PrevPage();
            UpdateData();
        }

        private void filter_changed(object sender, EventArgs e)
        {
            UpdateByFilter();
        }
        private void sorter_changed(object sender, EventArgs e)
        {
            UpdateBySort(sender);
        }

        private void UpdateBySort(object sender)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedItem.ToString() != "Нет")
            {
                string fieldName = comboBox.Tag.ToString();

                Func<ClientView,string> func = c => typeof(ClientView).GetProperty(fieldName).GetValue(c)?.ToString();

                dgv_client.DataSource = cmb_lastName.SelectedItem.ToString() == "По возрастанию" ?
                    clientsModel.SortAscending(filter, func).ToList()
                    : clientsModel.SortDescending(filter, func).ToList();
            }
            else
            {
                UpdateData();
            }
        }

        private void UpdateByFilter()
        {
            if (cmb_gender.SelectedIndex > 0)
            {
                filter = c => c.Gender.Name == cmb_gender.SelectedItem.ToString()
                && (c.LastName + " " + c.FirstName + " " + c.Patronymic).isMatch(txt_name.Text)
                && c.Email.isMatch(txt_email.Text)
                && c.Phone.isMatch(txt_phone.Text);
            }
            else
            {
                filter = c => (c.Gender.Name == "Женский" || c.Gender.Name == "Мужской")
                && (c.LastName + " " + c.FirstName + " " + c.Patronymic).isMatch(txt_name.Text)
                && c.Email.isMatch(txt_email.Text)
                && c.Phone.isMatch(txt_phone.Text);
            }

            UpdateData();
        }

        private void btn_showBirthDay_Click(object sender, EventArgs e)
        {
            using (DataBaseEntities db = new DataBaseEntities())
            {
                filter = c => c.Birthday.Value.Month == DateTime.Now.Month;
            }

            UpdateData();
        }
        void UpdateData()
        {
            dgv_client.DataSource = clientsModel.Get(filter).ToList(); 
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            Delete_client();
        }

        private void Delete_client()
        {
            if (MessageBox.Show("Вы дейтсвительно хотите удалить запись?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                using (DataBaseEntities db = new DataBaseEntities())
                {
                    int ID = (int)dgv_client[0, dgv_client.SelectedRows[0].Index].Value;
                    var client = db.Client.FirstOrDefault(c => c.ID == ID);

                    if (client.VisitInfo.Count > 0)
                    {
                        MessageBox.Show("Невозможно удалить запись, так как есть данные о посещении.");
                    }
                    else
                    {
                        db.Client.Remove(client);

                        if (client.TagOfClient.Select(t => t.Tag).ToList().Count > 0)
                        {
                            foreach (var tag in client.TagOfClient.Select(t => t.Tag).ToList())
                            {
                                db.Tag.Remove(tag);
                            }
                        }

                        db.SaveChanges();
                        UpdateData();
                    }
                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Forms.Client client = new Forms.Client((int)dgv_client[0, dgv_client.SelectedRows[0].Index].Value);
            client.ShowDialog();
            UpdateData();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Forms.Client client = new Forms.Client();
            client.ShowDialog();
            UpdateData();
        }

        private void data_clients_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            status.Items[0].Text = dgv_client.RowCount > 0 ? string.Format("{0} из {1}",
                        clientsModel.Count(filter, ID: (int)dgv_client[0, dgv_client.RowCount - 1].Value),
                        clientsModel.Count()) : null;
        }

        private void num_countRowsOnPage_ValueChanged(object sender, EventArgs e)
        {
            if(clientsModel != null)
                clientsModel.MaxTakeRows = (int)num_countRowsOnPage.Value;
        }
    }
}
