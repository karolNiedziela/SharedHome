﻿namespace SharedHome.Application.Common.Queries
{
    public abstract class PagedBase
    {
        public int CurrentPage { get; set; }

        // Number of elements per page
        public int PageSize { get; set; }

        public int TotalPages { get; set; }
        
        public int TotalItems { get; set; }

        public int? CustomTotalItems { get; set; }

        protected PagedBase()
        {

        }

        protected PagedBase(int currentPage, int pageSize, int totalPages,
            int totalItems, int? customTotalItems = null)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalItems = totalItems;
            CustomTotalItems = customTotalItems;
        }
    }
}