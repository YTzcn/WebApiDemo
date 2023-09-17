using Yahya.WebApiDemo.Entities;
using Yahya.WebApiDemo.Models;

namespace Yahya.WebApiDemo.DataAcces
{
    public interface IProductDal :IEntityRepository<Product>
    {
        List<ProductModel> GetProductWithDetails();
    }
}
