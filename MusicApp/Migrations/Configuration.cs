namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MusicApp.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<MusicApp.Models.MusicStoreEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MusicApp.Models.MusicStoreEntities context)
        {
            //  This method will be called after migrating to the latest version.
            var genres = context.Genres.ToList();
            var artists = context.Artists.ToList();


            new List<Album>
            {
             new Album { Title = "Supposed Former Infatuation Junkie", Genre = genres.Single(g => g.Name == "Rock"), Price = 8.99M, Artist = artists.Single(a => a.Name == "Alanis Morissette"), AlbumArtUrl = "/Content/Images/AlbumCovers/Alanis_Morissette_-_Supposed_Former_Infatuation_Junkie.png" },
               new Album { Title = "Jagged Little Pill", Genre = genres.Single(g => g.Name == "Rock"), Price = 8.99M, Artist = artists.Single(a => a.Name == "Alanis Morissette"), AlbumArtUrl = "/Content/Images/AlbumCovers/Alanis_Morissette_-_Jagged_Little_Pill.jpg" },
                new Album { Title = "Under Rug Swept", Genre = genres.Single(g => g.Name == "Rock"), Price = 8.99M, Artist = artists.Single(a => a.Name == "Alanis Morissette"), AlbumArtUrl = "/Content/Images/AlbumCovers/Alanis_Morissette_-_Under_Rug_Swept.png" },
                new Album { Title = "So-Called Chaos", Genre = genres.Single(g => g.Name == "Rock"), Price = 8.99M, Artist = artists.Single(a => a.Name == "Alanis Morissette"), AlbumArtUrl = "/Content/Images/AlbumCovers/SoCalledChaos.png" },
                new Album { Title = "Flavors of Entanglement", Genre = genres.Single(g => g.Name == "Rock"), Price = 8.99M, Artist = artists.Single(a => a.Name == "Alanis Morissette"), AlbumArtUrl = "/Content/Images/AlbumCovers/Flavors_of_Entanglement.jpg" },
                new Album { Title = "Havoc and Bright Lights", Genre = genres.Single(g => g.Name == "Rock"), Price = 8.99M, Artist = artists.Single(a => a.Name == "Alanis Morissette"), AlbumArtUrl = "/Content/Images/AlbumCovers/havoc.gif" }
            }.ForEach(a => context.Albums.Add(a));
          
            //var albums = context.Albums.Where(a => a.Artist.Name == "Alanis Morissette").ToList();
            //albums.ForEach(a => context.Albums.Remove(a));

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
