using AutoMapper;
using Store.Api.Dtos;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<ProductPost, Product>();
            CreateMap<Product, ProductPost>();
            CreateMap<ProductsGet, Product>();
            CreateMap<Product, ProductsGet>();
            CreateMap<Product, ProductGetResult>();
            CreateMap<ProductGetResult, Product>();
            CreateMap<CustomerGet, Customer>();
            CreateMap<Customer, CustomerGet>();
            CreateMap<CustomerPost, Customer>();
            CreateMap<Customer, CustomerPost>();
            CreateMap<Customer, CustomerGetResult>();
            CreateMap<CustomerGetResult, Customer>();            
        }
    }
}
