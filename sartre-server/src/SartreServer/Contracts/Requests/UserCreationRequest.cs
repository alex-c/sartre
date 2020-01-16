namespace SartreServer.Contracts.Requests
{
    /// <summary>
    /// A request to create a new user.
    /// </summary>
    public class UserCreationRequest
    {
        /// <summary>
        /// A unique login name.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The user name as it will be displayed in the UI. Can be changed later.
        /// </summary>
        public string Name { get; set; }

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
