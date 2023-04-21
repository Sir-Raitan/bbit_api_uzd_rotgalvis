using AutoMapper;
using bbit_2_uzd.Controllers.Extensions;
using bbit_2_uzd.Models;
using bbit_2_uzd.Models.DTO;
using bbit_2_uzd.Services.Communication;
using bbit_2_uzd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace bbit_2_uzd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ApiScope")]
    public class HouseController : ControllerBase
    {
        private IHouseService _houseService;
        private IMapper _mapper;

        public HouseController(IHouseService houseService, IMapper mapper)
        {
            _houseService = houseService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<HouseDTO>> GetAllHouses(Guid? tenant_id)
        {
            if (tenant_id != null)
            {
                var tenantHouses = await _houseService.GetTenantHouses((Guid)tenant_id);
                var tenantHousesFinal = _mapper.Map<IEnumerable<House>, IEnumerable<HouseDTO>>(tenantHouses);
                return tenantHousesFinal;
            }

            var houses = await _houseService.GetAllHouses();

            var housesFinal = _mapper.Map<IEnumerable<House>, IEnumerable<HouseDTO>>(houses);

            return housesFinal;
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<HouseDTO>> GetHouse(Guid id)
        {
            HouseResponse response = await _houseService.GetHouse(id);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            HouseDTO house = _mapper.Map<House, HouseDTO>(response.Resource);

            return house;
        }

        [HttpPut("Update/{id}")]
        [Authorize(Policy = "RequireManagerPrivileges")]
        public async Task<IActionResult> PutHouse(Guid id, HouseModifyDTO house)
        {
            House updatedHouse;
            try
            {
                updatedHouse = _mapper.Map<HouseModifyDTO, House>(house);
            }
            catch (AutoMapperMappingException e)
            {
                return this.ResolveError(HttpStatusCode.BadRequest, $"Some of the provided values are invalid: {e.Message}");
            }
            catch (Exception e)
            {
                return this.ResolveError(HttpStatusCode.InternalServerError, $"There was a problem while updating the house: {e.Message}");
            }

            HouseResponse response = await _houseService.PutHouse(id, updatedHouse);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            return Ok(response.Resource);
        }

        [HttpPost("Create")]
        [Authorize(Policy = "RequireManagerPrivileges")]
        public async Task<ActionResult<HouseDTO>> PostHouse(HouseModifyDTO house)
        {
           
            House newHouse;
            try
            {
                newHouse = _mapper.Map<HouseModifyDTO, House>(house);
            }
            catch (AutoMapperMappingException e)
            {
                return this.ResolveError(HttpStatusCode.BadRequest, $"Some of the provided values are invalid: {e.Message}");
            }
            catch (Exception e)
            {
                return this.ResolveError(HttpStatusCode.InternalServerError, $"There was a problem while creating the house: {e.Message}");
            }

            HouseResponse response = await _houseService.PostHouse(newHouse);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            HouseModifyDTO houseFinal = _mapper.Map<House, HouseModifyDTO>(response.Resource);

            return CreatedAtAction("GetHouse", new { id = response.Resource.Id }, houseFinal);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "RequireManagerPrivileges")]
        public async Task<IActionResult> DeleteHouse(Guid id)
        {
            HouseResponse response = await _houseService.DeleteHouse(id);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            return NoContent();
        }
    }
}