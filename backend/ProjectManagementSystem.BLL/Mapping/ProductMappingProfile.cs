using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductManagementSystem.BLL.DTOs.Product;
using ProductManagementSystem.BLL.DTOs.ProductImage;
using ProductManagementSystem.DAL.Entities;

namespace ProductManagementSystem.BLL.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductRequest, Product>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<PatchProductRequest, Product>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember is not null)
                );


            CreateMap<CreateProductImageRequest, ProductImage>()
                .ForMember(dest => dest.ImageId, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore());

            CreateMap<Product, DetailProductResponse>();
            CreateMap<Product, GetAllProductsResponse>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src =>
                        src.ProductImage != null
                            ? src.ProductImage.ImageUrl
                            : "/uploads/default.jpg"))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description ?? string.Empty));
        }
    }
}
