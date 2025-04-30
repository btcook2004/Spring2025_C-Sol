using Library.eCommerce.DTO;
using Library.eCommerce.Models;

namespace Api.eCommerce.Database
{
    public class FakeCartDatabase
    {
        private static List<Item> cart = new List<Item> {};
        public static List<Item> Cart
        {
            get
            {
                return cart;
            }
        }
        public static IEnumerable<Item> Search(string? query)
        {
            return Cart.Where(p => p?.Product?.Name?.ToLower().ToLower()
                            .Contains(query?.ToLower() ?? string.Empty) ?? false);
        }
    }
}
