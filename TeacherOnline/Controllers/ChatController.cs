using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.BLL.SignalR;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;
using TeacherOnline.DTO.ViewModel;
using TeacherOnline.Models;

namespace TeacherOnline.Controllers
{
    public class ChatController : Controller
    {
        IProfile _profile;
        IChat _chat;
        IMessage _message;

        public ChatController(IProfile profile, IChat chat, IMessage message)
        {
            _profile = profile;
            _chat = chat;
            _message = message;
        }

        //-------------------------------------------------------------------------------------------
        //Method Get

        [HttpGet]
        public IActionResult Index(int id)
        {
            //var chatvm = new ChatsVM();
            //chatvm.chats = _chat.Find(u => u.IdUser1 == (int)HttpContext.Session.GetInt32("Id")
            //|| u.IdUser2 == (int)HttpContext.Session.GetInt32("Id")).ToList();

            //chatvm.
            //var index = Convert.ToInt32(HttpContext.Request.QueryString.Value);
            var chat = _chat.Get(id);
            return View(chat);
        }

        [HttpGet]
        public IActionResult Users()
        {
            var chat = _chat.Find(u => u.IdUser1 == (int)HttpContext.Session.GetInt32("Id")
            || u.IdUser2 == (int)HttpContext.Session.GetInt32("Id")).ToList();
            ViewData["Id"] = HttpContext.Session.GetInt32("Id").ToString();
            return View(chat);
        }


        [HttpPost]
        public IActionResult CreateRoom(int Id)
        {
            var chat = new Chat()
            {
                IdUser1 = (int)HttpContext.Session.GetInt32("Id"),
                IdUser2 = Id
            };
            var id = _chat.Create(chat);
            return View("Index", id);
        }

        [HttpPost]
        public async  Task<IActionResult> SendMessage(int chatId, string userId,string message, [FromServices] IHubContext<ChatHub> chats)
        {
            //тут валидация должна быть.... или на фронте....
            var mes = new Message
            {
                IdAuthor = (int)HttpContext.Session.GetInt32("Id"),
                Message1 = message,
                IdChat = chatId,
                Time = DateTime.Now
            };
            var updateMes = _message.Create(mes);
            await chats.Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", new
            {
                Text = updateMes.Message1,
                Name = $"{updateMes.IdAuthorNavigation.LastName} {updateMes.IdAuthorNavigation.FirstName}" ,
                Timestamp = updateMes.Time.ToString("dd/MM/yyyy hh:mm")
            });
            return Ok();
        }


        //-------------------------------------------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}