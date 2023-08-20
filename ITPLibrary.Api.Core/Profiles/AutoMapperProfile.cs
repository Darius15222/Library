using ITPLibrary.Data.Shared.Dtos.BookManagement;
using ITPLibrary.Data.Shared.Dtos.OrderDto;
using ITPLibrary.Data.Shared.Dtos.ShoppingCartDto;
using ITPLibrary.Data.Shared.Dtos.ShoppingCartItemDto;
using ITPLibrary.Data.Shared.Dtos.UserDtos;
using ITPLibrary.Data.Shared.Dtos.UserDtos.UserManagement;

namespace ITPLibrary.Api.Core.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Book, BookDto>().ReverseMap();

        CreateMap<Book, BookDetailsDto>()
                .ForMember(dest => dest.LongDescription, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();

        CreateMap<Book, BookSellerDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail))
                .ForMember(dest => dest.Best, opt => opt.MapFrom(src => src.Best))
                .ReverseMap();

        CreateMap<Book, PromotedBooksDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ReverseMap();


        CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
        CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();

        CreateMap<Order, OrderDto>().ReverseMap();

        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<RegisterDto, User>().ReverseMap();
        CreateMap<LoginDto, User>().ReverseMap();
    }
}
