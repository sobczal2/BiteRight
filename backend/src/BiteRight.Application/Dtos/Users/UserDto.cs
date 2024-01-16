namespace BiteRight.Application.Dtos.Users;

public class UserDto
{
    public Guid Id { get; set; }
    public string IdentityId { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime JoinedAt { get; set; }
    public ProfileDto Profile { get; set; } = default!;
}