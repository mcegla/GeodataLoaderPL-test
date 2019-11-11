using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace GeodataLoader.Source.Helpers
{
    //========================================================
    //=== Klasa odpowiedzialna za wyszukiwanie plików .xml ===
    //--------------------------------------------------------
    //======= Class responsible for finding .xml files =======
    //========================================================

    // wykonane przez: / made by:
    // jaggi
    public class FileFinder
    {
        //"*OIPR_P.xml"
        private static string fileNameFormat = @"*{0}.xml";
        private static string fileNameFormatRegex = @".*{0}\.xml";

        public static string FindFileInFolder(string folder, string file)
        {
            return
                Directory                   //"*OIPR_P.xml"
                    .GetFiles(folder, String.Format(fileNameFormat, file))
                    .SingleOrDefault();
        }

        public static string[] FindFilesInFolder(string folder, string[] files)
        {
            var regexes = files.Select(f => new Regex(String.Format(fileNameFormatRegex, f)));

            return
                Directory
                    .GetFiles(folder)
                    .Where(file => regexes.Any(r => r.IsMatch(file)))
                    .ToArray();
        }
    }

    //========================================================
    //=== Klasa odpowiedzialna za wyszukiwanie plików .asc ===
    //--------------------------------------------------------
    //======= Class responsible for finding .asc files =======
    //========================================================

    // zmiany pierwszej: / first one edits:
    // mcegla
    public class FileFinderASC //for ASCII files still with type
    {
        //"*OIPR_P.xml"
        private static string fileNameFormat = @"*{0}.asc";
        private static string fileNameFormatRegex = @".*{0}\.asc$";

        public static string FindFileInFolder(string folder)
        {
            return
                Directory 
                    .GetFiles(folder, String.Format(fileNameFormat, ""))
                    .SingleOrDefault();
        }

        public static string[] FindFilesInFolder(string folder)
        {
            var regex =  new Regex(String.Format(fileNameFormatRegex, ""));

            return
                Directory
                    .GetFiles(folder)
                    .Where(file => regex.IsMatch(file))
                    .ToArray();
        }
    }
}