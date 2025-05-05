using Acme.Store.Api.ViewModels;
using Acme.Store.Auth.Models;
using Acme.Store.Business.Models;
using AutoMapper;
using Microsoft.OpenApi.Extensions;
using System.Globalization;

namespace Acme.Store.Api.Configurations
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            // Produto
            CreateMap<Produto, ProdutoExibirViewModel>()
                .ForMember(dest => dest.UnidadeVenda, opt => opt.MapFrom(src => src.UnidadeVenda.GetDisplayName()))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Nome))
                .ForMember(dest => dest.Vendedor, opt => opt.MapFrom(src => src.Vendedor.Nome))
                .ReverseMap();

            CreateMap<Produto, ProdutoEditarViewModel>()
                .ReverseMap();

            CreateMap<Produto, ProdutoIncluirViewModel>()
                .ReverseMap();


            // Usuario
            CreateMap<Usuario, UsuarioViewModel>()
                .ReverseMap();

            // Login
            CreateMap<Usuario, LoginViewModel>()
                .ReverseMap();


            // Vendedor
            CreateMap<Vendedor, VendedorViewModel>()
                .ReverseMap();

            CreateMap<Vendedor, VendedorIncluirViewModel>()
                .ReverseMap();

            CreateMap<VendedorIncluirViewModel, Usuario>();


           // Categoria
            CreateMap<Categoria, CategoriaViewModel>()
                .ReverseMap();


        }
    }
}
