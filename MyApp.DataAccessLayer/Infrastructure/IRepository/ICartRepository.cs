using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.DataAccessLayer.Infrastructure.IRepository
{
    public  interface ICartRepository : IRepository<Cart> 
    {
        int IncrementCartItem(Cart cart, int count);


    }
}
