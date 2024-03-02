using AutoMapper;
using BaliFramework.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteShare.Core.Services;
using NoteShare.Models.Subject;
using NoteShare.Models.Util;

namespace NoteShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectService studentPreferenceService, IMapper mapper)
        {
            _subjectService = studentPreferenceService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<SubjectDto>))]
        public async Task<IActionResult> GetSubjects([FromQuery] SearchQueryParameters queryParameters)
        {
            var result = await _subjectService.GetSubjects(queryParameters);
            var mapped = _mapper.Map<PagedResult<SubjectDto>>(result);
            return Ok(mapped);
        }
    }
}
