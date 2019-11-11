using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace GeodataLoader.Source.Helpers
{
    public class FileFinder
    {
        //"*OIPR_P.xml"
        private static string fileNameFormat = @"*{0}.xml";
        private static string fileNameFormatRegex = @".*{0}\.xml";

        public static string FindFileInFolder(string folder, string file)
        {
            CommonHelpers.Log($"dir :{folder}");// + folder);
            CommonHelpers.Log($"file :{file}");// + file);
            CommonHelpers.Log($"Directory.Exists(folder) :{Directory.Exists(folder)}");// + Directory.Exists(folder).ToString());
            if (!Directory.Exists(folder))
                throw new DirectoryNotFoundException(folder);

            return
                Directory                   //"*OIPR_P.xml"
                    .GetFiles(folder, String.Format(fileNameFormat, file))
                    .SingleOrDefault();
        }

        public static string[] FindFilesInFolder(string folder, string[] files)
        {
            if (!Directory.Exists(folder))
                throw new DirectoryNotFoundException(folder);

            var regexes = files.Select(f => new Regex(String.Format(fileNameFormatRegex, f)));

            return
                Directory
                    .GetFiles(folder)
                    .Where(file => regexes.Any(r => r.IsMatch(file)))
                    .ToArray();
        }
    }
}