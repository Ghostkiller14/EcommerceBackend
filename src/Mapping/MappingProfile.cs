using AutoMapper;

public class MappingProfile : Profile{
    public MappingProfile(){
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Rating , RatingDto>();
        CreateMap<CreateRatingDto , Rating>();
        CreateMap<UpdateRatingDto , Rating>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));




        CreateMap<Order , OrderDto>();
        CreateMap<CreateOrderDto , Order>();
        CreateMap<UpdateOrderDto , Order>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));








    }
}
