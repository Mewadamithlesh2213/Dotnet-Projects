using MyApp.Models;
using MyApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.DataAccessLayer.Infrastructure.IRepository
{
    public  interface ICategoriesRepository : IRepository<Category> 
    {
        void Add(CateegoryVM vm);
        void Update(Category category);





    }
}
