using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IEstimate
    {
        void Create(Estimate item);
        void Delete(int id);
        void Update(Estimate item);
        IEnumerable<Estimate> GetAll();
        IQueryable<Estimate> Get(int id);
        IEnumerable<Estimate> Find(Func<Estimate, bool> predicate);
    }
}
