﻿
using HelloJobPH.Server.Service.HumanResource;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumanResourceController : ControllerBase
    {
        private readonly IHumanResourceService _humanService;
        public HumanResourceController(IHumanResourceService humanService)
        {
            _humanService = humanService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _humanService.RetrieveAllAsync();
                return Ok(list);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("soft-delete/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _humanService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound($"Job post with ID {id} not found.");
            }

            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<HumanResourceDtos>> Create(HumanResourceDtos jobPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdJobPost = await _humanService.AddAsync(jobPostDto);
            return CreatedAtAction(nameof(GetById), new { id = createdJobPost.HumanResourceId }, createdJobPost);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<HumanResourceDtos>> GetById(int id)
        {
            var jobPost = await _humanService.GetByIdAsync(id);
            if (jobPost == null)
            {
                return NotFound($"Job post with ID {id} not found.");
            }
            return Ok(jobPost);
        }
    }
}
