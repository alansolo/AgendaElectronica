using AgendaElectronica.Api.Dto;

namespace AgendaElectronica.Api.Class
{
    public class RespuestaAgenda
    {
        public bool EsCorrecto { get; set; }
        public string Mensaje { get; set; }
        public AgendaDto Agenda { get; set; }
    }
}
