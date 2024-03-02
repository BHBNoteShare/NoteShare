using BaliFramework.Extensions;
using BaliFramework.Models;
using BaliFramework.Services;
using NoteShare.Data.Entities;
using NoteShare.Models.Util;

namespace NoteShare.Core.Services
{
    public interface ISubjectService
    {
        Task<PagedResult<Subject>> GetSubjects(SearchQueryParameters queryParameters);
    }

    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Subject>> GetSubjects(SearchQueryParameters queryParameters)
        {
            var subjects = _unitOfWork.GetRepository<Subject>().GetAsQueryable();
            if (!string.IsNullOrEmpty(queryParameters.SearchText))
            {
                subjects = subjects.Where(s => s.Name.Contains(queryParameters.SearchText));
            }
            return await subjects.GetPagedResult<Subject,Subject>(pageNum: queryParameters.PageNumber,pageSize: queryParameters.PageSize);
        }
    }
}
