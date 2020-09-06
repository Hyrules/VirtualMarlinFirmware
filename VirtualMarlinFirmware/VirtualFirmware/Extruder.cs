using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Media3D;
using VirtualRepRapFirmware.VirtualFirmware.MCodes;

namespace VirtualRepRapFirmware.VirtualFirmware
{
    public class Extruder
    {
        private DispatcherTimer _heatingTimer;

        private double _x = 0.00;
        private double _y = 0.00;
        private double _z = -0.00;

        private double[] _currentTemps;
        private double[] _targetTemps;
        private int _timeElapsed;

        private double _roomTemp;

        private int _current_hotend;    
        
        public Extruder()
        {
            _currentTemps = new double[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            _targetTemps = new double[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            _timeElapsed = 0;
            _current_hotend = 0;
            _heatingTimer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            _heatingTimer.Tick += _heatingTimer_Tick;
            _heatingTimer.Start();
            
        }

        public double X => _x;
        public double Y => _y;
        public double Z => _z;

        public double CurrentTemp { get => _currentTemps[Current_hotend]; }
        public double TargetTemp { get => _targetTemps[Current_hotend]; }

        public int Current_hotend { get => _current_hotend; set => _current_hotend = value; }

        public void MoveExtruder(double x, double y , double z)
        {
            _x += x;
            _y += y;
            _z += z;
        }

        private double CalculateTemp(double temperature)
        {
            /*double deltatemp = (temperature - _currentTemps[_current_hotend]);
            double tth = (0.009 * 900) * deltatemp;
            return tth / deltatemp;*/
            double startval = (TargetTemp - _roomTemp) / _roomTemp;

            return TargetTemp / (1 + startval * Math.Pow(Math.E, -0.1 * _timeElapsed)); 

        }

        public void SetHotEndTargetTemp(M104 m104)
        {
            _targetTemps[m104.I] = m104.T;
            _roomTemp = CurrentTemp;
        }

        private void _heatingTimer_Tick(object sender, EventArgs e)
        {
            
            if (_targetTemps[Current_hotend] == 0)
            {
                _timeElapsed = 0;
                Random rd = new Random();
                int multiplier = rd.Next(21, 23);
                double temphe = rd.NextDouble() + multiplier;

                _currentTemps[Current_hotend] = temphe;
            }
            else
            {
                _currentTemps[Current_hotend] = CalculateTemp(TargetTemp);
                
                if (_targetTemps[Current_hotend] - _currentTemps[Current_hotend]  < 1)
                {
                    _currentTemps[Current_hotend] = _targetTemps[Current_hotend];
                }
                else
                {
                    _timeElapsed++;
                }
            }
        }
    }
}
