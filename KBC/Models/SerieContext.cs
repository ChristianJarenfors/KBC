using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KBC.Models
{
    public class SerieContext : DbContext
    {
        public SerieContext() : base(@"Data Source=killedbythecredits-215839.mssql.binero.se;Initial Catalog=215839-killedbythecredits;Integrated Security=False;User ID=215839_nd73572;Connect Timeout=300;Encrypt=False;Packet Size=4096;Password=84tFuG34hHv-T4.E,tv;MultipleActiveResultsSets=True;") { }
        public DbSet<Serie> Serie { get; set; }
        public DbSet<SerieGenre> Genre { get; set; }
        public DbSet<User> Users { get; set; }
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