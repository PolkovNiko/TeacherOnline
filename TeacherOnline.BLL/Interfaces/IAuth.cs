using System.Security.Claims;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IAuth
    {
        int Id { get; set; }

        ClaimsPrincipal LogIn(User user);
        void Registration(User user);
    }
}
