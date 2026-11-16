namespace CorsairSolutions.RoundTable.Domain
{
    
    /// <summary>
    /// Reprsents a response artifact produced by a council seat during drafting or final review.
    /// </summary>
    public sealed record DraftArtifact
    {

        #region Properties

        /// <summary>
        /// Gets the council seat that produced this draft.
        /// </summary>
        public CouncilSeat Seat { get; }

        /// <summary>
        /// Gest the Markdown text of the response.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets a value indicating whether this artifact is a Stage 3 final revision.
        /// </summary>
        public bool IsRevision { get; }

        /// <summary>
        /// Gets the UTC timestamp when this artifact was created.
        /// </summary>
        public DateTimeOffset CreatedAtUtc { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DraftArtifact"/> record.
        /// </summary>
        /// <param name="seat">The council seat that authored the artifact.</param>
        /// <param name="content">The Markdown response content.</param>
        /// <param name="isRevision">Indicates whether this artifact is a post-critique final revision.</param>
        /// <param name="createdAtUtc">The UTC timestamp when the artifact was generated.</param>
        public DraftArtifact(CouncilSeat seat, string content, bool isRevision, DateTimeOffset createdAtUtc)
        {

            ArgumentNullException.ThrowIfNull(seat);
            ArgumentException.ThrowIfNullOrWhiteSpace(content);

            Seat = seat;
            Content = content;
            IsRevision = isRevision;
            CreatedAtUtc = createdAtUtc;
        }

        #endregion

    }
}
