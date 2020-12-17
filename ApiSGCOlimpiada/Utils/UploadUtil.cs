using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Utils
{
    public abstract class UploadUtil
    {
        public static async Task<string> UploadAnexosPdfAsync(IFormFile arquivo, string path, string fornecedor, long idSolicitacao)
        {
            string[] permittedExtensions = { ".pdf" };
            var extension = Path.GetExtension(arquivo.FileName).ToLowerInvariant();
            if (arquivo != null && !string.IsNullOrEmpty(extension) && permittedExtensions.Contains(extension))
            {
                try
                {
                    var fileDirectory = $@"{Directory.GetCurrentDirectory()}\{path}";
                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    var fileName = $@"{path}\{Regex.Replace(fornecedor, @"\W", "_")}_{idSolicitacao}.pdf";
                    using (FileStream filestream = System.IO.File.Create(fileName))
                    {
                        await arquivo.CopyToAsync(filestream);
                        filestream.Flush();
                        return fileName;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Arquivo veio nulo, ou não é da extenção pdf");
                return null;
            }
        }
    }
}
