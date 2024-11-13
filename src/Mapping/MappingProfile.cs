using AutoMapper;
public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<User, UserDto>();
        CreateMap<UserRegisterDto, User>();
        CreateMap<UserLoginDto, User>();
        CreateMap<CreateUserDto, User>();

        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Password, opt => opt.Condition(src => src.Password != null))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Product, List<ProductDto>>();

        CreateMap<Rating , RatingDto>();
        CreateMap<CreateRatingDto , Rating>();
        CreateMap<UpdateRatingDto , Rating>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Order , OrderDto>();
        CreateMap<Order , List<OrderDto>>();

        CreateMap<CreateOrderDto , Order>();
        CreateMap<UpdateOrderDto , Order>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateCategoryDto, Category>();
        CreateMap<Category, CategoryDto>();

        CreateMap<Shipping, ShippingDto>();
        CreateMap<CreateShippingDto, Shipping>();
        CreateMap<UpdateShippingDto, Shipping>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<OrderItem, OrderItemDto>();
    }
}
