namespace Core.User
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
