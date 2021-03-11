using ApiSGCOlimpiada.Data.EmailDAO;
using ApiSGCOlimpiada.Data.OcupacaoDAO;
using ApiSGCOlimpiada.Data.OrcamentoDAO;
using ApiSGCOlimpiada.Data.ProdutoPedidoOrcamentoDAO;
using ApiSGCOlimpiada.Data.ResponsavelDAO;
using ApiSGCOlimpiada.Models;
using ApiSGCOlimpiada.Services;
using ClosedXML.Excel;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviarEmailController : ControllerBase
    {
        private readonly IMailer _mailer;
        private readonly IEmailDAO _dao;
        private readonly IOrcamentoDAO _daoOrcamento;
        private readonly IResponsavelDAO _daoResponsavel;
        private readonly IOcupacaoDAO _daoOcupacao;
        private readonly IProdutoPedidoOrcamentoDAO _daoProdutoCompras;

        public EnviarEmailController(IMailer mailer, IEmailDAO dao, IOrcamentoDAO daoOrcamento,
            IResponsavelDAO daoResponsavel, IOcupacaoDAO daoOcupacao, IProdutoPedidoOrcamentoDAO daoProdutoCompras)
        {
            this._mailer = mailer;
            this._dao = dao;
            this._daoOrcamento = daoOrcamento;
            this._daoResponsavel = daoResponsavel;
            this._daoOcupacao = daoOcupacao;
            this._daoProdutoCompras = daoProdutoCompras;
        }
        [HttpPost("{idSolicitacao}")]
        public async Task<IActionResult> SendMail([FromBody] EmailModel dados, long idSolicitacao)
        {
            List<Orcamento> orcamentos = (List<Orcamento>)_daoOrcamento.GetOrcamentoBySolicitacao(idSolicitacao);
            List<ProdutoPedidoOrcamento> produtosCompras = (List<ProdutoPedidoOrcamento>)_daoProdutoCompras.GetSolicitacao(idSolicitacao);
            EmailModel data = _dao.GetDadosSolicitacao(idSolicitacao);
            data.CentroResponsabilidade = dados.CentroResponsabilidade;
            data.ClasseValor = dados.ClasseValor;
            data.ContaContabil = dados.ContaContabil;
            data.CodUnidadeOrganizacional = dados.CodUnidadeOrganizacional;
            data.UnidadeOrganizacional = dados.UnidadeOrganizacional;
            var find = orcamentos.Min(r => r.ValorTotal);
            data.Orcamento = orcamentos.Find(r => r.ValorTotal == find);
            data.Responsaveis = (List<Responsavel>)_daoResponsavel.GetBySolicitacao(idSolicitacao);
            data.Ocupacoes = _daoOcupacao.GetBySolicitacao(idSolicitacao);
            data.orcamentoAnexos = new List<byte[]>();
            foreach (var item in orcamentos)
            {
                if (!string.IsNullOrEmpty(item.Anexo))
                {
                    var path = Path.Combine($@"{Directory.GetCurrentDirectory()}\AnexoOrcamentos", item.Anexo);
                    var memory = new MemoryStream();
                    using var stream = new FileStream(path, FileMode.Open);
                    await stream.CopyToAsync(memory);
                    memory.Position = 0;
                    byte[] bacon = memory.ToArray();
                    memory.Close();
                    data.orcamentoAnexos.Add(bacon);
                }
                else
                {
                    data.orcamentoAnexos = null;
                }
            }
            List<Planilha> planilhas = (List<Planilha>)_daoProdutoCompras.GetDadosProdutoBySolicitacao(idSolicitacao);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Produto");
                var currentRow = 3;

                worksheet.Cell(currentRow, 1).Value = "CodigoProtheus";
                worksheet.Cell(currentRow, 2).Value = "Descricao";
                worksheet.Cell(currentRow, 3).Value = "Código protheus do grupo";
                worksheet.Cell(currentRow, 4).Value = "Grupo";
                int cont = 0;
                for (int i = 0; i < 3; i++)
                {
                    worksheet.Cell(1, 7 + cont).Value = "Orçamento " + (i + 1);

                    worksheet.Cell(2, 7 + cont).Value = orcamentos[i].Fornecedor;
                    worksheet.Cell(currentRow, 7 + cont).Value = "Valor unitário";
                    worksheet.Cell(currentRow, 8 + cont).Value = "Quantidade";
                    worksheet.Cell(currentRow, 9 + cont).Value = "Total Item";
                    worksheet.Cell(currentRow, 10 + cont).Value = "Ipi";
                    worksheet.Cell(currentRow, 11 + cont).Value = "Icms";
                    worksheet.Cell(currentRow, 12 + cont).Value = "Desconto";
                    worksheet.Cell(currentRow, 13 + cont).Value = "Valor do frete";
                    cont += 8;
                }

                foreach (var item in planilhas)
                {
                    currentRow++;
                    cont = 0;
                    worksheet.Cell(currentRow, 1).Value = string.Concat("0000000", item.Produto.CodigoProtheus);
                    worksheet.Cell(currentRow, 2).Value = item.Produto.Descricao;
                    worksheet.Cell(currentRow, 3).Value = item.Produto.Grupo.CodigoProtheus;
                    worksheet.Cell(currentRow, 4).Value = item.Produto.Grupo.Descricao;
                    for (int i = 0; i < item.ProdutoPedidoOrcamentosList.Count; i++)
                    {

                        worksheet.Cell(currentRow, 7 + cont).Value = item.ProdutoPedidoOrcamentosList[i].valor;
                        worksheet.Cell(currentRow, 8 + cont).Value = item.ProdutoPedidoOrcamentosList[i].Quantidade;
                        worksheet.Cell(currentRow, 9 + cont).Value = item.ProdutoPedidoOrcamentosList[i].TotalItem;
                        worksheet.Cell(currentRow, 10 + cont).Value = item.ProdutoPedidoOrcamentosList[i].Ipi;
                        worksheet.Cell(currentRow, 11 + cont).Value = item.ProdutoPedidoOrcamentosList[i].Icms;
                        worksheet.Cell(currentRow, 12 + cont).Value = item.ProdutoPedidoOrcamentosList[i].Desconto;
                        worksheet.Cell(currentRow, 13 + cont).Value = orcamentos[i].ValorFrete;
                        worksheet.Cell(item.ProdutoPedidoOrcamentosList.Count + 3, 7 + cont).Value = "Valor Final";
                        worksheet.Cell(item.ProdutoPedidoOrcamentosList.Count + 4, 7 + cont).Value = orcamentos[i].ValorTotal;
                        cont += 8;
                    }
                }
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    data.planilha = content;
                }
            }
            await this._mailer.SendAsync(new MailServices(data));
            return Ok();
        }
        [HttpGet("teste/{idSolicitacao}")]
        public IEnumerable<Planilha> getBacon(long idSolicitacao)
        {
            return _daoProdutoCompras.GetDadosProdutoBySolicitacao(idSolicitacao);
        }

        [HttpPost("download/email/{idSolicitacao}")]
        public async Task<IActionResult> downloadEmail([FromBody] EmailModel dados, long idSolicitacao)
        {
            List<Orcamento> orcamentos = (List<Orcamento>)_daoOrcamento.GetOrcamentoBySolicitacao(idSolicitacao);
            List<ProdutoPedidoOrcamento> produtosCompras = (List<ProdutoPedidoOrcamento>)_daoProdutoCompras.GetSolicitacao(idSolicitacao);
            EmailModel data = _dao.GetDadosSolicitacao(idSolicitacao);
            data.CentroResponsabilidade = dados.CentroResponsabilidade;
            data.ClasseValor = dados.ClasseValor;
            data.ContaContabil = dados.ContaContabil;
            data.CodUnidadeOrganizacional = dados.CodUnidadeOrganizacional;
            data.UnidadeOrganizacional = dados.UnidadeOrganizacional;
            var find = orcamentos.Min(r => r.ValorTotal);
            data.Orcamento = orcamentos.Find(r => r.ValorTotal == find);
            data.Responsaveis = (List<Responsavel>)_daoResponsavel.GetBySolicitacao(idSolicitacao);
            data.Ocupacoes = _daoOcupacao.GetBySolicitacao(idSolicitacao);
            string message = await this._mailer.RenderAsync(new DownloadEmail(data));
            var Renderer = new IronPdf.HtmlToPdf();
            var PDF = Renderer.RenderHtmlAsPdf(message);
            var path = Path.Combine($@"{Directory.GetCurrentDirectory()}\EmailPdf", $"emailSolicitacao{DateTime.Now}.pdf");
            return File(PDF.BinaryData, "application/pdf", path);
        }
    }
}
