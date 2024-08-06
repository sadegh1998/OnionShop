using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.ApplicationContract.Order;
using System.Linq;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";
        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            CartItems = serializer.Deserialize<List<CartItem>>(value);
            if(CartItems != null)
            {
                foreach (var item in CartItems)
                {
                    item.TotalItemPrice = item.UnitPrice * item.Count;
                }
            }
            else
            {
                CartItems = new List<CartItem>();
            }
           
        }
        public IActionResult OnGetRemoveItemFromCard(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemForDelete = cartItems.FirstOrDefault(x=>x.Id == id);
            cartItems.Remove(itemForDelete);
            var options = new CookieOptions {Expires = DateTime.Now.AddDays(2) };
            Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
            return RedirectToPage("/Cart");
        }
    }
}
