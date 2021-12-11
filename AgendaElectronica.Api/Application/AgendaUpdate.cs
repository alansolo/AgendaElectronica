using AgendaElectronica.Api.Class;
using AgendaElectronica.Api.Dto;
using AgendaElectronica.Api.Model;
using AgendaElectronica.Api.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgendaElectronica.Api.Application
{
    public class AgendaUpdate
    {
        public class AgendaUpdateCommand : IRequest<RespuestaAgenda>
        {
            public int Id { get; set; }
            public AgendaDto AgendaDto { get; set; }
        }

        public class AgendaUpdateHandler : IRequestHandler<AgendaUpdateCommand, RespuestaAgenda>
        {
            private readonly ApplicationDBContext _aplicationDBContext;
            private readonly IMapper _mapper;

            public AgendaUpdateHandler(ApplicationDBContext applicationDBContext, IMapper mapper)
            {
                _aplicationDBContext = applicationDBContext;
                _mapper = mapper;
            }

            public async Task<RespuestaAgenda> Handle(AgendaUpdateCommand request, CancellationToken cancellationToken)
            {
                RespuestaAgenda response = new RespuestaAgenda();
                Agenda Agenda = new Agenda();
                Agenda AgendaNew = new Agenda();

                if (request.Id <= 0)
                {
                    response.EsCorrecto = false;
                    response.Agenda = request.AgendaDto;
                    response.Mensaje = "No se agrego informacion para el Id, intentalo de nuevo.";

                    return response;
                }

                Agenda = await _aplicationDBContext.Agenda.Where(n => n.Id == request.Id).FirstOrDefaultAsync();

                if(Agenda == null)
                {
                    response.EsCorrecto = false;
                    response.Agenda = request.AgendaDto;
                    response.Mensaje = "No se encontro informacion con el Id proporcionado, intentalo de nuevo.";

                    return response;
                }

                AgendaNew = _mapper.Map<AgendaDto, Agenda>(request.AgendaDto);
                Agenda.Name = AgendaNew.Name;
                Agenda.LastName = AgendaNew.LastName;
                Agenda.MiddleName = AgendaNew.MiddleName;
                Agenda.Gender = AgendaNew.Gender;
                Agenda.Telephone = AgendaNew.Telephone;
                Agenda.Mobile = AgendaNew.Mobile;
                Agenda.Email = AgendaNew.Email;

                _aplicationDBContext.Agenda.Update(Agenda);
                var result = await _aplicationDBContext.SaveChangesAsync();

                if (result <= 0)
                {
                    response.EsCorrecto = false;
                    response.Agenda = request.AgendaDto;
                    response.Mensaje = "No se pudo actualizar correctamente el usuario en la agenda.";

                    throw new Exception("No se pudo actualizar correctamente el usuario en la agenda.");
                }

                response.EsCorrecto = true;
                response.Mensaje = "OK";
                response.Agenda = request.AgendaDto;
                return response;
            }

        }
    }
}
