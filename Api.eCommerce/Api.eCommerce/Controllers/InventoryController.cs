using Api.eCommerce.EC;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Library.eCommerce.Util;
using Microsoft.AspNetCore.Mvc;
using Spring2025_P1.Models;

namespace Api.eCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<Item?> Get()
        {
            return new InventoryEC().Get();
        }
        [HttpGet("{id}")]
        public Item? GetById(int id)
        {
            return new InventoryEC().Get().FirstOrDefault(i => i?.Id == id);
        }
        [HttpDelete("{id}")]
        public Item? Delete(int id)
        {
            return new InventoryEC().Delete(id);
        }
        [HttpPost]
        public Item? AddOrUpdate([FromBody]Item item)
        {
            var newItem = new InventoryEC().AddOrUpdate(item);
            return item;
        }
        [HttpPost("Search")]
        public IEnumerable<Item> Search([FromBody] QueryRequest query)
        {
            return new InventoryEC().Get(query.Query);
        }
    }
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/Cart")]
        public IEnumerable<Item?> Get()
        {
            return new CartEC().Get();
        }
        [HttpPost("/Cart")]
        public Item AddOrUpdate([FromBody] Item item)
        {
            return new CartEC().AddOrUpdate(item);
        }
        [HttpPost("/Cart/Search")]
        public IEnumerable<Item> Search([FromBody] QueryRequest query)
        {
            return new CartEC().Get(query.Query);
        }
        [HttpGet("/Cart/{id}")]
        public Item? GetById(int id)
        {
            return new CartEC().Get().FirstOrDefault(i => i?.Id == id);
        }
        [HttpDelete("/Cart")]
        public bool Delete(int id)
        {
            return new CartEC().Delete();
        }
        [HttpGet("/Cart/Subtotal")]
        public double GetSubtotal()
        {
            return new CartEC().Subtotal();
        }
        [HttpGet("/Cart/Grandtotal")]
        public double GetGrandtotal()
        {
            return new CartEC().Grandtotal();
        }
    }
}
