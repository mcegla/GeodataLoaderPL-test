namespace ASCIIParserPL
{
    /// <summary>
    ///     standard ARC/INFO ASCII GRID header model
    /// </summary>
    public struct ASCIIHeader
    {
        public int nrows;
        public int ncols;
        public double xllcenter;
        public double yllcenter;
        public double cellsize;
        public double nodata_value;
    }
}
