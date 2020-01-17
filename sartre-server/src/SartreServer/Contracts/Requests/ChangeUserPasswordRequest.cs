namespace SartreServer.Contracts.Requests
{
    /// <summary>
    /// A request to change a user's password.
    /// </summary>
    public class ChangeUserPasswordRequest
    {
        /// <summary>
        /// The user's unique login name.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// A user password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// A repetion of the user password, for safety.
        /// </summary>
        public string PasswordRepetition { get; set; }
    }
}
