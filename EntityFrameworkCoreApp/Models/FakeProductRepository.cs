using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product> { 
        
            new Product(){ProductId=1,Name="Iphone 5",Description="Iphone 5 Description",Price=1232,Category="Telefon"},
            new Product(){ProductId=1,Name="Iphone 6",Description="Iphone 6 Description",Price=1332,Category="Telefon"},
            new Product(){ProductId=1,Name="Iphone 7",Description="Iphone 7 Description",Price=1432,Category="Telefon"},
            new Product(){ProductId=1,Name="Iphone 8",Description="Iphone 8 Description",Price=1532,Category="Telefon"}

        }.AsQueryable();

        public void CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int productid)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
