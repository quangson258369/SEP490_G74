using HCS.Business.RequestModel.CategoryRequestModel;
using HCS.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCS.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var result = await _categoryService.GetCategory(id);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetCategories();

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryAddModel category)
        {
            var result = await _categoryService.AddCategory(category);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromQuery]int id, [FromBody] CategoryUpdateModel category)
        {
            var result = await _categoryService.UpdateCategory(id, category);

            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task DeleteCategory(int id)
        {
           await _categoryService.DeleteCategory(id);
        }
    }
}