using AutoMapper;
using Domain.Models;
using WebMVC.ViewModels;

namespace WebMVC.AutoMapperUtils
{
    public static class MapperConfig
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteViewModel, Cliente>().ReverseMap();
                cfg.CreateMap<EnderecoViewModel, Endereco>().ReverseMap();
            });

            return config;
        }
    }
}