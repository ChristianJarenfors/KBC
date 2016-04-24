using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KBC.Models
{
    public class SerieContext : DbContext
    {
        public SerieContext() : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False") { }
        public DbSet<Serie> Serie { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public static void SetUpGenres(List<int> GenreToBeAdded, Serie S, SerieContext SC)
        {
            foreach (int Genrenumber in GenreToBeAdded)
            {
                //using (SerieContext SC = new SerieContext())
                //{
                foreach (var item in SC.Genre)
                {
                    //using (SerieContext Sc = new SerieContext)
                    //{
                    if (item.GenreType == (GenreCollection)Genrenumber)
                    {
                        //var s = S;
                        if (S.GenreId == null)
                        {
                            S.GenreId = new List<int>() { item.Id };
                            S.GenreTypes = new List<Genre>() { item };
                        }
                        else
                        {
                            S.GenreId.Add(item.Id);
                            S.GenreTypes.Add(item);
                        }
                        if (item.SerieId == null)
                        {
                            item.SerieId = new List<int>() { S.Id };
                            item.Serie = new List<Serie>() { S };
                        }
                        else
                        {
                            item.SerieId.Add(S.Id);
                            item.Serie.Add(S);
                        }

                        //Serie s = (from x in SC.Serie
                        //         where x.Id == S.Id
                        //         select x).FirstOrDefault();
                        //var genre = (from x in SC.Genre
                        //            where x.Id == item.Id
                        //            select x).First();
                        //s.GenreTypes.Add(genre);
                        //genre.Serie.Add(s);
                        //SC.Serie.Add(s);
                        //SC.Genre.Add(genre);
                        



                    }
                }
                SC.SaveChanges();
                //}
                //}
            }
        }
    }
}