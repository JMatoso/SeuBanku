﻿namespace SeuBanku.Helpers.Paging
{
    public class PagedList<TEntity> : List<TEntity>
    {
        public Metadata Metadata { get; set; }
        public PagedList(List<TEntity> items, int count, int pageNumber, int pageSize)
        {
            Metadata = new Metadata
            {
                Count = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                Total = (int)Math.Ceiling(count / (double)pageSize)
            };

            AddRange(items);
        }

        public static PagedList<TEntity> ToPagedList(IEnumerable<TEntity> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<TEntity>(items, count, pageNumber, pageSize);
        }
    }
}
