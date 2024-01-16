using System;

namespace AutoGlass.Application.Results
{
    public class RequestResult
    {
        public int StatusCode { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
        public PaginationInfo Pagination { get; private set; }

        public RequestResult Ok(object data, PaginationInfo pagination = null)
        {
            this.StatusCode = 200;
            this.Message = "Success";
            this.Data = data;
            this.Pagination = pagination;
            return this;
        }

        public RequestResult BadRequest(string detail, object data = null)
        {
            this.StatusCode = 400;
            this.Message = $"Bad Request: {detail}";
            this.Data = data;
            return this;
        }

        public class PaginationInfo
        {
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }

            public PaginationInfo(int currentPage, int pageSize, int totalCount)
            {
                CurrentPage = currentPage;
                PageSize = pageSize;
                TotalCount = totalCount;
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            }
        }
    }
}
