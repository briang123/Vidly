using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly
{
    public class MappingProfile : Profile
    {
        //commented out block is for latest version of AutoMapper vs. v4.1
        //public static void Run()
        //{
        //    Mapper.Initialize(a =>
        //    {
        //        a.AddProfile<MappingProfile>();
        //    });
        //}

        public MappingProfile()
        {
            //Mapper.CreateMap<Customer, CustomerDto>().ReverseMap(); //instead of 2 create maps model/dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<CustomerDto, Customer>();
            Mapper.CreateMap<MovieDto, Movie>();

            // Dto to Domain
            //Mapper.CreateMap<CustomerDto, Customer>()
            //    .ForMember(c => c.Id, opt => opt.Ignore());

            //Mapper.CreateMap<MovieDto, Movie>()
            //    .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}