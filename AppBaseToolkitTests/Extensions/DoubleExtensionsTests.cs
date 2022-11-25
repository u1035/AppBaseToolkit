using AppBaseToolkit.Extensions;
using FluentAssertions;

namespace AppBaseToolkitTests.Extensions
{
    public class DoubleExtensionsTests
    {
        [Fact]
        public void IsZeroTest_RealZero_ShouldBeTrue()
        {
            var someDouble = 0.0;
            someDouble.IsZero().Should().BeTrue();
        }

        [Fact]
        public void IsZeroTest_SmallValueBelowPrecision_ShouldBeTrue()
        {
            var someDouble = 0.000000001;
            someDouble.IsZero().Should().BeTrue();
        }

        [Fact]
        public void IsZeroTest_SmallValueAbovePrecision_ShouldBeFalse()
        {
            var someDouble = 0.01;
            someDouble.IsZero().Should().BeFalse();
        }

        [Fact]
        public void IsZeroTest_AnyValue_ShouldBeFalse()
        {
            var someDouble = 1.7;
            someDouble.IsZero().Should().BeFalse();
        }

        [Fact]
        public void IsEqualToTest_TwoEqualNumbers_ShouldBeTrue()
        {
            var someDouble1 = 0.12345;
            var someDouble2 = 0.12345;
            someDouble1.IsEqualTo(someDouble2).Should().BeTrue();
        }

        [Fact]
        public void IsEqualToTest_TwoDifferentNumbers_ShouldBeFalse()
        {
            var someDouble1 = 0.12344;
            var someDouble2 = 0.12345;
            someDouble1.IsEqualTo(someDouble2).Should().BeFalse();
        }

        [Fact]
        public void IsEqualToTest_TwoDifferentNumbers_RoughPrecision_ShouldBeTrue()
        {
            var someDouble1 = 0.12344;
            var someDouble2 = 0.12345;
            someDouble1.IsEqualTo(someDouble2, 0.1).Should().BeTrue();
        }

        [Fact]
        public void IsEqualToTest_CalculatedValueToPredefined_ShouldBeTrue()
        {
            //better use debugger to see what's inside values :-)
            double someDouble1 = 1;
            var someDouble2 = 1522D * 11 * 0.2558411 / (1522D * 11 * 0.2558411);
            someDouble1.IsEqualTo(someDouble2).Should().BeTrue();
        }
    }
}