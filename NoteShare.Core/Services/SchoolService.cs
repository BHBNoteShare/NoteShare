using NoteShare.Core.Extensions;
using NoteShare.Data.Entities;
using NoteShare.Models;

namespace NoteShare.Core.Services
{
    public interface ISchoolService
    {
        Task<PagedResult<School>> GetSchools(QueryParameters queryParameters);
    }

    public class SchoolService : ISchoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SchoolService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<School>> GetSchools(QueryParameters queryParameters)
        {
            var schools = _unitOfWork.GetRepository<School>().GetAsQueryable();
            return await schools.GetPagedResult();
        }
    }
}
