using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonAdmin.Dal.Simple
{
    public class SimplePersonDao : IPersonDao
    {
        private static IList<Person> personList = new List<Person> {
            new Person { Id=1, FirstName="John", LastName="Doe",        DateOfBirth=DateTime.Now.AddYears(-10) },
            new Person { Id=2, FirstName="Jane", LastName="Doe",        DateOfBirth=DateTime.Now.AddYears(-20) },
            new Person { Id=3, FirstName="Max",  LastName="Mustermann", DateOfBirth=DateTime.Now.AddYears(-30) }
        };

        public async Task<IEnumerable<Person>> FindAllAsync()
        {
            return await Task.FromResult(personList);
        }

        public async Task<Person> FindByIdAsync(int id)
        {
            return await Task.FromResult(personList.Single(x => x.Id == id));
        }

        public Task InsertAsync(Person person)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePersonAsync(Person person)
        {
            var p = await FindByIdAsync(person.Id);
            if (p is null) return false;

            p.FirstName = person.FirstName;
            p.LastName = person.LastName;
            p.DateOfBirth = person.DateOfBirth;

            return true;
        }
    }
}
