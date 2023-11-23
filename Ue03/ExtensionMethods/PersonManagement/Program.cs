using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersonManagement
{
    class Program
    {
        private static void Main()
        {
            PersonRepository personRepository = new PersonRepository();
            IEnumerable<Person> persons;

            try
            {
                using var reader = new StreamReader("persons.json");
                var jsonText = reader.ReadToEnd();
                persons = JsonSerializer.Deserialize<IEnumerable<Person>>(jsonText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                personRepository.AddPersons(persons);
            }
            catch (FileNotFoundException fnfEx)
            {
                Console.WriteLine(fnfEx.Message);
                return;
            }

            using TextWriter textWriter = Console.Out;

            textWriter.WriteLine("=====================================================");
            textWriter.WriteLine("Person list");
            textWriter.WriteLine("=====================================================");

            personRepository.PrintPersons(textWriter);

            textWriter.WriteLine();
            textWriter.WriteLine("=====================================================");
            textWriter.WriteLine("Persons in Hagenberg");
            textWriter.WriteLine("=====================================================");

            personRepository.FindPersonsByCity("Hagenberg").ForEach(textWriter.WriteLine);

            textWriter.WriteLine();
            textWriter.WriteLine("=====================================================");
            textWriter.WriteLine("Person names");
            textWriter.WriteLine("=====================================================");

            foreach (var (first, last) in personRepository.GetPersonNames())
            {
                textWriter.WriteLine($"{first} {last}");
            }

            textWriter.WriteLine();
            textWriter.WriteLine("=====================================================");
            textWriter.WriteLine($"Youngest person");
            textWriter.WriteLine("=====================================================");

            textWriter.WriteLine(personRepository.FindYoungestPerson());

            //textWriter.WriteLine();
            //textWriter.WriteLine("=====================================================");
            //textWriter.WriteLine("Persons sorted by age ascending");
            //textWriter.WriteLine("=====================================================");
            //
            // TODO
            //
        }
    }
}
