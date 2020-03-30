using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mingxiaoyu.Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IRepository<Blog> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BlogsController(IRepository<Blog> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var list = _repository.Table.ToList().Select(x => x.Url).ToArray();

            return list;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(Guid id)
        {
            var entity = _repository.Table.FirstOrDefault(x => x.Id == id);
            return entity.Url;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            var user = _repository.Insert(new Blog() { Url = value });
            _unitOfWork.Commit();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] string value)
        {
            var entity = _repository.Table.FirstOrDefault(x => x.Id == id);
            entity.Url = value;
            _repository.Update(entity);
            _unitOfWork.Commit();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var entity = _repository.Table.FirstOrDefault(x => x.Id == id);
            _repository.Delete(entity);
            _unitOfWork.Commit();
        }
    }
}
