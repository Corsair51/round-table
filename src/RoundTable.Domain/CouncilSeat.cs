namespace CorsairSolutions.RoundTable.Domain
{

    /// <summary>
    /// Represents the identity and reasoning charter of a seat at the Round Table.
    /// </summary>
    public sealed record CouncilSeat
    {

        #region Properties

        /// <summary>
        /// Gets the unqiue identifier for this council seat.
        /// </summary>
        public string SeatId { get; }

        /// <summary>
        /// Gets the human-readable name for this seat.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Gets the analytical role title assigned to this seat.
        /// </summary>
        public string RoleTitle { get; }

        /// <summary>
        /// Gets the syste prompt instructions defining this seat's perspective.
        /// </summary>
        public string SystemCharter { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CouncilSeat" /> record.
        /// </summary>
        /// <param name="seatId">The unique identifier for the seat (e.g., "gemini", "copilot").</param>
        /// <param name="displayName">The human-readable display name shown in the UI.</param>
        /// <param name="roleTitle">The analytical persona or role title assigned to this seat.</param>
        /// <param name="systemCharter">The system prompt charger guiding this seat's reasoning style.</param>
        public CouncilSeat(string seatId, string displayName, string roleTitle, string systemCharter)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(seatId);
            ArgumentException.ThrowIfNullOrWhiteSpace(displayName);

            SeatId = seatId;
            DisplayName = displayName;
            RoleTitle = roleTitle ?? string.Empty;
            SystemCharter = systemCharter ?? string.Empty;
        }

        #endregion
    }
}
