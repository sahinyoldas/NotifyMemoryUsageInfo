using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyMemoryUsageInfoService
{
    public static class ExtensionMethod
    {
        public static void WriteToFile(this ILogger logger, string Message)
        {
            string path = ConstantFields.SystemText.LogDirectoryPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = ConstantFields.SystemText.LogFilePath + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                writeToNewLogFile(filepath);
            }
            else
            {
                writeToExistsLogFile(filepath);
            }

            void writeToNewLogFile(string newFilePath)
            {
                using (StreamWriter sw = File.CreateText(newFilePath))
                {
                    sw.WriteLine(Message);
                }
            }

            void writeToExistsLogFile(string existingFilePath)
            {
                using (StreamWriter sw = File.AppendText(existingFilePath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        public static bool IsNotNullOrNotEmpty<T>(this ICollection<T> list)
        {
            if (list != null && list.Any())
                return true;

            else
                return false;
        }

    }
}
