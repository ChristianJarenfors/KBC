using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KBC.Models
{
    public class SerieContext : DbContext
    {
        public SerieContext() : base("Webb") { }
        public DbSet<Serie> Serie { get; set; }
        public DbSet<SerieGenre> Genre { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder
                                                modelBuilder)
        {
            modelBuilder.Entity<Serie>()
                .HasMany(s => s.SerieImgsURL)
                .WithRequired(i => i.Serie)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Serie>()
                .HasMany(s => s.Genres)
                .WithRequired(g => g.Serie)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<User>()
                .HasMany(u => u.SeriesFollowed)
                .WithMany();

        }
        public static void SetUpGenres(List<int> GenreToBeAdded, int SerieId, SerieContext SC)
        {
            var s = (from x in SC.Serie
                     where x.SerieId == SerieId
                     select x).First();
            foreach (int i in GenreToBeAdded)
            {
                SC.Genre.Add(new SerieGenre()
                {
                    Genre = (GenreType)i,
                    Serie = s,
                    SerieId = SerieId
                });
            }
        }
        //public static void SetUpGenres(List<int> GenreToBeAdded, Serie S, SerieContext SC)
        //{
        //    foreach (int Genrenumber in GenreToBeAdded)
        //    {
        //        using (SerieContext SC = new SerieContext())
        //        {
        //            foreach (var item in SC.Genre)
        //            {
        //                using (SerieContext Sc = new SerieContext)
        //                {
        //                    if (item.GenreType == (GenreCollection)Genrenumber)
        //                    {
        //                        var s = S;
        //                        if (S.GenreIds == null)
        //                        {
        //                            S.GenreIds = new List<int>() { item.GenreId };
        //                            S.GenreTypes = new List<Genre>() { item };
        //                        }
        //                        else
        //                        {
        //                            S.GenreIds.Add(item.GenreId);
        //                            S.GenreTypes.Add(item);
        //                        }
        //                        if (item.SerieIds == null)
        //                        {
        //                            item.SerieIds = new List<int>() { S.SerieId };
        //                            item.Serie = new List<Serie>() { S };
        //                        }
        //                        else
        //                        {
        //                            item.SerieIds.Add(S.SerieId);
        //                            item.Serie.Add(S);
        //                        }

        //                        Serie s = (from x in SC.Serie
        //                                   where x.Id == S.Id
        //                                   select x).FirstOrDefault();
        //                        var genre = (from x in SC.Genre
        //                                     where x.Id == item.Id
        //                                     select x).First();
        //                        s.GenreTypes.Add(genre);
        //                        genre.Serie.Add(s);
        //                        SC.Serie.Add(s);
        //                        SC.Genre.Add(genre);




        //                    }
        //                }
        //                SC.SaveChanges();
        //            }
        //        }
        //    }
        //}
    }
}