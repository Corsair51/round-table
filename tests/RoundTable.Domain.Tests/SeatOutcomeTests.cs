using FluentAssertions;
using FluentAssertions.Execution;

namespace CorsairSolutions.RoundTable.Domain.Tests
{
    
    public sealed class SeatOutcomeTests
    {

        #region Public Methods

        [Fact]
        public void RecordStages_WhenCompletedNormally_CapturesAllArtifactsWithoutFault()
        {

            var geminiSeat = new CouncilSeat("gemini", "Gemini", "Architect", "Charter");
            var copilotSeat = new CouncilSeat("copilot", "Copilot", "Analyst", "Charter");
            var timestamp = new DateTimeOffset(2026, 11, 15, 20, 0, 0, TimeSpan.Zero);

            var outcome = new SeatOutcome(geminiSeat);
            var initialDraft = new DraftArtifact(geminiSeat, "Initial architeture proposal", false, timestamp.AddSeconds(5));
            var critique = new PeerCritique(copilotSeat, geminiSeat, "Missing timout handling", timestamp.AddSeconds(5));
            var finalRevision = new DraftArtifact(geminiSeat, "Revised architecture proposal", true, timestamp.AddSeconds(10));

            outcome.RecordInitialDraft(initialDraft);
            outcome.AddCritiqueReceived(critique);
            outcome.RecordFinalRevision(finalRevision);

            using (new AssertionScope())
            {
                outcome.Seat.Should().Be(geminiSeat);
                outcome.InitialDraft.Should().Be(initialDraft);
                outcome.CritiquesReceived.Should().ContainSingle().Which.Should().Be(critique);
                outcome.FinalRevision.Should().Be(finalRevision);
                outcome.IsFaulted.Should().BeFalse();
                outcome.ErrorMessage.Should().BeNull();
            }
        }

        [Fact]
        public void RecordFailure_AfterInitialDraftCaptured_PreservesInitialDraftAndSetsFaultState()
        {

            var copilotSeat = new CouncilSeat("copilot", "Copilot", "Analyst", "Charter");
            var timestamp = new DateTimeOffset(2026, 11, 15, 20, 0, 0, TimeSpan.Zero);

            var outcome = new SeatOutcome(copilotSeat);
            var initialDraft = new DraftArtifact(copilotSeat, "Initial deep-dive analysis", false, timestamp);

            outcome.RecordInitialDraft(initialDraft);
            outcome.RecordFailure("Copilot timed out during cross-critique.");

            using (new AssertionScope())
            {

                outcome.InitialDraft.Should().Be(initialDraft);
                outcome.FinalRevision.Should().BeNull();
                outcome.IsFaulted.Should().BeTrue();
                outcome.ErrorMessage.Should().Be("Copilot timed out during cross-critique.");
            }
        }

        #endregion
    }
}
