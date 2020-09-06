using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualRepRapFirmware.VirtualFirmware.MCodes;

namespace VirtualRepRapFirmware.VirtualFirmware.Settings
{
    public class FeedRate
    {
        public FeedRate()
        {
            FeedRatePercent = 100;
        }

        public void SetFeedRatePercent(M220 m220)
        {

            if (m220.B)
                BackupFeedRate = FeedRatePercent;

            FeedRatePercent = m220.S;
        }

        public void RestoreFeedRatePercent()
        {
            FeedRatePercent = BackupFeedRate.GetValueOrDefault(FeedRatePercent);
        }

        public uint FeedRatePercent { get; private set; }
        public uint? BackupFeedRate { get; private set; }
    }
}
