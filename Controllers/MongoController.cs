using Microsoft.AspNetCore.Mvc;
using Mongo.Models;
using Mongo.Repository;
using System.Text.Json;

namespace Mongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MongoController : ControllerBase
    {
        private readonly ILogger<MongoController> _logger;
        private readonly IPessoaRepository _pessoaRepository;

        public MongoController(ILogger<MongoController> logger, IPessoaRepository pessoaRepository)
        {
            _logger = logger;
            _pessoaRepository = pessoaRepository;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetPessoa([FromQuery] string id)
        {
            _logger.LogInformation("Obter Pessoa: {0}", id);

            var pessoa = await _pessoaRepository.GetAsync(id);
            
            return Ok(pessoa);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePessoa([FromBody] Pessoa pessoa)
        {
            var pessoaString = JsonSerializer.Serialize(pessoa);
            _logger.LogInformation($"Criar Pessoa: {pessoaString}");

            await _pessoaRepository.CreateAsync(pessoa);

            var uri = $"{Request.Scheme}://{Request.Host}/mongo/get?id={1}";
            return Created(new Uri(uri), pessoa);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePessoa([FromBody] Pessoa pessoa)
        {
            var pessoaString = JsonSerializer.Serialize(pessoa);
            _logger.LogInformation($"Atualizar Pessoa: {pessoaString}");

            await _pessoaRepository.UpdateAsync(pessoa);

            return Ok(pessoa);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePessoa([FromBody] string id)
        {
            _logger.LogInformation("Remover Pessoa: {0}", id);

            await _pessoaRepository.RemoveAsync(id);

            return Ok(id);
        }
    }
}