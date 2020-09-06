using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VirtualRepRapFirmware.VirtualFirmware.MCodes
{
    public class MCommandRegex
    {
        internal const string T_REGEX = @"T(\d{1,})";
        internal const string S_REGEX = @"S(\d{1,})";
        internal const string I_REGEX = @"I(\d{1,})";
        internal const string B_REGEX = @"B(\d{1,})";
        internal const string E_REGEX = @"E(\d{1,}.\d{2})";
        internal const string X_REGEX = @"X(\d{1,}.\d{2})";
        internal const string Y_REGEX = @"Y(\d{1,}.\d{2})";
        internal const string Z_REGEX = @"Z(\d{1,}.\d{2})";
        internal const string F_FLAG_REGEX = @" F";
        internal const string D_FLAG_REGEX = @" D";
        internal const string S_FLAG_REGEX = @" S";
        internal const string B_FLAG_REGEX = @" B";
        internal const string R_FLAG_REGEX = @" R";
    }

    public interface IMCommand
    {
        string Name
        {
            get;
        }
        
        string[] Reply
        {
            get;
        }
        
    }

    public class M140 : MCommandRegex, IMCommand
    {
        public M140(string command)
        {
            Match s = Regex.Match(command, T_REGEX);
            if (s.Success)
                S = Convert.ToDouble(s.Groups[1].Value);
            Match i = Regex.Match(command, I_REGEX);
            if (i.Success)
                I = Convert.ToInt32(i.Groups[1].Value);
        }

        public string Name => "M140";
        public string[] Reply => new string[] { "ok" };

        public int I { get; }
        public double S { get; }
    }

    public class M105 : MCommandRegex, IMCommand
    {
       
        public M105(string command)
        {
            Match t = Regex.Match(command, T_REGEX);
            if (t.Success)
                T = Convert.ToInt32(t.Groups[1].Value);
        }

        public string Name => "M105";
        public string[] Reply => new string[] { "T:{0:N2} /{1:N2} B:{2:N2} /{3:N2} @:0 B@:0" };
        public int T { get; }
    }

    public class M114 : MCommandRegex, IMCommand
    {

        public M114(string command)
        {
            Match d = Regex.Match(command, D_FLAG_REGEX);
            if (d.Success)
                D = true;

        }
        public string Name => "M114";

        public string[] Reply => new string[] { "X: {0:N2} Y: {1:N2} Z: {2:N2} E: 0.00 Count X:0 Y: 0 Z: 0"};

        public bool D { get; } 
    }

    public class M503 : MCommandRegex, IMCommand
    {
        public M503(string command)
        {
            Match s = Regex.Match(command, S_FLAG_REGEX);
            if (s.Success)
                S = true;
        }

        public string Name => "M503";

        public string[] Reply => new string[] {
            "G21",
            "M149 C",
            "M200 S0 D1.75",
            "M92 X80.00 Y80.00 Z400.00 E388.60",
            "M203 X500.00 Y500.00 Z10.00 E50.00",
            "M201 X500.00 Y500.00 Z100.00 E5000.00",
            "M204 P500.00 R1000.00 T500.00",
            "M205 B20000.00 S0.00 T0.00 J0.06",
            "M206 X0.00 Y0.00 Z0.00",
            "M420 S1 Z0.00",
            "G29 W I0 J0 Z0.14750",
            "G29 W I1 J0 Z0.05000",
            "G29 W I2 J0 Z - 0.02000",
            "G29 W I3 J0 Z - 0.04000",
            "G29 W I4 J0 Z - 0.04250",
            "G29 W I0 J1 Z0.08250",
            "G29 W I1 J1 Z0.02500",
            "G29 W I2 J1 Z - 0.02500",
            "G29 W I3 J1 Z0.00000",
            "G29 W I4 J1 Z0.03000",
            "G29 W I0 J2 Z0.00500",
            "G29 W I1 J2 Z0.00250",
            "G29 W I2 J2 Z - 0.02250",
            "G29 W I3 J2 Z0.03750",
            "G29 W I4 J2 Z0.06750",
            "G29 W I0 J3 Z - 0.02250",
            "G29 W I1 J3 Z - 0.03500",
            "G29 W I2 J3 Z - 0.01500",
            "G29 W I3 J3 Z0.03250",
            "G29 W I4 J3 Z0.09250",
            "G29 W I0 J4 Z0.02750",
            "G29 W I1 J4 Z0.00000",
            "G29 W I2 J4 Z0.01250",
            "G29 W I3 J4 Z0.07750",
            "G29 W I4 J4 Z0.12000",
            "M145 S0 H200 B60 F128",
            "M145 S1 H240 B70 F255",
            "M145 S2 H240 B70 F255",
            "M301 P27.15 I2.46 D74.93",
            "M304 P48.89 I8.65 D184.26",
            "M851 X-43.00 Y-20.00 Z-2.60",
            "M603 L100.00 U100.00"
        };

        public bool S { get; }
    }

    public class M115 : MCommandRegex, IMCommand
    {
        public M115(string command)
        {

        }

        public string Name => "M115";

        public string[] Reply => new string[]{
            "FIRMWARE_NAME: Marlin bugfix-2.0.x(Aug 30 2020 12:01:43)",
            "SOURCE_CODE_URL: https://github.com/MarlinFirmware/Marlin", 
            "PROTOCOL_VERSION:1.0 MACHINE_TYPE:CR10S-Pro - V1.05", 
            "EXTRUDER_COUNT:1 UUID:cede2a2f-41a2-4748-9b12-c55c62f367ff",
            "SERIAL_XON_XOFF: 0",
            "BINARY_FILE_TRANSFER: 0",
            "EEPROM: 1",
            "VOLUMETRIC: 1",
            "AUTOREPORT_TEMP: 1",
            "PROGRESS: 0",
            "PRINT_JOB: 1",
            "AUTOLEVEL: 1",
            "RUNOUT: 0",
            "Z_PROBE: 1",
            "LEVELING_DATA: 1",
            "BUILD_PERCENT: 0",
            "SOFTWARE_POWER: 1",
            "TOGGLE_LIGHTS: 0",
            "CASE_LIGHT_BRIGHTNESS: 0",
            "EMERGENCY_PARSER: 0",
            "PROMPT_SUPPORT: 0",
            "SDCARD: 1",
            "AUTOREPORT_SD_STATUS: 0",
            "LONG_FILENAME: 1",
            "THERMAL_PROTECTION: 1",
            "MOTION_MODES: 0",
            "ARCS: 1",
            "BABYSTEPPING: 1",
            "CHAMBER_TEMPERATURE: 0",
        };
    }

    public class M104 : MCommandRegex, IMCommand
    {
        public M104(string command)
        {
            Match b = Regex.Match(command, B_REGEX);
            if (b.Success)
                B = Convert.ToDouble(b.Groups[1].Value);

            Match f = Regex.Match(command, F_FLAG_REGEX);
            if (f.Success)
                F = true;

            Match i = Regex.Match(command, I_REGEX);
            if (i.Success)
                I = Convert.ToInt32(i.Groups[1].Value);

            Match s = Regex.Match(command, S_REGEX);
            if (s.Success)
                S = Convert.ToDouble(s.Groups[1].Value);

            Match t = Regex.Match(command, T_REGEX);
            if (t.Success)
                I = Convert.ToInt32(i.Groups[1].Value);
        }

        public string Name => "M104";

        public string[] Reply => new string[] {       
        };

        public double B { get; }
        public bool F { get; }
        public int I { get; }
        public double S { get; }
        public int T { get; }
    }

    public class M92 : MCommandRegex, IMCommand
    {
        public M92(string command)
        {
            Match e = Regex.Match(command, E_REGEX);
            if (e.Success)
                E = Convert.ToDouble(e.Groups[1].Value);

            Match t = Regex.Match(command, T_REGEX);
            if (t.Success)
                T = Convert.ToInt32(t.Groups[1].Value);

            Match x = Regex.Match(command, X_REGEX);
            if (x.Success)
                X = Convert.ToDouble(e.Groups[1].Value);

            Match y = Regex.Match(command, Y_REGEX);
            if (y.Success)
                Y = Convert.ToDouble(e.Groups[1].Value);

            Match z = Regex.Match(command, Z_REGEX);
            if (z.Success)
                Z = Convert.ToDouble(e.Groups[1].Value);
        }

        public string Name => "M92";

        public string[] Reply => new string[] {
            "M92 X{0} Y{1} Z{2} E{3}"
        };

        public double? E { get; }
        public int? T { get; }
        public double? X { get; }
        public double? Y { get; }
        public double? Z { get; }
    }

    public class M220: MCommandRegex, IMCommand
    {
        public M220(string command)
        {
            Match b = Regex.Match(command, B_FLAG_REGEX);
            if (b.Success)
                B = true;
            Match r = Regex.Match(command, R_FLAG_REGEX);
            if (r.Success)
                R = true;
            Match s = Regex.Match(command, S_REGEX);
            if (s.Success)
                S = Convert.ToUInt32(s.Groups[1].Value);
        }

        public string Name => "M92";

        public string[] Reply => new string[] {
            "FR:{0}%"
        };

        public bool B { get; }
        public bool R { get; }
        public uint S { get; }
    }
}
