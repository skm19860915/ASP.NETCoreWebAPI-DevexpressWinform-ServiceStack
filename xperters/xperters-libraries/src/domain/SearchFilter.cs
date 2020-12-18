namespace xperters.domain
{
    public class SearchFilter
    {
        public SearchFilter() { }
        public string FilterType { get; set; }
        public string Values { get; set; }
        public SearchFilter(string filterType, string values)//, object[] displayValues)
        {
            FilterType = filterType;
            Values = values;
        }
    }

}

