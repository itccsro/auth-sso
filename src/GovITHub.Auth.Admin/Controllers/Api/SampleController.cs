using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GovITHub.Auth.Admin.Services;
using GovITHub.Auth.Admin.Models;

namespace GovITHub.Auth.Admin.Controllers.Api
{
    [Route("api/[controller]")]
    public class SampleController : Controller
    {
        private ISampleRepository _context;
        public SampleController(ISampleRepository ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IEnumerable<Sample> GetAll()
        {
            return _context.GetAll();
        }

        [HttpGet("{id}", Name = "GetSample")]
        public IActionResult GetById(string id)
        {
            var item = _context.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Sample item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.Add(item);
            return CreatedAtRoute("GetSample", new { id = item.Key }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Sample item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = _context.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var todo = _context.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Remove(id);
            return new NoContentResult();
        }
    }
}
