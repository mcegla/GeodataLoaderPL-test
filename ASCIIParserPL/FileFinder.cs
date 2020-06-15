using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ASCIIParserPL
{
    internal class FileFinder
    {
        #region fields
        private static string fileNameFormatASC = @"*{0}.asc";
        private static string fileNameFormatRegexASC = @".*{0}\.asc$";
        #endregion


        #region methods
        private string FindFileInFolder(string folder, string file, string fileNameFormat)
        {
            try
            {
                return
                    Directory
                        .GetFiles(folder, String.Format(fileNameFormat, file))
                        .SingleOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error;{e}");
                return null;
            }
        }

        public string FindASCFileInFolder(string folder)
        {
            return FindFileInFolder(folder, "", fileNameFormatASC);
        }

        public string[] FindASCFilesInFolder(string folder)
        {
            var regex = new Regex(String.Format(fileNameFormatRegexASC, ""));

            try
            {
                return
                    Directory
                        .GetFiles(folder)
                        .Where(file => regex.IsMatch(file))
                        .ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error;{e}");
                return null;
            }
        }
        #endregion
    }
}