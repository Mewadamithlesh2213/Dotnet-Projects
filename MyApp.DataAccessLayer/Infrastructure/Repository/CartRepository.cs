using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModels;
using MywebApplication.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class CartRepository: Repository<Cart>, ICartRepository



    {

        private readonly ApplicationDbContext _context; 
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int IncrementCartItem(Cart cart, int count)
        {
            cart.Count += count;
            return cart.Count;
        }
    }
}
