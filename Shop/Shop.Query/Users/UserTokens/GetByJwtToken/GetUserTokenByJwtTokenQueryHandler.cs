using Common.Query;
using Dapper;
using Shop.Domain.UserAgg.Repository;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens.GetByJwtToken;

internal class GetUserTokenByJwtTokenQueryHandler : IBaseQueryHandler<GetUserTokenByJwtTokenQuery, UserTokenDto?>
{
    private readonly DapperContext _dapperContext;

    public GetUserTokenByJwtTokenQueryHandler(IUserRepository repository, DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<UserTokenDto?> Handle(GetUserTokenByJwtTokenQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT TOP(1) * FROM {_dapperContext.UserTokens} WHERE HashJwtToken=@hashJwtToken";
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto?>(sql,
            new { hashJwtToken = request.HashJwtToken });
    }
}