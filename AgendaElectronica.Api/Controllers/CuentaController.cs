using AgendaElectronica.Api.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgendaElectronica.Api.Controllers
{
    public class CuentaController : ControllerBase
    {
        private readonly ILogger<AgendaController> _logger;
        private readonly IMediator _mediator;
        public CuentaController(ILogger<AgendaController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult<string>> Post(CuentaInsert.CuentaInsertCommand parameter)
        {
            try
            {
                var result = await _mediator.Send(parameter);
                if (result)
                {
                    return Ok("Se agrego correctamente el usuario.");
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

        [HttpPost("ObtenerToken")]
        public async Task<ActionResult<string>> Post([FromBody]TokenSelect.TokenSelectCommand parameter)
        {
            try
            {
                var result = await _mediator.Send(parameter);
                if (!string.IsNullOrEmpty(result.Token))
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("No se pudo crer el token correctamente, intentelo de nuevo o consulte al administrador de sistemas para tener mas detalle.", parameter);
                    return BadRequest("No se pudo crer el token correctamente, intentelo de nuevo o consulte al administrador de sistemas para tener mas detalle.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, parameter);
                return BadRequest("No se pudo procesar correctamente la solicitud, intentelo de nuevo o consulte al administrador de sistemas para tener mas detalle.");

            }
        }
    }
}
