using KBC.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KBC.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Delete(int id)
        {
            SerieContext SC = new SerieContext();
            Serie theSerie;
            if (Request["Radera"] ==null)
            {
                theSerie = SC.Serie.ToList().Where(s => s.SerieId == id).First();
                return View(theSerie);
            }
            else
            {
                int number = int.Parse(Request["Radera"]);
                theSerie = SC.Serie.ToList().Where(s => s.SerieId == number).First();
                SC.Serie.Remove(theSerie);
                SC.SaveChanges();
                return View("SearchResult",SC.Serie.ToList());
            }
          
           
        }
        [HttpPost]
        public ActionResult Edit(Serie S)
        {
            SerieContext SC = new SerieContext();
            foreach (var item in SC.Serie)
            {
                if(item.SerieId==S.SerieId)
                {
                    item.Name = S.Name;
                    item.NumberOfVotes = S.NumberOfVotes;
                    item.ReleaseDatum = S.ReleaseDatum;
                    item.Creator = S.Creator;
                    item.Description = S.Description;
                    item.AverageGrade = S.AverageGrade;
                }
            }
            SC.SaveChanges();
            return View("SearchResult", SC.Serie.ToList());
        }
        public ActionResult Edit(int id )
        {
            SerieContext SC = new SerieContext();
            var theSerie = SC.Serie.ToList().Where(s => s.SerieId == id).First();
            return View(theSerie);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Serie S)
        {
            SerieContext SC = new SerieContext();
            SC.Serie.Add(S);
            SC.SaveChanges();

            return RedirectToAction("SearchResult");
        }
        // GET: Search
        public ActionResult SearchResult()
        {
            #region Setting Up Parameters
            IList<Serie> ResultList = new List<Serie>();
            string textstring = Request["Search"];
            DateTime From;
            DateTime.TryParse(Request["From"], out From);
            DateTime To;
            DateTime.TryParse(Request["To"], out To);
            double Grade;
            if (Request["Grade"] != null)
            {
                var culture = CultureInfo.InvariantCulture;
                Grade = double.Parse(Request["Grade"], culture);
            }
            else
            {
                Grade = 0;
            }
            List<int> Genres = new List<int>();
            Genres = AddGenres(Genres);
            List<GenreType> gg = GetGenres(Genres);
            #endregion

            using (SerieContext SC = new SerieContext())
            {

                //ResultList = SeriesBasedOnGenre(gg, SC);
                ResultList = CallesMetod(gg);
                ResultList = SeriesSelectedBasedOnRelease(ResultList, From, To, SC);
                ResultList = SeriesSelectedBasedOnGrade(ResultList, Grade, SC);
                ResultList = SeriesSelectedBasedOnTextString(ResultList, textstring, SC);

                ResultList = ImgListAdded(ResultList, SC);
            }

            return View(ResultList);
        }
        public ActionResult GenreSelection(int GenreID)
        {
            IList<Serie> ResultList = new List<Serie>();
            ResultList = CallesMetod(GetGenres(new List<int>() { GenreID }));


            return View("SearchResult", ResultList);
        }

        private IList<Serie> ImgListAdded(IList<Serie> List, SerieContext SC)
        {
            //Om Listan är skild från 0 eller null lägg till bilderna
            if (!(List.Count == 0) || (List == null))
            {

                foreach (var item in List)
                {

                    item.SerieImgsURL = (from x in item.SerieImgsURL
                                         where x.SerieId == item.SerieId
                                         select x).ToList();


                }
                //List = SC.Serie.ToList();
            }

            return List;
        }
        #region Sökmetoder
        private IList<Serie> SeriesSelectedBasedOnGrade(IList<Serie> List, double v, SerieContext SC)
        {
            IList<Serie> newList;
            if ((List.Count == 0) || (List == null))
            {
                List = SC.Serie.ToList();
            }
            newList = (from x in List
                       where x.AverageGrade >= v
                       select x).ToList();
            return newList;
        }

        private IList<Serie> SeriesSelectedBasedOnTextString(IList<Serie> List, string textstring, SerieContext SC)
        {
            IList<Serie> newList;
            if ((List.Count == 0) || (List == null))
            {
                List = SC.Serie.ToList();
            }
            if (textstring != null)
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

        private IList<Serie> SeriesSelectedBasedOnRelease(IList<Serie> List, DateTime From, DateTime to, SerieContext SC)
        {
            IList<Serie> newList;
            if ((List.Count == 0) || (List == null))
            {
                List = SC.Serie.ToList();
            }
            DateTime D = new DateTime(1800, 1, 1, 0, 0, 0);
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
        List<Serie> CallesMetod(List<GenreType> genres)
        {
            var animated = Request["Animated"];
            var action = Request["Action"];
            var fantasy = Request["Fantasy"];

            var db = new SerieContext();

            var series = new List<Serie>();

            foreach (var serie in db.Serie.ToList())
            {
                foreach (var genre in genres)
                {


                    if (serie.Genres.Any(g => g.Genre == genre))
                    {
                        if (!series.Any(s => s.SerieId == serie.SerieId))
                        {
                            series.Add(serie);
                        }
                    }
                }
            }





            return series;
        }
        #endregion

        #region Gammal Metod
        public IList<Serie> SeriesBasedOnGenre(IList<GenreType> genres, SerieContext SC)
        {
            //var series = new List<Serie>();

            //var series = SC.Serie.Where(s => s.Genres.Where(g => g.Genre == ))

            //foreach (var serie in SC.Serie.ToList())
            //{
            //    foreach (var genre in genres)
            //    {
            //        if (serie.Genres.Any(g => g.Genre == genre))
            //        {
            //            series.Add(serie);
            //        }
            //    }                
            //}



            IList<Serie> List = new List<Serie>();
            HashSet<Serie> Kortare = new HashSet<Serie>();

            foreach (var Genre in genres)
            {
                foreach (var item in SC.Genre)
                {
                    if ((GenreType)Genre == item.Genre)
                    {
                        using (SerieContext Sc = new SerieContext())
                        {
                            var s = (from x in Sc.Serie.ToList()
                                     where x.Genres.Contains(item)
                                     select x).ToList();
                            if (!(s == null))
                            {
                                if ((List.Count == 0 || List == null))
                                {
                                    List = s;
                                }
                                else
                                {
                                    List.Union(s);
                                }
                            }


                            //if (!Kortare.Any(s=>s.SerieId)
                            //{ Kortare.Add(s); }
                        }
                    }
                }
            }
            //foreach (var item in Kortare)
            //{
            //    var s = (from x in SC.Serie
            //             where x.SerieId == item.SerieId
            //             select x).First();
            //    List.Add(s);
            //}
            //List = Kortare.ToList();
            return List;
        }
        #endregion

        #region Genre methods
        public List<GenreType> GetGenres(List<int> i)
        {
            List<GenreType> genres = new List<GenreType>();
            foreach (var item in i)
            {
                genres.Add((GenreType)item);
            }
            return genres;

        }
        public List<int> AddGenres(List<int> List)
        {
            for (int i = 0; i < 8; i++)
            {
                List = AddGenre(List, i);
            }
            return List;
        }
        public List<int> AddGenre(List<int> List, int Genre)
        {

            if (Request[((GenreType)Genre).ToString()] != null)
            {
                List.Add(Genre);
            }
            return List;
        }

        #endregion




    }
}