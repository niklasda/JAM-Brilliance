using JetBrains.Annotations;

namespace JAM.Core.Models.Hard
{
    public class WantedKidWantedCount
    {
        public WantedKidWantedCount(int kidCountId, string name, bool isEnabled)
        {
            WantedKidWantedCountId = kidCountId;
            Name = name;
            IsEnabled = isEnabled;
        }

        [UsedImplicitly]
        public int WantedKidWantedCountId { get; set; }

        public string Name { get; set; }

        public bool IsEnabled { get; set; }
    }
}