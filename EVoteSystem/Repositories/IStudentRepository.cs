using System.Collections.Generic;
using System.Threading.Tasks;
using EVoteSystem.Models;

namespace EVoteSystem.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> FindStudentByIdAsync(int? sid);
        Task<Student> FindStudentByUsernameAsync(string username);
        Task<IList<Student>> NotCandidateStudents(int? id);
        Task<Student> FindStudentByNationalId(string nationalId);
    }
}