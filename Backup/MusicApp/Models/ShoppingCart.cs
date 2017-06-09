using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicApp.Models
{
    public class ShoppingCart
    {
        MusicStoreEntities storeDB = new MusicStoreEntities();

        private string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
	            {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
	            }
            }

            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Carts.Where(c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }

            storeDB.SaveChanges();
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Album album)
        {
            var cartItem = storeDB.Carts.SingleOrDefault
                            ( c => c.CartId == ShoppingCartId && 
                              c.AlbumId == album.AlbumId);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    AlbumId = album.AlbumId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                storeDB.Carts.Add(cartItem);
            }
            else
            {
                ++ cartItem.Count;
            }

            //Save changes
            storeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            //Get the cart
            var cartItem = storeDB.Carts.Single
                            (   cart => cart.CartId == ShoppingCartId && 
                                cart.RecordId == id );

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    --cartItem.Count;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }

                //Save changes
                storeDB.SaveChanges();
            }

            return itemCount;
        }

        public void EmptyCart()
        { 
            //Remove all items from user's shopping cart
            var cartItems = storeDB.Carts.Where(c => c.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }

            storeDB.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(c => c.CartId == ShoppingCartId).ToList();
        }

        //retrieves the total number of albums a user has in their shopping cart.
        public int GetCount()
        {
            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count). Sum();
            
            return count ?? 0; 
        }

        public decimal GetTotal()
        {
            //decimal? total = (from cartItem in storeDB.Carts
            //                join album in storeDB.Albums 
            //                on cartItem.AlbumId equals album.AlbumId
            //                where cartItem.CartId == ShoppingCartId
            //                select album.Price * (int?)cartItem.Count).Sum();

            //version by tutorial
            decimal? total = (from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Album.Price).Sum();

            return total ?? decimal.Zero; 
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var price = storeDB.Albums.Find(item.AlbumId).Price;
                var orderDetail = new OrderDetail 
                                    {   AlbumId = item.AlbumId,
                                        OrderId = order.OrderId,
                                        UnitPrice = price, //item.Album.Price,
                                        Quantity = item.Count};
                
                orderTotal += (item.Count * price);

                storeDB.OrderDetails.Add(orderDetail);
            }

            // Set the order's total to the orderTotal count
            order.Total = orderTotal;
            // Save the order
            storeDB.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
            
        }
    }
}