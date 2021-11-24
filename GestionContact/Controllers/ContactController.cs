using AutoMapper;
using GestionContact.Helpers;
using GestionContact.Models;
using GestionContact.ParametersModels;
using GestionContactEF.Core.Interfaces;
using GestionContactEF.Dal.Models;
using GestionContactEF.Dal.ViewModels;
using MailService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace GestionContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthRequired]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _repo;
        private readonly IAdresseRepository _repoAdresse;
        private readonly IMapper _mapper;
        private readonly IEmailService _repoMail;
        private readonly IImageRepository _imageRepo;

        public ContactController(IContactRepository repo, IMapper mapper, IAdresseRepository repoAdresse, IEmailService repoMail, IImageRepository imageRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _repoAdresse = repoAdresse;
            _repoMail = repoMail;
            _imageRepo = imageRepo;
        }
        // GET: api/Contact
        [HttpGet("{userId}")]
        //documentation pour swagger
        [Produces("application/json", Type = typeof(IEnumerable<ContactApi>))]
        public async Task<IActionResult> Get([FromQuery] GetContactParameters parameters)
        {
            try
            {
                IEnumerable<Contact> entity = await _repo.Get(parameters.Firstname, parameters.Lastname);
                if (entity == null)
                {
                    return NotFound();
                }
                IEnumerable<ContactApi> entityApis = _mapper.Map<IEnumerable<ContactApi>>(entity);

                foreach (ContactApi e in entityApis)
                {
                    e.ImageUri = "/api/image/" + e.ImageId;
                }

                return Ok(entityApis);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return NotFound();
        }

        // GET api/Contact/5
        [HttpGet("{id}/{userId}")]
        //documentation pour swagger
        [Produces("application/json", Type = typeof(ContactApi))]
        public async Task<IActionResult> Get(int id, [FromQuery] GetContactParameters parameters)
        {
            try
            {
                Contact tmpentity = await _repo.GetOne(id, parameters.Email);

                if (tmpentity == null)
                {
                    return NotFound();
                }
                ContactApi tmpentityApi = _mapper.Map<ContactApi>(tmpentity);
                tmpentityApi.ImageUri = "/api/image/" + tmpentityApi.ImageId;
                
                return Ok(tmpentityApi);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return NotFound();
        }

        // POST api/Contact
        [HttpPost]
        //documentation pour swagger
        [Produces("application/json", Type = null)]
        public async Task<IActionResult> Post([FromBody] ContactApi value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Contact tmpentity = _mapper.Map<Contact>(value);

            if (tmpentity == null)
            {
                return BadRequest();
            }
            try
            {
                if (await _repo.AlreadyExists(tmpentity.Id))
                {
                    ModelState.AddModelError("le contact existe déjà.", "un contact avec le même email existe déjà");
                    return BadRequest(ModelState);
                }
                else
                {
                    if(await _repoAdresse.AlreadyExists(value.Rue, value.Ville, value.CP, value.Numero, value.Boite))
                    {
                        Adresse ad = await _repoAdresse.GetOne(value.Rue, value.Ville, value.CP, value.Numero, value.Boite);
                        if (value.MimeType != "" && value.File != null)
                        {
                            Image img = new Image();
                            img.MimeType = value.MimeType;
                            img.File = value.File;
                            Image i = await _imageRepo.Post(img);
                            tmpentity.ImageId = i.Id;
                        }
                        tmpentity.AdresseId = ad.Id;
                        await _repo.Post(tmpentity);
                        //EmailMessage email = new EmailMessage();
                        //    email.Body = "vous venez d'être inscris à la salle de sport";
                        //    email.Subject = "inscription à la salle de sport";
                        //    email.Body = "bonjour";
                        //    email.ToEmail = "yolegrand@hotmail.com";
                        //    await _repoMail.SendEmailAsync(email);

                        if (value.MimeType != "" && value.File != null)
                        {
                            Image img = new Image();
                            img.MimeType = value.MimeType;
                            img.File = value.File;
                            Image i = await _imageRepo.Post(img);
                        }
                    }
                    else
                    {
                        Adresse add = new Adresse
                        {
                            Rue = value.Rue,
                            Ville = value.Ville,
                            Numero = value.Numero,
                            CP = value.CP,
                            Boite = value.Boite
                        };
                        Adresse adresseCreate = await _repoAdresse.Post(add);
                        if (value.MimeType != "" && value.File != null)
                        {
                            Image img = new Image();
                            img.MimeType = value.MimeType;
                            img.File = value.File;
                            Image i = await _imageRepo.Post(img);
                            tmpentity.ImageId = i.Id;
                        }
                        tmpentity.AdresseId = adresseCreate.Id;
                        await _repo.Post(tmpentity);
                        //EmailMessage email = new EmailMessage();
                        //    email.Body = "vous venez d'être inscris à la salle de sport";
                        //    email.Subject = "inscription à la salle de sport";
                        //    email.Body = "bonjour";
                        //    email.ToEmail = "yolegrand@hotmail.com";
                        //    await _repoMail.SendEmailAsync(email);
                        
                    }
                    
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
            return NoContent();
        }

        // PUT api/Contact/5
        [HttpPut("{id}")]
        [Produces("application/json", Type = null)]
        public async Task<IActionResult> Put(int id, [FromBody] ContactApi value)
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
                Contact entity = _mapper.Map<Contact>(value);
                Adresse add = new Adresse
                {
                    Id = value.AdresseId,
                    Rue = value.Rue,
                    Ville = value.Ville,
                    Numero = value.Numero,
                    CP = value.CP,
                    Boite = value.Boite
                };
                await _repoAdresse.Put(value.AdresseId, add);
                await _repo.Put(id, entity);
            }
            catch
            {
                throw;
            }
            return NoContent();
        }

        // DELETE api/Contact/5
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

        [HttpGet]
        [Route("GetPagination")]
        public async Task<IActionResult> GetPagination([FromQuery] Parameters productParameters)
        {
            var contacts = await _repo.GetContacts(productParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(contacts.MetaData));
            return Ok(contacts);
        }
    }
}