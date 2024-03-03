using AutoMapper;
using BaliFramework.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteShare.Core.Services;
using NoteShare.Models.Note;

namespace NoteShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IMapper _mapper;

        public NoteController(INoteService noteService, IMapper mapper)
        {
            _noteService = noteService;
            _mapper = mapper;
        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ListNoteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromQuery] NoteSearchDto searchDto)
        {
            var result = await _noteService.SearchNote(searchDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadNoteDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string id)
        {
            var note = await _noteService.GetNoteById(id);
            var result = _mapper.Map<ReadNoteDto>(note);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadNoteDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateNoteDto createNoteDto)
        {
            var note = await _noteService.CreateNote(createNoteDto);
            var result = _mapper.Map<ReadNoteDto>(note);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadNoteDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromForm] UpdateNoteDto updateNoteDto)
        {
            var note = await _noteService.UpdateNote(updateNoteDto);
            var result = _mapper.Map<ReadNoteDto>(note);
            return Ok(result);
        }

        [HttpDelete("File/{fileId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadNoteDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteFile(string fileId)
        {
            var note = await _noteService.DeleteNoteFile(fileId);
            var result = _mapper.Map<ReadNoteDto>(note);
            return Ok(result);
        }
    }
}
