using File = TeacherOnline.DAL.Entities.File;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IFile
    {
        void Create(File item);
        void Delete(int id);
        void Update(File item);
        IEnumerable<File> GetAll();
        File Get(int id);
        IEnumerable<File> Find(Func<File, bool> predicate);
    }
}
