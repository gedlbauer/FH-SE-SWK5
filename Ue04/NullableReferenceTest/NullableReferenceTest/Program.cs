using System;
#nullable enable
namespace NullableReferenceTest
{
    class Person
    {
        public string? FirstName { get; set; }
        public string LastName { get; set; }

        public Person(string? firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person(null, "Huber");
        }
    }
}
