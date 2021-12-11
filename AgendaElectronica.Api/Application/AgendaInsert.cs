using AgendaElectronica.Api.Class;
using AgendaElectronica.Api.Dto;
using AgendaElectronica.Api.Model;
using AgendaElectronica.Api.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgendaElectronica.Api.Application
{
    public class AgendaInsert
    {
        public class AgendaInsertCommand : IRequest<RespuestaAgenda>
        {
            public AgendaDto AgendaDto { get; set; }
        }

        public class AgendaInsertHandler : IRequestHandler<AgendaInsertCommand, RespuestaAgenda>
        {
            private readonly ApplicationDBContext _aplicationDBContext;
            private readonly IMapper _mapper;

            public AgendaInsertHandler(ApplicationDBContext applicationDBContext, IMapper mapper)
            {
                _aplicationDBContext = applicationDBContext;
                _mapper = mapper;
            }
            public async Task<RespuestaAgenda> Handle(AgendaInsertCommand request, CancellationToken cancellationToken)
            {
                RespuestaAgenda response = new RespuestaAgenda();

                var existEmail = await _aplicationDBContext.Agenda.Where(n => n.Email.ToUpper() == request.AgendaDto.Email).ToListAsync();
                if (existEmail.Any())
                {
                    response.EsCorrecto = false;
                    response.Agenda = new AgendaDto();
                    response.Mensaje = "El email del usuario ya existe en la base de datos.";

                    throw new Exception("El email del usuario ya existe en la base de datos.");
                }

                var agenda = _mapper.Map<AgendaDto, Agenda>(request.AgendaDto);

                _aplicationDBContext.Agenda.Add(agenda);
                var result = await _aplicationDBContext.SaveChangesAsync();

                if (result <= 0)
                {
                    response.EsCorrecto = false;
                    response.Agenda = new AgendaDto();
                    response.Mensaje = "No se pudo agregar correctamente el usuario a la agenda.";

                    throw new Exception("No se pudo agregar correctamente el usuario a la agenda.");
                }

                var agendaDto = _mapper.Map<Agenda, AgendaDto>(agenda);
                response.EsCorrecto = true;
                response.Mensaje = "OK";
                response.Agenda = agendaDto;

                return response;
            }

        }
    }
}
