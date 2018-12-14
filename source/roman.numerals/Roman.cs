using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace roman.numerals
{
    public class Roman
    {
        private readonly Dictionary<int, string> _mapDecimalToRoman = new Dictionary<int, string>
        {
            { 1, "I" },
            { 5, "V" },
            { 10, "X" },
            { 50, "L" },
            { 100, "C" },
            { 500, "D" },
            { 1000, "M" }
        };
        private Dictionary<string, int> _mapRomanToDecimal = null;
        private List<int> _decimalCheckpoints = null;

        public Roman()
        {
            CreateDoubleRomanNumeralMaps();
            CreateRomanToDecimalMaps();
            CreateListOfDecimalCheckpoints();
        }

        private void CreateDoubleRomanNumeralMaps()
        {
            var newMaps = new List<KeyValuePair<int, string>>();
            foreach (var map in _mapDecimalToRoman)
            {
                var lowerThanMe = _mapDecimalToRoman
                    .Where(m => m.Key < (map.Key / 2))
                    .OrderByDescending(m => m.Key)
                    .FirstOrDefault();

                if (lowerThanMe.Key == 0) continue;

                newMaps.Add(new KeyValuePair<int, string>(map.Key - lowerThanMe.Key, $"{lowerThanMe.Value}{map.Value}"));
            }

            foreach (var newMap in newMaps)
            {
                _mapDecimalToRoman.Add(newMap.Key, newMap.Value);
            }
        }

        private void CreateRomanToDecimalMaps()
        {
            _mapRomanToDecimal = _mapDecimalToRoman
                .OrderByDescending(m => m.Value.Length)
                .ThenBy(m => m.Key)
                .ToDictionary(m => m.Value, m => m.Key);
        }

        private void CreateListOfDecimalCheckpoints()
        {
            _decimalCheckpoints = _mapDecimalToRoman
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
                var checkpoint = _decimalCheckpoints
                    .Where(c => c <= decimalValue)
                    .FirstOrDefault();

                var romanValue = _mapDecimalToRoman[checkpoint];

                decimalValue = decimalValue - checkpoint;

                result.Append(romanValue);
            }

            return result.ToString();
        }

        public int ToInteger(string romanNumerals)
        {
            var charSummary = CountConsecutiveCharacters(romanNumerals);
            if (charSummary.Any(c => c.Count > 3))
            {
                throw new ArgumentOutOfRangeException(nameof(romanNumerals), $"Ivalid numeral combination");
            }

            var totalValue = 0;
            var index = 0;
            while (index < romanNumerals.Length)
            {
                var singleNumeral = romanNumerals.Substring(index, 1);
                var doubleNumeral = (index + 1 < romanNumerals.Length)
                    ? romanNumerals.Substring(index, 2)
                    : "";

                if (_mapRomanToDecimal.TryGetValue(doubleNumeral, out var doubleValue))
                {
                    totalValue += doubleValue;
                    index += 2;
                }
                else if (_mapRomanToDecimal.TryGetValue(singleNumeral, out var singleValue))
                {
                    totalValue += singleValue;
                    index += 1;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(romanNumerals), $"{singleNumeral} is not a valid numeral");
                }
            }

            return totalValue;
        }

        private List<(char Character, int Count)> CountConsecutiveCharacters(string romanNumerals)
        {
            var results = new List<(char, int)>();
            char currentCharacter = '\0';
            int count = 0;
            foreach (var numeral in romanNumerals)
            {
                if (currentCharacter == numeral)
                {
                    count++;
                }
                else
                {
                    if (currentCharacter != '\0')
                    {
                        results.Add((currentCharacter, count));
                    }

                    currentCharacter = numeral;
                    count = 1;
                }
            }

            if (currentCharacter != '\0')
            {
                results.Add((currentCharacter, count));
            }

            return results;
        }
    }
}
