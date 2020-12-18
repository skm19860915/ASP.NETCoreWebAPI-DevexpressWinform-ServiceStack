namespace xperters.models
{
    public class SearchFilterView
    {
        public SearchFilterView() { }
        public string FilterType { get; set; }
        public string Values { get; set; }
        public SearchFilterView(string filterType, string values)
        {
            FilterType = filterType;
            Values = values;
        }
    }

}
