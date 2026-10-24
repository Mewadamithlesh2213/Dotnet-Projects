using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.DataAccessLayer.Infrastructure.IRepository
{
    public  interface IProductRepository : IRepository<Product> 
    {
        void Update(Product product);





    }
}
