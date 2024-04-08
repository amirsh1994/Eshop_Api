using Shop.Domain.CommentAgg;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments;

internal static class CommentMapper
{
    public static CommentDto? Map(this Comment? c)
    {
        if (c==null)
        {
            return null;
        }
        return new CommentDto()
        {
            Id = c.Id,
            CreationDate = c.CreationDate,
            Status = c.Status,
            ProductID = c.ProductId,
            UserId = c.UserId,
            Text = c.Text

        };
    }
    public static CommentDto MapFilterComment(this Comment c)
    {
        if (c == null)
        {
            return null;
        }
        return new CommentDto()
        {
            Id = c.Id,
            CreationDate = c.CreationDate,
            Status = c.Status,
            ProductID = c.ProductId,
            UserId = c.UserId,
            Text = c.Text

        };
    }
}