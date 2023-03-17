using AutoMapper;
using bbit_2_uzd.Controllers.Extensions;
using bbit_2_uzd.Models;
using bbit_2_uzd.Models.DTO;
using bbit_2_uzd.Services.Communication;
using bbit_2_uzd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bbit_2_uzd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        public ApartmentController(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }

        // GET: api/Dzivokli
        [HttpGet("GetAll")]
        public async Task<IEnumerable<ApartmentGetDTO>> GetAllApartments(Guid? house_id)
        {
            if (house_id != null)
            {
                var apartmentsRaw = await _apartmentService.GetApartmentsFromHouse((Guid)house_id);

                var apartments = _mapper.Map<IEnumerable<Apartment>, IEnumerable<ApartmentGetDTO>>(apartmentsRaw);

                return apartments;
            }
            var allApartmentsRaw = await _apartmentService.GetAllApartments();

            var allApartments = _mapper.Map<IEnumerable<Apartment>, IEnumerable<ApartmentGetDTO>>(allApartmentsRaw);

            return allApartments;
        }

        // GET: api/Dzivokli/5
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<ApartmentGetDTO>> GetApartment(Guid id)
        {
            ApartmentResponse response = await _apartmentService.GetApartment(id);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            ApartmentGetDTO apartment = _mapper.Map<Apartment, ApartmentGetDTO>(response.Resource);
            return Ok(apartment);
        }

        // PUT: api/Dzivokli/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutApartment(Guid id, ApartmentModifyDTO apartment)
        {
            Apartment updatedApartment;
            try
            {
                updatedApartment = _mapper.Map<ApartmentModifyDTO, Apartment>(apartment);
            }
            catch (AutoMapperMappingException e)
            {
                return this.ResolveError(HttpStatusCode.BadRequest, $"Some of the provided values are invalid: {e.Message}");
            }
            catch (Exception e)
            {
                return this.ResolveError(HttpStatusCode.InternalServerError, $"There was a problem while updating the apartment: {e.Message}");
            }

            ApartmentResponse response = await _apartmentService.PutApartment(id, updatedApartment);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            var apartmentFinal = _mapper.Map<Apartment, ApartmentGetDTO>(response.Resource);
            return Ok(apartmentFinal);
        }

        // POST: api/Dzivokli
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Create")]
        public async Task<ActionResult<ApartmentModifyDTO>> PostApartment(ApartmentModifyDTO apartment)
        {
            Apartment newApartment;
            try
            {
                newApartment = _mapper.Map<ApartmentModifyDTO, Apartment>(apartment);
            }
            catch (AutoMapperMappingException e)
            {
                return this.ResolveError(HttpStatusCode.BadRequest, $"Some of the provided values are invalid: {e.Message}");
            }
            catch (Exception e)
            {
                return this.ResolveError(HttpStatusCode.InternalServerError, $"There was a problem while creating the apartment: {e.Message}");
            }

            ApartmentResponse response = await _apartmentService.PostApartment(newApartment);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            ApartmentGetDTO apartmentFinal = _mapper.Map<Apartment, ApartmentGetDTO>(response.Resource);

            //return Ok(apartmentFinal);
            return CreatedAtAction("GetApartment", new { id = response.Resource.Id }, apartmentFinal);
        }

        // DELETE: api/Dzivokli/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteApartment(Guid id)
        {
            ApartmentResponse response = await _apartmentService.DeleteApartment(id);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            return NoContent();
        }
    }
}