using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductManagementSystem.BLL.DTOs.Category;
using ProductManagementSystem.DAL.Entities;

namespace ProductManagementSystem.BLL.Mapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // — DTO → Entity —
            CreateMap<CreateCategoryRequest, Category>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore());
            CreateMap<UpdateCategoryRequest, Category>();
            CreateMap<PatchCategoryRequest, Category>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember is not null));

            // — Entity → Response DTO —
            CreateMap<Category, GetCategoryResponse>();
        }
    }
}
