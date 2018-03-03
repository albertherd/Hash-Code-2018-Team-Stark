using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Reader
{
    public static class FileReader
    {
        public static string ReadFile(string path)
        {
            string createText = String.Empty;

            if (File.Exists(path))
            {               
                createText = File.ReadAllText(path);
            }

            return createText;

        }

        public static string GetFirstLine(string fileString)
        {
            return fileString.Split(new[] { '\r', '\n' }).FirstOrDefault();
        }

        public static List<string> GetOtherLines(string fileString)
        {
            fileString= fileString.TrimEnd('\n');

            List<string> otherLines = fileString.Split(new[] { '\n' }).ToList();
            otherLines.RemoveAt(0);

            return otherLines;
        }
    }
}
