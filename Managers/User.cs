namespace SnipIt.Managers
{

    public class User
    {
        // model for the user
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } // db email
        public DateTime CreatedDate { get; set; } // db createddate
    }
}