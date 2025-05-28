using Microsoft.AspNetCore.Mvc;
using Sprint03.Domain.Entities;
using Sprint03.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint03.Presentation.Controllers
{
    [ApiController]
    [Route("api/nomeusuarios")]
    public class NomeUsuarioController : ControllerBase
    {
        private readonly NomeUsuarioRepository _repository;

        public NomeUsuarioController(NomeUsuarioRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtém todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obtém todos os usuários", Description = "Retorna uma lista de usuários cadastrados.")]
        [SwaggerResponse(200, "Retorna a lista de usuários", typeof(List<NomeUsuario>))]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Dados do usuário.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém um usuário pelo ID", Description = "Retorna os dados de um usuário específico.")]
        [SwaggerResponse(200, "Retorna o usuário", typeof(NomeUsuario))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(user);
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="user">Dados do usuário.</param>
        /// <returns>Usuário cadastrado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo usuário", Description = "Adiciona um novo usuário ao banco de dados.")]
        [SwaggerResponse(201, "Usuário criado com sucesso", typeof(NomeUsuario))]
        public async Task<IActionResult> Create([FromBody] NomeUsuario user)
        {
            await _repository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <param name="user">Dados atualizados.</param>
        /// <returns>Resposta de sucesso ou erro.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza um usuário", Description = "Modifica os dados de um usuário existente.")]
        [SwaggerResponse(200, "Usuário atualizado com sucesso")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<IActionResult> Update(int id, [FromBody] NomeUsuario user)
        {
            var existingUser = await _repository.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound("Usuário não encontrado.");

            user.Id = id;
            await _repository.UpdateUserAsync(user);
            return Ok("Usuário atualizado com sucesso.");
        }

        /// <summary>
        /// Remove um usuário do sistema.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Resposta de sucesso ou erro.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Exclui um usuário", Description = "Remove um usuário do banco de dados.")]
        [SwaggerResponse(200, "Usuário removido com sucesso")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            await _repository.DeleteUserAsync(id);
            return Ok("Usuário removido com sucesso.");
        }
    }
}
