namespace TournamentManager.Shared.Structs;

public struct UserInfo
{
    public string Username;
    public string Email;
    public string FirstName;
    public string LastName;
    public string? ProfilePictureUrl;
    
    public UserInfo(string username, string email, string firstName, string lastName, string? profilePictureUrl = null)
    {
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        ProfilePictureUrl = profilePictureUrl;
    }
}