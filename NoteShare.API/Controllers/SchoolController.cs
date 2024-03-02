using AutoMapper;
using BaliFramework.Models;
using Microsoft.AspNetCore.Mvc;
using NoteShare.Core.Services;
using NoteShare.Models.School;
using NoteShare.Models.Util;

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
        public async Task<IActionResult> GetSchools([FromQuery] SearchQueryParameters queryParameters)
        {
            var subjects = await _schoolService.GetSchools(queryParameters);
            var mapped = _mapper.Map<PagedResult<SchoolDto>>(subjects);
            return Ok(mapped);
        }
    }
}
