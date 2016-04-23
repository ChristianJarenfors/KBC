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
            DateTime From = DateTime.Parse(Request["From"]);
            DateTime To = DateTime.Parse(Request["To"]);
            List<int> Genres = new List<int>();
            Genres = AddGenres(Genres);
            using (SerieContext SC = new SerieContext())
            {
                ResultList = SeriesBasedOnGenre(Genres, SC);
                ResultList = SeriesSelectedBasedOnRelease(ResultList, From, To);
                ResultList = SeriesSelectedBasedOnTextString(ResultList, textstring);
                if (ResultList==null)
                {
                    ResultList = SC.Serie.ToList();
                }
            }
            return View(ResultList);
        }

        private List<Serie> SeriesSelectedBasedOnTextString(List<Serie> List, string textstring)
        {
            var newList = (from x in List
                           where x.Name.Contains(textstring) || x.Description.Contains(textstring)
                           orderby x.GenreIds
                           select x).ToList();
            return newList;
        }

        private List<Serie> SeriesSelectedBasedOnRelease(List<Serie> List, DateTime From, DateTime to)
        {
            var newList = (from x in List
                          where From <= x.ReleaseDatum && x.ReleaseDatum <= to
                          select x).ToList();

            return newList;
        }

        public List<Serie> SeriesBasedOnGenre( List<int> GenreId,SerieContext SC)
        {
            List<Serie> List = new List<Serie>();
            HashSet < Serie > Kortare = new HashSet<Serie>();
            foreach (int Genre in GenreId)
            {
                foreach (Genre item in SC.Genre)
                {
                    if ((GenreCollection)Genre == item.GenreType)
                    {
                        foreach (Serie itemToBeAdded in item.Serie)
                        {
                            Kortare.Add(itemToBeAdded);
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
                List = AddGenre(List, ((GenreCollection)i).ToString());
            }
            return List;
        }
        public List<int> AddGenre(List<int> List, string Genre) {
            if (Request[Genre]!=null)
            {
                List.Add(int.Parse(Request[Genre]));
            }
            return List;
        }
    }
}