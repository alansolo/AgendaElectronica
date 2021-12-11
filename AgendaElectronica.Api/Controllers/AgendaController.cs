using AgendaElectronica.Api.Application;
using AgendaElectronica.Api.Class;
using AgendaElectronica.Api.Dto;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaElectronica.Api.Controllers
{
    [ApiController]
    [Route("api/Agenda")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AgendaController : ControllerBase
    {
        private readonly ILogger<AgendaController> _logger;
        private readonly IMediator _mediator;

        public AgendaController(ILogger<AgendaController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("ObtenerAgenda")]
        public async Task<ActionResult<List<AgendaDto>>> Get()
        {
            int? id = null;

            try
            {
                var result = await _mediator.Send(new AgendaSelect.AgendaGetCommand { Id = id });
                if (result.Count <= 0)
                {
                    return NotFound("No se encontro informacion con el parametro proporcionado.");
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest("No se pudo procesar correctamente la solicitud, intentelo de nuevo o consulte al administrador de sistemas para tener mas detalle.");
            }
        }

        [HttpGet("ObtenerAgenda/{id}")]
        public async Task<ActionResult<List<AgendaDto>>> Get(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Se debe agregar la informacion necesaria para realizar la solicitud.");
                }

                AgendaSelect.AgendaGetCommand parameter = new AgendaSelect.AgendaGetCommand();
                parameter.Id = id;
                var result = await _mediator.Send(parameter);
                if(result.Count <= 0)
                {
                    return NotFound("No se encontro informacion con el parametro proporcionado.");
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message, "Id: " + id);
                return BadRequest("No se pudo procesar correctamente la solicitud, intentelo de nuevo o consulte al administrador de sistemas para tener mas detalle.");
            }
        }

        [HttpPost("AgregarAgenda")]
        public async Task<ActionResult<RespuestaAgenda>> Post(AgendaInsert.AgendaInsertCommand parameter)
        {
            try
            {
                var result = await _mediator.Send(parameter);
                if (result.EsCorrecto)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("No se pudo procesar correctamente la solicitud, intentelo de nuevo o consulte al administrador de sistemas para tener mas detalle.", parameter);
                    return BadRequest("No se pudo procesar correctamente la solicitud, intentelo de nuevo o consulte al administrador de sistemas para tener mas detalle.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, parameter);
                return BadRequest("No se pudo procesar correctamente la solicitud, intentelo de nuevo o consulte al administrador de sistemas para tener mas detalle.");
            }
        }

        [HttpPut("EditarAgenda/{id}")]
        public async Task<ActionResult<RespuestaAgenda>> Put(int id, AgendaUpdate.AgendaUpdateCommand parameter)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Se debe agregar la informacion necesaria para realizar la solicitud.");
                }

                parameter.Id = id;
                var result = await _mediator.Send(parameter);
                if (result.EsCorrecto)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No se encontro el usuario en la agenda con el Id especificado, asegurese de que esta agregando correctamente el usuario correcto.");
                }

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message, parameter);
                return BadRequest("No se pudo procesar correctamente la solicitud, intentelo de nuevo o consulte al administrador de sistemas para tener mas detalle.");
            }
        }

        [HttpDelete("EliminarAgenda/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Se debe agregar la informacion necesaria para realizar la solicitud.");
                }

                AgendaDelete.AgendaDeleteCommand parameter = new AgendaDelete.AgendaDeleteCommand();
                parameter.Id = id;
                var result = await _mediator.Send(parameter);

                if (result)
                {
                    return Ok("Se elimino correctamente la informacion en la Agenda.");
                }
                else
                {
                    return NotFound("No se encontro el usuario en la agenda con el Id especificado, asegurese de que esta agregando correctamente el usuario correcto.");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message, "Id: " + id);
                return BadRequest("No se pudo procesar correctamente la solicitud, intentelo de nuevo o consulte al administrador de sistemas para tener mas detalle.");
            }
        }
    }
}
