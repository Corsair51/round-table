namespace CorsairSolutions.RoundTable.Domain
{

    /// <summary>
    /// Caputres all artifactsand fault-isolation state for a single council seat within a session.
    /// </summary>
    public sealed class SeatOutcome
    {

        #region Fields

        private readonly List<PeerCritique> critiquesReceived = new List<PeerCritique>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the council seat associated with this outcome.
        /// </summary>
        public CouncilSeat Seat { get; }

        /// <summary>
        /// Gets the Stage 1 initial draft produced by this seat, if completed.
        /// </summary>
        public DraftArtifact? InitialDraft { get; private set; }

        /// <summary>
        /// Gets the Stage 2 peer critiques targeting this seat's initial draft.
        /// </summary>
        public IReadOnlyList<PeerCritique> CritiquesReceived => critiquesReceived.AsReadOnly();

        /// <summary>
        /// Gets the Stage 3 final revised artifact produced by this seat, if completed.
        /// </summary>
        public DraftArtifact? FinalRevision { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this seat encountered a timeout or failure during execution.
        /// </summary>
        public bool IsFaulted => !string.IsNullOrWhiteSpace(ErrorMessage);
        
        /// <summary>
        /// Gets the failure reaons if this seat time out or threw an error.
        /// </summary>
        public string? ErrorMessage { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SeatOutcome" /> class for the specified seat.
        /// </summary>
        /// <param name="seat">The council seat tracked by this outcome.</param>
        public SeatOutcome(CouncilSeat seat)
        {

            ArgumentNullException.ThrowIfNull(seat);

            Seat = seat;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Records the Stage 1 initial draft for this seat.
        /// </summary>
        /// <param name="draft"></param>
        public void RecordInitialDraft(DraftArtifact draft)
        {

            ArgumentNullException.ThrowIfNull(draft);

            InitialDraft = draft;
        }

        /// <summary>
        /// Adds a Stage 2 peer critique targeting this seat's draft.
        /// </summary>
        /// <param name="critique">The peer critique to attach.</param>
        public void AddCritiqueReceived(PeerCritique critique)
        {

            ArgumentNullException.ThrowIfNull(critique);

            critiquesReceived.Add(critique);
        }

        /// <summary>
        /// Records the Stage 3 final revised artifact for this seat.
        /// </summary>
        /// <param name="revision"></param>
        public void RecordFinalRevision(DraftArtifact revision)
        {

            ArgumentNullException.ThrowIfNull(revision);

            FinalRevision = revision;
        }

        /// <summary>
        /// Marks this seat as faulted while preserving any earlier stage artifacts already captured.
        /// </summary>
        /// <param name="errorMessage">A descriptive message explaining the timout or failure.</param>
        public void RecordFailure(string errorMessage)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(errorMessage);

            ErrorMessage = errorMessage;
        }
        #endregion
    }
}
