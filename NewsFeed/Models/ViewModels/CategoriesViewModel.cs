namespace NewsFeed.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public bool CurrentUserSubscribed { get; set; }
    }
}
