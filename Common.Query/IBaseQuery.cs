using MediatR;

namespace Common.Query;

public interface IBaseQuery<out TResponse> : IRequest<TResponse> where TResponse : class//یعنی یکدونه تی ریسپانس میگیره وهمونو برمیگردونه
{

}