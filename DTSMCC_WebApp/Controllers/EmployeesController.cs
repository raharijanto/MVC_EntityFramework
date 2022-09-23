using DTSMCC_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DTSMCC_WebApp.Controllers
{
    public class EmployeesController : Controller
    {
        SqlConnection connectSql;
        string connectionString = 
            "Data Source=DESKTOP-QC1NOEP;" +
            "Initial Catalog=DBMCC001;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False";

        // Read
        public IActionResult Index()
        {
            connectSql = new SqlConnection(connectionString);
            SqlCommand commandSql = new SqlCommand();
            commandSql.Connection = connectSql;
            commandSql.CommandText = 
                "select employees.employeeid, employees.employeename, jobs.jobname " +
                "from employees " +
                "join jobs " +
                "on employees.idjob = jobs.jobid";
            List<Employees> Employees = new List<Employees>();
            List<Jobs> Jobs = new List<Jobs>();
            
            try
            {
                connectSql.Open();
                using (SqlDataReader readData = commandSql.ExecuteReader())
                {
                    if (readData.HasRows)
                    {
                        while (readData.Read())
                        {
                            Employees worker = new Employees();
                            Jobs occupation = new Jobs();
                            worker.EmployeeId = Convert.ToInt32(readData[0]);
                            worker.EmployeeName = (readData[1]).ToString();
                            worker.IdJob = Convert.ToInt32(readData[2]);
                            Employees.Add(worker);
                            
                        }
                    }
                    readData.Close();
                }
                connectSql.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return View(Employees);
        }

        // Read by Id
        public IActionResult IndexId(int id)
        {
            connectSql = new SqlConnection(connectionString);
            SqlCommand commandSql = new SqlCommand();
            commandSql.Connection = connectSql;
            commandSql.CommandText = 
                "select " +
                    "employees.employeeid, employees.employeename, jobs.jobname " +
                "from " +
                    "employees " +
                "join " +
                    "jobs on employees.idjob = jobs.jobid " +
                "where " +
                    "employees.employeeid = @id";

            SqlParameter paramSql = new SqlParameter();
            paramSql.ParameterName = "@id";
            paramSql.Value = id;
            commandSql.Parameters.Add(paramSql);
            List<Employees> Employees = new List<Employees>();

            try
            {
                connectSql.Open();
                using (SqlDataReader readData = commandSql.ExecuteReader())
                {
                    if (readData.HasRows)
                    {
                        while (readData.Read())
                        {
                            Employees worker = new Employees();
                            Jobs occupation = new Jobs();
                            worker.EmployeeId = Convert.ToInt32(readData[0]);
                            worker.EmployeeName = (readData[1]).ToString();
                            worker.IdJob = Convert.ToInt32(readData[2]);
                            Employees.Add(worker);
                        }
                    }
                    readData.Close();
                }
                connectSql.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return View(Employees);
        }

        // Create
        // - Get
        public IActionResult Create()
        {

            return View();
        }

        // - Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employees employees)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                SqlTransaction transaction = connect.BeginTransaction();

                SqlCommand command = connect.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "insert into employees" +
                        "(employeeid, employeename, idjob)" +
                        "values (@employeeid, @employeename, @idjob)";

                    SqlParameter paramSQL = new SqlParameter();
                    paramSQL.ParameterName = "@employeeid";
                    paramSQL.Value = employees.EmployeeId;

                    SqlParameter paramSQL2 = new SqlParameter();
                    paramSQL2.ParameterName = "@employeename";
                    paramSQL2.Value = employees.EmployeeName;

                    SqlParameter paramSQL3 = new SqlParameter();
                    paramSQL3.ParameterName = "@idjob";
                    paramSQL3.Value = employees.IdJob;

                    command.Parameters.Add(paramSQL);
                    command.Parameters.Add(paramSQL2);
                    command.Parameters.Add(paramSQL3);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
            return View(employees);
        }

        // Update
        // - Get
        public IActionResult Update()
        {
            return View();
        }
        
        // - Put
        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employees employees)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                SqlTransaction transaction = connect.BeginTransaction();

                SqlCommand command = connect.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "update employees " +
                        "set idjob = @idjob " +
                        "where employeeid = @employeeid";

                    SqlParameter paramSQL = new SqlParameter();
                    paramSQL.ParameterName = "@employeeid";
                    paramSQL.Value = employees.EmployeeId;

                    //SqlParameter paramSQL2 = new SqlParameter();
                    //paramSQL2.ParameterName = "@employeename";
                    //paramSQL2.Value = employees.EmployeeName;

                    SqlParameter paramSQL3 = new SqlParameter();
                    paramSQL3.ParameterName = "@idjob";
                    paramSQL3.Value = employees.IdJob;

                    command.Parameters.Add(paramSQL);
                    //command.Parameters.Add(paramSQL2);
                    command.Parameters.Add(paramSQL3);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
            return View(employees);
        }
        
        // Delete
        // - Get
        public IActionResult Delete()
        {
            return View();
        }
        
        // - Post
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employees employees)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                SqlTransaction transaction = connect.BeginTransaction();

                SqlCommand command = connect.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "delete from employees " +
                        "where employeeid = @employeeid";

                    SqlParameter paramSQL = new SqlParameter();
                    paramSQL.ParameterName = "@employeeid";
                    paramSQL.Value = employees.EmployeeId;

                    //SqlParameter paramSQL2 = new SqlParameter();
                    //paramSQL2.ParameterName = "@employeename";
                    //paramSQL2.Value = employees.EmployeeName;

                    //SqlParameter paramSQL3 = new SqlParameter();
                    //paramSQL3.ParameterName = "@idjob";
                    //paramSQL3.Value = employees.IdJob;

                    command.Parameters.Add(paramSQL);
                    //command.Parameters.Add(paramSQL2);
                    //command.Parameters.Add(paramSQL3);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
            return View(employees);
        }
    }
}
