using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Navigation;
using VirtualRepRapFirmware.MVVM;
using System.Windows;

namespace VirtualRepRapFirmware.SerialCommunication
{
    public sealed class Serial : ValidatableBindableBase
    {
        private static readonly Serial _instance = new Serial();
        private SerialPort _port = new SerialPort();
        private bool _isConnected = false;

        #region CTOR
        static Serial()
        {
            
        }

        private Serial()
        {
            _port.DataReceived += _port_DataReceived;
            _port.BaudRate = 115200;
        }
        #endregion

        public static List<string> GetListPorts()
        {
            return SerialPort.GetPortNames().ToList();
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (DataReveived != null)
                DataReveived.Invoke(null, _port.ReadLine());
        }

        public static Serial Instance => _instance;

        public bool IsConnected { get => _isConnected; private set => SetProperty(ref _isConnected, value); }
        public int BaudRate { get => _port.BaudRate;}
        public void SetPortName(string portName)
        {
            _port.PortName = portName;
        }

        public void SetBaudRate(int baudrate)
        {
            _port.BaudRate = baudrate;
        }

        public void WritePort(string data)
        {
            if (_port.PortName == null || _port.PortName == string.Empty)
                throw new Exception("Port Name not set");

            _port.WriteLine(data);
        }

        public void Connect()
        {
            if (_port.IsOpen) return;
            _port.Open();
            IsConnected = true;
        }

        public void Disconnect()
        {
            if (_port.IsOpen)
                _port.Close();
        }

        public event EventHandler<string> DataReveived;
    }
}
