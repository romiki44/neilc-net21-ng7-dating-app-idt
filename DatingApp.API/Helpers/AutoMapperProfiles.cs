using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest=>dest.PhotoUrl, opt=> {
                    opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                })
                .ForMember(dest=>dest.Age, opt=> {
                    opt.ResolveUsing(d=>d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForDetailDto>()
                .ForMember(dest=>dest.PhotoUrl, opt=> {
                    opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                })
                .ForMember(dest=>dest.Age, opt=> {
                    opt.ResolveUsing(d=>d.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotosForDetailDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(d=>d.SenderKnownAs, opt => {
                    opt.MapFrom(src=>src.Sender.KnowAs);
                })
                .ForMember(d=>d.RecipientKnownAs, opt=> {
                    opt.MapFrom(src=>src.Recipient.KnowAs);
                })
                .ForMember(d=>d.SenderPhotoUrl, opt => {
                    opt.MapFrom(src=>src.Sender.Photos.FirstOrDefault(p=>p.IsMain).Url);
                })
                .ForMember(d=>d.RecipientPhotoUrl, opt => {
                    opt.MapFrom(src=>src.Recipient.Photos.FirstOrDefault(p=>p.IsMain).Url);
                });;
        }
    }
}