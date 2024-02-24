using Microsoft.EntityFrameworkCore;
using NoteShare.Core.Extensions;
using NoteShare.Data.Entities;
using NoteShare.Models;
using NoteShare.Models.StudentPreferences;

namespace NoteShare.Core.Services
{
    public interface IStudentPreferenceService
    {
        Task<PagedResult<StudentPreference>> AddStudentPreferences(List<StudentPreferenceDto> studentPreferences);
        Task<PagedResult<StudentPreference>> UpdateStudentPreferences(List<StudentPreferenceDto> studentPreferences);
        Task<PagedResult<StudentPreference>> DeleteStudentPreferences(List<StudentPreferenceDto> studentPreferences);
        Task<PagedResult<StudentPreference>> GetStudentPreferences();
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

        public async Task<PagedResult<StudentPreference>> AddStudentPreferences(List<StudentPreferenceDto> studentPreferences)
        {
            var student = await _authService.GetStudent() ?? throw new Exception("Nem található a diák");
            foreach (var studentPreferenceDto in studentPreferences)
            {
                await AddStudentPreference(studentPreferenceDto, student.Id);
            }
            return await GetStudentPreferences();
        }

        public async Task<PagedResult<StudentPreference>> UpdateStudentPreferences(List<StudentPreferenceDto> studentPreferences)
        {
            var student = await _authService.GetUser() ?? throw new Exception("Nem található a diák");
            foreach (var studentPreferenceDto in studentPreferences)
            {
                var studentPreference = await GetStudentPreference(studentPreferenceDto, student.Id);
                if (studentPreference == null)
                {
                    await AddStudentPreference(studentPreferenceDto, student.Id);
                }
                else
                {
                    studentPreference.Level = studentPreferenceDto.SubjectLevel;
                    studentPreference.PreferenceId = studentPreferenceDto.SubjectId;

                    _unitOfWork.Context().Set<StudentPreference>().Update(studentPreference);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            return await GetStudentPreferences();
        }

        public async Task<PagedResult<StudentPreference>> DeleteStudentPreferences(List<StudentPreferenceDto> studentPreferences)
        {
            var student = await _authService.GetStudent() ?? throw new Exception("Nem található a diák");
            foreach (var studentPreferenceDto in studentPreferences)
            {
                var studentPreference = await GetStudentPreference(studentPreferenceDto, student.Id);
                if (studentPreference != null)
                {
                    _unitOfWork.Context().Set<StudentPreference>().Remove(studentPreference);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            return await GetStudentPreferences();
        }

        public async Task<PagedResult<StudentPreference>> GetStudentPreferences()
        {
            var student = await _authService.GetStudent() ?? throw new Exception("Nem található a diák");
            return await _unitOfWork.Context().Set<StudentPreference>().Where(sp => sp.StudentId == student.Id).GetPagedResult();
        }

        public async Task AddStudentPreference(StudentPreferenceDto studentPreferenceDto, string studentId)
        {
            if (!await ExistsStudentPreference(studentPreferenceDto, studentId))
            {
                throw new Exception($"A diák már hozzáadta ezt a tantárgyat({studentPreferenceDto.SubjectId})");
            }
            var studentPreference = new StudentPreference
            {
                StudentId = studentId,
                Level = studentPreferenceDto.SubjectLevel,
                PreferenceId = studentPreferenceDto.SubjectId
            };

            await _unitOfWork.Context().Set<StudentPreference>().AddAsync(studentPreference);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<bool> ExistsStudentPreference(StudentPreferenceDto studentPreferenceDto, string studentId)
        {
            return await _unitOfWork.Context().Set<StudentPreference>().AnyAsync(sp => sp.StudentId == studentId && sp.Level == studentPreferenceDto.SubjectLevel && sp.PreferenceId == studentPreferenceDto.SubjectId);
        }

        private async Task<StudentPreference?> GetStudentPreference(StudentPreferenceDto studentPreferenceDto, string studentId)
        {
            return await _unitOfWork.Context().Set<StudentPreference>().FirstOrDefaultAsync(sp => sp.StudentId == studentId && sp.Level == studentPreferenceDto.SubjectLevel && sp.PreferenceId == studentPreferenceDto.SubjectId);
        }
    }
}
