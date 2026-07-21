using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdiomasApi.Application.Services;
using IdiomasApi.Domain.Entities;
using IdiomasApi.Domain.ValueObjects;

namespace IdiomasApi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TurmasController : ControllerBase
    {
        private readonly SecretariaService _secretariaService;
        private readonly ITurmaRepository _turmaRepository;

        public TurmasController(SecretariaService secretariaService, ITurmaRepository turmaRepository)
        {
            _secretariaService = secretariaService;
            _turmaRepository = turmaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarTurmaRequest request)
        {
            try
            {
                await _secretariaService.CriarNovaTurmaAsync(request.NumeroTurma, request.AnoLetivo, request.Nivel, request.IdiomaCurso);
                return StatusCode(201, new { mensagem = "Turma criada com sucesso na secretaria!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                await _secretariaService.FecharTurmaAsync(id);
                return Ok(new { mensagem = "Turma removida com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new {erro = ex.Message});
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> ObterTodas()
        {
            var Turmas = await _turmaRepository.ObterTodosAsync();
            return Ok(Turmas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> ObterPorIdAsync(Guid id)
        {
            var turma = await _turmaRepository.ObterComAlunosPorIdAsync(id);
            if (turma == null)
                return NotFound(new { erro = "Turma não encontrada." });

            return Ok(turma);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] CriarTurmaRequest request)
        {
            var turma = await _turmaRepository.ObterPorIdAsync(id);
            if (turma == null)
                return NotFound(new { erro = "Turma não encontrada." });

            return Ok(new { mensagem = "Dados da turma atualizados com sucesso!" });
        }

        public class CriarTurmaRequest
        {
            public string NumeroTurma {  get; set; } = string.Empty;
            public int AnoLetivo { get; set; }
            public NivelTurma Nivel {  get; set; }
            public Idioma IdiomaCurso { get; set; }
        }
    }
}