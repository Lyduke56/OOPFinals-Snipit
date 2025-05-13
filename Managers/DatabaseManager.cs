using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace SnipIt.Managers
{
    public static class DatabaseManager
    {
        // Get the path to the database in the "databases" folder under the application path
        public static string DbPath => Path.Combine(
            Application.StartupPath, "databases", "SnipIt.accdb");

        public static string ConnectionString =>
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DbPath};";

        public static OleDbConnection GetConnection()
        {
            return new OleDbConnection(ConnectionString);
        }

        public static int ExecuteNonQuery(string commandText, Dictionary<string, object> parameters = null)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand(commandText, connection))
                {
                    // Prepare parameters for OleDb
                    if (parameters != null)
                    {
                        var paramValues = new List<object>();
                        foreach (var param in parameters)
                        {
                            paramValues.Add(param.Value ?? DBNull.Value);
                        }

                        // Add parameters in order
                        for (int i = 0; i < paramValues.Count; i++)
                        {
                            command.Parameters.AddWithValue($"param{i}", paramValues[i]);
                        }
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteQuery(string commandText, Dictionary<string, object> parameters = null)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand(commandText, connection))
                {
                    // Prepare parameters for OleDb
                    if (parameters != null)
                    {
                        var paramValues = new List<object>();
                        foreach (var param in parameters)
                        {
                            paramValues.Add(param.Value ?? DBNull.Value);
                        }

                        // Add parameters in order
                        for (int i = 0; i < paramValues.Count; i++)
                        {
                            command.Parameters.AddWithValue($"param{i}", paramValues[i]);
                        }
                    }

                    using (var adapter = new OleDbDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
        private static void PrepareParameters(OleDbCommand command, string commandText, Dictionary<string, object> parameters)
        {
            command.Parameters.Clear();

            try
            {
                // Collect all parameter names from the command text
                List<string> paramNames = new List<string>();
                int index = 0;
                while ((index = commandText.IndexOf('@', index)) != -1)
                {
                    int endIndex = index + 1;
                    while (endIndex < commandText.Length &&
                           (char.IsLetterOrDigit(commandText[endIndex]) || commandText[endIndex] == '_'))
                    {
                        endIndex++;
                    }

                    string paramName = commandText.Substring(index, endIndex - index);
                    paramNames.Add(paramName);
                    index = endIndex;
                }

                // Add parameters in the order they appear
                foreach (string paramName in paramNames)
                {
                    string cleanParamName = paramName.TrimStart('@');
                    if (parameters.TryGetValue(cleanParamName, out object value))
                    {
                        // just for debugging and other stuff
                        Console.WriteLine($"Adding parameter: {cleanParamName} = {value ?? "NULL"}");
                        command.Parameters.AddWithValue(cleanParamName, value ?? DBNull.Value);
                    }
                    else
                    {
                        Console.WriteLine($"Parameter {cleanParamName} not found in dictionary");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the full details of the parameter preparation error
                MessageBox.Show($"Error preparing parameters: {ex.Message}\n{ex.StackTrace}",
                    "Parameter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        // test db connection
        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}