using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;

namespace BusinessLogicLayer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();


            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
        }
    }
}
