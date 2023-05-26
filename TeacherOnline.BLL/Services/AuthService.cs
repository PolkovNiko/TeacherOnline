using System.Security.Claims;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;


namespace TeacherOnline.BLL.Services
{
    public class AuthService : IAuth
    {
        AssistantTeachingContext _context;
        public int Id {get;set;}

        public AuthService(AssistantTeachingContext context) 
        { 
            _context = context;
        }

        public ClaimsPrincipal LogIn(User user)
        {
            User? valid = _context.Users.FirstOrDefault(val => val.Login == user.Login && val.Password == user.Password);
            if (valid is null) throw new Exception("вы кто такие? я вас не звал. покиньте сайт!");   //тут вариант отдельный блок валидации написать, возможно в сервис и использовать здесь через DI
            Id = valid.Id;
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, valid.Rank), new Claim(ClaimTypes.Email, valid.Email) };
            var claimIdentitys = new ClaimsIdentity(claims, "Cookies");
            var claimPrincipal = new ClaimsPrincipal(claimIdentitys);
            return claimPrincipal;
        }

        public void Registration(User user)
        {
            user.Id = new UserService(_context).GetAll().Count();
            new UserService(_context).Create(user);
        }
    }
}
