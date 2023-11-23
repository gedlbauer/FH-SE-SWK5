using Dal.Common;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAdmin.Dal.Ado
{
    public abstract class AdoPersonDao : IPersonDao
    {
        private readonly AdoTemplate template;
        protected abstract string LastInsertedIdQuery { get; }

        public AdoPersonDao(IConnectionFactory connectionFactory)
        {
            template = new AdoTemplate(connectionFactory);
        }

        private Person MapRowToPerson(IDataRecord record) => new Person
        {
            Id = (int)record["Id"],
            FirstName = (string)record["first_name"],
            LastName = (string)record["last_name"],
            DateOfBirth = (DateTime)record["date_of_birth"]
        };

        public async Task<IEnumerable<Person>> FindAllAsync()
        {
            return await template.QueryAsync("SELECT * FROM person", MapRowToPerson);
        }

        public async Task<Person> FindByIdAsync(int id)
        {
            return await template.QuerySingleAsync(
                "SELECT * FROM person WHERE id=@id",
                MapRowToPerson,
                new QueryParameter("@id", id));
        }

        public async Task<bool> UpdatePersonAsync(Person person)
        {
            return await template.ExecuteAsync(
                "UPDATE person SET first_name=@first_name, last_name=@last_name, date_of_birth=@date_of_birth WHERE id=@id",
                new QueryParameter("@id", person.Id),
                new QueryParameter("@first_name", person.FirstName),
                new QueryParameter("@last_name", person.LastName),
                new QueryParameter("@date_of_birth", person.DateOfBirth)
                ) == 1;
        }

        public async Task InsertAsync(Person person)
        {
            const string SQL_INSERT = "INSERT INTO person(first_name, last_name, date_of_birth) VALUES (@first_name, @last_name, @date_of_birth)";
                        
            var r = (int) await template.ExecuteScalarAsync<object>(
                $"{SQL_INSERT}; {LastInsertedIdQuery}",
                new QueryParameter("@first_name", person.FirstName),
                new QueryParameter("@last_name", person.LastName),
                new QueryParameter("@date_of_birth", person.DateOfBirth));
            person.Id = r;
        }
    }
}
