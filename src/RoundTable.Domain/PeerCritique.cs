namespace CorsairSolutions.RoundTable.Domain
{

    /// <summary>
    /// Represent's a structured adversial peer review of another council seat's draft.
    /// </summary>
    public sealed record PeerCritique
    {

        #region Properties

        /// <summary>
        /// Gets the seat that authored the critique.
        /// </summary>
        public CouncilSeat ReviewerSeat { get; }

        /// <summary>
        /// Gets the seat whose initial draft was critiqued.
        /// </summary>
        public CouncilSeat TargetSeat { get; }

        /// <summary>
        /// Gets the structured Markdown critique content.
        /// </summary>
        public string CritiqueContent { get;  }

        /// <summary>
        /// Gets the UTC timestamp when this critique was created.
        /// </summary>
        public DateTimeOffset CreatedAtUtc { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PeerCritique"/> record.
        /// </summary>
        /// <param name="reviewerSeat">The council seat performing the review.</param>
        /// <param name="targetSeat">The council seat whose draft was reviewed.</param>
        /// <param name="critiqueContent">The structured critique findings.</param>
        /// <param name="createdAtUtc">The UTC timestamp when the critique was produced.</param>
        public PeerCritique(CouncilSeat reviewerSeat, CouncilSeat targetSeat, string critiqueContent, DateTimeOffset createdAtUtc)
        {

            ArgumentNullException.ThrowIfNull(reviewerSeat);
            ArgumentNullException.ThrowIfNull(targetSeat);
            ArgumentException.ThrowIfNullOrWhiteSpace(critiqueContent);

            ReviewerSeat = reviewerSeat;
            TargetSeat = targetSeat;
            CritiqueContent = critiqueContent;
            CreatedAtUtc = createdAtUtc;
        }

        #endregion
    }
}
