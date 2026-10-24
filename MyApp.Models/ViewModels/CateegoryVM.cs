using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models.ViewModels
{
    public class CateegoryVM
    {
        public Category Category { get; set; } = new Category();

        // Yahan '?' laga diya taaki validation fail na ho
        public IEnumerable<Category>? categories { get; set; } = new List<Category>();
    }
}