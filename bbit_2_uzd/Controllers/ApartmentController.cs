﻿using AutoMapper;
using bbit_2_uzd.Controllers.Extensions;
using bbit_2_uzd.Models;
using bbit_2_uzd.Models.DTO;
using bbit_2_uzd.Services.Communication;
using bbit_2_uzd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bbit_2_uzd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ApiScope")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        public ApartmentController(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<ApartmentGetDTO>> GetAllApartments(Guid? house_id, Guid? tenant_id)
        {
            if (house_id != null && tenant_id == null)
            {
                var apartmentsRaw = await _apartmentService.GetApartmentsFromHouse((Guid)house_id);

                var apartments = _mapper.Map<IEnumerable<Apartment>, IEnumerable<ApartmentGetDTO>>(apartmentsRaw);

                return apartments;
            }
            else if (house_id != null && tenant_id != null)
            {
                var tenantApartmentsRaw = await _apartmentService.GetTenantApartmentsFromHouse((Guid)house_id, (Guid)tenant_id);

                var tenantApartments = _mapper.Map<IEnumerable<Apartment>, IEnumerable<ApartmentGetDTO>>(tenantApartmentsRaw);

                return tenantApartments;
            }
            var allApartmentsRaw = await _apartmentService.GetAllApartments();

            var allApartments = _mapper.Map<IEnumerable<Apartment>, IEnumerable<ApartmentGetDTO>>(allApartmentsRaw);

            return allApartments;
        }

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

        [HttpPut("Update/{id}")]
        [Authorize(Policy = "RequireManagerPrivileges")]
        public async Task<IActionResult> PutApartment(Guid id, ApartmentModifyDTO apartment)
        {
            Apartment updatedApartment;
            try
            {
                updatedApartment = _mapper.Map<ApartmentModifyDTO, Apartment>(apartment);
            }
            catch (AutoMapperMappingException e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return this.ResolveError(HttpStatusCode.BadRequest, $"Some of the provided values are invalid: {message}");
            }
            catch (Exception e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return this.ResolveError(HttpStatusCode.InternalServerError, $"There was a problem while updating the apartment: {message}");
            }

            ApartmentResponse response = await _apartmentService.PutApartment(id, updatedApartment);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            var apartmentFinal = _mapper.Map<Apartment, ApartmentGetDTO>(response.Resource);
            return Ok(apartmentFinal);
        }

        [HttpPost("Create")]
        [Authorize(Policy = "RequireManagerPrivileges")]
        public async Task<ActionResult<ApartmentModifyDTO>> PostApartment(ApartmentModifyDTO apartment)
        {
            Apartment newApartment;
            try
            {
                newApartment = _mapper.Map<ApartmentModifyDTO, Apartment>(apartment);
            }
            catch (AutoMapperMappingException e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return this.ResolveError(HttpStatusCode.BadRequest, $"Some of the provided values are invalid: {message}");
            }
            catch (Exception e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return this.ResolveError(HttpStatusCode.InternalServerError, $"There was a problem while creating the apartment: {message}");
            }

            ApartmentResponse response = await _apartmentService.PostApartment(newApartment);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            ApartmentGetDTO apartmentFinal = _mapper.Map<Apartment, ApartmentGetDTO>(response.Resource);

            return CreatedAtAction("GetApartment", new { id = response.Resource.Id }, apartmentFinal);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "RequireManagerPrivileges")]
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