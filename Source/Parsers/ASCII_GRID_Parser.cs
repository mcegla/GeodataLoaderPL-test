using GeodataLoader.Source.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

//==================================================================================
//=== Ta część kodu jest odpowiedzialna za parsowanie danych o NMT z plików .asc ===
//----------------------------------------------------------------------------------
//==== This part of code is responsible for parsinging DEM data from .asc files ====
//==================================================================================

// wykonane przez: / made by:
// jaggi

namespace GeodataLoader.Source.Parsers
{
    public class ASCII_GRID_Container
    {
        public static ASCII_GRID_Parser[] Items;
        public static Dictionary<Vector2, ASCII_GRID_Parser> Grid;
        public static float[] Image { get; private set; }
        public static int Rows;
        public static int Cols;
        public static int MinX;
        public static int MinY;
        public static int MaxY;

        public static void Init(GeodataLoaderConfiguration config)
        {
            var files = FileFinderASC.FindFilesInFolder(config.DEM);
            Items = files.Select(f => new ASCII_GRID_Parser(f)).ToArray();
            var minX = Items.OrderBy(x => x.ReadHeader().xllcenter).First();
            var maxX = Items.OrderByDescending(x => x.ReadHeader().xllcenter).First();

            var maxY = Items.OrderByDescending(x => x.ReadHeader().yllcenter).First();            
            var minY = Items.OrderBy(x => x.ReadHeader().yllcenter).First();

            MinX = (int)GetMinUnits(minX);
            var maxXUnit = GetMaxUnits(maxX) - MinX;
            MinY = (int)GetMinUnits(minY, true);
            MaxY = (int)GetMaxUnits(maxY, true);
            var maxYUnit = MaxY - MinY;

            Rows = (int)(maxYUnit);
            Cols = (int)(maxXUnit);
        }

        public static void InitImage(int range)
        {
            Image = new float[2 * range * 2 * range];
            // min of all data from given sheets within range
            // NOT min of all data within range
            var min = (float)Items.Min(x => x.MinValue);
            for (int i = 0; i < Image.Length; i++)
            {
                Image[i] = min;
            }
        }

        public static void DropOutOfRange(Vector2 middle, int range)
        {
            Func<ASCII_GRID_Parser, Vector2, bool> isInside = (x, m) =>
                    x.Header.xllcenter < m.x
                    && x.Header.xllcenter + x.Header.ncols > m.x
                    && x.Header.yllcenter < m.y
                    && x.Header.yllcenter + x.Header.nrows > m.y;

            var topRightCorner = middle + new Vector2(range, range);
            var bottomLeftCorner = middle - new Vector2(range, range);

            Items = 
                Items.Where(item =>
                    !(item.Header.xllcenter > topRightCorner.x
                    || item.Header.xllcenter + item.Header.ncols < bottomLeftCorner.x
                    || item.Header.yllcenter < bottomLeftCorner.y
                    || item.Header.yllcenter + item.Header.nrows > topRightCorner.y))
                .ToArray();
        }

        public static byte[] Test(GeodataLoaderConfiguration config)
        {
            Init(config);
            var DEMR = GeodataLoaderConfiguration.DEMRange;
            var DEMRpx = GeodataLoaderConfiguration.DEMRangepx;
            DropOutOfRange(new Vector2(config.ParsedCenterX, config.ParsedCenterY), DEMR);
            ASCII_GRID_Container.ReadData();
            ASCII_GRID_Container.InitImage(DEMR);
            ASCII_GRID_Container.CreateImage(new Vector2(config.ParsedCenterX, config.ParsedCenterY), DEMR);
            Items = null;
            Image = Resize(Image, 2 * DEMR, 2 * DEMR, DEMRpx, DEMRpx);
            Image = Rotate180(Image, DEMRpx);
            var data = ConvertData(Image, DEMRpx, DEMRpx);
            return data;
        }

