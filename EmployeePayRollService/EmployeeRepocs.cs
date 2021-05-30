using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollService
{
    public class EmployeeRepo
    {
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = payroll_service; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        EmployeeModel employeeModel = new EmployeeModel();
        public void GetAllEmployee()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
               
                using (connection)
                {
                    string query = @"Select * from employee_payroll1;";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employeeModel.EmployeeID = dr.GetInt32(0);

                            employeeModel.EmployeeName = dr.GetString(1);
                            employeeModel.BasicPay = dr.GetDecimal(2);
                            employeeModel.StartDate = dr.GetDateTime(3);
                            employeeModel.Gender = dr.GetString(4);
                            employeeModel.PhoneNumber = dr.GetInt32(5);
                            employeeModel.Address = dr.GetString(6);
                            employeeModel.Department = dr.GetString(7);
                            employeeModel.Deductions = dr.GetDecimal(8);
                            employeeModel.TaxablePay = dr.GetDecimal(9);
                            employeeModel.Tax = dr.GetDecimal(10);
                            employeeModel.NetPay = dr.GetDecimal(11);
                            System.Console.WriteLine(employeeModel.EmployeeName + " " + employeeModel.BasicPay + " " + employeeModel.StartDate + " " + employeeModel.Gender + " " + employeeModel.PhoneNumber + " " + employeeModel.Address + " " + employeeModel.Department + " " + employeeModel.Deductions + " " + employeeModel.TaxablePay + " " + employeeModel.Tax + " " + employeeModel.NetPay);
                            System.Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public bool AddEmployee(EmployeeModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    //var qury=values()
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    //command.Parameters.AddWithValue("@City", model.City);
                    //command.Parameters.AddWithValue("@Country", model.Country);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        public bool UpdateEmployee(EmployeeModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand update = new SqlCommand("spUpdateEmployeeSalary",connection);
                    update.CommandType = System.Data.CommandType.StoredProcedure;
                    update.Parameters.AddWithValue("@id", model.EmployeeID);
                    update.Parameters.AddWithValue("@BBasic_Pay", model.BasicPay);
                    connection.Open();
                    var result = update.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        public void RetriveDateRange()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = "SELECT * from employee_payroll1 where start between '2020-02-01' and GETDATE()";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employeeModel.EmployeeName = dr.GetString(1);
                            employeeModel.StartDate = dr.GetDateTime(3);
                            Console.WriteLine("Name= {0}  Date={1}",employeeModel.EmployeeName,employeeModel.StartDate);

                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("No such Data Found!!");
                    }
                }

                
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            
        }
        public void Operations()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string SumQuery =@"select sum (Basic_Pay) from employee_payroll1 where gender='M' group by gender";
                    string AverageQuery = "select avg (Basic_Pay) from employee_payroll1 where gender='M' group by gender";
                    string MinQuery = "select min (Basic_Pay) from employee_payroll1 where gender='M' group by gender";
                    string MaxQuery = "select max(Basic_Pay) from employee_payroll1 where gender = 'M' group by gender";
                    string CountQuery = "select count (Basic_Pay) from employee_payroll1 where gender ='M' group by gender";

                    SqlCommand Sumcmd = new SqlCommand(SumQuery, connection);
                    connection.Open();
                    SqlDataReader dr = Sumcmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            decimal Sum=  dr.GetDecimal(0);
                            //employeeModel.Gender = dr.GetString(4);
                            Console.WriteLine("Salary= {0}  Gender={1}", Sum, employeeModel.Gender);

                        }

                    }
                    else
                    {
                        Console.WriteLine("No such Data Found!!");
                    }
                    connection.Close();
                    
                    SqlCommand Avgcmd = new SqlCommand(AverageQuery, connection);
                    connection.Open();
                    SqlDataReader Avgdr = Avgcmd.ExecuteReader();
                    if (Avgdr.HasRows)
                    {
                        while(Avgdr.Read())
                        {
                            decimal AVG = Avgdr.GetDecimal(0);
                            Console.WriteLine("Average of Salaries = "+AVG);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No such Data Found!!");
                        
                    }
                    connection.Close();

                    SqlCommand Mincmd = new SqlCommand(MinQuery, connection);
                    connection.Open();
                    SqlDataReader Mindr = Mincmd.ExecuteReader();
                    if (Mindr.HasRows)
                    {
                        while (Mindr.Read())
                        {
                            decimal MIN = Mindr.GetDecimal(0);
                            Console.WriteLine("Minimum Salary = " + MIN);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No such Data Found!!");

                    }
                    connection.Close();


                    SqlCommand Maxcmd = new SqlCommand(MaxQuery, connection);
                    connection.Open();
                    SqlDataReader Maxdr = Maxcmd.ExecuteReader();
                    if (Maxdr.HasRows)
                    {
                        while (Maxdr.Read())
                        {
                            decimal MAX = Maxdr.GetDecimal(0);
                            Console.WriteLine("Maximum Salary = " + MAX);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No such Data Found!!");

                    }
                    connection.Close();


                    SqlCommand Countcmd = new SqlCommand(CountQuery, connection);
                    connection.Open();
                    SqlDataReader Countdr = Countcmd.ExecuteReader();
                    if (Countdr.HasRows)
                    {
                        while (Countdr.Read())
                        {
                            int COUNT = Countdr.GetInt32(0);
                            Console.WriteLine("Total Employees = " + COUNT);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No such Data Found!!");

                    }
                    

                }
                


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }








        }

    }
}
            
        

    



