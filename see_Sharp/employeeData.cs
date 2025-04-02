using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagementSystem
{
    // This class handles database operations for employee data
    class EmployeeData
    {
        // Properties to store employee information
        public int ID { set; get; }                // Primary key in database
        public string EmployeeID { set; get; }     // Employee's unique identifier
        public string Name { set; get; }           // Employee's full name
        public string Gender { set; get; }         // Employee's gender
        public string Contact { set; get; }        // Employee's contact information
        public string Position { set; get; }       // Employee's job position
        public string Image { set; get; }          // Path to employee's image
        public int Salary { set; get; }            // Employee's salary amount
        public string Status { set; get; }         // Current employment status

        // Database connection string - specifies the server to connect to
        // Note: Uses Windows Authentication (Integrated Security)
        SqlConnection connect =
            new SqlConnection(@"Data Source=LAPTOP-KWOKIAN-;Integrated Security=True;Encrypt=False");

        /// <summary>
        /// Retrieves all active employees with complete information from the database
        /// </summary>
        /// <returns>List of employee objects with all properties populated</returns>
        public List<EmployeeData> employeeListData()
        {
            // Create empty list to store employee data
            List<EmployeeData> listdata = new List<EmployeeData>();

            // Only open connection if it's not already open
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    // Open database connection
                    connect.Open();

                    // SQL query to get all non-deleted employees
                    string selectData = "SELECT * FROM employees WHERE delete_date IS NULL";

                    // Create and execute SQL command
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Loop through each row in the result set
                        while (reader.Read())
                        {
                            // Create new employee object and populate its properties
                            EmployeeData ed = new EmployeeData();
                            ed.ID = (int)reader["id"];
                            ed.EmployeeID = reader["employee_id"].ToString();
                            ed.Name = reader["full_name"].ToString();
                            ed.Gender = reader["gender"].ToString();
                            ed.Contact = reader["contact_number"].ToString();
                            ed.Position = reader["position"].ToString();
                            ed.Image = reader["image"].ToString();
                            ed.Salary = (int)reader["salary"];
                            ed.Status = reader["status"].ToString();

                            // Add employee to the list
                            listdata.Add(ed);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Display any errors that occur
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    // Always close the connection when done
                    connect.Close();
                }
            }
            return listdata;
        }

        /// <summary>
        /// Retrieves only salary-related information for active employees
        /// </summary>
        /// <returns>List of employee objects with only ID, name, position, and salary</returns>
        public List<EmployeeData> salaryEmployeeListData()
        {
            // Create empty list to store employee data
            List<EmployeeData> listdata = new List<EmployeeData>();

            // Only open connection if it's not already open
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    // Open database connection
                    connect.Open();

                    // SQL query to get all non-deleted employees
                    string selectData = "SELECT * FROM employees WHERE delete_date IS NULL";

                    // Create and execute SQL command
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Loop through each row in the result set
                        while (reader.Read())
                        {
                            // Create new employee object with only salary-related properties
                            EmployeeData ed = new EmployeeData();
                            ed.EmployeeID = reader["employee_id"].ToString();
                            ed.Name = reader["full_name"].ToString();
                            ed.Position = reader["position"].ToString();
                            ed.Salary = (int)reader["salary"];

                            // Add employee to the list
                            listdata.Add(ed);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Display any errors that occur
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    // Always close the connection when done
                    connect.Close();
                }
            }
            return listdata;
        }
    }
}