using Acme.Store.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Business.Models
{
    public class Produto : Entity
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public UnidadeVenda UnidadeVenda { get; set; }

        public double Preco { get; set; }

        public int QuantidadeEstoque { get; set; }

        public Guid? CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public Guid? VendedorId { get; set; }

        public Vendedor Vendedor { get; set; }

        public string Imagem { get; set; }

    }
}
