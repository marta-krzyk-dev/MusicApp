using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicApp.Models;
using System.Configuration;

namespace MusicApp.Controllers
{
    public class HomeController : Controller
    {
        private MusicStoreEntities storeDB = new MusicStoreEntities();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var albums = GetTopSellingAlbums(5);

            return View(albums);
            
    //        return
    //ConfigurationManager.ConnectionStrings["MusicStoreEntities"].ConnectionString;
        }

        private List<Album> GetTopSellingAlbums(int count)
        { 
            //Group the order details by album and return the album with the highest count

            return storeDB.Albums
                    .OrderByDescending(a => a.OrderDetails.Count())
                    .Take(count)
                    .ToList();
        }
    }
}
