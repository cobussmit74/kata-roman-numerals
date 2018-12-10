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
            { 5, "V" },
            { 10, "X" },
            { 50, "L" },
            { 100, "C" },
            { 500, "D" },
            { 1000, "M" }
        };
        private readonly List<int> _checkpoints = null;

        public Roman()
        {
            CreateLowerboundCheckpoints();

            _checkpoints = _map
                    .Keys
                    .OrderByDescending(x => x)
                    .ToList();
        }

        private void CreateLowerboundCheckpoints()
        {
            var newMaps = new List<KeyValuePair<int, string>>();
            foreach (var map in _map)
            {
                var lowerThanMe = _map
                    .Where(m => m.Key < (map.Key / 2))
                    .OrderByDescending(m => m.Key)
                    .FirstOrDefault();

                if (lowerThanMe.Key == 0) continue;
                
                newMaps.Add(new KeyValuePair<int, string>(map.Key - lowerThanMe.Key, $"{lowerThanMe.Value}{map.Value}"));
            }

            foreach (var newMap in newMaps)
            {
                _map.Add(newMap.Key, newMap.Value);
            }
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
