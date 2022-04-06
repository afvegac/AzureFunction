using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using TimerTtriggerGoSocket.Service.Service;

namespace TimerTriggerGoSocket
{
    public class ReadXmlFunction
    {
        private readonly IReadXmlService _readXmlService;
        public ReadXmlFunction(IReadXmlService readXmlService)
        {
            _readXmlService = readXmlService;
        }
        [FunctionName("ReadXmlFunction")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var resultList = _readXmlService.Read();
            foreach (var resultXml in resultList)
            {
                log.LogInformation($"Cantidad de nodos tipo área: {resultXml.AreaNodes}");
                log.LogInformation($"Cantidad de nodos tipo área con mas de 2 empleados: {resultXml.AreaNodesMoreTwoEmployees}");
                foreach (var area in resultXml.ValueArea)
                    log.LogInformation(area);
            }
        }
    }
}
