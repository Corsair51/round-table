namespace CorsairSolutions.RoundTable.Domain
{

    /// <summary>
    /// Defines the domain port implemented by a council participant capable of producing drafts, reviewing peer outputs, and revising its work during a Round Table deliberation.
    /// </summary>
    public interface IAgentParticipant
    {

        #region Properties

        /// <summary>
        /// Gets the council seat identity and charter assigned to this participant.
        /// </summary>
        CouncilSeat Seat { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates an independent initial draft response for the user's prompt (Stage 1: Fan-Out).
        /// </summary>
        /// <param name="prompt">The user's original problem statement or architectural prompt.</param>
        /// <param name="cancellationToken">A token used to enforce per-agent timeout or session cancellation.</param>
        /// <returns>The participant's initial <see cref="DraftArtifact"/>.</returns>
        Task<DraftArtifact> GenerateDraftAsync(string prompt, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs a structured adversarial review of another participant's draft (Stage 2: Cross-Critique).
        /// </summary>
        /// <param name="originalPrompt">The user's original problem statement for context.</param>
        /// <param name="targetDraft">The peer participant's draft artifact being evaluated.</param>
        /// <param name="cancellationToken">A token used to enforce per-agent timeout or session cancellation.</param>
        /// <returns>A structured <see cref="PeerCritique"/> detailing flaws, blind spots, and edge cases.</returns>
        Task<PeerCritique> CritiqueDraftAsync(string originalPrompt, DraftArtifact targetDraft, CancellationToken cancellationToken = default);

        /// <summary>
        /// Produces a refined final response incorporating valid peer feedback (Stage 3: Revision).
        /// </summary>
        /// <param name="originalPrompt">The user's original problem statement.</param>
        /// <param name="originalDraft">This participant's Stage 1 draft artifact.</param>
        /// <param name="critiques">The peer critiques received for this participant's draft.</param>
        /// <param name="cancellationToken">A token used to enforce per-agent timeout or session cancellation.</param>
        /// <returns>The revised final <see cref="DraftArtifact"/> including a summary of changes.</returns>
        Task<DraftArtifact> ReviseDraftAsync(string originalPrompt, DraftArtifact originalDraft, IReadOnlyList<PeerCritique> critiques, CancellationToken cancellationToken = default);

        #endregion
    }
}
