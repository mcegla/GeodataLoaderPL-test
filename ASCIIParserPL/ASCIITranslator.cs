
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ASCIIParserPL
{
    // by jaggi
    public class ASCIITranslator
    {
        #region fields
        public static ASCIIParser[] Items;
        public static Dictionary<Vector2, ASCIIParser> Grid;
        public static float[] Image { get; private set; }
        public static int Rows;
        public static int Cols;
        public static int MinX;
        public static int MinY;
        public static int MaxY;
        #endregion


        #region methods
        public static void Init(string pathDEM)
        {
            FileFinder ff = new FileFinder();
            var files = ff.FindASCFilesInFolder(pathDEM);
            Items = files.Select(f => new ASCIIParser(f)).ToArray();
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
            Func<ASCIIParser, Vector2, bool> isInside = (x, m) =>
                    x.Header.xllcenter < m.X
                    && x.Header.xllcenter + x.Header.ncols > m.X
                    && x.Header.yllcenter < m.Y
                    && x.Header.yllcenter + x.Header.nrows > m.Y;

            var topRightCorner = middle + new Vector2(range, range);
            var bottomLeftCorner = middle - new Vector2(range, range);

            Items =
                Items.Where(item =>
                    !(item.Header.xllcenter > topRightCorner.X
                    || item.Header.xllcenter + item.Header.ncols < bottomLeftCorner.X
                    || item.Header.yllcenter < bottomLeftCorner.Y
                    || item.Header.yllcenter + item.Header.nrows > topRightCorner.Y))
                .ToArray();
        }

        public static byte[] ASCIITranslate(string pathDEM, Vector2 areaCenter, int sideLength)
        {
            Init(pathDEM);
            var DEMR = sideLength / 2;
            var DEMRpx = 1081;
            DropOutOfRange(areaCenter, DEMR);
            ASCIITranslator.ReadData();
            ASCIITranslator.InitImage(DEMR);
            ASCIITranslator.CreateImage(areaCenter, DEMR);
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
                var diff = middle.Y + range - GetMaxUnits(f, true);
                var yOffset = (int)diff * 2 * range;
                var xOffset = (int)(GetMinUnits(f) - (middle.X - range));
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

        private static double GetMinUnits(ASCIIParser grid, bool getY = false)
        {
            return
                getY
                    ? (grid.ReadHeader().yllcenter)
                    : (grid.ReadHeader().xllcenter);
        }
        private static double GetMaxUnits(ASCIIParser grid, bool getY = false)
        {
            return
                getY
                    ? (grid.ReadHeader().yllcenter + grid.Header.nrows)
                    : (grid.ReadHeader().xllcenter + grid.Header.ncols);
        }
        #endregion
    }
}
