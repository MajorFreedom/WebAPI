using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Pagination
{
    public class PagingParameters
    {
        private int maxPageSize = 15;

        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? maxPageSize : value; }
        }

        public int PageNumber { get; set; } = 1;


    }
}
