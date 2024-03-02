using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NoteShare.Data.Entities;
using BaliFramework.Services;

namespace NoteShare.Core.Services.Init
{
    public class SchoolsInit
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<School> schools;

        public SchoolsInit(IUnitOfWork unitOfWork, IOptions<NoteShareConfig> configOption)
        {
            _unitOfWork = unitOfWork;
            schools = configOption.Value.Schools;
        }

        public async Task Setup()
        {
            foreach (var school in schools)
            {
                if (!await _unitOfWork.GetDbSet<School>().AnyAsync(s=>s.Name.Equals(school.Name) && s.OM.Equals(school.OM)))
                {
                    await _unitOfWork.GetRepository<School>().AddAsync(school);
                }
            }
        }
    }
}
