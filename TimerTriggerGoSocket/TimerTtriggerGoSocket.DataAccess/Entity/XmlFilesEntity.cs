using System.ComponentModel.DataAnnotations;

namespace TimerTtriggerGoSocket.DataAccess.Entity
{
    public class XmlFilesEntity
    {
        [Key()]
        public int Id { get; set; }
        public string Xml { get; set; }
    }
}