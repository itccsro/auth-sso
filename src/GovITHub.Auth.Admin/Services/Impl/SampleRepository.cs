using GovITHub.Auth.Admin.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Admin.Services.Impl
{

    public class SampleRepository : ISampleRepository
    {
        private static ConcurrentDictionary<string, Sample> _items =
             new ConcurrentDictionary<string, Sample>();

        public SampleRepository()
        {
            Add(new Sample { Name = "Item1" });
        }

        public ModelQuery<Sample> GetAll(ModelQueryFilter filter)
        {
            if (filter == null) // get all
                return new ModelQuery<Sample>()
                {
                    List = _items.Values,
                    TotalItems = _items.Count
                };

            var query = _items.Skip(filter.CurrentPage * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage)
                .Select(p => p.Value);

            if(!string.IsNullOrEmpty(filter.SortBy))
            {
                if (filter.SortAscending)
                    query = query.OrderBy(filter.SortBy);
                else
                {
                    query = query.OrderByDescending(filter.SortBy);
                }
            }

            var count = query.Count();

            return new ModelQuery<Sample>()
            {
                List = query.ToList(),
                TotalItems = count
            };
        }

        public void Add(Sample item)
        {
            item.Key = Guid.NewGuid().ToString();
            _items[item.Key] = item;
        }

        public Sample Find(string key)
        {
            Sample item;
            _items.TryGetValue(key, out item);
            return item;
        }

        public Sample Remove(string key)
        {
            Sample item;
            _items.TryRemove(key, out item);
            return item;
        }

        public void Update(Sample item)
        {
            _items[item.Key] = item;
        }
    }
}
