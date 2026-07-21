using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdiomasApi.Application.Services;
using IdiomasApi.Domain.Entities;

namespace IdiomasApi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly SecretariaService _secretariaService;
        private readonly IAlunoRepository _alunoRepository;

        public AlunosController(SecretariaService secretariaService, IAlunoRepository alunoRepository)
        {
            _secretariaService = secretariaService;
            _alunoRepository = alunoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Matricular([FromBody] MatricularAlunoRequest request)
        {
            try
            {
                await _secretariaService.MatricularNovoAlunoAsync(
                    request.Nome,
                    request.Cpf,
                    request.Email,
                    request.Endereco,
                    request.TurmaIdInicial
                );

                return StatusCode(201, new { mensagem = "Aluno matriculado com sucesso na secretaria!" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {erro = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                await _secretariaService.CancelarCadastroAlunoAsync(id);
                return Ok(new { mensagem = "Cadastro do aluno cancelado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new {erro = ex.Message});
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new {erro = ex.Message});
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> ObterTodos()
        {
            var alunos = await _alunoRepository.ObterTodosAsync();
            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> ObterPorId(Guid id)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);
            if (aluno == null)
                return NotFound(new { erro = "Aluno não encontrado na secretaria." });

            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarAlunoRequest request)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);
            if (aluno == null)
                return NotFound(new { erro = "Aluno não encontrado" });

            return Ok(new { mensagem = "Dados do aluno atualizados com sucesso!" });
        }

        public class MatricularAlunoRequest
        {
            public string Nome { get; set; } = string.Empty;
            public string Cpf {  get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Endereco {  get; set; } = string.Empty;
            public Guid TurmaIdInicial { get; set; }
        }

        public class AtualizarAlunoRequest
        {
            public string Nome { get; set; } = string.Empty;
        }
    }
}