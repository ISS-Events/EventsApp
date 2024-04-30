namespace EventsApp
{
    public class User(string username, string profilePicture)
    {
        public string Username { get; set; } = username;

        public string ProfilePicture { get; set; } = profilePicture;
    }
}