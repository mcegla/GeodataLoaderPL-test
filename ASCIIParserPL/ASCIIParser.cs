using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace ASCIIParserPL
{
    // by jaggi
    public class ASCIIParser
    {
        private ASCIIHeader _header;
        private readonly string _fileName;
        private readonly byte[] _newLine =
            Encoding.UTF8.GetBytes(Environment.NewLine);

        public double[,] Data { get; private set; }
        public byte[] ConvertedData { get; private set; }
        public ushort[,] ConvertedDataRect { get; private set; }

        public double MinValue { get; protected set; } = Double.MaxValue;

        public ASCIIHeader Header
        {
            get
            {
                return this._header;
            }
        }

        const ushort _headerLinesCount = 6;

        public ASCIIParser(string fileName)
        {
            this._fileName = fileName;
            Name = new ASCIIName(fileName);
        }

        internal ASCIIName Name { get; private set; }

        private static readonly char[] _splitArray = new[] { ' ' };

        public ASCIIHeader ReadHeader()
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

                    for (int i = 0; i < tokens.Length; i++)
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
}
