//using HelloJobPH.Server.Data;
//using HelloJobPH.Server.Service.ApplicantRepo;
//using HelloJobPH.Shared.Model;
//using Microsoft.EntityFrameworkCore;

//namespace HelloJobPH.Server.Service.ApplicantRepo
//{
//    public class ApplicantRepository : IApplicantRepository
//    {
//        private readonly ApplicationDbContext _context;
//        public ApplicantRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<List<Applicant>> RetrieveAllAsync()
//        {
//            return await _context.Applicant.ToListAsync();
//        }

//        public async Task<Applicant> GetByIdAsync(int id)
//        {
//            return await _context.Applicant.FindAsync(id);
//        }


//        public async Task AddAsync(Applicant entity)
//        {
//            await _context.Applicant.AddAsync(entity);
//        }

//        public void Update(Applicant entity)
//        {
//            _context.Applicant.Update(entity);
//        }

//        public async Task SaveChangesAsync()
//        {
//            await _context.SaveChangesAsync();
//        }
//    }
//}
