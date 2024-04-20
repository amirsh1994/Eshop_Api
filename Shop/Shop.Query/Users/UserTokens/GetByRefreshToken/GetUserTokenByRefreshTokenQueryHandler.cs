using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens.GetByRefreshToken;

internal class GetUserTokenByRefreshTokenQueryHandler : IBaseQueryHandler<GetUserTokenByRefreshTokenQuery, UserTokenDto?>
{
    private readonly DapperContext _context;

    public GetUserTokenByRefreshTokenQueryHandler(DapperContext context)
    {
        _context = context;
    }

    public async Task<UserTokenDto?> Handle(GetUserTokenByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();
        var sql = $"SELECT TOP(1) * FROM{_context.UserTokens} WHERE HashRefreshToken=@hashRefreshToken";
        var result = await connection.QueryFirstOrDefaultAsync<UserTokenDto>(sql, new { hashRefreshToken = request.HashRefreshToken });

        return result;
    }
}