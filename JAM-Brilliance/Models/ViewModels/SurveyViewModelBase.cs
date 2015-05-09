using System;

namespace JAM.Brilliance.Models.ViewModels
{
    public class SurveyViewModelBase
    {
        public int SurveyId { get; set; }

        public bool IsReadOnly { get; set; }
    }
}