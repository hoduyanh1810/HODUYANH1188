using System;

namespace BTH.Models
{
    public class Person
    {
        public string Name { get; set; } = "";
        public int YearOfBirth { get; set; }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - YearOfBirth;
            }
        }
    }
}
