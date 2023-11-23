using Dal.Common;
using PersonAdmin.Dal.Ado;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Dal.Simple;
using PersonAdmin.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace PersonAdmin.Client
{
    class DalTester
    {
        private readonly IPersonDao personDao;
        public DalTester(IPersonDao personDao)
        {
            this.personDao = personDao;
        }
        public async Task TestFindAllAsync()
        {
            (await personDao.FindAllAsync())
                .ToList()
                .ForEach(x => Console.WriteLine($"{x.Id,5} | {x.FirstName,-10} | {x.LastName,-15} | {x.DateOfBirth,10:d}"));
        }

        public async Task TestFindByIdAsync()
        {
            Person person1 = await personDao.FindByIdAsync(1);
            Console.WriteLine($"FindById(1) -> {person1?.ToString() ?? "<null>"}");

            Person person2 = await personDao.FindByIdAsync(99);
            Console.WriteLine($"FindById(99) -> {person2?.ToString() ?? "<null>"}");
        }

        public async Task TestUpdateAsync()
        {
            Person person = await personDao.FindByIdAsync(1);
            Console.WriteLine($"before update: person -> {person?.ToString() ?? "<null>"}");
            if (person == null) return;

            person.DateOfBirth = DateTime.Now.AddYears(-100);
            await personDao.UpdatePersonAsync(person);

            person = await personDao.FindByIdAsync(1);
            Console.WriteLine($"after update:  person -> {person?.ToString() ?? "<null>"}");
        }

        public async Task TestInsertAsync()
        {
            Person newPerson = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Now
            };
            await personDao.InsertAsync(newPerson); // id is updated! 
            Console.WriteLine($"person inserted -> {newPerson}");
        }

        public async Task TestTransactionsAsync()
        {
            Person person1 = await personDao.FindByIdAsync(1);
            Person person2 = await personDao.FindByIdAsync(2);

            DateTime oldDate1 = person1.DateOfBirth;
            DateTime oldDate2 = person2.DateOfBirth;
            DateTime newDate1 = DateTime.MinValue;
            DateTime newDate2 = DateTime.MinValue;

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    person1.DateOfBirth = newDate1 = oldDate1.AddDays(1);
                    person2.DateOfBirth = newDate2 = oldDate2.AddDays(1);
                    await personDao.UpdatePersonAsync(person1);
                    // throw new ArgumentException(); // comment this out to rollback transaction
                    await personDao.UpdatePersonAsync(person2);
                    scope.Complete();
                }
            }
            catch
            {
            }

            person1 = await personDao.FindByIdAsync(1);
            person2 = await personDao.FindByIdAsync(2);

            if (oldDate1 == person1.DateOfBirth && oldDate2 == person2.DateOfBirth)
                Console.WriteLine("Transaction was ROLLED BACK.");
            else if (newDate1 == person1.DateOfBirth && newDate2 == person2.DateOfBirth)
                Console.WriteLine("Transaction was COMMITTED.");
            else
                Console.WriteLine("No Transaction.");
        }

    }

    class Program
    {
        private static void PrintTitle(string text = "", int length = 60, char ch = '-')
        {
            int preLen = (length - (text.Length + 2)) / 2;
            int postLen = length - (preLen + text.Length + 2);
            Console.WriteLine($"{new string(ch, preLen)} {text} {new string(ch, postLen)}");
        }

        private static async Task Main()
        {
            DalTester tester1 = new DalTester(new SimplePersonDao());
            PrintTitle("SimplePersonDao.FindAll");
            await tester1.TestFindAllAsync();
            var config = ConfigurationUtil.GetConfiguration();
            var factory = DefaultConnectionFactory.FromConfiguration(config, "PersonDbConnection");
            DalTester tester2 = new(new MSSQLPersonDao(factory));
            PrintTitle("AdoPersonDao.FindAll");
            await tester2.TestFindAllAsync();

            PrintTitle("AdoPersonDao.FindById");
            await tester2.TestFindByIdAsync();
            PrintTitle("AdoPersonDao.UpdatePersonAsync");
            await tester2.TestUpdateAsync();
            PrintTitle("AdoPersonDao.FindAll");
            await tester2.TestFindAllAsync();

            PrintTitle("AdoPersonDao.InsertAsync");
            await tester2.TestInsertAsync();
            PrintTitle("AdoPersonDao.FindAll");
            await tester2.TestFindAllAsync();
        }
    }
}
