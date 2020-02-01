namespace SartreServer.Contracts.Requests
{
    /// <summary>
    /// A request to update a user's information.
    /// </summary>
    public class UserUpdateRequest
    {
        /// <summary>
        /// The user name as it will be displayed in the UI.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short user biography.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        /// An URL to the user's website or an online profile.
        /// </summary>
        public string Website { get; set; }
    }
}
