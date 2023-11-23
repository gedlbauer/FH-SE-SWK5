using PersonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAdmin.Dal.Interface
{
    public interface IPersonDao
    {
        Task<IEnumerable<Person>> FindAllAsync();
        Task<Person> FindByIdAsync(int id);
        Task<bool> UpdatePersonAsync(Person person);
        Task InsertAsync(Person person);
    }
}
