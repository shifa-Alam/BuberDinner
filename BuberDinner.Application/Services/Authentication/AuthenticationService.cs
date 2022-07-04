using BuberDinner.Application.Common.Interfaces.Authentication;

namespace BuberDinner.Application.Services.Authentication;
public class AuthenticationService : IAuthenticationService
{
  private readonly IJwtTokenGenerator  _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

 
    public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
    {
      // Check if user already exists
        // Create user (generate unique ID)
        // Create JWT token
        var UserId= Guid.NewGuid();
        var Token =_jwtTokenGenerator.GenerateToken(UserId,FirstName,LastName);

        return new AuthenticationResult(Guid.NewGuid(), FirstName, LastName, Email, Token);
    }
       public AuthenticationResult Login(string Email, string Password)
    {
        
        return new AuthenticationResult(Guid.NewGuid(), "FirstName", "LastName", Email, "Token");
    }

}