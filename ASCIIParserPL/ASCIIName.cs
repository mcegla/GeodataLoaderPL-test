using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ASCIIParserPL
{
    // by jaggi
    /// <summary>
    ///     DEM name parsing
    /// </summary>
    internal class ASCIIName
    {
        private static Vector2 GridMinPos = new Vector2(float.MaxValue, float.MaxValue);

        public readonly string Name;
        public readonly int[] ParsedName;
        public readonly int[] ParsedLevels;
        public readonly Vector2 GridPos;
        public Vector2 GridShiftedPos => GridPos - GridMinPos; // e.g. N-34-123-A-b-1-2        
        private static readonly int[] ZeroValues = new int[] { 'A', 1, 1, 'A', 'a', 1, 1 }; // lowest possible
        private static readonly int[] CoeffX = new int[] { 0, 12 * 2 * 2 * 2 * 2, 2 * 2 * 2 * 2, 2 * 2 * 2, 2 * 2, 2, 1 };
        private static readonly int[] CoeffY = new int[] { 12 * 2 * 2 * 2 * 2, 0, 2 * 2 * 2 * 2, 2 * 2 * 2, 2 * 2, 2, 1 };
        private static readonly Regex Regex = new Regex(@"([A-Z])-(\d{1,2})-(\d{1,3})-([A-Z])-([a-z])-(\d)-(\d)");

        public ASCIIName(string name)
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
            if (match.Groups.Count != 8)
            {
                throw new Exception("invalid name");
            }

            for (int i = 1; i < 8; i++)
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
                GridPos.X += ParsedLevels[i] * CoeffX[i];
                GridPos.Y += ParsedLevels[i] * CoeffY[i];
            }
            GridPos.X += (ParsedLevels[2] % 12) * CoeffX[2];
            GridPos.Y += (ParsedLevels[2] / 12) * CoeffY[2];
            for (int i = 3; i < ParsedLevels.Length; i++)
            {
                GridPos.X += (ParsedLevels[i] % 2) * CoeffX[i];
                GridPos.Y += ParsedLevels[i] > 1 ? CoeffY[i] : 0;
            }

            if (GridMinPos.X > GridPos.X)
                GridMinPos.X = GridPos.X;

            if (GridMinPos.Y > GridPos.Y)
                GridMinPos.Y = GridPos.Y;
        }
    }
}
