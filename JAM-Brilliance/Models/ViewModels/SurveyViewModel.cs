namespace JAM.Brilliance.Models.ViewModels
{
    public class SurveyViewModel
    {
        public SurveyPage1ViewModel Page1 { get; set; }

        public SurveyPage2ViewModel Page2 { get; set; }

        public SurveyPage3ViewModel Page3 { get; set; }

        public SurveyPage4ViewModel Page4 { get; set; }

        public SurveyPage5ViewModel Page5 { get; set; }

        public SurveyPage6ViewModel Page6 { get; set; }

        public void SetAsReadOnly()
        {
            if (Page1 != null)
            {
                Page1.IsReadOnly = true;
            }

            if (Page2 != null)
            {
                Page2.IsReadOnly = true;
            }

            if (Page3 != null)
            {
                Page3.IsReadOnly = true;
            }

            if (Page4 != null)
            {
                Page4.IsReadOnly = true;
            }

            if (Page5 != null)
            {
                Page5.IsReadOnly = true;
            }

            if (Page6 != null)
            {
                Page6.IsReadOnly = true;
            }
        }
    }
}