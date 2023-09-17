using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Yahya.WebApiDemo.DataAcces;
using Yahya.WebApiDemo.Entities;

namespace Yahya.WebApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        IProductDal _productDal;
        public ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [HttpGet("")]
        [Authorize(Roles = "Editor")]
        public IActionResult Get()
        {
            var products = _productDal.GetAll();
            return Ok(products);
        }
        [HttpGet("{productId}")]
        [Authorize(Roles = "Editor")]
        public IActionResult Get(int productId)
        {
            try
            {
                var products = _productDal.GetById(x => x.ProductId == productId);
                if (products == null)
                {
                    return NotFound("There is not product in this ıd :"+productId);
                }
                return Ok(products);
            }
            catch{}

            return BadRequest();
        }
        [HttpPost("")]
        public IActionResult Post(Product product)
        {
            try
            {
                _productDal.Add(product);
                return new StatusCodeResult(201);
            }
            catch(Exception exception) {
               throw new Exception(exception.Message+"  "+exception.InnerException);
            }
            return Ok();
        }
        [HttpPut("")]
        public IActionResult Put(Product product)
        {
            try
            {
                _productDal.Update(product);
                return Ok(product);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message + "  " + exception.InnerException);
            }
            return Ok();
        }
        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            try
            {
                _productDal.Delete(new Product() { ProductId=productId});
                return Ok();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message + "  " + exception.InnerException);
            }
            return Ok();
        }
        [HttpGet("GetProductDetails")]
        public IActionResult GetProductsWithDetails()
        {
            try
            {
                var result = _productDal.GetProductWithDetails();
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
            return BadRequest();
        }
    }

}
