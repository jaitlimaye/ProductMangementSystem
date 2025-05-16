using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.BLL.DTOs.Category;
using ProductManagementSystem.BLL.Interfaces.Services.Categories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IListCategoriesService _listService;
        private readonly IGetCategoryService _getService;
        private readonly ICreateCategoryService _createService;
        private readonly IUpdateCategoryService _updateService;
        private readonly IPatchCategoryService _patchService;
        private readonly IDeleteCategoryService _deleteService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            IListCategoriesService listService,
            IGetCategoryService getService,
            ICreateCategoryService createService,
            IUpdateCategoryService updateService,
            IPatchCategoryService patchService,
            IDeleteCategoryService deleteService,
            ILogger<CategoryController> logger)
        {
            _listService = listService;
            _getService = getService;
            _createService = createService;
            _updateService = updateService;
            _patchService = patchService;
            _deleteService = deleteService;
            _logger = logger;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoryResponse>>> GetList()
        {
            _logger.LogInformation("Fetching all categories");
            var categories = await _listService.ExecuteAsync();
            _logger.LogInformation("Retrieved {Count} categories", categories?.Count() ?? 0);
            return Ok(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryResponse>> Get(int id)
        {
            _logger.LogInformation("Fetching category with ID {CategoryId}", id);
            var category = await _getService.ExecuteAsync(id);
            if (category == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} not found", id);
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetCategoryResponse>> Create([FromBody] CreateCategoryRequest request)
        {
            _logger.LogInformation("Creating new category with Name {Name}", request.Name);
            try
            {
                var created = await _createService.ExecuteAsync(request);
                _logger.LogInformation("Created category {CategoryId}", created.CategoryId);
                return CreatedAtAction(nameof(Get), new { id = created.CategoryId }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category with Name {Name}", request.Name);
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetCategoryResponse>> Update(int id, [FromBody] UpdateCategoryRequest request)
        {
            _logger.LogInformation("Updating category {CategoryId}", id);
            if (id != request.CategoryId)
            {
                _logger.LogWarning("URL ID {UrlId} does not match payload CategoryId {PayloadId}", id, request.CategoryId);
                return BadRequest("ID in URL must match CategoryId in payload.");
            }

            try
            {
                var updated = await _updateService.ExecuteAsync(request);
                if (updated == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found for update", id);
                    return NotFound();
                }
                _logger.LogInformation("Updated category {CategoryId}", id);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category {CategoryId}", id);
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetCategoryResponse>> Patch(int id, [FromBody] PatchCategoryRequest request)
        {
            _logger.LogInformation("Patching category {CategoryId}", id);
            try
            {
                var patched = await _patchService.ExecuteAsync(id, request);
                if (patched == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found for patch", id);
                    return NotFound();
                }
                _logger.LogInformation("Patched category {CategoryId}", id);
                return Ok(patched);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error patching category {CategoryId}", id);
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting category {CategoryId}", id);
            try
            {
                await _deleteService.ExecuteAsync(id);
                _logger.LogInformation("Deleted category {CategoryId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category {CategoryId}", id);
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
