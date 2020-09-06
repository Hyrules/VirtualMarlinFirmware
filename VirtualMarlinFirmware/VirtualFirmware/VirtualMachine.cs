using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VirtualRepRapFirmware.MVVM;
using VirtualRepRapFirmware.SerialCommunication;
using VirtualRepRapFirmware.VirtualFirmware.MCodes;

namespace VirtualRepRapFirmware.VirtualFirmware
{
    public class VirtualMachine : ValidatableBindableBase
    {

        private Serial _comm;

        public VirtualMachine()
        {
            Hotbed = new HotBed();            
            Extruder = new Extruder();
            _comm = Serial.Instance;
            _comm.DataReveived += _comm_DataReveived;
            Settings = new VirtualMachineSettings();
        }

        private void _comm_DataReveived(object sender, string e)
        {
            
            if (DataReceived != null)
                DataReceived.Invoke(this, e);
            ProcessReveivedData(e);
        }



        public void SendCommand(string command)
        {
            WritePort(command);
        }

        private void WritePort(string text, bool addOk = true)
        {
            _comm.WritePort(text);
            if (DataSent != null)
                DataSent.Invoke(this, text);

            if (addOk && text != "ok")
            {
                _comm.WritePort("ok");
                if (DataSent != null)
                    DataSent.Invoke(this, "ok");

            }

        }

        private void WritePort(string[] text, bool addOk = true)
        {
            foreach(string c in text)
            {
                _comm.WritePort(c);
                if (DataSent != null)
                    DataSent.Invoke(this, c);
            }

            if (addOk)
            {
                _comm.WritePort("ok");
                if (DataSent != null)
                    DataSent.Invoke(this, "ok");
            }
        }

        public void ReadFirmwareBin(string path)
        {
            try
            {
               /* FileStream firmware = File.OpenRead(path);
                
                BinaryReader br = new BinaryReader(firmware);
                byte[] data = br.ReadBytes(int.MaxValue);
                firmware.Close();*/
            }
            catch(Exception)
            {

            }
            
        }

        public void Connect()
        {
            _comm.Connect();
        }

        public void ReadConfigFiles(string path)
        {

        }

        private static T CreateMCommandInstance<T>(string className, string mcommand)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var type = assembly.GetTypes()
                .First(t => t.Name == className);

            return (T)Activator.CreateInstance(type, mcommand);
        }

        private void ProcessReveivedData(string data)
        {
            string[] args = data.Split(' ').ToArray();
            
            switch (args[0])
            {
                case "M105":
                    M105 m105 = CreateMCommandInstance<M105>(args[0], data); 
                    WritePort(string.Format(m105.Reply[0], Extruder.CurrentTemp, Extruder.TargetTemp, Hotbed.CurrentTemp, Hotbed.TargetTemp));
                    break;
                case "M503":
                    M503 m503 = CreateMCommandInstance<M503>(args[0], data);
                    WritePort(m503.Reply);
                    break;
                case "M92":
                    M92 m92 = CreateMCommandInstance<M92>(args[0], data);
                    Settings.AxisStepsPerUnit.SetAxisStepsPerUnit(m92);
                    WritePort(string.Format(m92.Reply[0],m92.X,m92.Y,m92.Z,m92.E));
                    break;
                case "M115":
                    M115 m115 = CreateMCommandInstance<M115>(args[0], data) ;
                    WritePort(m115.Reply);
                    break;
                case "M114":
                    M114 m114 = CreateMCommandInstance<M114>(args[0], data);
                    WritePort(string.Format(m114.Reply[0],Extruder.X,Extruder.Y, Extruder.Z));

                    break;
                case "M220":
                    
                    M220 m220 = CreateMCommandInstance<M220>(args[0], data);
                    Settings.FeedRate.SetFeedRatePercent(m220);
                    WritePort(string.Format(m220.Reply[0],Settings.FeedRate.FeedRatePercent));
                    break;
                case "M221":
                    WritePort("E0 Flow: 100%");
                    break;
                case "G1":
                    if (args.Count() == 1) WritePort("ok");
                    double X = 0;
                    double Y = 0;
                    double Z = 0;

                    for (int a = 1; a <= args.Count() - 1; a++)
                    {
                        if (args[a].StartsWith("X"))
                        {
                            X += Convert.ToDouble(args[a].Substring(1));
                        }

                        if (args[a].StartsWith("Y"))
                        {
                            Y += Convert.ToDouble(args[a].Substring(1));
                        }

                        if (args[a].StartsWith("Z"))
                        {
                            Z += Convert.ToDouble(args[a].Substring(1));
                        }

                    }
                    Extruder.MoveExtruder(X, Y, Z);
                    WritePort("ok");
                    break;
                case "M104":
                    M104 m104 = CreateMCommandInstance<M104>(args[0], data);
                    Extruder.SetHotEndTargetTemp(m104);
                    WritePort("ok");
                    break;
                case "M140":
                    M140 m140 = CreateMCommandInstance<M140>(args[0], data);
                    WritePort(m140.Reply[0]);
                    break;
                default:
                    WritePort("ok");
                    break;
            }
        }

        public void SetCommPort(string portname)
        {
            _comm.SetPortName(portname);
        }

        public void SetBaudRate(int baudrate)
        {
            _comm.SetBaudRate(baudrate);
        }

        public void Disconnect()
        {
            _comm.Disconnect();
        }

        public bool IsConnected
        {
            get => _comm.IsConnected;
        }

        public event EventHandler<string> DataReceived;
        public event EventHandler<string> DataSent;

        public Extruder Extruder { get ; private set; }
        public HotBed Hotbed { get; private set; }
        public VirtualMachineSettings Settings { get; private set; }
    }
}
