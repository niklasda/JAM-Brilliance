using System.Collections.Generic;
using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface IVisitorDataService
    {
        IEnumerable<HistoryEntry> GetVisitors(int surveyId);

        void AddVisitor(HistoryEntry he);

        IEnumerable<HistoryEntry> GetVisits(int surveyId);

        int GetVisitorsCount(int surveyId);
    }
}