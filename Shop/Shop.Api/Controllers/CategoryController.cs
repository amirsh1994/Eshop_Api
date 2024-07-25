using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories.DTOs;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Api.Controllers
{
    [PermissionChecker(Permission.Category_Management)]
    public class CategoryController : ApiController
    {
        private readonly ICategoryFacade _categoryFacade;

        public CategoryController(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }



        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<CategoryDto>>> GetCategories()
        {
            var results = await _categoryFacade.GetCategories();
            return QueryResult<List<CategoryDto>>(results);

        }



        [AllowAnonymous]
        [HttpGet("{id:long}")]
        public async Task<ApiResult<CategoryDto>> GetCategoryById(long id)
        {
            var result = await _categoryFacade.GetCategoryById(id);

            return QueryResult<CategoryDto>(result);
        }




        [HttpGet("getChild/{parentId:long}")]
        public async Task<ApiResult<List<ChildCategoryDto>>> GetCategoriesWithPrentId(long parentId)
        {
            var result = await _categoryFacade.GetCategoriesByParentId(parentId);

            return QueryResult<List<ChildCategoryDto>>(result);
        }




        [HttpPost]
        public async Task<ApiResult<long>> CreateCategory(CreateCategoryCommand command)//میره از فورم دریافتش میکنه 
        {
            var result = await _categoryFacade.Create(command);
            var url=Url.Action("GetCategoryById","Category", new { id = result.Data },Request.Scheme);
            return CommandResult<long>(result, HttpStatusCode.Created, url);
        }





        [HttpPost("AddChild")]
        public async Task<ApiResult<long>> AddChild(AddChildCategoryCommand command)
        {
            var result= await _categoryFacade.AddChild(command);
            var url = Url.Action("GetCategoryById", "Category", new { id = result.Data },Request.Scheme);
            return CommandResult<long>(result, HttpStatusCode.Created, url);
        }





        [HttpPut]
        public async Task<ApiResult> EditCategory(EditCategoryCommand command)
        {
            var result = await _categoryFacade.Edit(command);
            return CommandResult(result);
        }






        [HttpDelete("{categoryId:long}")]//توی روت میگیره
        public async Task<ApiResult> RemoveCategory(long categoryId)
        {
            var result = await _categoryFacade.Remove(categoryId);
            return CommandResult(result);
        }





    }
}
