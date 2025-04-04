// https://www.khanacademy.org/math/statistics-probability/summarizing-quantitative-data/variance-standard-deviation-sample/a/population-and-sample-standard-deviation-review

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Model class representing a row in the numbers_data table
    /// </summary>
    class NumberData
    {
        public int ID { get; set; }      // Primary key from database
        public double Value { get; set; } // Numeric value to analyze
    }
    
    /// <summary>
    /// Handles data retrieval and statistical analysis operations
    /// </summary>
    class DataAnalysis
    {
        // Database connection string - update server name as needed
        SqlConnection connect = 
            new SqlConnection(@"Data Source=LAPTOP-KWOKIAN-;Integrated Security=True;Encrypt=False");
        
        /// <summary>
        /// Retrieves all number records from the database
        /// </summary>
        /// <returns>List of NumberData objects containing values for analysis</returns>
        public List<NumberData> GetNumbersData()
        {
            List<NumberData> dataList = new List<NumberData>();
            
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    // Open connection to database
                    connect.Open();
                    
                    // SQL query to fetch all records
                    string selectData = "SELECT * FROM numbers_data";
                    
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        // Execute query and process results
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            // Map each database row to NumberData object
                            NumberData nd = new NumberData();
                            nd.ID = (int)reader["id"];
                            nd.Value = Convert.ToDouble(reader["value"]); // Handles conversion from various numeric types
                            dataList.Add(nd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any database errors
                    Console.WriteLine("Database Error: " + ex);
                }
                finally
                {
                    // Ensure connection is closed even if exception occurs
                    connect.Close();
                }
            }
            
            return dataList;
        }
        
        /// <summary>
        /// Calculates the sum of all numeric values in the dataset
        /// </summary>
        /// <param name="data">List of NumberData objects to sum</param>
        /// <returns>The total sum of all Value properties</returns>
        public double CalculateSum(List<NumberData> data)
        {
            // Uses LINQ for efficient summation
            return data.Sum(item => item.Value);
        }
        
        /// <summary>
        /// Calculates the standard deviation of the dataset
        /// Uses the formula: σ = sqrt(Σ(x - μ)² / (N-1))
        /// </summary>
        /// <param name="data">List of NumberData objects</param>
        /// <returns>Sample standard deviation (using N-1 denominator)</returns>
        public double CalculateStandardDeviation(List<NumberData> data)
        {
            // Handle edge cases - need at least 2 values for meaningful std dev
            if (data.Count <= 1)
            {
                return 0;
            }
            
            // Step 1: Calculate the mean (average)
            double mean = data.Average(item => item.Value);
            
            // Step 2: Calculate sum of squared differences from mean
            double sumOfSquaredDifferences = data.Sum(item => 
                Math.Pow(item.Value - mean, 2));
            
            // Step 3: Calculate variance (using N-1 for sample standard deviation)
            double variance = sumOfSquaredDifferences / (data.Count - 1);
            
            // Step 4: Take square root to get standard deviation
            return Math.Sqrt(variance);
        }
        
        /// <summary>
        /// Main analysis method that retrieves data and performs all calculations
        /// </summary>
        public void AnalyzeData()
        {
            // Get the data from database
            var numbersData = GetNumbersData();
            
            if (numbersData.Count > 0)
            {
                // Perform statistical calculations
                double sum = CalculateSum(numbersData);
                double stdDev = CalculateStandardDeviation(numbersData);
                
                // Output results
                Console.WriteLine($"Total number of records: {numbersData.Count}");
                Console.WriteLine($"Sum of all values: {sum}");
                Console.WriteLine($"Standard Deviation: {stdDev}");
            }
            else
            {
                Console.WriteLine("No data found in the numbers_data table.");
            }
        }
    }
    
    /// <summary>
    /// Entry point for the application
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Create analysis object and run calculations
            DataAnalysis analysis = new DataAnalysis();
            analysis.AnalyzeData();
            
            // Keep console window open until user presses a key
            Console.ReadLine();
        }
    }
}