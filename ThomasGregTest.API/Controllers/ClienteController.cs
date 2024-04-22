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
    public class ClienteController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(ResultadoEventos), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarClienteQuery query)
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
        public async Task<IActionResult> Atualizar([FromBody] AtualizarClienteQuery query)
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
                var response = await _mediator.Send(new SelecionarClienteQuery(id));
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResultadoEventos), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var response = await _mediator.Send(new ExcluirClienteQuery(id));
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("listarClientes")]
        [ProducesResponseType(typeof(List<ClienteResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var response = await _mediator.Send(new ListarClienteQuery());
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
