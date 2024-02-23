using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NoteShare.Data.Entities;

namespace NoteShare.Core.Services.Init
{
    public class SubjectsInit
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<Subject> subjects;

        public SubjectsInit(IUnitOfWork unitOfWork, IOptions<NoteShareConfig> configOption)
        {
            _unitOfWork = unitOfWork;
            subjects = configOption.Value.Subjects;
        }

        public async Task Setup()
        {
            foreach (var subject in subjects)
            {
                if (!await _unitOfWork.GetDbSet<Subject>().AnyAsync(s=>s.Name.Equals(subject.Name)))
                {
                    await _unitOfWork.GetRepository<Subject>().AddAsync(subject);
                }
            }
        }
    }
}
