﻿using AutoMapper;
using E_Shop_Engine.Domain.DomainModel;
using E_Shop_Engine.Utilities;
using E_Shop_Engine.Website.Areas.Admin.Models;
using E_Shop_Engine.Website.Models;

namespace E_Shop_Engine.Website.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductAdminViewModel>()
                    .ForMember(dest => dest.ImageBytes, opt => opt.AllowNull())
                    .ForMember(dest => dest.ImageBytes, opt => opt.MapFrom(src => src.ImageData))
                    .ForMember(dest => dest.ImageData, opt => opt.Ignore())
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                    .ForMember(dest => dest.SubcategoryName, opt => opt.MapFrom(src => src.Subcategory.Name));

                cfg.CreateMap<ProductAdminViewModel, Product>()
                    .ForMember(dest => dest.ImageData, opt => opt.AllowNull())
                    .ForMember(dest => dest.ImageData, opt => opt.MapFrom(
                        src => src.ImageBytes == null || src.ImageData != null
                        ? ConvertPostedFile.ToByteArray(src.ImageData)
                        : src.ImageBytes))
                    .ForMember(dest => dest.ImageMimeType, opt => opt.MapFrom(
                        src => src.ImageMimeType == null || (src.ImageMimeType != null && src.ImageData != null && src.ImageData.ContentType != src.ImageMimeType)
                        ? src.ImageData.ContentType
                        : src.ImageMimeType));

                cfg.CreateMap<Category, CategoryAdminViewModel>();

                cfg.CreateMap<CategoryAdminViewModel, Category>();

                cfg.CreateMap<Subcategory, SubcategoryAdminViewModel>()
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

                cfg.CreateMap<SubcategoryAdminViewModel, Subcategory>();

                cfg.CreateMap<Product, ProductViewModel>()
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                    .ForMember(dest => dest.SubcategoryName, opt => opt.MapFrom(src => src.Subcategory.Name));

                cfg.CreateMap<Category, CategoryViewModel>()
                    .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                    .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.Subcategories));

                cfg.CreateMap<Subcategory, SubcategoryViewModel>()
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.ID))
                    .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            });
        }
    }
}