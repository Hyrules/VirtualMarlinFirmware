using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Security.Policy;

namespace VirtualRepRapFirmware.VirtualFirmware
{
    public class HotBed
    {
        private double _currentTemp;
        private double _targetTemp;
        DispatcherTimer _heatingTimer;
        double _roomTemp;
        int _timeElapsed;

        public HotBed()
        {
            _roomTemp = 21;
            _timeElapsed = 0;
            _currentTemp = 0;
            _targetTemp = 0;
            _heatingTimer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            _heatingTimer.Tick += _heatingTimer_Tick;
            _heatingTimer.Start();
        }


        private double CalculateTemp(double temperature)
        {
            /*double deltatemp = (temperature - _currentTemps[_current_hotend]);
            double tth = (0.009 * 900) * deltatemp;
            return tth / deltatemp;*/
            double startval = (TargetTemp - _roomTemp) / _roomTemp;

            return TargetTemp / (1 + startval * Math.Pow(Math.E, -0.11 * _timeElapsed));

        }

        private void _heatingTimer_Tick(object sender, EventArgs e)
        {
            if (_targetTemp == 0)
            {
                _timeElapsed = 0;
                Random rd = new Random();
                int multiplier = rd.Next(21, 23);
                double temphe = rd.NextDouble() + multiplier;

                _currentTemp = temphe;
            }
            else
            {
                _currentTemp = CalculateTemp(TargetTemp);

                if (_targetTemp - _currentTemp < 1)
                {
                    _currentTemp = _targetTemp;
                }
                else
                {
                    _timeElapsed++;
                }
            }
        }

        public void SetHotBedTargetTemp(double temperature)
        {
            _targetTemp = temperature;
        }

        public double CurrentTemp { get => _currentTemp;  }
        public double TargetTemp { get => _targetTemp; }
    }
}
