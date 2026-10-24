using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MywebApplication.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    internal class ProductRepository : Repository<Product>, IProductRepository



    {

        private readonly ApplicationDbContext _context; 
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productDb = _context.products.FirstOrDefault(x=>x.Id == product.Id);

            if(productDb == null)
            {
                productDb.Name = product.Name;
                productDb.Description = product.Description;
                productDb.Price = product.Price;

                if(product.ImageUrl != null)
                {
                    productDb.ImageUrl = product.ImageUrl;

                }
            }
        }
    }
}
