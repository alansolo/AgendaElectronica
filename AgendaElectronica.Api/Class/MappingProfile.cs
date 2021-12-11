using AgendaElectronica.Api.Dto;
using AgendaElectronica.Api.Model;
using AutoMapper;

namespace AgendaElectronica.Api.Class
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {         
            CreateMap<AgendaDto, Agenda>();
            CreateMap<Agenda, AgendaDto>();
            //CreateMap<List<Agenda>, List<AgendaDto>>();
        }
    }
}
