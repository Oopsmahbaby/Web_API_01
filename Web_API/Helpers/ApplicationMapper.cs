using AutoMapper;
using Web_API.Data;
using Web_API.Models;

namespace Web_API.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
