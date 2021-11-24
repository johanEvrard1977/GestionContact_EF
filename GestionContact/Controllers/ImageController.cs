using AutoMapper;
using GestionContact.Models;
using GestionContactEF.Core.Interfaces;
using GestionContactEF.Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _repo;
        private readonly IMapper _mapper;

        public ImageController(IImageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: api/Image
        [HttpGet]
        //documentation pour swagger
        [Produces("application/json", Type = typeof(IEnumerable<ImageApi>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Image> entity = await _repo.Get();
                if (entity == null)
                {
                    return NotFound();
                }
                IEnumerable<ImageApi> entityApis = entity.Select(e => new ImageApi
                {
                    Id = e.Id,
                    ImageUri = "/api/Image/" + e.Id,
                    MimeType = e.MimeType,
                    File = e.File
                });

                return Ok(entityApis);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return NotFound();
        }

        // GET api/Image/5
        [HttpGet("{id}")]
        //documentation pour swagger
        [Produces("application/json", Type = typeof(ImageApi))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Image tmpentity = await _repo.GetOne(id);

                if (tmpentity == null)
                {
                    return NotFound();
                }

                //ImageApi tmpentityApi = _mapper.Map<ImageApi>(tmpentity);

                return File(tmpentity.File, tmpentity.MimeType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return NotFound();
        }

        // POST api/Image
        [HttpPost]
        //documentation pour swagger
        [Produces("application/json", Type = null)]
        public async Task<IActionResult> Post([FromBody] ImageApi value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Image tmpentity = _mapper.Map<Image>(value);

            if (tmpentity == null)
            {
                return BadRequest();
            }
            try
            {
                    await _repo.Post(tmpentity);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
            return NoContent();
        }

        // PUT api/Image/5
        [HttpPut("{id}")]
        [Produces("application/json", Type = null)]
        public async Task<IActionResult> Put(int id, [FromBody] ImageApi value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != value.Id || value == null)
            {
                return BadRequest();
            }

            try
            {
                Image entity = _mapper.Map<Image>(value);
                await _repo.Put(id, entity);
            }
            catch
            {
                throw;
            }
            return NoContent();
        }

        // DELETE api/Image/5
        [HttpDelete("{id}")]
        [Produces("application/json", Type = null)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repo.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}