using GMLParserPL.Configuration;
using GMLParserPL.Logic;
using GMLParserPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GMLParserPL.Translators
{
    /// <summary>
    ///     Main translating class, contains selection of the translated BDOT10k classes and initialization of the translators
    /// </summary>
    internal class TranslatorInitiator
    {
        #region fields
        private readonly string folderPath;
        private readonly List<string> bdotClasses;
        private readonly HashSet<string> firstBdotClasses;
        private readonly Config config;
        #endregion


        internal static HashSet<Segment> RoadSegment { get; set; } = new HashSet<Segment> { };
        internal static HashSet<Segment> GridSegment { get; set; } = new HashSet<Segment> { };
        internal static int ToleranceDP { get; } = 3; // 3 looks good, but might cause troubles with intersections

        internal TranslatorInitiator(string folderPath, Config config)
        {
            this.folderPath = folderPath;
            this.config = config;
            bdotClasses = config.BdotClassesUsed;
            firstBdotClasses = new HashSet<string> { "SKJZ_L", "SULN_L" }; // main linear classes should be created before any other class
        }


        #region methods
        internal void FindTranslator()
        {
            Console.WriteLine($"Error;In Find Translator");
            FileFinder ff = new FileFinder();
            foreach (var bdotClass in bdotClasses.OrderBy(x => !firstBdotClasses.Contains(x)))
            {
                var filePath = ff.FindXMLFileInFolder(folderPath, bdotClass);
                if (!String.IsNullOrEmpty(filePath))
                    ChooseTranslator(bdotClass, filePath, config)
                        .ParseAndTranslate();
            }
            Console.WriteLine($"Error;Out Find Translator");
        }

        private Translator ChooseTranslator(string bdotClass, string filePath, Config config)
        {
            try
            {
                Type type = Assembly.GetExecutingAssembly().GetType($"GMLParserPL.Translators.BDOT.{bdotClass}");
                return (Translator)Activator.CreateInstance(type, bdotClass, filePath, config);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ObjectTypeEnum.Error};Could not find type {bdotClass} {e}");
                return null;
            }
        }
        #endregion
    }
}
