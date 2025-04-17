using System.Drawing;

namespace Acme.Store.Business.Models.Info
{
    public static class ProdutoModelInfo
    {
        public const int NomeMinLength = 3;
        public const int NomeMaxLength = 150;
        public const int DescricaoMinLength = 3;
        public const int DescricaoMaxLength = 500;
        //public const int QuantidadeMinValue = 0;
        //public const int QuantidadeMaxValue = 10000000;
        //public const double PrecoMinValue = 0.0;
        //public const double PrecoMaxValue = 10000000.0;

        public static readonly Size DefaultImageSize = new Size(253, 164);
    }
}
