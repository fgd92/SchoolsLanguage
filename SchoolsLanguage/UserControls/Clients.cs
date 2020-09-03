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

namespace SchoolsLanguage.UserControls
{
    
    public partial class Clients : UserControl
    {
        int page = 0;
        int MaxPage = 0;
        Func<Client, bool> filter;
        public int MaxCountRows { get; set; }
        Dispatcher dispatcher;
        AutoCompleteStringCollection nameSource;
        AutoCompleteStringCollection emailSource;
        AutoCompleteStringCollection phoneNumberSource;

        public int Page
        {
            get
            {
                return page;
            }
            set
            {
                page = value;
            }
        }

        public Clients()
        {
            InitializeComponent();
            data_clients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            status.BackColor = Properties.Settings.Default.SecondColor;
            dispatcher = Dispatcher.CurrentDispatcher;
        }

        private void Fill_data_clients(int page, int countRows)
        {
            using (DataBaseEntities db = new DataBaseEntities())
            {
                if (page != db.Client.Count())
                {
                    data_clients.Rows.Clear();

                    db.Client.Where(filter).Skip(page * countRows).
                        Take(Math.Abs(countRows)).ToList().ForEach(cl =>
                        {
                            data_clients.Rows.Add(
                                cl.ID,
                                cl.FirstName,
                                cl.LastName,
                                cl.Patronymic,
                                cl.Gender?.Name,
                                cl?.Birthday == null ? "" : cl.Birthday.Value.ToString(),
                                cl.Phone,
                                cl.Email,
                                cl.RegistrationDate,
                                cl.VisitInfo.FirstOrDefault()?.DateLastVisit,
                                cl.VisitInfo.FirstOrDefault()?.CountVisit,
                                cl.TagOfClient.Select(t => t.Tag).FirstOrDefault()?.Title);

                            Tag tag = db.Client.FirstOrDefault(c => c.ID == cl.ID)
                            .TagOfClient.Select(t => t.Tag).FirstOrDefault();
                            Color color = Color.White;
                            if (tag != null)
                                color = ColorTranslator.FromHtml(String.Concat(tag?.Color?.Prepend('#')));

                            data_clients[data_clients.Columns.Count - 1,
                            data_clients.Rows.Count - 1].Style.BackColor = color;
                        });

                    if (data_clients.RowCount > 0)
                    {
                        int lastID = (int)data_clients[0, data_clients.RowCount - 1].Value;

                        status.Items[0].Text = string.Format("{0} из {1}",
                            db.Client.Where(filter).Count(c => c.ID <= lastID), db.Client.Count());
                    }
                }
            }
        }
        private void Create_columns()
        {
            data_clients.Columns.Add("ID", "Идентификатор");
            data_clients.Columns.Add("FirstName", "Имя");
            data_clients.Columns.Add("LastName", "Фамилия");
            data_clients.Columns.Add("Patronymic", "Отчество");
            data_clients.Columns.Add("Gender", "Пол");
            data_clients.Columns.Add("Birthday", "День рождения");
            data_clients.Columns.Add("Phone", "Телефон");
            data_clients.Columns.Add("Email", "Эл. почта");
            data_clients.Columns.Add("RegistrationDate", "Дата регистрации");
            data_clients.Columns.Add("DateLastVisit", "Последнее посещение");
            data_clients.Columns.Add("CountVisit", "Количесво посещений");
            data_clients.Columns.Add("Tag", "Список тегов");
            data_clients.Columns[0].Visible = false;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            Update_data_clients();
        }

        private void Update_data_clients()
        {
            Calculate_pages();
            Fill_data_clients(0, (int)num_countRowsOnPage.Value);
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            Create_columns();
            num_countRowsOnPage.Value = Math.Round((decimal)data_clients.Size.Height / data_clients.RowTemplate.Height, 0, MidpointRounding.AwayFromZero) - 2;
            Calculate_pages();

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

        private void Calculate_pages()
        {
            using (DataBaseEntities db = new DataBaseEntities())
            {
                MaxCountRows = db.Client.Count();
                num_countRowsOnPage.Maximum = MaxCountRows;
                MaxPage = MaxCountRows / (int)num_countRowsOnPage.Value - 1;
                Page = 0;
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (Page <= MaxPage)
            {
                Fill_data_clients(++Page, (int)num_countRowsOnPage.Value);
            }
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (Page > 0)
            {
                Fill_data_clients(--Page, (int)num_countRowsOnPage.Value);
            }
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
                data_clients.Sort(data_clients.Columns[comboBox.Tag.ToString()],
                cmb_lastName.SelectedItem.ToString() == "По возрастанию" ? ListSortDirection.Ascending : ListSortDirection.Descending);
            }
            else
            {
                data_clients.Sort(data_clients.Columns["ID"], ListSortDirection.Ascending);
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

            Fill_data_clients(Page, (int)num_countRowsOnPage.Value);
        }

        private void btn_showBirthDay_Click(object sender, EventArgs e)
        {
            using (DataBaseEntities db = new DataBaseEntities())
            {
                filter = c => c.Birthday.Value.Month == DateTime.Now.Month;
            }

            Fill_data_clients(Page, (int)num_countRowsOnPage.Value);
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
                    int ID = (int)data_clients[0, data_clients.SelectedRows[0].Index].Value;
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
                        Fill_data_clients(Page, (int)num_countRowsOnPage.Value);
                    }
                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Forms.Client client = new Forms.Client((int)data_clients[0, data_clients.SelectedRows[0].Index].Value);
            client.ShowDialog();
            Fill_data_clients(Page, (int)num_countRowsOnPage.Value);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Forms.Client client = new Forms.Client();
            client.ShowDialog();
            Fill_data_clients(Page, (int)num_countRowsOnPage.Value);
        }
    }
}
