using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Data;
using RepositoryDemo.GenericRepository;
using RepositoryDemo.Models;

namespace RepositoryDemo.Repository
{
    public class StudentRepository : GenericRepository<Student> , IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context)
            : base(context)
            {

            }
    }
}