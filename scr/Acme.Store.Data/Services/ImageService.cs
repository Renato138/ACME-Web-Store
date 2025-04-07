using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
//using static System.Net.Mime.MediaTypeNames;
//using Image = SixLabors.ImageSharp.Image;

namespace Acme.Store.Data.Services
{
    public static class ImageService
    {
        public static bool IsImage(Stream imageStream, out string message)
        {
            var isValid = false;
            message = string.Empty;

            try
            {
                imageStream.Position = 0;
                var imageInfo = Image.Identify(imageStream);
                isValid = (imageInfo != null);
                
                if (! isValid)
                    message = "Erro ao ler imagem informada.";
            }
            catch (ArgumentNullException)
            {
                message = "a imagem informada é nula.";
            }
            catch (NotSupportedException)
            {
                message = "Os dados da imagem não estão legíveis ou o formato da imagem não é suportado..";
            }
            catch (InvalidImageContentException)
            {
                message = "O conteúdo da imagem informada não é válido.";
            }
            catch (UnknownImageFormatException)
            {
                message = "O formato da imagem é desconhecido.";
            }
            catch (Exception)
            {
                message = "Erro ao ler imagem informada.";
            }

            return isValid;
        }

        public static bool IsBigger(Stream imageStream, System.Drawing.Size size)
        {
            imageStream.Position = 0;

            var imageInfo = Image.Identify(imageStream);
            if (imageInfo != null)
            {
                return imageInfo.Width > size.Width || imageInfo.Height > size.Height;
            }
            return false;
        }

        /// <summary>
        /// Redimensiona o tamanho da imagem para as dimensões especificadas no parâmetro 'size'.
        /// </summary>
        /// <param name="imageStream">O stream da image.</param>
        /// <param name="size">The general decoder options.</param>
        /// <returns>O <see cref="Stream"/>.</returns>
        public static async Task<Stream> Resize(Stream imageStream, System.Drawing.Size size, bool keepRatio = true)
        {
            imageStream.Position = 0;

            using (var image = await Image.LoadAsync(imageStream))
            {
                var widthDivider = (double)image.Width / (double)size.Width;
                var heightDivider = (double)image.Height / (double)size.Height;
                var width = 0;
                var height = 0;

                double divider = Math.Max(widthDivider, heightDivider);

                if (keepRatio)
                {
                    if (widthDivider > heightDivider)
                    {
                        width = (int)((double)image.Width / divider);
                        height = 0;
                    }
                    else
                    {
                        width = 0;
                        height = (int)((double)image.Height / divider);
                    }
                }
                else
                {
                    width = (int)((double)image.Width / divider);
                    height = (int)((double)image.Height / divider);
                }

                image.Mutate(x => x.Resize(width, height));

                var outStream = new MemoryStream();

                await image.SaveAsJpegAsync(outStream);

                return outStream;
            }
        }

        public static async Task<string> ToBase64String(Stream imageStream)
        {
            imageStream.Position = 0;

            using (var image = await Image.LoadAsync(imageStream))
            {
                return image.ToBase64String(JpegFormat.Instance);
            }
        }
    }
}
