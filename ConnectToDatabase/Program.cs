using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectToDatabase.Model;

namespace ConnectToDatabase
{
    class Program
    {
        private static readonly Entities Lesson2Db = new Entities();
        static void Main(string[] args)
        {
            foreach (department department in Lesson2Db.departments)
            {
                Console.WriteLine($"name = {department.department_name}");
            }
        }
    }
}
