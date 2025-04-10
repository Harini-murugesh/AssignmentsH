namespace CarRentalSystem.util
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    public class PropertyUtil
    {
        public static string GetConnectionString(string filePath)
        {
            var properties = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(line) && !line.Trim().StartsWith("#"))
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                        properties[parts[0].Trim()] = parts[1].Trim();
                }
            }

            string dbname = properties["CarRentalSystem"];

            return $"Data Source=(local)\\SQLEXPRESS;Initial Catalog=CarRentalSystem;Integrated Security=True;";
        }
    }
}
