using FluentAssertions;
using FluentAssertions.Execution;

namespace CorsairSolutions.RoundTable.Domain.Tests
{
    public sealed class CouncilSeatTests
    {

        #region Public Methods

        [Fact]
        public void Constructor_WithValidArguments_SetsProperties()
        {

            var seat = new CouncilSeat("gemini", "Gemini", "Solution Architect", "Provide pragmatic architectural designs.");

            using (new AssertionScope())
            {
                seat.SeatId.Should().Be("gemini");
                seat.DisplayName.Should().Be("Gemini");
                seat.RoleTitle.Should().Be("Solution Architect"); 
                seat.SystemCharter.Should().Be("Provide pragmatic architectural designs.");
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithWhitespaceSeatId_ThrowsArgumentException(string invalidSeatId)
        {

            Action act = () => new CouncilSeat(invalidSeatId, "Gemini", "Architect", "Charter");
            act.Should().Throw<ArgumentException>().WithParameterName("seatId");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithWhitespaceDisplayName_ThrowsArgumentException(string invalidDisplayName)
        {

            Action act = () => new CouncilSeat("gemini", invalidDisplayName, "Architect", "Charter");
            act.Should().Throw<ArgumentException>().WithParameterName("displayName");
        }

        #endregion
    }
}
