namespace API.Helpers
{
    public class Params
    {
        const int MaxPageSize = 50;
        private int _pageSize = 1;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize || value <= 0) ? MaxPageSize : value; }
        }
        private int _pageIndex = 1;
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = (value <= 0) ? 1 : value; }
        }
    }
}