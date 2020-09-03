using SchoolsLanguage.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsLanguage.Classes
{
    class ClientsModel
    {
        public List<dynamic> GetClient(Func<Client, bool> filter)
        {
            using (DataBaseEntities db = new DataBaseEntities())
            {
                return db.Client.Where(filter).Skip(page * countRows).
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
            }
        }
    }
}
