using API.Context;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        MyContext myContext;

        public JobsController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var dataGet = myContext.Jobs.ToList();
            if (dataGet.Count == 0)
            {
                return Ok(new { message = "Sukses mengambil data", statusCode = 200, data = "null" });
            }
            return Ok(new { message = "Sukses mengambil data", statusCode = 200, data = dataGet });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dataGet = myContext.Jobs.Find(id);
            if (dataGet == null)
            {
                return Ok(new { message = "Sukses mengambil data", statusCode = 200, data = "null" });
            }
            return Ok(new { message = "Sukses mengambil data", statusCode = 200, data = dataGet });
        }

        [HttpPut]
        public IActionResult Put(Jobs jobs)
        {
            var dataPut = myContext.Jobs.Find(jobs.JobId);
            dataPut.JobName = jobs.JobName;
            myContext.Jobs.Update(dataPut);
            var resultPut = myContext.SaveChanges();
            if (resultPut > 0)
            {
                return Ok(new { message = "Sukses memperbarui data", statusCode = 200 });
            }
            return BadRequest(new { message = "Gagal memperbarui data", statusCode = 400});
        }

        [HttpPost]
        public IActionResult Post(Jobs jobs)
        {
            myContext.Jobs.Add(jobs);
            var resultPost = myContext.SaveChanges();
            if (resultPost > 0)
            {
                return Ok(new { message = "Sukses menambahkan data", statusCode = 200 });
            }
            return BadRequest(new { message = "Gagal menambahkan data", statusCode = 400 });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dataDelete = myContext.Jobs.Find(id);
            myContext.Jobs.Remove(dataDelete);
            var resultDelete = myContext.SaveChanges();
            if (resultDelete > 0)
            {
                return Ok(new { message = "Sukses menghapus data", statusCode = 200 });
            }
            return BadRequest(new { message = "Gagal menghapus data", statusCode = 400 });
        }
    }
}
