namespace MyApp.Models.ViewModels
{
    public class CartVM
    {
        public IEnumerable<Cart> ListOfCart { get; set; }
        public double Total { get; set; }
    }
}