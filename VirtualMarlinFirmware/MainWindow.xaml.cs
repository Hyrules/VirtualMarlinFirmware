using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.Threading;
using System.Windows.Threading;

namespace VirtualMarlinFirmware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPort _port;
        private double X = 0.00;
        private double Y = 0.00;
        private double Z = -0.15;

        public MainWindow()
        {
            InitializeComponent();

            _port = new SerialPort("COM6",115200);

            _port.DataReceived += _port_DataReceived;
            _port.ErrorReceived += _port_ErrorReceived;
        }

        private void _port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _port.ReadLine();
            WriteToTerminal($"Recv: {data}");
            string[] args = data.Split(' ').ToArray();

            switch(args[0])
            { 
                case "M105":
                    Random rd = new Random();
                    int multiplier = rd.Next(21, 23);
                    double temphe = rd.NextDouble() + multiplier;
                    double tempbd = rd.NextDouble() + multiplier;

                    WritePort($"T:{temphe:N2} /0.00 B:{tempbd:N2} /0.00 @:0 B@:0");                    
                    WritePort("ok");
                    break;
                case "M503":
                    WritePort("G21");
                    WritePort("M149 C");
                    WritePort("M200 S0 D1.75");
                    WritePort("M92 X80.00 Y80.00 Z400.00 E388.60");
                    WritePort("M203 X500.00 Y500.00 Z10.00 E50.00");
                    WritePort("M201 X500.00 Y500.00 Z100.00 E5000.00");
                    WritePort("M204 P500.00 R1000.00 T500.00");
                    WritePort("M205 B20000.00 S0.00 T0.00 J0.06");
                    WritePort("M206 X0.00 Y0.00 Z0.00");

                    WritePort("M420 S1 Z0.00");
                    WritePort("G29 W I0 J0 Z0.14750");
                    WritePort("G29 W I1 J0 Z0.05000");
                    WritePort("G29 W I2 J0 Z - 0.02000");
                    WritePort("G29 W I3 J0 Z - 0.04000");
                    WritePort("G29 W I4 J0 Z - 0.04250");
                    WritePort("G29 W I0 J1 Z0.08250");
                    WritePort("G29 W I1 J1 Z0.02500");
                    WritePort("G29 W I2 J1 Z - 0.02500");
                    WritePort("G29 W I3 J1 Z0.00000");
                    WritePort("G29 W I4 J1 Z0.03000");
                    WritePort("G29 W I0 J2 Z0.00500");
                    WritePort("G29 W I1 J2 Z0.00250");
                    WritePort("G29 W I2 J2 Z - 0.02250");
                    WritePort("G29 W I3 J2 Z0.03750");
                    WritePort("G29 W I4 J2 Z0.06750");
                    WritePort("G29 W I0 J3 Z - 0.02250");
                    WritePort("G29 W I1 J3 Z - 0.03500");
                    WritePort("G29 W I2 J3 Z - 0.01500");
                    WritePort("G29 W I3 J3 Z0.03250");
                    WritePort("G29 W I4 J3 Z0.09250");
                    WritePort("G29 W I0 J4 Z0.02750");
                    WritePort("G29 W I1 J4 Z0.00000");
                    WritePort("G29 W I2 J4 Z0.01250");
                    WritePort("G29 W I3 J4 Z0.07750");
                    WritePort("G29 W I4 J4 Z0.12000");

                    WritePort("M145 S0 H200 B60 F128");
                    WritePort("M145 S1 H240 B70 F255");
                    WritePort("M145 S2 H240 B70 F255");

                    WritePort("M301 P27.15 I2.46 D74.93");
                    WritePort("M304 P48.89 I8.65 D184.26");

                    WritePort("M413 S1");
                    WritePort("M851 X-43.00 Y-20.00 Z-2.60");

                    WritePort("M603 L100.00 U100.00");

                    WritePort("ok");
                    break;
                case "M92":
                    WritePort("M92 X80.00 Y80.00 Z400.00 E388.60");
                    WritePort("ok");
                    break;
                case "M115":
                    WritePort("FIRMWARE_NAME: Marlin bugfix-2.0.x(Aug 30 2020 12:01:43) SOURCE_CODE_URL: https://github.com/MarlinFirmware/Marlin PROTOCOL_VERSION:1.0 MACHINE_TYPE:CR10S-Pro - V1.05 EXTRUDER_COUNT:1 UUID:cede2a2f-41a2-4748-9b12-c55c62f367ff");
    
                    WritePort("SERIAL_XON_XOFF: 0");
                    WritePort("BINARY_FILE_TRANSFER: 0");
                    WritePort("EEPROM: 1");
                    WritePort("VOLUMETRIC: 1");
                    WritePort("AUTOREPORT_TEMP: 1");
                    WritePort("PROGRESS: 0");
                    WritePort("PRINT_JOB: 1");
                    WritePort("AUTOLEVEL: 1");
                    WritePort("RUNOUT: 0");
                    WritePort("Z_PROBE: 1");
                    WritePort("LEVELING_DATA: 1");
                    WritePort("BUILD_PERCENT: 0");
                    WritePort("SOFTWARE_POWER: 1");
                    WritePort("TOGGLE_LIGHTS: 0");
                    WritePort("CASE_LIGHT_BRIGHTNESS: 0");
                    WritePort("EMERGENCY_PARSER: 0");
                    WritePort("PROMPT_SUPPORT: 0");
                    WritePort("SDCARD: 1");
                    WritePort("AUTOREPORT_SD_STATUS: 0");
                    WritePort("LONG_FILENAME: 1");
                    WritePort("THERMAL_PROTECTION: 1");
                    WritePort("MOTION_MODES: 0");
                    WritePort("ARCS: 1");
                    WritePort("BABYSTEPPING: 1");
                    WritePort("CHAMBER_TEMPERATURE: 0");
                    WritePort("ok");
                    break;
                case "M114":
                    WritePort($"X: {X:N2} Y: {Y:N2} Z: {Z:N2} E: 0.00 Count X:0 Y: 0 Z: 0");
                    WritePort("ok");
                    break;
                case "M220":
                    WritePort("FR:100%");
                    WritePort("ok");
                    break;
                case "M221":
                    WritePort("E0 Flow: 100%");
                    WritePort("ok");
                    break;
                case "G1":
                    if (args.Count() == 1) WritePort("ok");
                    for(int a = 1; a <= args.Count()-1;a++)
                    {
                        if(args[a].StartsWith("X"))
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
                    WritePort("ok");
                    break;
                default:
                    WritePort("ok");
                    break;
            }
            
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                WritePort(tbCommand.Text);                
                tbCommand.Text = "";
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                _port.Open();
                tbTerminal.Text += "Port Open" + System.Environment.NewLine;
            }
            catch(Exception ex)
            {
                tbTerminal.Text += $"Error opening port : {ex}" + System.Environment.NewLine;
            }
            
        }

        private void WritePort(string text)
        {
            WriteToTerminal($"Send: {text}");
            _port.WriteLine(text);
        }

        private void WriteToTerminal(string text)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Action(() => {
                    tbTerminal.Text += text + System.Environment.NewLine;
                    tbTerminal.ScrollToEnd();
                }));
            }
            else
            {
                tbTerminal.Text += text + System.Environment.NewLine;
                tbTerminal.ScrollToEnd();
            }
            
        }
    }
}
