using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using WebAPI.Models;

namespace WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();

            CreateMap<CommentDTO, CommentModel>();
            CreateMap<CommentModel, CommentDTO>();

            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();

            CreateMap<PostDTO, PostModel>();
            CreateMap<PostModel, PostDTO>();
        }
    }
}
