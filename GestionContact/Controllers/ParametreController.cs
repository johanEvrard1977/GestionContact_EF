using AutoMapper;
using GestionContact.Models;
using GestionContactEF.Core.Interfaces;
using GestionContactEF.Dal.ViewModels;
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
    public class ParametreController : ControllerBase
    {
        private readonly IParametreRepository _repo;
        private readonly IMapper _mapper;
        public ParametreController(IParametreRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: api/Occupation
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Parameters> typePictures = await _repo.Get();
            IEnumerable<ParametreApi> typePicturesApi = _mapper.Map<IEnumerable<Parameters>, IEnumerable<ParametreApi>>(typePictures);
            if (typePicturesApi == null)
            {
                return NotFound();
            }
            return Ok(typePicturesApi);
        }

        // GET api/Occupation/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Parameters tmpTypePicture = await _repo.GetOne(id);
            ParametreApi tmpTypePictureApi = _mapper.Map<Parameters, ParametreApi>(tmpTypePicture);
            if (tmpTypePictureApi == null)
            {
                return NotFound();
            }
            return Ok(tmpTypePictureApi);
        }

        // POST api/Parametre
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ParametreApi value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Parameters tmpTypePicture = _mapper.Map<ParametreApi, Parameters>(value);
            Parameters createdtmpTypePicture;
            try
            {createdtmpTypePicture = await _repo.Post(tmpTypePicture);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return BadRequest();
            }
            ParametreApi resultTPApi = _mapper.Map<Parameters, ParametreApi>(createdtmpTypePicture);
            if (tmpTypePicture == null)
            {
                return BadRequest();
            }
            return Ok(resultTPApi);
        }

        // PUT api/Parametre/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ParametreApi value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Parameters typepicture = _mapper.Map<ParametreApi, Parameters>(value);
            

            try
            {
                await _repo.Put(id, typepicture);
                return Ok(typepicture);
            }
            catch
            {
                if (!ParametreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE api/Parametre/5
        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] ParametreApi value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Parameters typepicture = _mapper.Map<ParametreApi, Parameters>(value);
            
            try
            {await _repo.Put(id, typepicture);
                return Ok(typepicture);
            }
            catch
            {
                if (!ParametreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

       
        private bool ParametreExists(int id)
        {
            return _repo.GetOne(id) != null;
        }
    }
}