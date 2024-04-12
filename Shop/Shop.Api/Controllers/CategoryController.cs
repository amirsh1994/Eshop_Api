using Common.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories.DTOs;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryFacade _categoryFacade;

        public CategoryController(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        [HttpGet]

        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            var results = await _categoryFacade.GetCategories();

            return Ok(results);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(long id)
        {
            var result = await _categoryFacade.GetCategoryById(id);

            return Ok(result);
        }

        [HttpGet("GetChildCategories/{parentId:long}")]
        public async Task<ActionResult<List<ChildCategoryDto>>> GetCategoriesWithPrentId(long parentId)
        {
            var result = await _categoryFacade.GetCategoriesByParentId(parentId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)//میره از فورم دریافتش میکنه 
        {
            var op = await _categoryFacade.Create(command);
            if (op.Status == OperationResultStatus.Success)
            {
                return Ok(op.Message);
            }

            return BadRequest(op.Message);
        }


        [HttpPost("AddChild")]
        public async Task<IActionResult> AddChild(AddChildCategoryCommand command)
        {
            var result= await _categoryFacade.AddChild(command);
            if (result.Status==OperationResultStatus.Success  )
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> EditCategory(EditCategoryCommand command)
        {
            var op = await _categoryFacade.Edit(command);
            if (op.Status == OperationResultStatus.Success)
                return Ok(op.Message);

            return BadRequest(op.Message);
        }


        [HttpDelete("{categoryId:long}")]//توی روت میگیره
        public async Task<IActionResult> RemoveCategory(long categoryId)
        {
            var result = await _categoryFacade.Remove(categoryId);

            if (result.Status == OperationResultStatus.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);



        }
    }
}
