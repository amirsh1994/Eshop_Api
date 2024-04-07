using MediatR;

namespace Common.Query;

public interface IBaseQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>//یعنی تی کوری و تی ریسپانس دریافت میکنه  کخ کوری از جنس همین بالاییه هستش که ریسپانسشو بر میگردونه
    where TQuery : IBaseQuery<TResponse>
    where TResponse : class
{

}