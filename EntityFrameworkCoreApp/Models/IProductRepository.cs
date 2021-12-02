using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.Models
{
    public interface IProductRepository
    {
        Product GetById(int productid);
        IQueryable<Product> Products { get; }
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        
    }
}
