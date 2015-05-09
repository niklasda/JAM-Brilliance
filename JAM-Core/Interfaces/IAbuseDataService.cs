using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface IAbuseDataService
    {
        void ReportSurveyAbuse(AbuseReport abuseReport);

        void ReportPictureAbuse(AbuseReport abuseReport);

        void ReportMessageAbuse(AbuseReport abuseReport);
    }
}