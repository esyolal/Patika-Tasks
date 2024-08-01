namespace task2_restfulapi.Services;
public class UserService : IUserService
{
    private readonly Dictionary<string, string> _users;
    private string _currentUser;

    public UserService()
    {
        _users = new Dictionary<string, string>
            {
                { "admin", "12345" },
                { "esyolal", "12345" }
            };
    }

    public bool ValidateUser(string username, string password)
    {
        if (_users.Any(u => u.Key == username && u.Value == password))
        {
            _currentUser = username;
            return true;
        }
        return false;
    }

    public string GetCurrentUser()
    {
        return _currentUser;
    }
}