﻿using TeacherOnline.DAL.Entities;

namespace TeacherOnline.DTO.ViewModel
{
    public class EstimateVM
    {
        public Estimate estimate { get; set; } = new Estimate();
        public IEnumerable<Profile> teacher { get; set; } = new List<Profile>();
        public IEnumerable<Profile> users { get; set; } = new List<Profile>();
        public IEnumerable<Subject> subjects { get; set; } = new List<Subject>();
        public IEnumerable<Estimate> estimateList { get; set; } = new List<Estimate>();
        public IEnumerable<Group> groups { get; set; } = new List<Group>();
        public Group OneGroup { get; set; } = new Group();
    }
}
