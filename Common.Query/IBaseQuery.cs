using Common.Query.Filter;
using MediatR;

namespace Common.Query;

public interface IBaseQuery<out TResponse> : IRequest<TResponse> where TResponse : class//یعنی یکدونه تی ریسپانس میگیره وهمونو برمیگردونه
{

}

public class QueryFilter<TResponse,TParam> : IBaseQuery<TResponse> where TResponse : BaseFilter where TParam: BaseFilterParam
{
    public TParam FilterParam { get; set; }

    public QueryFilter(TParam filterParam)
    {
      this.FilterParam=filterParam;
    }
}