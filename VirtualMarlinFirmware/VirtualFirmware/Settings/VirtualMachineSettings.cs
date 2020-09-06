using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualRepRapFirmware.VirtualFirmware.Settings;

namespace VirtualRepRapFirmware.VirtualFirmware
{
    public class VirtualMachineSettings
    {
        


        public VirtualMachineSettings()
        {
            AxisStepsPerUnit = new AxisStepsPerUnit();
            FeedRate = new FeedRate();
        }

        public AxisStepsPerUnit AxisStepsPerUnit { get; }
        public FeedRate FeedRate { get; }
    }




}
