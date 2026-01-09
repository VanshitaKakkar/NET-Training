using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2
{
    public class GenericFileHandler<T>
    {
        public List<T> Records { get; set; } = new List<T>();

        public void LoadFromFile(string filePath)
        {
            try
            {
                Records.Clear();

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                using (StreamReader reader = new StreamReader(fs))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Records.Add((T)(object)line);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        public void WriteToFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    foreach (var record in Records)
                    {
                        writer.WriteLine(record.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void LogError(Exception ex)
        {
            File.AppendAllText("errorlog.txt",
                $"[{DateTime.Now}] {ex.Message}{Environment.NewLine}");
        }
    }

}
