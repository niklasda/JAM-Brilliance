using JetBrains.Annotations;

namespace JAM.Core.Models.Hard
{
    public class KidWantedCount
    {
        public KidWantedCount(int kidCountId, string name, bool isEnabled)
        {
            KidWantedCountId = kidCountId;
            Name = name;
            IsEnabled = isEnabled;
        }

        [UsedImplicitly]
        public int KidWantedCountId { get; set; }

        public string Name { get; set; }

        public bool IsEnabled { get; set; }
    }
}