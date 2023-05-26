using TeacherOnline.DAL.Entities;

namespace TeacherOnline.DTO.ViewModel
{
    public class ChatsVM
    {
        public Profile profiles { get; set; } = new Profile();
        public List<Chat> chats { get; set; } = new List<Chat>();
        public Chat chat { get; set; } = new Chat();
        public Message message { get; set; } = new Message();
        public List<Message> messages { get; set; } = new List<Message>();

    }
}
