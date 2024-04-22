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
    public class LogradouroController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(ResultadoEventos), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarLogradouroQuery query)
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
        public async Task<IActionResult> Atualizar([FromBody] AtualizarLogradouroQuery query)
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
        [Route("{clienteId}/{id}")]
        [ProducesResponseType(typeof(ResultadoEventos), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Selecionar(int clienteId, int id)
        {
            try
            {
                var response = await _mediator.Send(new SelecionarLogradouroQuery(id, clienteId));
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("{clienteId}/{id}")]
        [ProducesResponseType(typeof(ResultadoEventos), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Excluir(int clienteId, int id)
        {
            try
            {
                var response = await _mediator.Send(new ExcluirLogradouroQuery(id, clienteId));
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("listarLogradouros")]
        [ProducesResponseType(typeof(List<LogradouroResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Listar([FromBody] ListarLogradouroQuery query)
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
    }
}
