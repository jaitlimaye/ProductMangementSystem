using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.BLL.DTOs.Product;
using ProductManagementSystem.BLL.Interfaces.Services.Products;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGetProductService _getService;
        private readonly ICreateProductService _createService;
        private readonly IUpdateProductService _updateService;
        private readonly IPatchProductService _patchService;
        private readonly IDeleteProductService _deleteService;
        private readonly IGetAllProductDetailsService _getAllService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IGetProductService getService,
            ICreateProductService createService,
            IUpdateProductService updateService,
            IPatchProductService patchService,
            IDeleteProductService deleteService,
            IGetAllProductDetailsService getAllService,
            ILogger<ProductController> logger)
        {
            _getService = getService;
            _createService = createService;
            _updateService = updateService;
            _patchService = patchService;
            _deleteService = deleteService;
            _getAllService = getAllService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetAllProductsResponse>>> GetList()
        {
            _logger.LogInformation("Fetching all products");
            var list = await _getAllService.ExecuteAsync();
            _logger.LogInformation("Retrieved {Count} products", list?.Count() ?? 0);
            return Ok(list);
        }
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<DetailProductResponse>> Get(int id)
        {
            _logger.LogInformation("Fetching product with ID {ProductId}", id);
            var product = await _getService.ExecuteAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", id);
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DetailProductResponse>> Post([FromForm] CreateProductRequest request)
        {
            _logger.LogInformation("Creating product {Name} in category {CategoryId}", request.Name, request.CategoryId);
            try
            {
                var created = await _createService.ExecuteAsync(request);
                _logger.LogInformation("Product created with ID {ProductId}", created.ProductId);
                return CreatedAtAction(nameof(Get), new { id = created.ProductId }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product {Name}", request.Name);
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DetailProductResponse>> Put(int id, [FromForm] UpdateProductRequest request)
        {
            _logger.LogInformation("Updating product {ProductId}", id);
            if (id != request.ProductId)
            {
                _logger.LogWarning("URL ID {UrlId} does not match payload ID {PayloadId}", id, request.ProductId);
                return BadRequest("ID in URL must match ID in payload.");
            }

            try
            {
                var updated = await _updateService.ExecuteAsync(request);
                if (updated == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found for update", id);
                    return NotFound();
                }
                _logger.LogInformation("Product {ProductId} updated successfully", id);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product {ProductId}", id);
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DetailProductResponse>> Patch(int id, [FromBody] PatchProductRequest request)
        {
            _logger.LogInformation("Patching product {ProductId}", id);
            try
            {
                var patched = await _patchService.ExecuteAsync(id, request);
                if (patched == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found for patch", id);
                    return NotFound();
                }
                _logger.LogInformation("Product {ProductId} patched successfully", id);
                return Ok(patched);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error patching product {ProductId}", id);
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting product {ProductId}", id);
            try
            {
                await _deleteService.ExecuteAsync(id);
                _logger.LogInformation("Product {ProductId} deleted", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product {ProductId}", id);
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