        public static float[] Rotate180(float[] sourceImage, int sourceHeight)
        {
            float[] rotatedImage = new float[sourceImage.Length];
            var sourceWidth = sourceImage.Length / sourceHeight;

            for (int i = 0; i < sourceHeight; i++)
            {
                var heighIndex = sourceHeight - 1 - i;
                heighIndex *= sourceWidth;

                for (int j = 0; j < sourceWidth; j++)
                {
                    rotatedImage[heighIndex + sourceWidth - 1 - j] = sourceImage[i * sourceWidth + j];
                }
            }
            return rotatedImage;
        }

        // przepróbkowanie metodą splotu sześciennego bazuje na: / bicubic resampling based on:
        // https://stackoverflow.com/questions/32089277/correctly-executing-bicubic-resampling

        private static double BiCubicKernel(double x)
        {
            if (x < 0)
            {
                x = -x;
            }

            double bicubicCoef = 0;

            if (x <= 1)
            {
                bicubicCoef = (1.5 * x - 2.5) * x * x + 1;
            }
            else if (x < 2)
            {
                bicubicCoef = ((-0.5 * x + 2.5) * x - 4) * x + 2;
            }

            return bicubicCoef;
        }

        public static float[] Resize(float[] source, int sourceWidth, int sourceHeight, int width, int height)
        {
            double heightFactor = sourceWidth / (double)width;
            double widthFactor = sourceHeight / (double)height;

            float[] res = new float[width * height];

            // Coordinates of source points
            double ox, oy, dx, dy, k1, k2;
            int ox1, oy1, ox2, oy2;

            // Width and height decreased by 1
            int maxHeight = sourceHeight - 1;
            int maxWidth = sourceWidth - 1;

            for (int y = 0; y < height; y++)
            {
                // Y coordinates
                oy = (y * widthFactor) - 0.5;

                oy1 = (int)oy;
                dy = oy - oy1;

                for (int x = 0; x < width; x++)
                {
                    // X coordinates
                    ox = (x * heightFactor) - 0.5f;
                    ox1 = (int)ox;
                    dx = ox - ox1;

                    // Destination color components
                    double newValue = 0.0;

                    for (int n = -1; n < 3; n++)
                    {
                        // Get Y cooefficient
                        k1 = BiCubicKernel(dy - n);

                        oy2 = oy1 + n;
                        if (oy2 < 0)
                        {
                            oy2 = 0;
                        }

                        if (oy2 > maxHeight)
                        {
                            oy2 = maxHeight;
                        }

                        for (int m = -1; m < 3; m++)
                        {
                            // Get X cooefficient
                            k2 = k1 * BiCubicKernel(m - dx);

                            ox2 = ox1 + m;
                            if (ox2 < 0)
                            {
                                ox2 = 0;
                            }

                            if (ox2 > maxWidth)
                            {
                                ox2 = maxWidth;
                            }

                            newValue += k2 * source[ox2 + oy2 * sourceWidth];
                        }
                    }

                    res[x + width * y] = (float)newValue;
                }
            }
            return res;
        }

