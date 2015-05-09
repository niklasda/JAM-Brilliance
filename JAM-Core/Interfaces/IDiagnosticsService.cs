namespace JAM.Core.Interfaces
{
    public interface IDiagnosticsService
    {
        bool IsDatabaseOk();
        
        bool IsAuthenticationOk();
        
        void ThrowUnlessSurveyComplete();
    }
}