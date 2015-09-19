using System;

namespace JAM.Models.ViewModels
{
    public class SurveyViewModelBase
    {
        public int SurveyId { get; set; }

        public bool IsReadOnly { get; set; }
    }
}