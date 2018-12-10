using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace roman.numerals
{
    public class Roman
    {
        private readonly Dictionary<int, string> _map = new Dictionary<int, string>
        {
            { 1, "I" },
            { 4, "IV" },
            { 5, "V" },
            { 9, "IX" },
            { 10, "X" },
            { 40, "XL" },
            { 50, "L" },
            { 90, "XC" },
            { 100, "C" },
            { 400, "CD" },
            { 500, "D" },
            { 900, "CM" },
            { 1000, "M" }
        };
        private readonly List<int> _checkpoints = null;

        public Roman()
        {
            _checkpoints = _map
                    .Keys
                    .OrderByDescending(x => x)
                    .ToList();
        }

        public string ToRomanNumerals(int decimalValue)
        {
            if ((decimalValue < 0) || (decimalValue >= 4000)) throw new ArgumentOutOfRangeException(nameof(decimalValue));
            if (decimalValue == 0) return string.Empty;

            var result = new StringBuilder();
            while (decimalValue > 0)
            {
                var checkpoint = _checkpoints
                    .Where(c => c <= decimalValue)
                    .FirstOrDefault();

                var romanValue = _map[checkpoint];

                decimalValue = decimalValue - checkpoint;

                result.Append(romanValue);

            }

            return result.ToString();
        }
    }
}
