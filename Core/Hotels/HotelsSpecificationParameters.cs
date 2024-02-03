namespace Core.Hotels
{
    public class HotelsSpecificationParameters
    {
        public int PageIndex { get; set; } = 1;
        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
