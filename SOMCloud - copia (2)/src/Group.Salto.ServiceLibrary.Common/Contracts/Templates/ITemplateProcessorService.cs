using Group.Salto.ServiceLibrary.Common.Mobility.Dto.TemplateProcessor;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Templates
{
    public interface ITemplateProcessorService
    {
        string ProcessTemplate(TemplateProcessorValuesDto templateProcessorValues);
        string GetHtmlContent(List<Tuple<string, string>> content);
    }
}
