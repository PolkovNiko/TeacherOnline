﻿using System.IO;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Services
{
    public class ProfileService : IProfile
    {
        AssistantTeachingContext _context;

        public ProfileService(AssistantTeachingContext context)
        {
            _context = context;
        }

        public void Create(Profile item, Stream stream)
        {
            if(stream != null && stream.Length > 0)
            {
                using (var memorystream = new MemoryStream())
                {
                    stream.CopyTo(memorystream);
                    item.Photo = memorystream.ToArray();
                }
                _context.Profiles.Add(item);
                _context.SaveChanges();
            }
            throw new Exception("Вы не прикрепили Фото");
        }

        public void Update(Profile item, Stream stream)
        {
            var Profile = _context.Profiles.FirstOrDefault(u => u.Id == item.Id);
            if (Profile != null)
            {
                if (stream != null && stream.Length > 0)
                {
                    using (var memorystream = new MemoryStream())
                    {
                        stream.CopyTo(memorystream);
                        Profile.Photo = memorystream.ToArray();
                    }
                }
                Profile.LastName = item.LastName;
                Profile.FirstName = item.FirstName;
                Profile.Otchestvo = item.Otchestvo;
                Profile.Group = item.Group;
                Profile.About = item.About;
                Profile.Date = item.Date;
                _context.Profiles.Update(Profile);
                _context.SaveChanges();
            }
            throw new Exception("Пользователь не найден!");
        }

        public void Delete(int id)
        {
            Profile Profile = _context.Profiles.FirstOrDefault(e => e.Id == id);
            if (Profile != null)
            {
                _context.Profiles.Remove(Profile);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Profile> Find(Func<Profile, bool> predicate)
        {
            return _context.Profiles.Where(predicate).AsEnumerable();
        }

        public IQueryable<Profile> Get(int id)
        {
            return _context.Profiles.Where(u => u.Id == id).AsQueryable();
        }

        public IEnumerable<Profile> GetAll()
        {
            return _context.Profiles.AsEnumerable();
        }
    }
}
