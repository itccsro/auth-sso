using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovITHub.Auth.Admin.Models;
using System.Collections.Concurrent;

namespace GovITHub.Auth.Admin.Services
{
    public class Organization
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
    }
    public interface IOrganizationRepository
    {
        Organization Find(long id);
        void Update(Organization item);
        Organization Remove(long id);
        void Add(Organization item);
        IEnumerable<Organization> GetAll(ModelQueryFilter filter);
    }

    public class OrganizationRepository : IOrganizationRepository
    {
        private static ConcurrentDictionary<long, Organization> _items =
             new ConcurrentDictionary<long, Organization>();

        public OrganizationRepository()
        {
            Add(new Organization() { Id = 0, Name = "GovITHub" });
        }

        public ModelQuery<Organization> GetAll(ModelQueryFilter filter)
        {
            if (filter == null) // get all
                return new ModelQuery<Organization>()
                {
                    List = _items.Values,
                    TotalItems = _items.Count
                };

            var query = _items.Skip(filter.CurrentPage * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage)
                .Select(p => p.Value);

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                if (filter.SortAscending)
                    query = query.OrderBy(filter.SortBy);
                else
                {
                    query = query.OrderByDescending(filter.SortBy);
                }
            }

            var count = query.Count();

            return new ModelQuery<Organization>()
            {
                List = query.ToList(),
                TotalItems = count
            };
        }

        public void Add(Organization item)
        {
            item.Id = 1;
            _items[item.Id] = item;
        }

        public Organization Find(long id)
        {
            Organization item;
            _items.TryGetValue(id, out item);
            return item;
        }

        public Organization Remove(long id)
        {
            Organization item;
            _items.TryRemove(id, out item);
            return item;
        }

        public void Update(Organization item)
        {
            _items[item.Id] = item;
        }

        IEnumerable<Organization> IOrganizationRepository.GetAll(ModelQueryFilter filter)
        {
            return _items.Values;
        }
    }

}
