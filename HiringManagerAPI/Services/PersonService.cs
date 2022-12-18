using HiringManagerAPI.Models;

namespace HiringManagerAPI.Services
{
    public static class PersonService
    {
        static List<Person> PersonList { get; set; }

        static PersonService()
        {
            PersonList = new List<Person>();
        }

        public static List<Person> GetAll() => PersonList;

        public static Person? Get(int id)
        {
            return PersonList?.FirstOrDefault(p => p.Id == id);
        }

        public static void Add(Person person)
        {
            int newId = (PersonList != null && PersonList.Count() > 0) ? (PersonList.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1) : 0;
            person.Id = newId++;
            PersonList.Add(person);
        }

        public static void Update(Person person)
        {
            if(person != null && person.Id >= 0)
            {
                Person oldPerson = Get(person.Id);

                int index = PersonList.FindIndex(0, PersonList.Count(), p => p.Id == person.Id);
                
                if(oldPerson != null)
                {
                    oldPerson = person;
                    PersonList[index] = oldPerson;
                }
            }
        }

        public static void Delete(int id)
        {
            Person person = Get(id);

            if(person != null)
            {
                PersonList.Remove(person);
            }
        }

    }
    
}
