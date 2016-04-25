namespace KBC.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KBC.Models.SerieContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KBC.Models.SerieContext context)
        {
            //  This method will be called after migrating to the latest version.

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

            //for (int i = 0; i < 8; i++)
            //{
            //    context.Genre.AddOrUpdate(new Genre() { GenreType = (GenreCollection)i, SerieIds = null, Serie = null });
            //}


            context.Serie.AddOrUpdate(
            new Serie()
            {
                SerieId=1001,
                Name = "Game of Thrones",
                ReleaseDatum = new DateTime(2008, 3, 13, 8, 0, 0),
                AverageGrade = 5,
                NumberOfVotes = 100,

                Description = "While a civil war brews between several noble families in Westeros,"
                + "the children of the former rulers of the land attempt to rise up to power."
                + "Meanwhile a forgotten race, bent on destruction, return after thousands of years in the North."

            },
            new Serie()
            {
                SerieId = 1002,
                Name = "How I met your mother",
                ReleaseDatum = new DateTime(2005, 5, 24, 8, 0, 0),
                AverageGrade = 5,
                NumberOfVotes = 100,
                
                Description = "How I Met Your Mother is a comedy about Ted (Josh Radnor) "
                + "and how he fell in love. It all starts when Ted's best friend, Marshall "
                + "(Jason Segel), drops the bombshell that he's going to propose to his long-time "
                + "girlfriend, Lily (Alyson Hannigan), a kindergarten teacher. At that moment, "
                + "Ted realizes that he had better get a move on if he too hopes to find true love."
                + " Helping him in his quest is Barney (Neil Patrick Harris), a friend with endless, "
                + "sometimes outrageous opinions, a penchant for suits and a foolproof way to meet women. "
                + "When Ted meets Robin (Cobie Smulders), he's sure it's love at first sight, but destiny may "
                + "have something else in store. The series is narrated through flashbacks from the future, "
                + "voiced by Bob Saget. The theme song is \"Hey Beautiful\" by The Solids."
            });
            SerieContext.SetUpGenres(new List<int>() { 0, 1, 2, 3, 4 }, 1001, context);
            SerieContext.SetUpGenres(new List<int>() { 0, 1, 2, 3, 4 }, 1002, context);
        }
        
    }
}
