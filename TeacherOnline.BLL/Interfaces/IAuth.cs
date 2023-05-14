using System.Security.Claims;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IAuth
    {
        ClaimsPrincipal LogIn(User user);
        void Registration(User user);
    }
}
