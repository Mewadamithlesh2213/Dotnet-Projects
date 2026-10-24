using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModels;
using MywebApplication.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    internal class CategoryRepository : Repository<Category>, ICategoriesRepository



    {

        private readonly ApplicationDbContext _context; 
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Add(CateegoryVM vm)
        {
            throw new NotImplementedException();
        }

        public void Update(Category category)
        {
            var categoryDb = _context.Categories.FirstOrDefault(x=>x.Id == category.Id);

            if (categoryDb != null)
            {
                categoryDb.Name = category.Name;
                categoryDb.DisplayOrder = category.DisplayOrder;
            }
        }
    }
}
