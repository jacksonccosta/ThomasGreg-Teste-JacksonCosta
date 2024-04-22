using ThomasGregTest.Application;
using ThomasGregTest.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ThomasGregTest.API.Controller.User
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UsuarioController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost, AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(typeof(ResultadoEventos), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] AutenticacaoUsuarioQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost, AllowAnonymous]
        [Route("refreshToken")]
        [ProducesResponseType(typeof(ResultadoEventos), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RefreshToken([FromBody] ValidaUsuarioRefreshTokenQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost, AllowAnonymous]
        [ProducesResponseType(typeof(ResultadoEventos), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarUsuarioQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResultadoEventos), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarUsuarioQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResultadoEventos), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Selecionar(int id)
        {
            try
            {
                var response = await _mediator.Send(new SelecionarUsuarioQuery(id));
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("listarUsuarios")]
        [ProducesResponseType(typeof(List<UsuarioResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var response = await _mediator.Send(new ListarUsuarioQuery());
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("obterUsuarioLogado")]
        [ProducesResponseType(typeof(ResultadoEventos), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtemLogado()
        {
            try
            {
                var response = await _mediator.Send(new ObterUsuarioLogadoQuery());
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
