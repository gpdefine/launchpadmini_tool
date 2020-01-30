using System;
using Midi;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows;
using System.IO;
using System.Text;

class Program
{
    static InputDevice inputDevice = ExampleUtil.ChooseInputDeviceFromConsole();
    static OutputDevice outputDevice = ExampleUtil.ChooseOutputDeviceFromConsole();

    static void Main(string[] args)
    {

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.TreatControlCAsInput = true;
        Console.Title = "Launchpad Controller v 1.0";

            if (InputDevice.InstalledDevices.Count == 0)
            {
                Console.WriteLine("No input devices.");
            }
            else
            {
                Console.WriteLine("Input Devices:");
                foreach (InputDevice device in InputDevice.InstalledDevices)
                {
                    Console.WriteLine("  {0}", device.Name);
                }
            }
        Console.WriteLine("");
        Leds();
    }
    static void Leds()
    {
       // InputDevice inputDevice0 = ExampleUtil.ChooseInputDeviceFromConsole();
       // Console.Clear();
       // OutputDevice outputDevice0 = ExampleUtil.ChooseOutputDeviceFromConsole();
        outputDevice.Open();


        // Initialize buttonpress func
        Thread buttonpress = new Thread(OnPressThread);
        buttonpress.Start();

        Console.Clear();
        Console.WriteLine("Enter 'exit' to close process");
        Console.WriteLine("");

        //Leeed

        LoadFile();
        outputDevice.SendNoteOn(Channel.Channel1, Pitch.C9, 14);
        outputDevice.SendNoteOn(Channel.Channel1, Pitch.GSharp7, 60);

        outputDevice.SendNoteOn(Channel.Channel1, Pitch.GSharpNeg1, 14);
        outputDevice.SendNoteOn(Channel.Channel1, Pitch.C1, 30);
        outputDevice.SendNoteOn(Channel.Channel1, Pitch.E2, 49);
        outputDevice.SendNoteOn(Channel.Channel1, Pitch.GSharp3, 60);

        while (true)
        {
            string a = Console.ReadLine();
            if (a == "exit")
            {
                outputDevice.Close();
                inputDevice.StopReceiving();
                inputDevice.Close();
                inputDevice.RemoveAllEventHandlers();
                // All done.
                Console.WriteLine();
                ExampleUtil.PressAnyKeyToContinue();
                break;
            }
        }

       

    }

