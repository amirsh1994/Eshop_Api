using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens.GetByRefreshToken;

public record GetUserTokenByRefreshTokenQuery(string HashRefreshToken) : IBaseQuery<UserTokenDto?>;