using FluentAssertions;
using FluentAssertions.Execution;

namespace CorsairSolutions.RoundTable.Domain.Tests
{
    
    public sealed class RoundTableSessionTests
    {

        #region Public Methods

        [Fact]
        public void Constructor_WithValidPromptAndSeats_InitializesSessionAndSeatOutcomes()
        {

            var seats = new[]
            {
                new CouncilSeat("gemini", "Gemini", "Architect", "Charter"),
                new CouncilSeat("copilot", "Copilot", "Analyst", "Charter")
            };

            var createdAt = new DateTimeOffset(2026, 11, 15, 20, 0, 0, TimeSpan.Zero);
            var session = new RoundTableSession("Design a bounded 3-stage pipeline.", seats, createdAt);

            using (new AssertionScope())
            {
                session.SessionId.Should().NotBeEmpty();
                session.Prompt.Should().Be("Design a bounded 3-stage pipeline.");
                session.CreatedAtUtc.Should().Be(createdAt);
                session.Outcomes.Should().HaveCount(2);
                session.GetOutcome("GEMINI").Seat.SeatId.Should().Be("gemini");
            }
        }

        [Fact]
        public void GetOutcome_WithUnregisteredSeatId_ThrowsKeyNotFoundException()
        {

            var seats = new[] { new CouncilSeat("gemini,", "Gemini", "Architect", "Charter") };
            var session = new RoundTableSession("Test prompt", seats, DateTimeOffset.UtcNow);

            Action act = () => session.GetOutcome("claude");

            act.Should().Throw<KeyNotFoundException>();
        }

        #endregion
    }
}
