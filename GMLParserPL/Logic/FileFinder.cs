using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace GMLParserPL.Logic
{
    internal class FileFinder
    {
        private static string fileNameFormatXML = @"*{0}.xml";
        private static string fileNameFormatRegexXML = @".*{0}\.xml";

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
                Console.WriteLine($"{ObjectTypeEnum.Error};{e}");
                return null;
            }
        }

        public string FindXMLFileInFolder(string folder, string file)
        {
            return FindFileInFolder(folder, file, fileNameFormatXML);
        }

        public string[] FindXMLFilesInFolder(string folder, string[] files)
        {
            var regexes = files.Select(f => new Regex(String.Format(fileNameFormatRegexXML, f)));
            try
            {
                return
                    Directory
                        .GetFiles(folder)
                        .Where(file => regexes.Any(r => r.IsMatch(file)))
                        .ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ObjectTypeEnum.Error};{e}");
                return null;
            }
        }
    }
}