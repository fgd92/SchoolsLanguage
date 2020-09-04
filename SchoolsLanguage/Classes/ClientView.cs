using SchoolsLanguage.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsLanguage.Classes
{
    public class ClientView
    {
        public int ID { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Patronymic { get; }
        public string Name { get; }
        public string Birthday { get; }
        public string Phone { get; }
        public string Email { get; }
        public DateTime RegistrationDate { get; }
        public DateTime? DateLastVisit { get; }
        public int CountVisit { get; }
        public string Title { get; }

        public ClientView(Client client)
        {
            int count = client.ClientService.Count();
            DateTime? lastVisit = null;
            if (client.ClientService.Count() > 0)
                lastVisit = client.ClientService.Last().StartTime;

            ID = client.ID;
            FirstName = client.FirstName;
            LastName = client.LastName;
            Patronymic = client.Patronymic;
            Name = client.Gender?.Name;
            Birthday = client?.Birthday == null ? "" : client.Birthday.Value.ToString();
            Phone = client.Phone;
            Email = client.Email;
            RegistrationDate = client.RegistrationDate;
            DateLastVisit = lastVisit;
            CountVisit = count;
            Title = client.TagOfClient.Select(t => t.Tag).FirstOrDefault()?.Title;
        }

        public override bool Equals(object obj)
        {
            return obj is ClientView other &&
                   ID == other.ID &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   Patronymic == other.Patronymic &&
                   Name == other.Name &&
                   Birthday == other.Birthday &&
                   Phone == other.Phone &&
                   Email == other.Email &&
                   RegistrationDate == other.RegistrationDate &&
                   DateLastVisit == other.DateLastVisit &&
                   CountVisit == other.CountVisit &&
                   Title == other.Title;
        }

        public override int GetHashCode()
        {
            var hashCode = 1027370663;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Patronymic);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Birthday);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Phone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + RegistrationDate.GetHashCode();
            hashCode = hashCode * -1521134295 + DateLastVisit.GetHashCode();
            hashCode = hashCode * -1521134295 + CountVisit.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            return hashCode;
        }
    }
}
