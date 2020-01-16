namespace SartreServer.Contracts
{
    /// <summary>
    /// A user login request.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// The user's unique login name.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        public string Password { get; set; }
    }
}
