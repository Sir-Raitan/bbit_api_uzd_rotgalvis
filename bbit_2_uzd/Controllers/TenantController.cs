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
    public class TenantController : ControllerBase
    {
        private ITenantService _tenantService;
        private IMapper _mapper;

        public TenantController(ITenantService tenantService, IMapper mapper)
        {
            _tenantService = tenantService;
            _mapper = mapper;
        }

        // GET: api/Iedzivotaji
        [HttpGet("GatAll")]
        public async Task<IEnumerable<TenantGetDTO>> GetAllTenants()
        {
            var tenantsRaw = await _tenantService.GetAllTenants();

            var tenants = _mapper.Map<IEnumerable<Tenant>, IEnumerable<TenantGetDTO>>(tenantsRaw);

            return tenants;
        }

        // GET: api/Iedzivotaji/5
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<TenantGetDTO>> GetTenant(Guid id)
        {
            TenantResponse response = await _tenantService.GetTenant(id);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            TenantGetDTO tenant = _mapper.Map<Tenant, TenantGetDTO>(response.Resource);

            return tenant;
        }

        // PUT: api/Iedzivotaji/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutTenant(Guid id, TenantModifyDTO tenant)
        {
            Tenant updatedTenant;
            try
            {
                updatedTenant = _mapper.Map<TenantModifyDTO, Tenant>(tenant);
            }
            catch (AutoMapperMappingException e)
            {
                return this.ResolveError(HttpStatusCode.BadRequest, $"Some of the provided values are invalid: {e.Message}");
            }
            catch (Exception e)
            {
                return this.ResolveError(HttpStatusCode.InternalServerError, $"There was a problem creating updating the tenant: {e.Message}");
            }

            TenantResponse response = await _tenantService.PutTenant(id, updatedTenant);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            var tenantFinal = _mapper.Map<Tenant, TenantGetDTO>(response.Resource);

            return Ok(tenantFinal);
        }

        // POST: api/Iedzivotaji
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Create")]
        public async Task<ActionResult<TenantModifyDTO>> PostTenant(TenantModifyDTO tenant)
        {
            Tenant newTenant;
            try
            {
                newTenant = _mapper.Map<TenantModifyDTO, Tenant>(tenant);
            }
            catch (AutoMapperMappingException e)
            {
                return this.ResolveError(HttpStatusCode.BadRequest, $"Some of the provided values are invalid: {e.Message}");
            }
            catch (Exception e)
            {
                return this.ResolveError(HttpStatusCode.InternalServerError, $"There was a problem while creating the tenant: {e.Message}");
            }

            TenantResponse response = await _tenantService.PostTenant(newTenant);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            TenantGetDTO tenantFinal = _mapper.Map<Tenant, TenantGetDTO>(response.Resource);

            return CreatedAtAction("GetTenant", new { id = response.Resource.Id }, tenantFinal);
        }

        // DELETE: api/Iedzivotaji/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTenant(Guid id)
        {
            TenantResponse response = await _tenantService.DeleteTenant(id);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            return NoContent();
        }
    }
}