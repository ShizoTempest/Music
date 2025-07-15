namespace Music.Filters
{
    public abstract class FilterBase<T>
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }

        public abstract IQueryable<T> ApplyFilter(IQueryable<T> query);
    }
}
