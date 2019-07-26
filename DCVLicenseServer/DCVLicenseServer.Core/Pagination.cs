using System;
using System.Collections.Generic;
using System.Text;

namespace DCVLicenseServer.Core
{
    public class Pagination
    {
        public Pagination() { }

        public Pagination(int page, int pre_page, int total)
        {
            pre_page = Math.Max(1, pre_page);

            current = page;
            per_page = pre_page;
            this.total = total;
            pages = (int)Math.Ceiling(((decimal)total / (decimal)pre_page));
            next = Math.Min(page + 1, this.pages);
            previous = Math.Max(page - 1, 0);
        }

        public int previous = 0;
        public int next = 2;
        public int current = 1;
        public int per_page = 20;
        public int total = 0;
        public int pages = 1;
    }
}
