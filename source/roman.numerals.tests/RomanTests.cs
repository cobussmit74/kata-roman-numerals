using NUnit.Framework;
using roman.numerals;
using System;

namespace Tests
{
    public class RomanTests
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
            Assert.That(()=> roman.ToRomanNumerals(-1), Throws.TypeOf<ArgumentOutOfRangeException>()
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
}