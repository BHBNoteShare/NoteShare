using BaliFramework.Models;
using BaliFramework.Services;
using Microsoft.Extensions.Options;
using NoteShare.Data.Entities;
using NoteShare.Models.Note;

namespace NoteShare.Core.Services
{
    public interface INoteService
    {
        Task<PagedResult<ListNoteDto>> SearchNote(NoteSearchDto searchDto);
        Task<Note> GetNoteById(string id);
        Task<Note> CreateNote(CreateNoteDto createNoteDto);
        Task<Note> UpdateNote(UpdateNoteDto updateNoteDto);
        Task<Note> DeleteNoteFile(string fileId);

        Task<PagedResult<NoteComment>> GetComments(string noteId, QueryParameters queryParameters);
        Task<PagedResult<NoteComment>> CreateComment(NoteCommentDto noteCommentDto);
        Task<PagedResult<NoteComment>> DeleteComment(string noteId, string commentId);
    }

    public class NoteService : INoteService
    {
        private readonly NoteFileSetting _fileSetting;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMinIOService _minIOService;

        public NoteService(IUnitOfWork unitOfWork,
            IMinIOService minIOService,
            IOptions<NoteShareConfig> settingOption)
        {
            _unitOfWork = unitOfWork;
            _minIOService = minIOService;
            _fileSetting = settingOption.Value.NoteFileSetting;
        }

        public Task<Note> GetNoteById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<ListNoteDto>> SearchNote(NoteSearchDto searchDto)
        {
            throw new NotImplementedException();
        }

        public Task<Note> CreateNote(CreateNoteDto createNoteDto)
        {
            throw new NotImplementedException();
        }

        public Task<Note> UpdateNote(UpdateNoteDto updateNoteDto)
        {
            throw new NotImplementedException();
        }

        public Task<Note> DeleteNoteFile(string fileId)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<NoteComment>> GetComments(string noteId, QueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<NoteComment>> DeleteComment(string noteId, string commentId)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<NoteComment>> CreateComment(NoteCommentDto noteCommentDto)
        {
            throw new NotImplementedException();
        }
    }
}
