using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<User, UserDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


        CreateMap<Rating , RatingDto>();
        CreateMap<CreateRatingDto , Rating>();
        CreateMap<UpdateRatingDto , Rating>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));





    }
}