        public static byte[] ConvertData(float[] source, int width, int height)
        {
            var res = new byte[2 * width * height];            

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    var index = j * width + i;
                    var val = source[index];
                    var converted = ConvertValue(val);

                    res[2 * index] = converted[0];
                    res[2 * index + 1] = converted[1];
                }
            }
            return res;
        }

        private static byte[] ConvertValue(float value)
        {
            if (value >= 1024)
                return new byte[] {
                    byte.MaxValue, byte.MaxValue };

            if (value <= 0)
                return new byte[] {
                    byte.MinValue, byte.MinValue };

            var lowResVal = (byte)(value / 4);
            var highResReminder = (byte)((value - lowResVal * 4) * 64);
            return new byte[] { highResReminder, lowResVal };
        }

        public static void CreateImage(Vector2 middle, int range)
        {
            foreach (var f in Items)
            {                
                var diff = middle.y + range - GetMaxUnits(f, true);
                var yOffset = (int) diff * 2 * range;
                var xOffset = (int) (GetMinUnits(f) - (middle.x - range));
                var zero = yOffset + xOffset;

                for (int i = 0; i < f.Header.nrows; i++)
                {
                    var z = zero + i * 2 * range;
                    for (int j = 0; j < f.Header.ncols; j++)
                    {
                        if (xOffset + j < 0)
                            continue;
                        if (xOffset + j > 2 * range)
                            break;
                        var val = f.Data[j, i];
                        var index = z + j;

                        if (!Double.IsNaN(val))
                        {
                            Image[index] = (float)(val);
                        }
                    }
                }
            }
        }

        public static void ReadHeaders()
        {
            foreach (var i in Items)
            {
                i.ReadHeader();
            }
        }
        public static void ReadData()
        {
            foreach (var i in Items)
            {
                i.ReadData();
            }
        }

        private static double GetMinUnits(ASCII_GRID_Parser grid, bool getY = false)
        {
            return
                getY
                    ? (grid.ReadHeader().yllcenter)
                    : (grid.ReadHeader().xllcenter);
        }
        private static double GetMaxUnits(ASCII_GRID_Parser grid, bool getY = false)
        {
            return
                getY
                    ? (grid.ReadHeader().yllcenter + grid.Header.nrows)
                    : (grid.ReadHeader().xllcenter + grid.Header.ncols);
        }
    }
    public class ASCII_GRID_Parser
    {
        private ASCII_GRID_HEADER _header;
        private readonly string _fileName;
        private readonly byte[] _newLine =
            Encoding.UTF8.GetBytes(Environment.NewLine);

        public double[,] Data { get; private set; }
        public byte[] ConvertedData { get; private set; }
        public ushort[,] ConvertedDataRect { get; private set; }

        public double MinValue { get; protected set; } = Double.MaxValue;

        public ASCII_GRID_HEADER Header
        {
            get
            {
                return this._header;
            }
        }

        const ushort _headerLinesCount = 6;

        public ASCII_GRID_Parser(string fileName)
        {
            this._fileName = fileName;
            Name = new ASCII_GRID_NAME(fileName);
        }

        public ASCII_GRID_NAME Name { get; private set; }

        private static readonly char[] _splitArray = new[] { ' ' };

        public ASCII_GRID_HEADER ReadHeader()
        {
            var headerLines = _headerLinesCount;
            using (var sr = new StreamReader(new FileStream(this._fileName, FileMode.Open)))
            {
                string line;
                while (headerLines-- > 0
                    && (line = sr.ReadLine()) != null)
                {
                    var tokens = line.Split(_splitArray, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length < 2)
                    {
                        throw new Exception("invalid header");
                    }

                    switch (tokens[0].ToLowerInvariant())
                    {
                        case "nrows":
                            this._header.nrows = Int32.Parse(tokens[1]);
                            break;
                        case "ncols":
                            this._header.ncols = Int32.Parse(tokens[1]);
                            break;
                        case "xllcenter":
                            this._header.xllcenter = Double.Parse(tokens[1], CultureInfo.InvariantCulture);
                            break;
                        case "yllcenter":
                            this._header.yllcenter = Double.Parse(tokens[1], CultureInfo.InvariantCulture);
                            break;
                        case "cellsize":
                            this._header.cellsize = Double.Parse(tokens[1], CultureInfo.InvariantCulture);
                            break;
                        case "nodata_value":
                            this._header.nodata_value = Double.Parse(tokens[1], CultureInfo.InvariantCulture);
                            break;
                    }
                }
            }

            return this._header;
        }

        public double[,] ReadData()
        {
            Data = new double[this._header.ncols, this._header.nrows];

            var linesToSkip = _headerLinesCount;
            using (var sr = new StreamReader(new FileStream(this._fileName, FileMode.Open)))
            {
                string line;
                // skip the header;
                while (linesToSkip-- > 0 && (line = sr.ReadLine()) != null)
                    continue;
                int row = 0;
                while ((line = sr.ReadLine()) != null
                    && row < this._header.nrows)
                {
                    var tokens = line.Split(_splitArray, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length < Header.ncols)
                        throw new Exception(
                            $"invalid data or header");
                                        
                    for(int i = 0; i < tokens.Length; i++)
                    {
                        var val = Double.Parse(tokens[i], CultureInfo.InvariantCulture);
                        if (val == Header.nodata_value)
                            Data[i, row] = Double.NaN;
                        else
                        {
                            Data[i, row] = val;

                            if (this.MinValue > val)
                                this.MinValue = val;
                        }
                    }
                    row++;
                }
            }
            return Data;
        }

        //private Dictionary<double, byte[]> _convertedValues;

        private byte[] ConvertValue(double value)
        {            
            if (value >= 1024)
                return new byte[] {
                    byte.MaxValue, byte.MaxValue };

            if (value == Double.NaN)
                return ConvertValue(MinValue);

            var lowResVal = (byte)(value / 4);
            var highResReminder = (byte)((value - lowResVal * 4) * 64);
            return new byte[] { highResReminder, lowResVal };
        }
    }

    public class ASCII_GRID_NAME
    {
        private static Vector2 GridMinPos = new Vector2(float.MaxValue, float.MaxValue);

        public readonly string Name;
        public readonly int[] ParsedName;
        public readonly int[] ParsedLevels;
        public readonly Vector2 GridPos;
        public Vector2 GridShiftedPos => GridPos - GridMinPos; // chcemy np. N-34-123-A-b-1-2        
        private static readonly int[] ZeroValues = new int[] { 'A', 1, 1, 'A', 'a', 1, 1 }; // najmniejsze możliwe
        private static readonly int[] CoeffX = new int[] { 0, 12*2*2*2*2, 2*2*2*2, 2*2*2, 2*2, 2, 1 }; // współczynnik grid x - literka nie wpływa na X, cyferki już tak 34, i jako że kolejne są w kwadratach też
        private static readonly int[] CoeffY = new int[] { 12*2*2*2*2, 0, 2*2*2*2, 2*2*2, 2*2, 2, 1 }; // współczynnik grid y - literka wpływa na Y, cyferki nie wpływają, ale kojene wartości już tak, bo poruszamy się po kwadratach
        private static readonly Regex Regex = new Regex(@"([A-Z])-(\d{1,2})-(\d{1,3})-([A-Z])-([a-z])-(\d)-(\d)");

        public ASCII_GRID_NAME(string name)
        {
            ParsedLevels = new int[7];
            ParsedName = new int[7];
            GridPos = new Vector2();
            Name = name;

            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Name.Trim()))
            {
                throw new Exception("No name to parse");
            }

            ParsedName = new int[7];
            var match = Regex.Match(Name);
            if(match.Groups.Count != 8)
            {
                throw new Exception("invalid name");
            }

            for(int i = 1; i < 8; i++)
            {
                if (i == 1 || i == 4 || i == 5)
                    ParsedName[i - 1] = match.Groups[i].Value[0];
                else
                    ParsedName[i - 1] = Int32.Parse(match.Groups[i].Value);
            }

            for (int i = 0; i < ParsedName.Length; i++)
                ParsedLevels[i] = ParsedName[i] - ZeroValues[i];

            for (int i = 0; i < 2; i++)
            {                
                GridPos.x += ParsedLevels[i] * CoeffX[i];
                GridPos.y += ParsedLevels[i] * CoeffY[i];
            }
            GridPos.x += (ParsedLevels[2] % 12) * CoeffX[2];
            GridPos.y += (ParsedLevels[2] / 12) * CoeffY[2];
            for (int i = 3; i < ParsedLevels.Length; i++)
            {
                GridPos.x += (ParsedLevels[i] % 2) * CoeffX[i];
                GridPos.y += ParsedLevels[i] > 1 ? CoeffY[i] : 0;
            }

            if (GridMinPos.x > GridPos.x)
                GridMinPos.x = GridPos.x;

            if (GridMinPos.y > GridPos.y)
                GridMinPos.y = GridPos.y;
        }
    }

    public struct ASCII_GRID_HEADER
    {
        public int nrows;
        public int ncols;
        public double xllcenter;
        public double yllcenter;
        public double cellsize;
        public double nodata_value;
    }
}
