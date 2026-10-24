using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MywebApplication.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public ICategoriesRepository Category { get; private set; }
        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            Cart = new CartRepository(context);
            ApplicationUser = new ApplicationUserRepository(context);
        }
        public ICategoriesRepository CategoriesRepository { get; private set; }

        public IProductRepository Product { get; private set; }
        public ICartRepository Cart { get; private set; }
        public IApplicationUser ApplicationUser  { get; private set; }


        public void save()
        {
           _context.SaveChanges();
        }
    }
}
