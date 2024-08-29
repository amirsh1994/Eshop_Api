using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Delete;
using Shop.Application.Comments.Edit;
using Shop.Domain.CommentAgg.Enums;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Comments;
using Shop.Query.Comments.DTOs;

namespace Shop.Api.Controllers;


public class CommentController : ApiController
{

    private readonly ICommentFacade _commentFacade;

    public CommentController(ICommentFacade commentFacade)
    {
        _commentFacade = commentFacade;
    }



    [PermissionChecker(Permission.Comment_Management)]
    [HttpGet]
    public async Task<ApiResult<CommentFilterResult?>> GetCommentByFilter([FromQuery] CommentFilterParam filterParam)
    {
        var result = await _commentFacade.GetCommentsByFilter(filterParam);

        return QueryResult<CommentFilterResult>(result);
    }

    [HttpGet("productComments")]
    public async Task<ApiResult<CommentFilterResult?>> GetProductCommentBy(int pageId = 1, int take = 10, long productId = 10)
    {
        var result = await _commentFacade.GetCommentsByFilter(new CommentFilterParam()
        {
            PageId = pageId,
            ProductId = productId,
            Take = take,
            Status = CommentStatus.Accepted

        });

        return QueryResult<CommentFilterResult>(result);
    }


    [PermissionChecker(Permission.Comment_Management)]//فقط ادمین میتونه بگیره
    [HttpGet("{commentId}")]
    public async Task<ApiResult<CommentDto?>> GetCommentById(long commentId)
    {
        var result = await _commentFacade.GetCommentById(commentId);
        return QueryResult<CommentDto>(result);
    }


    [HttpPost]
    [Authorize]
    public async Task<ApiResult> CreateComment(CreateCommentCommand command)
    {
        var result = await _commentFacade.CreateComment(command);
        return CommandResult(result);
    }


    [HttpPut]
    [Authorize]
    public async Task<ApiResult> UpdateComment(EditCommentCommand command)
    {
        var result = await _commentFacade.EditComment(command);
        return CommandResult(result);
    }


    [PermissionChecker(Permission.Comment_Management)]
    [HttpPut(" ChangeStatus")]
    public async Task<ApiResult> ChangeCommentStatus(ChangeCommentStatusCommand command)
    {
        var result = await _commentFacade.ChangeStatus(command);
        return CommandResult(result);
    }

    [Authorize]
    [HttpDelete("{commentId:long}")]
    public async Task<ApiResult> DeleteComment(long commentId)
    {
        var result = await _commentFacade.DeleteComment(new DeleteCommentByIdCommand(commentId, User.GetUserId()));
        return CommandResult(result);

    }
}

