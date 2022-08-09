using System;
using System.Collections.Generic;
using ExerciseLanbdaDelegatesLinq.Entities;
using System.IO;
using System.Globalization;
using System.Linq;

namespace ExerciseLanbdaDelegatesLinq
{
    class Program
    {
        private static double imputSalary;

        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Employees> list = new List<Employees>();

            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    string name = fields[0];
                    string email = fields[1];
                    double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);
                    list.Add(new Employees(name, email, salary));
                }
            }
            Console.Write("Enter salary: ");
            Program.imputSalary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            var emailSalary = list.Where(e => e.Salary > imputSalary).OrderBy(e => e.Email).DefaultIfEmpty();
            var sumSalary = list.Where(e => e.Name[0] == 'M').DefaultIfEmpty().Sum(e => e.Salary).ToString("F2", CultureInfo.InvariantCulture);
            Console.WriteLine($"Email of people whose salary is more than {imputSalary.ToString("F2", CultureInfo.InvariantCulture)}: ");
            foreach (Employees eS in emailSalary)
            {
                Console.WriteLine(eS.Email);
            }
            Console.Write("Sum of salary of people whose name starts with 'M': ");
            Console.WriteLine(sumSalary);
        }
    }
}
