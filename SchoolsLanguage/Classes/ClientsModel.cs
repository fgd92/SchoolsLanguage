using SchoolsLanguage.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsLanguage.Classes
{
    public class ClientsModel
    {
        /// <summary>
        /// Максимальное количество строк которое нужно получить
        /// </summary>
        public int MaxTakeRows { get; set; }
        /// <summary>
        ///  Максимальное количество строк которое нужно пропустить
        /// </summary>
        public int MaxSkipRows { get; set; }

        public int MaxRows { get; set; }

        private int page;
        private int maxPage;

        public int GetPage()
        {
            return page;
        }

        public void SetPage(int value)
        {
            MaxSkipRows = MaxTakeRows * value;
            page = value;
        }

        public void NextPage()
        {
            if (page + 1 < maxPage)
                SetPage(page + 1);
        }
        public void PrevPage()
        {
            if (page - 1 >= 0)
                SetPage(page - 1);
        }
        public ClientsModel(int maxTakeRows)
        {
            MaxRows = Count();
            MaxTakeRows = maxTakeRows;
            maxPage = MaxRows / maxTakeRows;
            SetPage(0);
        }
        /// <summary>
        /// Возвращает список клиентов
        /// </summary>
        /// <param name="predicate">Фильтрация</param>
        /// <param name="countSkipRows">Количество пропускаемых строк</param>
        /// <returns></returns>
        public IEnumerable<ClientView> Get(Func<Client, bool> predicate)
        {
            using (DataBaseEntities db = new DataBaseEntities())
            {
                if (MaxSkipRows != db.Client.Count())
                {
                    var list = db.Client.Include("ClientService").Include("TagOfClient").ToList()
                        .Where(predicate).Skip(MaxSkipRows)
                        .Take(MaxTakeRows).ToList();

                    foreach (var item in list)
                    {
                        ClientView clientService = new ClientView(item);
                        yield return clientService;
                    }
                }
            }
        }
        public IEnumerable<ClientView> SortAscending(Func<Client,bool> predicate,Func<ClientView,string> keySelector)
        {
            return Get(predicate).OrderBy(keySelector);
        }
        public IEnumerable<ClientView> SortDescending(Func<Client, bool> predicate, Func<ClientView, string> keySelector)
        {
            return Get(predicate).OrderByDescending(keySelector);
        }
        /// <summary>
        /// Возвращает количество клиентов до указаного ID
        /// </summary>
        /// <param name="predicate">Фильтрация</param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Count(Func<Client, bool> predicate, int ID)
        {
            using (DataBaseEntities db = new DataBaseEntities())
            {
                return db.Client.Where(predicate).OrderBy(c => c.ID).Count(c => c.ID <= ID);
            }
        }
        /// <summary>
        /// Возвращает количество клиентов
        /// </summary>
        public int Count()
        { 
            using(DataBaseEntities db = new DataBaseEntities())
            {
                return db.Client.Count();
            }
        }
    }
}
