using Common.Application;
using Shop.Domain.CommentAgg;

namespace Shop.Application.Comments.ChangeStatus;

public class ChangeCommentStatusCommandHandler : IBaseCommandHandler<ChangeCommentStatusCommand>
{
    private readonly ICommentRepository _repository;

    public ChangeCommentStatusCommandHandler(ICommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(ChangeCommentStatusCommand request, CancellationToken cancellationToken)
    {
        var currentComment = await _repository.GetTracking(request.CommentId);
        if (currentComment is null)
        {
            return OperationResult.NotFound();
        }
        currentComment.ChangeStatus(request.Status);
        await _repository.Save();
        return OperationResult.Success();
    }
}