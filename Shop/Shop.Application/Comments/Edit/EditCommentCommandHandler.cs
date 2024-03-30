using Common.Application;
using Shop.Domain.CommentAgg;

namespace Shop.Application.Comments.Edit;

public class EditCommentCommandHandler:IBaseCommandHandler<EditCommentCommand>
{
    private readonly ICommentRepository _repository;

    public EditCommentCommandHandler(ICommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var oldComment =await _repository.GetTracking(request.CommentId);//چون بصورت ترکینگ دریافتش کردیم اپدیت رو صدا نمیزنیم
        if (oldComment is null|| oldComment.UserId!=request.UserId)
        {
            return OperationResult.NotFound();
        }
        oldComment.Edit(request.Text);
        await _repository.Save();
        return OperationResult.Success();
    }
}