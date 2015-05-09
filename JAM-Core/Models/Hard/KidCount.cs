using JetBrains.Annotations;

namespace JAM.Core.Models.Hard
{
    public class KidCount
    {
        public KidCount(int kidCountId, string name, bool isEnabled)
        {
            KidCountId = kidCountId;
            Name = name;
            IsEnabled = isEnabled;
        }

        [UsedImplicitly]
        public int KidCountId { get; set; }

        public string Name { get; set; }

        public bool IsEnabled { get; set; }
    }
}