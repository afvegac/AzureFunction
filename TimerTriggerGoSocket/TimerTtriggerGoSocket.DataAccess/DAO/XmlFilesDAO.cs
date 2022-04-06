using System.Linq;
using System.Collections.Generic;
using TimerTriggerGoSocket.Common.Dto;
using TimerTtriggerGoSocket.DataAccess.Configuration;

namespace TimerTtriggerGoSocket.DataAccess.DAO
{
    public interface IXmlFilesDAO
    {
        List<XmlFilesDto> GetAll();
    }
    public class XmlFilesDAO : IXmlFilesDAO
    {
        private readonly GoSocketDbContext _contexto;
        public XmlFilesDAO(GoSocketDbContext contexto)
        {
            _contexto = contexto;
        }
        public List<XmlFilesDto> GetAll()
        {
            var entity = _contexto.XmlFiles;
            return entity.Select( x => new XmlFilesDto
            {
                Id = x.Id,
                Xml = x.Xml
            }).ToList();
        }
        
    }
}