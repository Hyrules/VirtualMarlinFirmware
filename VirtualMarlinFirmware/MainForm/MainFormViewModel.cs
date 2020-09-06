using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualRepRapFirmware.MVVM;
using VirtualRepRapFirmware.SerialCommunication;
using VirtualRepRapFirmware.VirtualFirmware;

namespace VirtualRepRapFirmware.MainForm
{
    public class MainFormViewModel : ValidatableBindableBase
    {
        private string _terminalText;
        private VirtualMachine _vm;
        private List<string> _portNames;
        private string _selectedPort;
        private int _selectedBaudRate;
        private readonly List<int> _baudRates;

        public MainFormViewModel()
        {
            TerminalText = string.Empty;
            SelectedBaudRate = 115200;
            _vm = new VirtualMachine();
            PortNames = Serial.GetListPorts();
            if (PortNames.Count > 0)
                SelectedPort = PortNames[0];
            _vm.DataReceived += _vm_DataReceived;
            _vm.DataSent += _vm_DataSent;
            _baudRates = new List<int>() { 115200, 250000, 500000,1000000 };
        }

        private void _vm_DataSent(object sender, string e)
        {
            WriteToTerminal($"Sent: {e}");
        }

        private void _vm_DataReceived(object sender, string e)
        {
            WriteToTerminal($"Recv: {e}");
        }


        private void WriteToTerminal(string text)
        {
            TerminalText += text + "\n";
        }

        public string TerminalText { get => _terminalText; set => SetProperty(ref _terminalText,value); }
        public List<string> PortNames { get => _portNames; set => SetProperty(ref _portNames, value); }
        public string SelectedPort { get => _selectedPort; set => SetProperty(ref _selectedPort, value); }
        public int SelectedBaudRate { get => _selectedBaudRate; set => SetProperty(ref _selectedBaudRate, value); }

        public ICommand ChoosePortCommand => new RelayCommand(param => ChoosePort());
        public ICommand ConnectPortCommand => new RelayCommand(param => ConnectPort(), param => CanConnect());
        public ICommand SendOkCommand => new RelayCommand(param => _vm.SendCommand("ok"), param => CanSendOk());
        public ICommand QuitCommand => new RelayCommand(param => Quit());
        public ICommand ChooseBaudRateCommand => new RelayCommand(param => ChooseBaudRate());

        public ICommand LoadFirmwareCommand => new RelayCommand(param => LoadFirmware());

        public List<int> BaudRates => _baudRates;

        private void LoadFirmware()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Binary file | *.bin";
            ofd.DefaultExt = ".bin";
            if(ofd.ShowDialog() == true)
            {
                _vm.ReadFirmwareBin(ofd.FileName);
            }
        }

        public bool CanSendOk()
        {
            return _vm.IsConnected;
        }

        public bool CanConnect()
        {
            return SelectedPort != null && SelectedPort != string.Empty && _vm.IsConnected == false;
        }

        public void ChoosePort()
        {
            _vm.SetCommPort(SelectedPort);
        }

        public void ConnectPort()
        {
            WriteToTerminal($"Opening Port : {_selectedPort}");
            _vm.Connect();
            _vm.SendCommand("ok");
            
        }

        private void ChooseBaudRate()
        {
            _vm.SetBaudRate(SelectedBaudRate);
        }

        public void Quit()
        {
            _vm.Disconnect();
        }

    }
}
