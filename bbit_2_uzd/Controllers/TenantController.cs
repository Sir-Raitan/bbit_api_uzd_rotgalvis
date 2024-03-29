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
    public class TenantController : ControllerBase
    {
        private ITenantService _tenantService;
        private IMapper _mapper;

        public TenantController(ITenantService tenantService, IMapper mapper)
        {
            _tenantService = tenantService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<TenantGetDTO>> GetAllTenants(Guid? apartment_id)
        {
            if (apartment_id != null)
            {
                var tenantsRaw = await _tenantService.GetTenantsWithApartment((Guid)apartment_id);

                var tenants = _mapper.Map<IEnumerable<Tenant>, IEnumerable<TenantGetDTO>>(tenantsRaw);

                return tenants;
            }
            var allTenantsRaw = await _tenantService.GetAllTenants();

            var allTenants = _mapper.Map<IEnumerable<Tenant>, IEnumerable<TenantGetDTO>>(allTenantsRaw);

            return allTenants;
        }

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

        [HttpPut("Update/{id}")]
        [Authorize(Policy = "RequireTenantEditPrivileges")]
        public async Task<IActionResult> PutTenant(Guid id, TenantModifyDTO tenant)
        {
            Tenant updatedTenant;
            try
            {
                updatedTenant = _mapper.Map<TenantModifyDTO, Tenant>(tenant);
            }
            catch (AutoMapperMappingException e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return this.ResolveError(HttpStatusCode.BadRequest, $"Some of the provided values are invalid: {message}");
            }
            catch (Exception e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return this.ResolveError(HttpStatusCode.InternalServerError, $"There was a problem creating updating the tenant: {message}");
            }

            TenantResponse response = await _tenantService.PutTenant(id, updatedTenant);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            var tenantFinal = _mapper.Map<Tenant, TenantGetDTO>(response.Resource);

            return Ok(tenantFinal);
        }

        [HttpPost("Create")]
        [Authorize(Policy = "RequireManagerPrivileges")]
        public async Task<ActionResult<TenantModifyDTO>> PostTenant(TenantModifyDTO tenant)
        {
            Tenant newTenant;
            try
            {
                newTenant = _mapper.Map<TenantModifyDTO, Tenant>(tenant);
            }
            catch (AutoMapperMappingException e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return this.ResolveError(HttpStatusCode.BadRequest, $"Some of the provided values are invalid: {message}");
            }
            catch (Exception e)
            {
                string message = e.InnerException == null ? e.Message : e.InnerException.Message;
                return this.ResolveError(HttpStatusCode.InternalServerError, $"There was a problem while creating the tenant: {message}");
            }

            TenantResponse response = await _tenantService.PostTenant(newTenant);

            if (!response.Success)
            {
                return this.ResolveError(response.StatusCode, response.Message);
            }

            TenantGetDTO tenantFinal = _mapper.Map<Tenant, TenantGetDTO>(response.Resource);

            return CreatedAtAction("GetTenant", new { id = response.Resource.Id }, tenantFinal);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "RequireManagerPrivileges")]
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