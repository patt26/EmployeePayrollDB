using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll!");
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel employee = new EmployeeModel();
            employee.EmployeeName = "Mohan";
            employee.Department = "Tech1";
            employee.PhoneNumber = 254689634;
            employee.Address = "02-Patna";
            employee.Gender = "M";
            employee.BasicPay = 10000.00M;
            employee.Deductions = 150000;
            employee.StartDate = Convert.ToDateTime("2020-11-03");

            if (repo.AddEmployee(employee))
                Console.WriteLine("Records added successfully");
            repo.GetAllEmployee();
            Console.ReadKey();
        }


    }
}
