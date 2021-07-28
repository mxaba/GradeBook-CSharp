using System;
using System.Collections.Generic;

namespace GradeBook {
    class Program {
        static void Main(string[] args)
        {

            IBook book = new DiskBook("Mcebo's Grade-Book");
            book.GradeAdded += OnGradeAdded;
            EnteredGrades(book);

            var stats = book.GetStatistics();

            Console.WriteLine($"For the book named: {book.Name}");
            Console.WriteLine($"The lowest grade is: {stats.Low:N2}");
            Console.WriteLine($"The highest grade is: {stats.High:N2}");
            Console.WriteLine($"The avverage grade is: {stats.Average:N2}");
            Console.WriteLine($"The Letter grade is: {stats.Letter:N2}");
        }

        private static void EnteredGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or 'q'to quit!");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }
                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        static void OnGradeAdded(object sender, EventArgs args){
            Console.WriteLine("A grade was added!");
        }
    }
}
