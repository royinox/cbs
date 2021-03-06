using Concepts;
using Concepts.DataCollector;
using Dolittle.Events.Processing;
using Events;
using Concepts.CaseReport;

namespace Read.CaseReports
{
    public class CaseReportEventProcessor : ICanProcessEvents
    {
        readonly ICaseReports _caseReports;
        readonly ICaseReportsFromUnknownDataCollectors _caseReportsFromUnknownDataCollectors;

        public CaseReportEventProcessor(
            ICaseReports caseReports,
            ICaseReportsFromUnknownDataCollectors caseReportsFromUnknownDataCollectors)
        {
            _caseReports = caseReports;
            _caseReportsFromUnknownDataCollectors = caseReportsFromUnknownDataCollectors;
        }
        
        public void Process(CaseReportReceived @event)
        {
            var caseReport = new CaseReport(@event.CaseReportId)
            {
                DataCollectorId = @event.DataCollectorId,
                HealthRiskId = @event.HealthRiskId,
                NumberOfFemalesUnder5 = @event.NumberOfFemalesUnder5,
                NumberOfFemalesAged5AndOlder = @event.NumberOfFemalesAged5AndOlder,
                NumberOfMalesUnder5 = @event.NumberOfMalesUnder5,
                NumberOfMalesAged5AndOlder = @event.NumberOfMalesAged5AndOlder,
                Location = new Location(@event.Latitude, @event.Longitude),
                Timestamp = @event.Timestamp,
                Message = @event.Message
            };
            _caseReports.Update(caseReport);
        }
        public void Process(CaseReportFromUnknownDataCollectorReceived @event)
        {
            // Save CaseReport in the CaseReportsFromUnkown... DB
            var caseReport = new CaseReportFromUnknownDataCollector(@event.CaseReportId)
            {
                Origin = @event.Origin,
                HealthRiskId = @event.HealthRiskId,
                NumberOfFemalesUnder5 = @event.NumberOfFemalesUnder5,
                NumberOfFemalesAged5AndOlder = @event.NumberOfFemalesAged5AndOlder,
                NumberOfMalesUnder5 = @event.NumberOfMalesUnder5,
                NumberOfMalesAged5AndOlder = @event.NumberOfMalesAged5AndOlder,
                Timestamp = @event.Timestamp,
                Message = @event.Message
            };
            _caseReportsFromUnknownDataCollectors.Update(caseReport);
        }   
        
        public void Process(CaseReportIdentified @event)
        {
            _caseReportsFromUnknownDataCollectors.Delete(e => e.Id == (CaseReportId)@event.CaseReportId);            
        }
    }
}
