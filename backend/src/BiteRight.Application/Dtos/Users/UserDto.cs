// # ==============================================================================
// # Solution: BiteRight
// # File: UserDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Users;

#endregion

namespace BiteRight.Application.Dtos.Users;

public class UserDto
{
    public UserDto(
        Guid id,
        string identityId,
        string username,
        string email,
        DateTime joinedAt,
        ProfileDto profile
    )
    {
        Id = id;
        IdentityId = identityId;
        Username = username;
        Email = email;
        JoinedAt = joinedAt;
        Profile = profile;
    }

    public Guid Id { get; set; }
    public string IdentityId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime JoinedAt { get; set; }
    public ProfileDto Profile { get; set; }

    public static UserDto FromDomain(
        User user
    )
    {
        return new UserDto(
            user.Id,
            user.IdentityId,
            user.Username,
            user.Email,
            user.JoinedAt,
            ProfileDto.FromDomain(user.Profile)
        );
    }
}