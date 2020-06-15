using GMLParserPL.Configuration;
using GMLParserPL.Translators;
using System;
using System.Globalization;
using System.Numerics;

namespace GMLParserPL
{
    /// <summary>
    ///     Aplikacja odpowiedzialna za przeliczanie i tłumaczenie BDOT10k i NMT do obiektów gry
    ///     <para />
    ///     Application responsible for recalculating and translating BDOT10k and DEM to game objects
    /// </summary>
    class GMLParserPL
    {
        private static Vector2 _centerRealXY;

        internal static Vector2 CenterRealXY { get => _centerRealXY; private set => _centerRealXY = value; }
        internal static int SideLength { get; private set; }
        internal static string PathTBD { get; private set; }


        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine($"{ObjectTypeEnum.Error};Args length different from expected: 4");
                return;
            }

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            CenterRealXY = new Vector2(float.Parse(args[0], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(args[1], CultureInfo.InvariantCulture.NumberFormat));
            SideLength = int.Parse(args[2], CultureInfo.InvariantCulture.NumberFormat);
            PathTBD = args[3];

            if (string.IsNullOrEmpty(PathTBD))
            {
                Console.WriteLine($"Error;Path to file was null or empty");
                return;
            }

            try
            {
                var newConfig = JSONSerializer.LoadConfig();
                TranslatorInitiator translator = new TranslatorInitiator(GMLParserPL.PathTBD, newConfig);
                translator.FindTranslator();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ObjectTypeEnum.Error};{e}");
            }
        }
    }
}
