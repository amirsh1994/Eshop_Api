using Common.Application;
using Shop.Domain.CommentAgg;

namespace Shop.Application.Comments.Delete;

public record DeleteCommentByIdCommand(long CommentId,long UserId) : IBaseCommand;



internal class DeleteCommentByIdCommandHandler:IBaseCommandHandler<DeleteCommentByIdCommand>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentByIdCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult> Handle(DeleteCommentByIdCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetTracking(request.CommentId);
        if (comment==null||comment.UserId!=request.UserId)
        {
            return OperationResult.NotFound();
        }

        await _commentRepository.DeleteAndSave(comment);
        return OperationResult.Success();
    }
}



