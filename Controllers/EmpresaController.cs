using Microsoft.AspNetCore.Mvc;
using TrabalhoApi.Models;
using TrabalhoApi.Services;
using TrabalhoApi.DTOs;
using System.Text.Json;

namespace TrabalhoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService _service;

        public EmpresaController(EmpresaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_service.ListarTodas());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var e = _service.BuscarPorId(id);
            if (e == null) return NotFound(new { mensagem = "Empresa não encontrada em nossa base de dados." });
            return Ok(e);
        }

        [HttpPost]
        public IActionResult Post([FromBody] JsonElement body)
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (body.ValueKind == JsonValueKind.Array)
                {
                    var dtos = JsonSerializer.Deserialize<List<EmpresaDTO>>(body.GetRawText(), options);
                    if (dtos == null || !dtos.Any()) return BadRequest(new { erro = "A lista enviada está vazia." });

                    var empresas = dtos.Select(d => new Empresa
                    {
                        Nome = d.Nome,
                        Cnpj = d.Cnpj,
                        Setor = d.Setor,
                        Endereco = d.Endereco,
                        DataFundacao = d.DataFundacao
                    }).ToList();

                    // Validação em lote
                    foreach (var e in empresas)
                    {
                        if (!TryValidateModel(e)) return BadRequest(ModelState);
                    }

                    _service.CriarEmLote(empresas);
                    return Ok(new { mensagem = $"{empresas.Count} empresas cadastradas com sucesso!" });
                }
                else
                {
                    var dto = JsonSerializer.Deserialize<EmpresaDTO>(body.GetRawText(), options);
                    if (dto == null) return BadRequest(new { erro = "Dados da empresa não informados corretamente." });

                    var e = new Empresa
                    {
                        Nome = dto.Nome,
                        Cnpj = dto.Cnpj,
                        Setor = dto.Setor,
                        Endereco = dto.Endereco,
                        DataFundacao = dto.DataFundacao
                    };

                    if (!TryValidateModel(e)) return BadRequest(ModelState);

                    _service.Criar(e);
                    return CreatedAtAction(nameof(GetById), new { id = e.Id }, e);
                }
            }
            catch (Exception ex)
            {
                // AJUSTE AQUI: Pega o erro real do banco de dados (InnerException)
                var erroReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, new { erro = "Erro ao salvar no banco de dados.", detalhe = erroReal });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EmpresaDTO dto)
        {
            try
            {
                var eDb = _service.BuscarPorId(id);
                if (eDb == null) return NotFound(new { mensagem = "Não foi possível atualizar: Empresa não encontrada." });

                eDb.Nome = dto.Nome;
                eDb.Cnpj = dto.Cnpj;
                eDb.Setor = dto.Setor;
                eDb.Endereco = dto.Endereco;
                eDb.DataFundacao = dto.DataFundacao;

                if (!TryValidateModel(eDb)) return BadRequest(ModelState);

                _service.Atualizar(eDb);
                return Ok(new { mensagem = "Dados da empresa atualizados com sucesso.", dados = eDb });
            }
            catch (Exception ex)
            {
                var erroReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, new { erro = "Erro ao tentar atualizar a empresa.", detalhe = erroReal });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var e = _service.BuscarPorId(id);
            if (e == null) return NotFound(new { mensagem = "Exclusão cancelada: Empresa não encontrada." });

            _service.Deletar(e);
            return Ok(new { mensagem = "Empresa removida com sucesso de nossa base." });
        }
    }
}