using BiteRight.Application.Dtos.Users;

namespace BiteRight.Application.Queries.Users.Me;

public class MeResponse
{
    public UserDto User { get; }
    
    public MeResponse(UserDto user)
    {
        User = user;
    }
}