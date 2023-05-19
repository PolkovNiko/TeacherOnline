using TeacherOnline.DAL.Entities;
using TeacherOnline.DTO.ModelsDTO;

namespace TeacherOnline.DTO
{
    public class MapperCfg : AutoMapper.Profile
    {
        public MapperCfg()
        {
            CreateMap<Profile, ProfileDTO>();
            CreateMap<ProfileDTO, Profile>();
            CreateMap<IEnumerable<GroupDTO>, IEnumerable<Group>>();
            CreateMap<GroupDTO, Group>();
        }
    }
}
