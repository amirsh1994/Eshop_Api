using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetById;

public class GetCommentByIdQueryHandler:IBaseQueryHandler<GetCommentByIdQuery, CommentDto>
{
    private readonly ShopContext _context;

    public GetCommentByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == request.CommentId, cancellationToken);

        return comment.Map();
    }
}