    static void LoadFile()
    {
        foreach (Pitch a in (Pitch[])Enum.GetValues(typeof(Pitch)))
        {
            if (File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\LaunchpadTool\" + a.ToString()))
            {
                outputDevice.SendNoteOn(Channel.Channel1, a, int.Parse(File.ReadAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\LaunchpadTool\" + a.ToString())));
            }
        }
    }

    static void OnPressThread()
    {
        try
        {
            inputDevice.Open();
            inputDevice.StartReceiving(null);
            Summarizer summarizer = new Summarizer(inputDevice);
        }
        catch (Exception)
        {
        }

    }


    public class Summarizer
    {
        private bool colorize = true;
        private int velocity = 1;
        private bool position = true;

        public Summarizer(InputDevice inputDevice)
        {
            this.inputDevice = inputDevice;
            pitchesPressed = new Dictionary<Pitch, bool>();
            inputDevice.NoteOn += new InputDevice.NoteOnHandler(this.NoteOn);
        }
        public bool inputpressed = false;
        static class KeyboardSend
        {
            [DllImport("user32.dll")]
            private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
            
            private const int KEYEVENTF_EXTENDEDKEY = 1;
            private const int KEYEVENTF_KEYUP = 2;

            public static void KeyDown(ConsoleKey vKey)
            {
                keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            }

            public static void KeyDown(ConsoleModifiers vKey)
            {
                keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            }

            public static void KeyUp(ConsoleKey vKey)
            {
                keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            }

            public static void KeyUp(ConsoleModifiers vKey)
            {
                keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            }
        }

        public void ButtonPress(Pitch an)
        {
            switch (an)
            {
                case (Pitch.E8): 
                    {  
                        KeyboardSend.KeyDown(ConsoleKey.A);
                        break;
                    }
                case (Pitch.F8):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.B);
                        break;
                    }
                case (Pitch.FSharp8):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.C);
                        break;
                    }
                case (Pitch.G8):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.D);
                        break;
                    }
                case (Pitch.GSharp8):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.E);
                        break;
                    }
                case (Pitch.A8):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F);
                        break;
                    }
                case (Pitch.ASharp8):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.G);
                        break;
                    }
                case (Pitch.B8):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.H);
                        break;
                    }

                case (Pitch.C7):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.I);
                        break;
                    }
                case (Pitch.CSharp7):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.J);
                        break;
                    }
                case (Pitch.D7):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.K);
                        break;
                    }
                case (Pitch.DSharp7):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.L);
                        break;
                    }
                case (Pitch.E7):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.M);
                        break;
                    }
                case (Pitch.F7):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.N);
                        break;
                    }
                case (Pitch.FSharp7):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.O);
                        break;
                    }
                case (Pitch.G7):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.P);
                        break;
                    }

                case (Pitch.GSharp5):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.Q);
                        break;
                    }
                case (Pitch.A5):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.R);
                        break;
                    }
                case (Pitch.ASharp5):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.S);
                        break;
                    }
                case (Pitch.B5):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.T);
                        break;
                    }
                case (Pitch.C6):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.U);
                        break;
                    }
                case (Pitch.CSharp6):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.V);
                        break;
                    }
                case (Pitch.D6):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.W);
                        break;
                    }
                case (Pitch.DSharp6):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.X);
                        break;
                    }

                case (Pitch.E4):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.Y);
                        break;
                    }
                case (Pitch.F4):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.Z);
                        break;
                    }
                case (Pitch.FSharp4):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.NumPad0);
                        break;
                    }
                case (Pitch.G4):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.NumPad1);
                        break;
                    }
                case (Pitch.GSharp4):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.NumPad2);
                        break;
                    }
                case (Pitch.A4):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.NumPad3);
                        break;
                    }
                case (Pitch.ASharp4):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.NumPad4);
                        break;
                    }
                case (Pitch.B4):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.NumPad5);
                        break;
                    }

                case (Pitch.C3):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.NumPad6);
                        break;
                    }
                case (Pitch.CSharp3):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.NumPad7);
                        break;
                    }
                case (Pitch.D3):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.NumPad8);
                        break;
                    }
                case (Pitch.DSharp3):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.NumPad9);
                        break;
                    }
                case (Pitch.E3):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F1);
                        break;
                    }
                case (Pitch.F3):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F2);
                        break;
                    }
                case (Pitch.FSharp3):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F3);
                        break;
                    }
                case (Pitch.G3):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F4);
                        break;
                    }

                case (Pitch.GSharp1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F5);
                        break;
                    }
                case (Pitch.A1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F6);
                        break;
                    }
                case (Pitch.ASharp1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F7);
                        break;
                    }
                case (Pitch.B1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F8);
                        break;
                    }
                case (Pitch.C2):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F10);
                        break;
                    }
                case (Pitch.CSharp2):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F11);
                        break;
                    }
                case (Pitch.D2):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F12);
                        break;
                    }
                case (Pitch.DSharp2):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F13);
                        break;
                    }

                case (Pitch.E0):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F14);
                        break;
                    }
                case (Pitch.F0):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F15);
                        break;
                    }
                case (Pitch.FSharp0):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F16);
                        break;
                    }
                case (Pitch.G0):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F17);
                        break;
                    }
                case (Pitch.GSharp0):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F18);
                        break;
                    }
                case (Pitch.A0):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F19);
                        break;
                    }
                case (Pitch.ASharp0):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F20);
                        break;
                    }
                case (Pitch.B0):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F21);
                        break;
                    }

                case (Pitch.CNeg1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F22);
                        break;
                    }

                case (Pitch.CSharpNeg1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F23);
                        break;
                    }
                case (Pitch.DNeg1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.F24);
                        break;
                    }
                case (Pitch.DSharpNeg1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.Home);
                        break;
                    }
                case (Pitch.ENeg1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.Insert);
                        break;
                    }
                case (Pitch.FNeg1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.PageUp);
                        break;
                    }
                case (Pitch.FSharpNeg1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.PageDown);
                        break;
                    }
                case (Pitch.GNeg1):
                    {
                        KeyboardSend.KeyDown(ConsoleKey.End);
                        break;
                    }

            }
        }
        public void SaveFile(Pitch toappend, int _velocity)
        {
            try
            {
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\LaunchpadTool\" + toappend.ToString(), _velocity.ToString());
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\LaunchpadTool");
            }
        }

        public void NoteOn(NoteOnMessage msg)
        {

            foreach (Pitch a in (Pitch[])Enum.GetValues(typeof(Pitch)))
            {
                if (msg.Pitch == a && colorize == true)
                {
                    //EDIT THERE
                    outputDevice.SendNoteOn(Channel.Channel1, a, velocity);
                    if (position == true)
                    {

                        SaveFile(a, velocity);
                        ButtonPress(a);
                        position = false;
                    }
                    else
                    {
                        position = true;
                    }
                    break;
                }
                else 
                { 
                }

                if (msg.Pitch == Pitch.C9)
                {
                    //RESET
                    foreach (Pitch b in (Pitch[])Enum.GetValues(typeof(Pitch)))
                    {
                        outputDevice.SendNoteOn(Channel.Channel1, b, 0);

                        try
                        {
                            File.Delete(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\LaunchpadTool\" + b.ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                    colorize = true;                   
                    outputDevice.SendNoteOn(Channel.Channel1, Pitch.C9, 14);
                    outputDevice.SendNoteOn(Channel.Channel1, Pitch.GSharp7, 60);

                    outputDevice.SendNoteOn(Channel.Channel1, Pitch.GSharpNeg1, 14);
                    outputDevice.SendNoteOn(Channel.Channel1, Pitch.C1, 30);
                    outputDevice.SendNoteOn(Channel.Channel1, Pitch.E2, 49);
                    outputDevice.SendNoteOn(Channel.Channel1, Pitch.GSharp3, 60);
                    break;
                }

                switch (msg.Pitch)
                {
                    case (Pitch.GSharp7): 
                        {
                            //SETUP
                            colorize = false;
                            break;
                        }

                    //Colors

                    case (Pitch.GSharpNeg1):
                        {
                            velocity = 14;
                            //outputDevice.SendNoteOn(Channel.Channel1, Pitch.GSharpNeg1, 14);
                            break;
                        }
                    case (Pitch.C1):
                        {
                            velocity = 30;
                            //outputDevice.SendNoteOn(Channel.Channel1, Pitch.C1, 30);
                            break;
                        }
                    case (Pitch.E2):
                        {
                            velocity = 49;
                            //outputDevice.SendNoteOn(Channel.Channel1, Pitch.E2, 49);
                            break;
                        }
                    case (Pitch.GSharp3):
                        {
                            velocity = 60;
                            //outputDevice.SendNoteOn(Channel.Channel1, Pitch.GSharp3, 60);
                            break;
                        }
                }
            }
        }
            

        protected InputDevice inputDevice;
        protected Dictionary<Pitch, bool> pitchesPressed;
    }
}

public class ExampleUtil
{

    public InputDevice inputDevice;
    public OutputDevice outputDevice;

    public static OutputDevice ChooseOutputDeviceFromConsole()
    {
        if (OutputDevice.InstalledDevices.Count == 0)
        {
            return null;
        }
        if (OutputDevice.InstalledDevices.Count == 1)
        {
            return OutputDevice.InstalledDevices[0];
        }
        Console.WriteLine("Output Devices:");
        for (int i = 0; i < OutputDevice.InstalledDevices.Count; ++i)
        {
            Console.WriteLine("   {0}: {1}", i, OutputDevice.InstalledDevices[i].Name);
        }
        Console.Write("Choose the id of an output device...");
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            int deviceId = (int)keyInfo.Key - (int)ConsoleKey.D0;
            if (deviceId >= 0 && deviceId < OutputDevice.InstalledDevices.Count)
            {
                return OutputDevice.InstalledDevices[deviceId];
            }
        }
    }

    public static InputDevice ChooseInputDeviceFromConsole()
    {
        if (InputDevice.InstalledDevices.Count == 0)
        {
            return null;
        }
        if (InputDevice.InstalledDevices.Count == 1)
        {
            return InputDevice.InstalledDevices[0];
        }
        Console.WriteLine("Input Devices:");
        for (int i = 0; i < InputDevice.InstalledDevices.Count; ++i)
        {
            Console.WriteLine("   {0}: {1}", i, InputDevice.InstalledDevices[i]);
        }
        Console.Write("Choose the id of an input device...");
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            int deviceId = (int)keyInfo.Key - (int)ConsoleKey.D0;
            if (deviceId >= 0 && deviceId < InputDevice.InstalledDevices.Count)
            {
                return InputDevice.InstalledDevices[deviceId];
            }
        }
    }

    public static void PressAnyKeyToContinue()
    {
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey(true);
    }

    private static Dictionary<ConsoleKey, int> mockKeys = new Dictionary<ConsoleKey, int>
        {
            {ConsoleKey.Q,        53},
            {ConsoleKey.D2,       54},
            {ConsoleKey.W,        55},
            {ConsoleKey.D3,       56},
            {ConsoleKey.E,        57},
            {ConsoleKey.D4,       58},
            {ConsoleKey.R,        59},
            {ConsoleKey.T,        60},
            {ConsoleKey.D6,       61},
            {ConsoleKey.Y,        62},
            {ConsoleKey.D7,       63},
            {ConsoleKey.U,        64},
            {ConsoleKey.I,        65},
            {ConsoleKey.D9,       66},
            {ConsoleKey.O,        67},
            {ConsoleKey.D0,       68},
            {ConsoleKey.P,        69},
            {ConsoleKey.OemMinus, 70},
            {ConsoleKey.Oem4,     71},
            {ConsoleKey.Oem6,     72}
        };

    /// <summary>
    /// If the specified key is one of the computer keys used for mock MIDI input, returns true
    /// and sets pitch to the value.
    /// </summary>
    /// <param name="key">The computer key pressed.</param>
    /// <param name="pitch">The pitch it mocks.</param>
    /// <returns></returns>

    public static bool IsMockPitch(ConsoleKey key, out Pitch pitch)
    {
        if (mockKeys.ContainsKey(key))
        {
            pitch = (Pitch)mockKeys[key];
            return true;
        }
        pitch = 0;
        return false;
    }
}
