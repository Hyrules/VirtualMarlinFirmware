using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualRepRapFirmware.VirtualFirmware.MCodes;

namespace VirtualRepRapFirmware.VirtualFirmware.Settings
{
    public class AxisStepsPerUnit
    {
        public double X = 80.00;
        public double Y = 80.00;
        public double Z = 400.00;
        public double E = 388.6;

        public void SetAxisStepsPerUnit(M92 m92)
        {
            X = m92.X ?? X;
            Y = m92.Y ?? Y;
            Z = m92.Z ?? Z;
            E = m92.E ?? E;
        }
    }
}
