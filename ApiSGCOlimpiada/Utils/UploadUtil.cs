using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Utils
{
    public abstract class UploadUtil
    {
        public static async Task<string> UploadAnexosPdfAsync(IFormFile arquivo, string path)
        {
            if (arquivo != null)
            {
                try
                {
                    string[] permittedExtensions = { ".pdf" };
                    var fileDirectory = $@"{Directory.GetCurrentDirectory()}\{path}";
                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    var fileName = $@"{path}\{DateTime.Now.ToString().Replace(" ", "T").Replace("/", "").Replace(":", "")}{arquivo.FileName}";
                    var extension = Path.GetExtension(fileName).ToLowerInvariant();
                    if ((!string.IsNullOrEmpty(extension)) && permittedExtensions.Contains(extension))
                    {

                        using (FileStream filestream = System.IO.File.Create(fileName))
                        {
                            await arquivo.CopyToAsync(filestream);
                            filestream.Flush();
                            return fileName;
                        }
                    }
                    Console.WriteLine("Extenção de arquivo inválida. Envie arquivos pdf");
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Ocorreu uma falha no envio do arquivo...");
                return null;
            }
        }
    }
}
