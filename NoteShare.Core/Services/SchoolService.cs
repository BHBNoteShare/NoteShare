using NoteShare.Core.Extensions;
using NoteShare.Data.Entities;
using NoteShare.Models;

namespace NoteShare.Core.Services
{
    public interface ISchoolService
    {
        Task<PagedResult<School>> GetSchools(SearchQueryParameters queryParameters);
    }

    public class SchoolService : ISchoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SchoolService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<School>> GetSchools(SearchQueryParameters queryParameters)
        {
            var schools = _unitOfWork.GetRepository<School>().GetAsQueryable();
            if (!string.IsNullOrEmpty(queryParameters.SearchText))
            {
                schools = schools.Where(s => s.OM.Contains(queryParameters.SearchText) || s.Name.Contains(queryParameters.SearchText));
            }
            return await schools.GetPagedResult(queryParameters.PageNumber, queryParameters.PageSize);
        }
    }
}
