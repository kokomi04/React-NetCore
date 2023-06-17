using AutoMapper;
using webapi.Commons;
using webapi.Models;

namespace webapi.ViewModels
{
    public class UserVM : IMapFrom<User>
    {
        public string Name { get; set; }
        public string passWord { get; set; }
        public string Email { get; set; }

        //public void Mapping(Profile profile)
        //{
        //    var t = profile.CreateMap<User, UserVM>().ReverseMap()
        //        .ForMember(u=>u.UserName, o => o.MapFrom(um =>um.Name));
        //}
    }
}
