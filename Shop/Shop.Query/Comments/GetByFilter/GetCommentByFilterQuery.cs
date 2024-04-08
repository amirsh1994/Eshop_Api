using Common.Query;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetByFilter;

public class GetCommentByFilterQuery:QueryFilter<CommentFilterResult, CommentFilterParam>
{
    public GetCommentByFilterQuery(CommentFilterParam filterParam) : base(filterParam)
    {
    }
}