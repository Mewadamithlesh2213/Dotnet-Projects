using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModels;
using MywebApplication.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    internal class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUser    



    {

        private readonly ApplicationDbContext _context; 
        public ApplicationUserRepository (ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
