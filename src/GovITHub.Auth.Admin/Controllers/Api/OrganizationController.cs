using GovITHub.Auth.Common.Data;
using GovITHub.Auth.Common.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GovITHub.Auth.Admin.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrganizationController : Controller
    {
        private IOrganizationRepository _repository;

        public OrganizationController(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery]int currentPage, [FromQuery]int itemsPerPage,
            [FromQuery]bool sortAscending, [FromQuery]string sortBy)
        {
            var filter = new ModelQueryFilter(currentPage, itemsPerPage, sortAscending, sortBy);
            return new ObjectResult(_repository.GetAll(filter));
        }

        [HttpGet("{id}", Name = "GetOrganization")]
        public IActionResult GetById(long id)
        {
            var item = _repository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Organization item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _repository.Add(item);
            return CreatedAtRoute("GetOrganization", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Organization item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _repository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _repository.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _repository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _repository.Remove(id);
            return new NoContentResult();
        }


    }
}
