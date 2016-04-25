using KBC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KBC.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult SearchResult()
        {


            List<Serie> ResultList = new List<Serie>();
            string textstring = Request["Search"];
            DateTime From; /* = DateTime.Parse(Request["From"]);*/
            DateTime.TryParse(Request["From"],out From);
            DateTime To; /*= DateTime.Parse(Request["To"]);*/
            DateTime.TryParse(Request["To"], out To);
            List<int> Genres = new List<int>();
            Genres = AddGenres(Genres);
            using (SerieContext SC = new SerieContext())
            {
                //Serie S = new Serie()
                //{

                //    Name = "Game of Thrones",
                //    ReleaseDatum = new DateTime(2008, 3, 13, 8, 0, 0),
                //    AverageGrade = 5,
                //    NumberOfVotes = 100,
                //    Description = "While a civil war brews between several noble families in Westeros,"
                //+ "the children of the former rulers of the land attempt to rise up to power."
                //+ "Meanwhile a forgotten race, bent on destruction, return after thousands of years in the North."
                //};
                //SC.Serie.Add(S);
                //SC.SaveChanges();
                //SerieContext.SetUpGenres(new List<int>() { 0, 1, 2, 3, 4 }, S, SC);
                //SC.Serie.Add(S);
                ResultList = SeriesBasedOnGenre(Genres, SC);
                ResultList = SeriesSelectedBasedOnRelease(ResultList, From, To,SC);
                ResultList = SeriesSelectedBasedOnTextString(ResultList, textstring,SC);
                //SerieGenre sg = new SerieGenre { Genre = GenreCollection.};
                //if (ResultList.Count==0)
                //{
                //    ResultList = SC.Serie.ToList();
                //}
                //SC.SaveChanges();
            }
            
            return View(ResultList);
        }

        private List<Serie> SeriesSelectedBasedOnTextString(List<Serie> List, string textstring,SerieContext SC)
        {
            List<Serie> newList;
            if ((List.Count == 0) || (List == null))
            {
                List = SC.Serie.ToList();
            }
            if (textstring!=null)
            {
                newList = (from x in List
                           where x.Name.ToLower().Contains(textstring.ToLower()) || x.Description.ToLower().Contains(textstring.ToLower())
                          
                           select x).ToList();
            }
            else
            {
                return List;
            }
            return newList;
        }

        private List<Serie> SeriesSelectedBasedOnRelease(List<Serie> List, DateTime From, DateTime to,SerieContext SC)
        {
            List<Serie> newList;
            if ((List.Count == 0)||(List==null))
            {
                List = SC.Serie.ToList();
            }
            DateTime D = new DateTime(1800, 1, 1,0,0,0);
            if (From > D && to > D)
            {
                newList = (from x in List
                           where (From <= x.ReleaseDatum) && (x.ReleaseDatum <= to)
                           select x).ToList();
                return newList;
            }
            else
            {
                return List;
            }
            
            
        }

        public List<Serie> SeriesBasedOnGenre( List<int> GenreId,SerieContext SC)
        {
            List<Serie> List = new List<Serie>();
            HashSet < Serie > Kortare = new HashSet<Serie>();
            foreach (int Genre in GenreId)
            {
                foreach (var item in SC.Genre)
                {
                    if ((GenreCollection)Genre == item.Genre)
                    {
                        using (SerieContext Sc = new SerieContext())
                        {
                            var s = (from x in Sc.Serie
                                     where x.SerieId == item.SerieId
                                     select x).First();
                            Kortare.Add(s);
                        } 
                    }
                }
            }
            List = Kortare.ToList();
            return List;
        }
        public List<int> AddGenres(List<int> List)
        {
            for (int i = 0; i < 8; i++)
            {
                List = AddGenre(List, i);
            }
            return List;
        }
        public List<int> AddGenre(List<int> List, int Genre) {
            if (Request[((GenreCollection)Genre).ToString()]!=null)
            {
                List.Add(Genre);
            }
            return List;
        }
        
    }
}