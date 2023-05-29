using Microsoft.EntityFrameworkCore;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Services
{
    public class EstimateService : IEstimate
    {
        AssistantTeachingContext _context;

        public EstimateService(AssistantTeachingContext context)
        {
            _context = context;
        }

        public void Create(Estimate item)
        {
            //var count = GetAll().Count();
            //item.Id = count == 0 ? 1 : count + 1;
            _context.Estimates.Add(item);
            _context.SaveChanges();
        }

        public void Update(Estimate item)
        {
            var estimate = _context.Estimates.FirstOrDefault(u=> u.Id == item.Id);
            if(estimate != null)
            {
                estimate.IdSubject = item.IdSubject;
                estimate.IdTeacher = item.IdTeacher;
                estimate.IdUser = item.IdUser;
                estimate.Score = item.Score;
                estimate.Type = item.Type;
                estimate.DateUpdate = item.DateUpdate;
                _context.Estimates.Update(estimate);
                _context.SaveChanges();
                return;
            }
            throw new Exception("estimates is not found?");
        }

        public void Delete(int id)
        {
            Estimate estimates = _context.Estimates.FirstOrDefault(e => e.Id == id);
            if(estimates != null)
            {
                _context.Estimates.Remove(estimates);
                _context.SaveChanges();
                return;
            }
            throw new Exception("estimates is not found?");
        }

        public IEnumerable<Estimate> Find(Func<Estimate, bool> predicate)
        {
            return _context.Estimates.Include(u => u.IdUserNavigation).Include(u => u.IdTeacherNavigation).Include(u => u.IdSubjectNavigation).Where(predicate);
        }

        public Estimate Get(int id)
        {
            return _context.Estimates.Include(u => u.IdUserNavigation).Include(u => u.IdTeacherNavigation).Include(u => u.IdSubjectNavigation).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Estimate> GetAll()
        {
            return _context.Estimates.Include(u => u.IdUserNavigation).Include(u => u.IdTeacherNavigation).Include(u => u.IdSubjectNavigation);
        }

    }
}
