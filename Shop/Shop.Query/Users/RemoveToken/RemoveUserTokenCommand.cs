using Common.Application;

namespace Shop.Query.Users.RemoveToken;

public record RemoveUserTokenCommand(long UserId, long TokenId) : IBaseCommand;