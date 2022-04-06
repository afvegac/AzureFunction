using System;
using System.Xml;
using System.Collections.Generic;
using TimerTriggerGoSocket.Common.Dto;
using TimerTriggerGoSocket.Common.Constants;
using TimerTtriggerGoSocket.DataAccess.DAO;

namespace TimerTtriggerGoSocket.Service.Service
{
    public interface IReadXmlService
    {
        List<ResultXmlDto> Read();
    }

    public class ReadXmlService : IReadXmlService
    {
        private readonly IXmlFilesDAO _xmlFilesDAO;
        public ReadXmlService(IXmlFilesDAO xmlFilesDAO) => _xmlFilesDAO = xmlFilesDAO;
        public List<ResultXmlDto> Read()
        {
            var resultList = new List<ResultXmlDto>();
            var listXmlDto = _xmlFilesDAO.GetAll();

            foreach (var XmlDto in listXmlDto)     
                resultList.Add(ProcessNodes(XmlDto.Xml));

            return resultList;
        }
        public ResultXmlDto ProcessNodes(string xmlString)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            var areasSalarys = new List<string>();
            int AreaNodesMoreTwoEmployees = 0;
            var areaNodes = xmlDoc.GetElementsByTagName(XmlFile.Area).Count;

            foreach (XmlNode n1 in xmlDoc.DocumentElement.ChildNodes)
            {
                var areaName = string.Empty;
                decimal salaryTotal = 0;

                foreach (XmlNode n2 in n1.ChildNodes)
                    switch (n2.Name)
                    {
                        case XmlFile.NameArea:
                            areaName = n2.InnerText;
                            break;
                        case XmlFile.Employees:
                            if (n2.ChildNodes.Count > 2)
                                AreaNodesMoreTwoEmployees++;

                            foreach (XmlNode n3 in n2.ChildNodes)
                                salaryTotal += Convert.ToDecimal(n3.Attributes[XmlFile.Salary].Value);

                            areasSalarys.Add($"{areaName}|{salaryTotal}");
                            break;

                    }
            }

            return new ResultXmlDto()
            {
                AreaNodes = areaNodes,
                AreaNodesMoreTwoEmployees = AreaNodesMoreTwoEmployees,
                ValueArea = areasSalarys
            };
        }
    }
}