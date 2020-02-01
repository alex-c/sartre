namespace SartreServer.Contracts.Requests
{
    /// <summary>
    /// A request to change a user's password.
    /// </summary>
    public class PasswordChangeRequest
    {
        /// <summary>
        /// The user's previous password.
        /// </summary>
        public string PreviousPassword { get; set; }

        /// <summary>
        /// A new user password.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// A repetion of the user password, for safety.
        /// </summary>
        public string NewPasswordRepetition { get; set; }
    }
}
