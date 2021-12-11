using AgendaElectronica.Api.Dto;
using AgendaElectronica.Api.Model;
using AgendaElectronica.Api.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgendaElectronica.Api.Application
{
    public class AgendaSelect
    {
        public class AgendaGetCommand: IRequest<List<AgendaDto>>
        {
            public int? Id { get; set; }
        }

        public class AgendaGetHandler : IRequestHandler<AgendaGetCommand, List<AgendaDto>>
        {
            private readonly ApplicationDBContext _aplicationDBContext;
            private readonly IMapper _mapper;

            public AgendaGetHandler(ApplicationDBContext applicationDBContext, IMapper mapper)
            {
                _aplicationDBContext = applicationDBContext;
                _mapper = mapper;
            }

            public async Task<List<AgendaDto>> Handle(AgendaGetCommand request, CancellationToken cancellationToken)
            {
                List<Agenda> ListAgenda = new List<Agenda>();
                List<AgendaDto> ListAgendaDto = new List<AgendaDto>();

                if(request.Id == null)
                {
                    ListAgenda = await _aplicationDBContext.Agenda.ToListAsync();
                    ListAgendaDto = _mapper.Map<List<Agenda>, List<AgendaDto>>(ListAgenda);
                }
                else
                {
                    ListAgenda = await _aplicationDBContext.Agenda.Where(n => n.Id == request.Id).ToListAsync();
                    ListAgendaDto = _mapper.Map<List<Agenda>, List<AgendaDto>>(ListAgenda);
                }

                return ListAgendaDto;
            }

        }
    }
}
