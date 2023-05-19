using AutoMapper;
using TeacherOnline.DAL.Entities;
using TeacherOnline.DTO.ModelsDTO;
using TeacherOnline.DTO.ViewModel;
using Profile = TeacherOnline.DAL.Entities.Profile;

namespace TeacherOnline.DTO
{
    public class ConvertService : IConvertModels
    {
        IMapper mapper;
        public ConvertService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public Profile ConvetToProfile(UserProfileVM profile)
        {
            return mapper.Map<Profile>(profile.Profile);
        }
        public ProfileDTO ConvetToProfileDTO(Profile profile)
        {
            return mapper.Map<ProfileDTO>(profile);
        }
        public IEnumerable<GroupDTO> ConvetToGroupDTO(IEnumerable<Group> group)
        {
            var groupsDTO = new List<GroupDTO>();
            foreach(var item in group)
            {
                groupsDTO = mapper.Map<List<GroupDTO>>(group);
            }
            return groupsDTO;
        }
    }
}
