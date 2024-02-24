﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NoteShare.Core.Services;
using NoteShare.Models;
using NoteShare.Models.School;

namespace NoteShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;
        private readonly IMapper _mapper;

        public SchoolController(ISchoolService schoolService, IMapper mapper)
        {
            _schoolService = schoolService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<SchoolDto>))]
        public async Task<IActionResult> GetSchools([FromQuery] QueryParameters queryParameters)
        {
            var subjects = await _schoolService.GetSchools(queryParameters);
            var mapped = _mapper.Map<PagedResult<SchoolDto>>(subjects);
            return Ok(mapped);
        }
    }
}
