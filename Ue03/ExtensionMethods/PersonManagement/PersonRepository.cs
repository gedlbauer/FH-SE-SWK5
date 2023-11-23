using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace PersonManagement
{
    public class PersonRepository
    {
        private readonly IList<Person> persons = new List<Person>();

        public void AddPerson(Person person)
        {
            persons.Add(person);
        }

        public void AddPersons(IEnumerable<Person> persons)
        {
            this.persons.AddRange(persons);
        }

        public void PrintPersons(TextWriter textWriter)
        {
            persons.ForEach(x => textWriter.WriteLine(x));
        }

        public IEnumerable<(string, string)> GetPersonNames()
        {
            return persons.Select(x => (x.FirstName, x.LastName));
            //return persons.Map(x => (x.FirstName, x.LastName)); // selbst gepfuschtes Select
        }

        public IEnumerable<Person> FindPersonsByCity(string city)
        {
            return persons.Where(x => x.City == city);
            //return persons.FilterBy(x => x.City == city); // selbst gepfuschtes Where
        }

        public Person FindYoungestPerson()
        {
            //return persons.OrderByDescending(x => x.DateOfBirth).FirstOrDefault() ?? throw new ArgumentException("Undefined for empty Collection");
            return persons.MaxBy((x, y) => DateTime.Compare(x.DateOfBirth, y.DateOfBirth));
        }


        public IEnumerable<Person> FindPersonsSortedByAgeAscending()
        {
            return null;
        }
    }
}
