using SchoolsLanguage.Classes;
using SchoolsLanguage.ModelDB;
using SchoolsLanguage.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolsLanguage.Forms
{
    public partial class Client : BaseForm
    {
        ImageList imageList = new ImageList();
        ModelDB.Client client;
        List<Gender> genders;
        List<Tag> tags;

        public Client()
        {
            InitializeComponent();
            Fill_gender();

            client = new ModelDB.Client();
            bindingSourceClient.Add(client);
            using (DataBaseEntities db = new DataBaseEntities())
            {
                client.ID = db.Client.ToList().OrderByDescending(c=>c.ID).First().ID + 1;
            }

            btn_showVisitInfo.Visible = false;
        }

        public Client(int ID)
        {
            InitializeComponent();
            Fill_gender();

            Fill_data(ID);
        }

        void Fill_listTag()
        {
            imageList.ImageSize = new Size(30, 30);
            listTags.SmallImageList = imageList;

            using (DataBaseEntities db = new DataBaseEntities())
            {
                tags = db.Client.FirstOrDefault(c => c.ID == client.ID)?.TagOfClient.ToList().Select(t => t.Tag)?.ToList();

                for (int i = 0; i < tags.Count; i++)
                {
                    Tag item = tags[i];
                    Add_tagInList(item);
                }
            }
        }

        private void Fill_data(int ID)
        {
            using (DataBaseEntities db = new DataBaseEntities())
            {
                bindingSourceClient.Add(db.Client.FirstOrDefault(c => c.ID == ID));

                client = ((ModelDB.Client)bindingSourceClient[0]);

                switch (client.Gender.Name)
                {
                    case "Мужской":
                        rbtn_male.Checked = true;
                        break;
                    default:
                        rbtn_female.Checked = true;
                        break;
                }
                string photoPath = Environment.CurrentDirectory + @"\" + client.PhotoPath;

                if (File.Exists(photoPath))
                    img_photo.Image = new Bitmap(photoPath);
            }
            Fill_listTag();
        }

        private void Client_Load(object sender, EventArgs e)
        {
        }

        private void Fill_gender()
        {
            using (DataBaseEntities db = new DataBaseEntities())
            {
                genders = db.Gender.AsNoTracking().ToList();

                rbtn_female.Tag = genders.FirstOrDefault(g => g.Name == "Женский");
                rbtn_male.Tag = genders.FirstOrDefault(g => g.Name == "Мужской");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(bindingSourceClient[0]);

            if (!Validator.TryValidateObject(client, context, results, true))
            {
                foreach (var error in results)
                {
                    MessageBox.Show(error.ErrorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (MessageBox.Show("Сохранить?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (DataBaseEntities db = new DataBaseEntities())
                {
                    if (!db.Client.ToList().Exists(c => c.ID == client.ID))
                    {
                        client.RegistrationDate = DateTime.Now;
                        db.Client.Add((ModelDB.Client)bindingSourceClient[0]);
                    }
                    var tagOfClients = db.TagOfClient.ToList();
                    foreach (ListViewItem item in listTags.Items)
                    {
                        Tag tag = (Tag)item.Tag;
                        TagOfClient tagOfClient = new TagOfClient { ClientID = client.ID, TagID = tag.ID };

                        if (tagOfClients.Exists(t => t.TagID == tagOfClient.TagID && t.ClientID == client.ID) == false)
                        {
                            db.Tag.Add(tag);
                            db.TagOfClient.Add(tagOfClient);
                        }
                    }
                    db.SaveChanges();
                }
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "*.png|*.png|*.jpg|*.jpg";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(openFile.FileName);

                if (file.Length / (1024 * 1024) < 2)
                {
                    img_photo.Image = new Bitmap(file.FullName);
                    client.PhotoPath = "Клиенты\\" + file.Name;
                }
                else
                {
                    MessageBox.Show("Размер фотографии не должен превышать 2 мегабайта",
                        "Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void rbtn_checked(object sender, EventArgs e)
        {
            RadioButton radioButton = ((RadioButton)sender);

            client.GenderCode = ((Gender)radioButton.Tag).Code;
        }

        private void date_birthDay_ValueChanged(object sender, EventArgs e)
        {
            client.Birthday = new DateTime(date_birthDay.Value.Ticks);
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Закрыть окно? \nДанные могут быть потеряны", "Закрытие",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

            }
            else
                e.Cancel = true;
        }
        private void addTag_Click(object sender, EventArgs e)
        {
            addTag addTag = new addTag();

            if(addTag.ShowDialog() == DialogResult.OK)
            {
                Tag item = (Tag)addTag.Tag;
                Add_tagInList(item);
            }
        }

        private void Add_tagInList(Tag item)
        {
            int X = 30, Y = 30;
            Bitmap bitmap = new Bitmap(X, Y);
            Color colorTag = ColorTranslator.FromHtml("#" + item.Color);
            Enumerable.Range(0, X).ToList().ForEach(x =>
            {
                Enumerable.Range(0, Y).ToList().ForEach(y =>
                bitmap.SetPixel(x, y, colorTag));
            });

            imageList.Images.Add(bitmap);
            ListViewItem listViewItem = new ListViewItem(new string[] { "", item.Title }, imageList.Images.Count - 1)
            {
                Tag = item
            };
            listTags.Items.Add(listViewItem);
        }

        private void deleteTag_Click(object sender, EventArgs e)
        {
            if (listTags.SelectedItems.Count > 0)
            {
                using (DataBaseEntities db = new DataBaseEntities())
                {
                    Tag tag = (Tag)listTags.SelectedItems[0].Tag;
                    TagOfClient tagOfClient = db.TagOfClient.FirstOrDefault(t => t.TagID == tag.ID && t.ClientID == client.ID);

                    if (db.TagOfClient.ToList().Exists(t => t.ID == tagOfClient.ID
                    && t.ClientID == tagOfClient.ClientID))
                    {
                        listTags.Items.Remove(listTags.SelectedItems[0]);
                        db.TagOfClient.Remove(tagOfClient);
                    }

                    db.SaveChanges();
                }
            }
        }

        private void btn_showVisitInfo_Click(object sender, EventArgs e)
        {
            VisitsInfoOfClient visitsInfoOfClient = new VisitsInfoOfClient(client.ID);
            visitsInfoOfClient.ShowDialog();
        }
    }
}
