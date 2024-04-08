using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetByFilter;

internal class GetCommentByFilterQueryHandler:IBaseQueryHandler<GetCommentByFilterQuery,CommentFilterResult>
{
    private readonly ShopContext _context;

    public GetCommentByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params=request.FilterParam;

        var result = _context.Comments.OrderByDescending(x => x.Id).AsQueryable();

        if (@params.Status != null)
            result = result.Where(x => x.Status == @params.Status);

        if (@params.UserId != null)
            result = result.Where(x => x.UserId == @params.UserId);

        if (@params.StartDate != null)
            result = result.Where(x => x.CreationDate >= @params.StartDate.Value.Date);

        if (@params.EndDate != null)
            result = result.Where(x => x.CreationDate <= @params.EndDate.Value.Date);



        var skip = (@params.PageId - 1) * @params.Take;

        var model = new CommentFilterResult()
        {
            Data =await result.Skip(skip).Take(@params.Take).Select(comment=>comment.MapFilterComment()).ToListAsync(cancellationToken),
            FilterParam = @params

        };
        model.GeneratePaging(result,@params.Take,@params.PageId);
        return model;
    }
}