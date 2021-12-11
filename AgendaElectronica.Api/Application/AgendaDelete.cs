using AgendaElectronica.Api.Model;
using AgendaElectronica.Api.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgendaElectronica.Api.Application
{
    public class AgendaDelete
    {
        public class AgendaDeleteCommand : IRequest<bool>
        {
            public int Id { get; set; }
        }

        public class AgendaDeleteHandler : IRequestHandler<AgendaDeleteCommand, bool>
        {
            private readonly ApplicationDBContext _aplicationDBContext;
            private readonly IMapper _mapper;

            public AgendaDeleteHandler(ApplicationDBContext applicationDBContext, IMapper mapper)
            {
                _aplicationDBContext = applicationDBContext;
                _mapper = mapper;
            }

            public async Task<bool> Handle(AgendaDeleteCommand request, CancellationToken cancellationToken)
            {
                Agenda Agenda = new Agenda();

                if (request.Id <= 0)
                {
                    return false;
                }

                Agenda = await _aplicationDBContext.Agenda.Where(n => n.Id == request.Id).FirstOrDefaultAsync();

                if (Agenda == null)
                {
                    return false;
                }

                _aplicationDBContext.Agenda.Remove(Agenda);
                var result = await _aplicationDBContext.SaveChangesAsync();

                if (result <= 0)
                {
                    throw new Exception("No se pudo actualizar correctamente el usuario en la agenda.");
                }

                return true;
            }

        }
    }
}
