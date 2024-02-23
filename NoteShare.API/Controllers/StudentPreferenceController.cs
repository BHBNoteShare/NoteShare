using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteShare.Core.Extensions;
using NoteShare.Core.Services;
using NoteShare.Data.Entities;
using NoteShare.Models;
using NoteShare.Models.StudentPreferences;

namespace NoteShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentPreferenceController : ControllerBase
    {
        private readonly IStudentPreferenceService _studentPreferenceService;
        private readonly IMapper _mapper;

        public StudentPreferenceController(IStudentPreferenceService studentPreferenceService, IMapper mapper)
        {
            _studentPreferenceService = studentPreferenceService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("subjects")]
        public async Task<IActionResult> GetSubjects([FromQuery] QueryParameters queryParameters)
        {
            var result = await _studentPreferenceService.GetSubjects(queryParameters);

            var mapped = PaggingExtension.MapPagedResult<Subject, SubjectDto>(result, _mapper);
            return Ok(mapped);
        }
    }
}
