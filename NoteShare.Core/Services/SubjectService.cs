using NoteShare.Core.Extensions;
using NoteShare.Data.Entities;
using NoteShare.Models;

namespace NoteShare.Core.Services
{
    public interface ISubjectService
    {
        Task<PagedResult<Subject>> GetSubjects(QueryParameters queryParameters);
    }

    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Subject>> GetSubjects(QueryParameters queryParameters)
        {
            var subjects = _unitOfWork.GetRepository<Subject>().GetAsQueryable();
            return await subjects.GetPagedResult();
        }
    }
}
