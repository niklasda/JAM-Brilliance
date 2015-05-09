using JetBrains.Annotations;

namespace JAM.Core.Models.Hard
{
    public class Referrer
    {
        public Referrer(int referrerId, string name, bool isEnabled)
        {
            ReferrerId = referrerId;
            Name = name;
            IsEnabled = isEnabled;
        }

        [UsedImplicitly]
        public int ReferrerId { get; set; }

        public string Name { get; set; }

        public bool IsEnabled { get; set; }
    }
}