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

        public IEnumerable<Sample> GetAll()
        {
            return _items.Values;
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
