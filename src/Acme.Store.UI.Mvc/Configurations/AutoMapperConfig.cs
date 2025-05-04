using Acme.Store.Business.Models;
using Acme.Store.UI.Mvc.Models;
using AutoMapper;
using Microsoft.OpenApi.Extensions;
using System.Globalization;

namespace Acme.Store.UI.Mvc.Configurations
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            var brasilCI = CultureInfo.GetCultureInfo("pt-BR");
            var usaCI = CultureInfo.GetCultureInfo("en-US");

            // Produto
            CreateMap<Produto, ProdutoExibirViewModel>()
                .ForMember(dest => dest.UnidadeVenda, opt => opt.MapFrom(src => src.UnidadeVenda.GetDisplayName()))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Nome))
                .ForMember(dest => dest.Vendedor, opt => opt.MapFrom(src => src.Vendedor.Nome))
                .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Preco.ToString("C", brasilCI)))
                .ForMember(dest => dest.QuantidadeEstoque, opt => opt.MapFrom(src => src.QuantidadeEstoque.ToString("N0", brasilCI)));

            CreateMap<Produto, ProdutoEditarViewModel>()
                .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Preco.ToString("N2", usaCI)))
                .ForMember(dest => dest.QuantidadeEstoque, opt => opt.MapFrom(src => src.QuantidadeEstoque.ToString("N0", usaCI)));

            CreateMap<ProdutoEditarViewModel, Produto>()
                .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => Convert.ToDouble(src.Preco.Replace("R$", "").Replace(",", "").Trim(), usaCI)))
                .ForMember(dest => dest.QuantidadeEstoque, opt => opt.MapFrom(src => int.Parse(src.QuantidadeEstoque.Replace(",", "").Trim())));

            CreateMap<Produto, ProdutoIncluirViewModel>()
                .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Preco.ToString("N2", usaCI)))
                .ForMember(dest => dest.QuantidadeEstoque, opt => opt.MapFrom(src => src.QuantidadeEstoque.ToString("N0", usaCI)));

            CreateMap<ProdutoIncluirViewModel, Produto>()
                .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => Convert.ToDouble(src.Preco.Replace("R$", "").Replace(",", "").Trim(), usaCI)))
                .ForMember(dest => dest.QuantidadeEstoque, opt => opt.MapFrom(src => int.Parse(src.QuantidadeEstoque.Replace(",", "").Trim())));


            // Vendedor
            CreateMap<Vendedor, VendedorViewModel>()
                .ReverseMap();

            CreateMap<Vendedor, VendedorIncluirViewModel>()
                .ReverseMap();

            CreateMap<Vendedor, SelectListViewModel>();


            // Categoria
            CreateMap<Categoria, CategoriaViewModel>()
                .ReverseMap();

            CreateMap<Categoria, SelectListViewModel>();
        }
    }
}
