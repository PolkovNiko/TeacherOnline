﻿using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Services
{
    public class GroupService : IGroup
    {
        AssistantTeachingContext _context;

        public GroupService(AssistantTeachingContext context)
        {
            _context = context;
        }

        public void Create(Group item)
        {
            _context.Groups.Add(item);
            _context.SaveChanges();
        }
        public void Update(Group item)
        {
            var Group = _context.Groups.FirstOrDefault(u => u.Id == item.Id);
            if (Group != null)
            {
                Group.Name = item.Name;
                Group.Specialty = item.Specialty;
                _context.Groups.Update(Group);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            Group Group = _context.Groups.FirstOrDefault(e => e.Id == id);
            if (Group != null)
            {
                _context.Groups.Remove(Group);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Group> Find(Func<Group, bool> predicate)
        {
            return _context.Groups.Where(predicate).AsEnumerable();
        }

        public IQueryable<Group> Get(int id)
        {
            return _context.Groups.Where(u => u.Id == id).AsQueryable();
        }

        public IEnumerable<Group> GetAll()
        {
            return _context.Groups.AsEnumerable();
        }
    }
}
