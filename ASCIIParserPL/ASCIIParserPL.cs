using System;
using System.Globalization;
using System.Numerics;

namespace ASCIIParserPL
{
    // by jaggi
    /// <summary>
    ///     Proces odpowiedzialny z import danych NMT z plików ASCII
    ///     <para />
    ///     Process resposible for import DEM data in ASCII format
    /// </summary>
    class ASCIIParserPL
    {
        private static Vector2 _centerRealXY;
        private static int sideLength = 17296;


        internal static Vector2 CenterRealXY { get => _centerRealXY; private set => _centerRealXY = value; }
        internal static string PathDEM { get; private set; }

        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            if (args.Length != 3)
            {
                Console.WriteLine($"Error;Args length different from expected: 3");
                return;
            }
            CenterRealXY = new Vector2(float.Parse(args[0], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(args[1], CultureInfo.InvariantCulture.NumberFormat));
            PathDEM = args[2];

            if (string.IsNullOrEmpty(PathDEM))
            {
                Console.WriteLine($"Error;Path to file was null or empty");
                return;
            }

            try
            {
                var demData = ASCIITranslator.ASCIITranslate(PathDEM, CenterRealXY, sideLength);
                string stringDEM = Convert.ToBase64String(demData);
                string outDEMMessage = $"Terrain;{stringDEM}";
                Console.WriteLine(outDEMMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error;{e}");
            }
        }
    }
}
