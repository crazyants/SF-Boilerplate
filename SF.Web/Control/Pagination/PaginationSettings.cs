

namespace SF.Web.Control.Pagination
{
    
    public class PaginationSettings
    {
        public int TotalItems { get; set; } = 0;
       
        public int ItemsPerPage { get; set; } = 10;
        
        public int CurrentPage { get; set; } = 1;
        
        public int MaxPagerItems { get; set; } = 10;

        public bool ShowFirstLast { get; set; } = false;

        public bool ShowNumbered { get; set; } = true;

        public bool UseReverseIncrement { get; set; } = false;

        public bool SuppressEmptyNextPrev { get; set; } = false;

        public bool SuppressInActiveFirstLast { get; set; } = false;

    }

}
