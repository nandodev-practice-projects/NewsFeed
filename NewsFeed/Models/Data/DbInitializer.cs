using System;
using System.Linq;

namespace NewsFeed.Models.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
            {
                new Category{CategoryID = 1001, Name="Art & Culture"},
                new Category{CategoryID = 1002, Name="Entertainment"},
                new Category{CategoryID = 1003, Name="Performing Arts"},
                new Category{CategoryID = 1004, Name="Business Travel"},
                new Category{CategoryID = 1005, Name="Economics"},
                new Category{CategoryID = 1006, Name="Careers"},
                new Category{CategoryID = 1007, Name="Healt Care"},
                new Category{CategoryID = 1008, Name="Sport & Leisure"}
            };
            foreach (Category s in categories)
            {
                context.Categories.Add(s);
            }
            context.SaveChanges();

            var news = new New[]
            {
                new New{
                    Title ="Ancient Israeli Cave Transformed Into Art Gallery.",
                    Content ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean non orci varius, pharetra eros vitae, molestie orci. Integer facilisis quam non magna pellentesque, vitae vestibulum mauris blandit.",
                    AuthorUserName = "user1",
                    CategoryID = 1001,
                    Created = DateTime.Parse("2019-12-03")},
                new New{
                    Title ="The World's Most Expensive Coin Is Up for Sale.",
                    Content ="Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Pellentesque pulvinar porttitor tortor elementum ullamcorper.",
                    AuthorUserName = "user1",
                    CategoryID = 1001,
                    Created = DateTime.Parse("2020-01-03")},
                new New{
                    Title ="Britain's Oldest Example of Christian Graffiti.",
                    Content ="Quisque vitae lorem rutrum, tempor lectus quis, rutrum ante. Ut lobortis tortor ac diam rutrum, eget dignissim purus dapibus.",
                    AuthorUserName = "user1",
                    CategoryID = 1001,
                    Created = DateTime.Parse("2020-02-13")},
                new New{
                    Title ="Maecenas placerat sem nisl.",
                    Content = "Mauris cursus tincidunt justo, id efficitur ligula finibus a. Suspendisse sed mauris enim. Aenean sed fringilla sem, quis vestibulum diam.",
                    AuthorUserName = "user1",
                    CategoryID = 1005,
                    Created = DateTime.Parse("2020-02-21")},
                new New{
                    Title ="U.S. Defense Department reaffirms $10 billion cloud deal to Microsoft.",
                    Content = "Vestibulum mi risus, dapibus eget fermentum nec, eleifend in urna.",
                    AuthorUserName = "user1",
                    CategoryID = 1005,
                    Created = DateTime.Parse("2020-03-10")},
                new New{
                    Title ="U.S. weighs export controls on China’s top chip maker",
                    Content ="Duis cursus vel ipsum aliquet consectetur. Pellentesque ac euismod elit, nec sagittis odio. Pellentesque fermentum hendrerit ligula, non venenatis nulla congue eu. Suspendisse ut dolor sem.",
                    AuthorUserName = "user1",
                    CategoryID = 1005,
                    Created = DateTime.Parse("2020-04-25")},
                new New{
                    Title ="Most Businesses Were Unprepared for Covid-19. Domino’s Delivered.",
                    Content ="Nullam sed arcu in mauris interdum cursus sit amet in sem. Pellentesque quis lacus hendrerit, pulvinar arcu sit amet, placerat ex. Vivamus vel egestas felis.",
                    AuthorUserName = "user1",
                    CategoryID = 1005,
                    Created = DateTime.Parse("2020-05-18")},
                  new New{
                    Title ="United Adds October Capacity.",
                    Content ="Nullam sed arcu in mauris interdum cursus sit amet in sem. Pellentesque quis lacus hendrerit, pulvinar arcu sit amet, placerat ex. Vivamus vel egestas felis.",
                    AuthorUserName = "user1",
                    CategoryID = 1004,
                    Created = DateTime.Parse("2020-03-01")},
                  new New{
                    Title = "Air Canada Partners for Study to Test Quarantine Policy.",
                    Content ="Nullam sed arcu in mauris interdum cursus sit amet in sem. Pellentesque quis lacus hendrerit, pulvinar arcu sit amet, placerat ex. Vivamus vel egestas felis.",
                    AuthorUserName = "user1",
                    CategoryID = 1004,
                    Created = DateTime.Parse("2020-09-05")},
                  new New{
                    Title = "Cadmium levels found to be four times higher in waste pickers.",
                    Content ="Nullam sed arcu in mauris interdum cursus sit amet in sem. Pellentesque quis lacus hendrerit, pulvinar arcu sit amet, placerat ex. Vivamus vel egestas felis.",
                    AuthorUserName = "user1",
                    CategoryID = 1007,
                    Created = DateTime.Parse("2020-08-20")},
                  new New{
                    Title ="As threat of valley fever grows beyond the Southwest, push is on for vaccine.",
                    Content ="Nullam sed arcu in mauris interdum cursus sit amet in sem. Pellentesque quis lacus hendrerit, pulvinar arcu sit amet, placerat ex. Vivamus vel egestas felis.",
                    AuthorUserName = "user1",
                    CategoryID = 1007,
                    Created = DateTime.Parse("2020-01-18")},
            };

            foreach (New c in news)
            {
                context.News.Add(c);
            }
            context.SaveChanges();

            var subscriptions = new Subscription[]
            {
                new Subscription{Subscriber="user1", CategoryID=1005, Created=DateTime.Now},
                new Subscription{Subscriber="user1", CategoryID=1001, Created=DateTime.Now},
                new Subscription{Subscriber="user2", CategoryID=1005, Created=DateTime.Now},
                new Subscription{Subscriber="user2", CategoryID=1001, Created=DateTime.Now}
            };

            foreach (Subscription s in subscriptions)
            {
                context.Subscriptions.Add(s);
            }

            context.SaveChanges();
        }
    }
}
