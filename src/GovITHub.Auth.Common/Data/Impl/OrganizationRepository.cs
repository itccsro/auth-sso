using GovITHub.Auth.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GovITHub.Auth.Common.Data.Impl
{
    public class OrganizationRepository : IOrganizationRepository, IDisposable
    {
        ApplicationDbContext _dbContext;
        public OrganizationRepository(ApplicationDbContext dbContext){
            _dbContext = dbContext; 
        }

        public ModelQuery<Organization> GetAll(ModelQueryFilter filter)
        {
            if (filter == null) // get all
                return new ModelQuery<Organization>()
                {
                    List = _dbContext.Organizations,
                    TotalItems = _dbContext.Organizations.Count()
                };

            var query = _dbContext.Organizations.Skip(filter.CurrentPage * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage)
                .Select(p => p);

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
        }

        public Organization Find(long id)
        {
            Organization item = _dbContext.Organizations.FirstOrDefault(t => t.Id == id);
            return item;
        }

        public Organization Remove(long id)
        {
            Organization item = _dbContext.Organizations.FirstOrDefault(t => t.Id == id);
            if (item != null)
            {
                _dbContext.Organizations.Remove(item);
                _dbContext.SaveChanges();
            }
            return item;
        }

        public void Update(Organization item)
        {
            Organization itemDb = _dbContext.Organizations.FirstOrDefault(t => t.Id == item.Id);
            if (item != null)
            {
                itemDb.Name = item.Name;
                _dbContext.SaveChanges();
            }
        }

        #region IDisposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}
