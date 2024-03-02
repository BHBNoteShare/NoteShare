using AutoMapper;
using BaliFramework.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteShare.Core.Services;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<StudentPreferenceDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddStudentPreferences([FromBody] List<StudentPreferenceDto> studentPreferencesDto)
        {
            var studentPreferences = await _studentPreferenceService.AddStudentPreferences(studentPreferencesDto);
            var mapped = _mapper.Map<PagedResult<StudentPreferenceDto>>(studentPreferences);
            return Ok(mapped);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<StudentPreferenceDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteStudentPreferences([FromBody] List<StudentPreferenceDto> studentPreferencesDto)
        {
            var studentPreferences = await _studentPreferenceService.DeleteStudentPreferences(studentPreferencesDto);
            var mapped = _mapper.Map<PagedResult<StudentPreferenceDto>>(studentPreferences);
            return Ok(mapped);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<StudentPreferenceDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetStudentPreferences([FromQuery] QueryParameters queryParameters)
        {
            var studentPreferences = await _studentPreferenceService.GetStudentPreferences(queryParameters);
            var mapped = _mapper.Map<PagedResult<StudentPreferenceDto>>(studentPreferences);
            return Ok(mapped);
        }
    }
}
