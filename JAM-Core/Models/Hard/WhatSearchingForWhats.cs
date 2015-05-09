using JetBrains.Annotations;

namespace JAM.Core.Models.Hard
{
    public class WhatSearchingForWhat
    {
        public WhatSearchingForWhat(int searchingForId, string name, bool isEnabled)
        {
            WhatSearchingForWhatId = searchingForId;
            Name = name;
            IsEnabled = isEnabled;
        }

        [UsedImplicitly]
        public int WhatSearchingForWhatId { get; set; }

        public string Name { get; set; }

        public bool IsEnabled { get; set; }
    }
}