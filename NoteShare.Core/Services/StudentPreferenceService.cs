using NoteShare.Core.Extensions;
using NoteShare.Data.Entities;
using NoteShare.Models;
using NoteShare.Models.StudentPreferences;

namespace NoteShare.Core.Services
{
    public interface IStudentPreferenceService
    {
        Task<PagedResult<Subject>> GetSubjects(QueryParameters queryParameters);
        Task<bool> AddStudentPreferences(List<StudentPreferenceDto> studentPreferences);
    }

    public class StudentPreferenceService : IStudentPreferenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public StudentPreferenceService(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<bool> AddStudentPreferences(List<StudentPreferenceDto> studentPreferences)
        {
            var user = await _authService.GetUser() ?? throw new Exception("Nem található a felhasználó");
            if (user.UserType != Models.Auth.UserType.Student)
            {
                throw new Exception("A felhasználó nem diák");
            }
            throw new NotImplementedException();
        }

        public async Task<PagedResult<Subject>> GetSubjects(QueryParameters queryParameters)
        {
            var subjects = _unitOfWork.GetRepository<Subject>().GetAsQueryable();
            return await subjects.GetPagedResult();
        }
    }
}
