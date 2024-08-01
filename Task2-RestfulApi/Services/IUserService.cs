namespace task2_restfulapi.Services
{
    public interface IUserService
    {
        bool ValidateUser(string username, string password);
        string GetCurrentUser();
    }

}
