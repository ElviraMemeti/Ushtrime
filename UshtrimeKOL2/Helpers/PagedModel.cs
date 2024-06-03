namespace SOA2024.MovieReview.API.Helpers
{
    public class PagedModel<TModel>
    {
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public List<TModel> Items { get; set; }

        public PagedModel()
        {
            Items = new List<TModel>();
        }
    }
}
