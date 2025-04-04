# Std Dev & Sum
Code:
```cs
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
    ```

    see full code at 