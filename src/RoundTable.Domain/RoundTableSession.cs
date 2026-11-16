namespace CorsairSolutions.RoundTable.Domain
{

    /// <summary>
    /// Represents a single multi-agent Round Table deliberation session for a user prompt.
    /// </summary>
    public sealed class RoundTableSession
    {

        #region Fields

        private readonly Dictionary<string, SeatOutcome> outcomesBySeatId = new(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unique identifier for this deliberation session.
        /// </summary>
        public Guid SessionId { get; }

        /// <summary>
        /// Gets the original user prompt being deliberated.
        /// </summary>
        public string Prompt { get; }

        /// <summary>
        /// Gets the UTC timestamp when this session was created.
        /// </summary>
        public DateTimeOffset CreatedAtUtc { get; }

        /// <summary>
        /// Gets the outcomes for all seats participating in this session.
        /// </summary>
        public IReadOnlyCollection<SeatOutcome> Outcomes => outcomesBySeatId.Values.ToList().AsReadOnly();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RoundTableSession" /> class.
        /// </summary>
        /// <param name="prompt">The user's original prompt or problem statement.</param>
        /// <param name="seats">The council seats participating in this session.</param>
        /// <param name="createdAtUtc">The UTC timestamp when the session started.</param>
        public RoundTableSession(string prompt, IEnumerable<CouncilSeat> seats, DateTimeOffset createdAtUtc)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(prompt);
            ArgumentNullException.ThrowIfNull(seats);

            var seatList = seats.ToList();

            if (seatList.Count == 0)
            {
                throw new ArgumentException("A session requires at least one council seat.", nameof(seats));
            }

            SessionId = Guid.NewGuid();
            Prompt = prompt;
            CreatedAtUtc = createdAtUtc;

            foreach (var seat in seatList)
            {
                outcomesBySeatId.Add(seat.SeatId, new SeatOutcome(seat));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the <see cref="SeatOutcome" /> for the specified seat identifier.
        /// </summary>
        /// <param name="seatId">The unique seat identifier.</param>
        /// <returns>The matching <see cref="SeatOutcome" />.</returns>
        public SeatOutcome GetOutcome(string seatId)
        {

            if (!outcomesBySeatId.TryGetValue(seatId, out var outcome))
            {
                throw new KeyNotFoundException($"Seat '{seatId}' is not registered in this session.");
            }

            return outcome;
        }

        #endregion
    }
}
