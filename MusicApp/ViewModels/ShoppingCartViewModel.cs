using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicApp.Models;

namespace MusicApp.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; } //total price for all items in the cart
    }
}