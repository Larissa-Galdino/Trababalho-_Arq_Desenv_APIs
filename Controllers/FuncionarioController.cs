using Microsoft.AspNetCore.Mvc;
using TrabalhoApi.Models;
using TrabalhoApi.Services;
using TrabalhoApi.DTOs;
using System.Text.Json;

namespace TrabalhoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioService _service;

        public FuncionarioController(FuncionarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_service.ListarTodos());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var f = _service.BuscarPorId(id);
            return f == null ? NotFound(new { mensagem = "Funcionário não encontrado." }) : Ok(f);
        }

        [HttpPost]
        public IActionResult Post([FromBody] JsonElement body)
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (body.ValueKind == JsonValueKind.Array)
                {
                    var dtos = JsonSerializer.Deserialize<List<FuncionarioDTO>>(body.GetRawText(), options);
                    if (dtos == null || !dtos.Any()) return BadRequest(new { erro = "Lista vazia." });

                    var funcionarios = dtos.Select(d => new Funcionario
                    {
                        Nome = d.Nome,
                        Cargo = d.Cargo,
                        Departamento = d.Departamento,
                        Salario = d.Salario,
                        DataAdmissao = d.DataAdmissao,
                        EmpresaId = d.EmpresaId
                    }).ToList();

                    foreach (var f in funcionarios)
                    {
                        if (!TryValidateModel(f)) return BadRequest(ModelState);
                    }

                    _service.CriarEmLote(funcionarios);
                    return Ok(new { mensagem = $"{funcionarios.Count} funcionários cadastrados com sucesso!" });
                }
                else
                {
                    var dto = JsonSerializer.Deserialize<FuncionarioDTO>(body.GetRawText(), options);
                    if (dto == null) return BadRequest(new { erro = "Dados inválidos." });

                    var f = new Funcionario
                    {
                        Nome = dto.Nome,
                        Cargo = dto.Cargo,
                        Departamento = dto.Departamento,
                        Salario = dto.Salario,
                        DataAdmissao = dto.DataAdmissao,
                        EmpresaId = dto.EmpresaId
                    };

                    if (!TryValidateModel(f)) return BadRequest(ModelState);

                    _service.Criar(f);
                    return CreatedAtAction(nameof(GetById), new { id = f.Id }, f);
                }
            }
            // TRATAMENTO ESPECÍFICO PARA ERRO DE FORMATAÇÃO (JSON/SALÁRIO)
            catch (JsonException jEx)
            {
                return BadRequest(new
                {
                    erro = "Erro na formatação do JSON.",
                    detalhe = "Verifique se os números (Salário) estão usando ponto (.) em vez de vírgula (,).",
                    tecnico = jEx.Message
                });
            }
            // TRATAMENTO PARA ERROS DE BANCO DE DADOS (MYSQL)
            catch (Exception ex)
            {
                var erroReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, new { erro = "Erro ao processar funcionários.", detalhe = erroReal });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FuncionarioDTO dto)
        {
            try
            {
                var fDb = _service.BuscarPorId(id);
                if (fDb == null) return NotFound(new { mensagem = "Funcionário não encontrado." });

                fDb.Nome = dto.Nome;
                fDb.Cargo = dto.Cargo;
                fDb.Departamento = dto.Departamento;
                fDb.Salario = dto.Salario;
                fDb.DataAdmissao = dto.DataAdmissao;
                fDb.EmpresaId = dto.EmpresaId;

                if (!TryValidateModel(fDb)) return BadRequest(ModelState);

                _service.Atualizar(fDb);
                return Ok(new { mensagem = "Funcionário atualizado.", dados = fDb });
            }
            catch (Exception ex)
            {
                var erroReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, new { erro = "Erro na atualização.", detalhe = erroReal });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var f = _service.BuscarPorId(id);
            if (f == null) return NotFound(new { mensagem = "Funcionário não encontrado." });

            _service.Deletar(f);
            return Ok(new { mensagem = "Funcionário removido com sucesso." });
        }
    }
}