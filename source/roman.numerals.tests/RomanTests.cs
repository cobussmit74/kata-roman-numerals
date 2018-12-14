using NUnit.Framework;
using System;

namespace roman.numerals.tests
{
    [TestFixture]
    public class RomanTests
    {
        [TestFixture]
        public class ToRomanNumerals
        {
            [Test]
            public void Given0_ReturnsEmptyString()
            {
                //arrange
                var roman = new Roman();
                //act
                var actual = roman.ToRomanNumerals(0);
                //assert
                Assert.That(actual, Is.Empty);
            }

            [Test]
            public void GivenNegative_ShouldThrow()
            {
                //arrange
                var roman = new Roman();
                //act
                //assert
                Assert.That(() => roman.ToRomanNumerals(-1), Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With.Property(nameof(ArgumentOutOfRangeException.ParamName))
                    .EqualTo("decimalValue"));
            }

            [TestFixture]
            public class SingleLetters
            {
                [TestCase(1, "I")]
                [TestCase(5, "V")]
                [TestCase(10, "X")]
                [TestCase(50, "L")]
                [TestCase(100, "C")]
                [TestCase(500, "D")]
                [TestCase(1000, "M")]
                public void GivenX_ReturnsY(int number, string expected)
                {
                    //arrange
                    var roman = new Roman();
                    //act
                    var actual = roman.ToRomanNumerals(number);
                    //assert
                    Assert.That(actual, Is.EqualTo(expected));
                }
            }

            [TestFixture]
            public class From1To5
            {
                [Test]
                public void Given2_ReturnsII()
                {
                    //arrange
                    var roman = new Roman();
                    //act
                    var actual = roman.ToRomanNumerals(2);
                    //assert
                    Assert.That(actual, Is.EqualTo("II"));
                }

                [Test]
                public void Given3_ReturnsIII()
                {
                    //arrange
                    var roman = new Roman();
                    //act
                    var actual = roman.ToRomanNumerals(3);
                    //assert
                    Assert.That(actual, Is.EqualTo("III"));
                }

                [Test]
                public void Given4_ReturnsIV()
                {
                    //arrange
                    var roman = new Roman();
                    //act
                    var actual = roman.ToRomanNumerals(4);
                    //assert
                    Assert.That(actual, Is.EqualTo("IV"));
                }
            }

            [TestFixture]
            public class CherryPickedValues
            {
                [TestCase(6, "VI")]
                [TestCase(7, "VII")]
                [TestCase(8, "VIII")]
                [TestCase(9, "IX")]

                [TestCase(18, "XVIII")]
                [TestCase(40, "XL")]
                [TestCase(41, "XLI")]
                [TestCase(44, "XLIV")]
                [TestCase(49, "XLIX")]

                [TestCase(99, "XCIX")]

                [TestCase(1994, "MCMXCIV")]

                [TestCase(3999, "MMMCMXCIX")]
                public void GivenX_ReturnsY(int number, string expected)
                {
                    //arrange
                    var roman = new Roman();
                    //act
                    var actual = roman.ToRomanNumerals(number);
                    //assert
                    Assert.That(actual, Is.EqualTo(expected));
                }
            }

            [Test]
            public void Given4000_ShouldThrow()
            {
                //arrange
                var roman = new Roman();
                //act
                //assert
                Assert.That(() => roman.ToRomanNumerals(4000), Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With.Property(nameof(ArgumentOutOfRangeException.ParamName))
                    .EqualTo("decimalValue"));
            }
        }

        [TestFixture]
        public class ToInteger
        {
            [Test]
            public void GivenEmptyString_Returns0()
            {
                //arrange
                var roman = new Roman();
                //act
                var actual = roman.ToInteger("");
                //assert
                Assert.That(actual, Is.EqualTo(0));
            }
            
            [Test]
            public void GivenInvalidString_Throws()
            {
                //arrange
                var roman = new Roman();
                //act
                //assert
                Assert.That(() => roman.ToInteger("FOOBAR"), Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With.Property(nameof(ArgumentOutOfRangeException.ParamName))
                    .EqualTo("romanNumerals"));
            }

            [Test]
            public void GivenInvalidCombination_Throws()
            {
                //arrange
                var roman = new Roman();
                //act
                //assert
                Assert.That(() => roman.ToInteger("IIII"), Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With.Property(nameof(ArgumentOutOfRangeException.ParamName))
                    .EqualTo("romanNumerals"));
            }

            [TestFixture]
            public class SingleLetters
            {
                [TestCase("I", 1)]
                [TestCase("V", 5)]
                [TestCase("X", 10)]
                [TestCase("L", 50)]
                [TestCase("C", 100)]
                [TestCase("D", 500)]
                [TestCase("M", 1000)]
                public void GivenX_ReturnsY(string numerals, int expected)
                {
                    //arrange
                    var roman = new Roman();
                    //act
                    var actual = roman.ToInteger(numerals);
                    //assert
                    Assert.That(actual, Is.EqualTo(expected));
                }
            }

            [TestFixture]
            public class From1To5
            {
                [Test]
                public void GivenII_Returns2()
                {
                    //arrange
                    var roman = new Roman();
                    //act
                    var actual = roman.ToInteger("II");
                    //assert
                    Assert.That(actual, Is.EqualTo(2));
                }

                [Test]
                public void GivenIII_Returns3()
                {
                    //arrange
                    var roman = new Roman();
                    //act
                    var actual = roman.ToInteger("III");
                    //assert
                    Assert.That(actual, Is.EqualTo(3));
                }

                [Test]
                public void GivenIV_Returns4()
                {
                    //arrange
                    var roman = new Roman();
                    //act
                    var actual = roman.ToInteger("IV");
                    //assert
                    Assert.That(actual, Is.EqualTo(4));
                }
            }

            [TestFixture]
            public class CherryPickedValues
            {
                [TestCase("VI", 6)]
                [TestCase("VII", 7)]
                [TestCase("VIII", 8)]
                [TestCase("IX", 9)]

                [TestCase("XVIII", 18)]
                [TestCase("XL", 40)]
                [TestCase("XLI", 41)]
                [TestCase("XLIV", 44)]
                [TestCase("XLIX", 49)]

                [TestCase("XCIX", 99)]

                [TestCase("MCMXCIV", 1994)]

                [TestCase("MMMCMXCIX", 3999)]
                public void GivenX_ReturnsY(string numerals, int expected)
                {
                    //arrange
                    var roman = new Roman();
                    //act
                    var actual = roman.ToInteger(numerals);
                    //assert
                    Assert.That(actual, Is.EqualTo(expected));
                }
            }
        }
    }
}