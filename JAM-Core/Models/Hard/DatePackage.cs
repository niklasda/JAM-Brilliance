namespace JAM.Core.Models.Hard
{
    public class DatePackage
    {
        public DatePackage(int datePackageId, string name, bool isEnabled)
        {
            DatePackageId = datePackageId;
            Name = name;
            IsEnabled = isEnabled;
        }

        public int DatePackageId { get; set; }

        public string Name { get; set; }

        public bool IsEnabled { get; set; }
    }
}