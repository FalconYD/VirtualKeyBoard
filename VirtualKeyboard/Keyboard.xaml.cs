﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;
using UserInterface;

namespace VirtualKeyboard
{
    /// <summary>
    /// Keyboard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Keyboard : Window
    {
        #region Process ID & IMEState
        [DllImport("imm32.dll")]
        private static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr IParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        private const int WM_IME_CONTROL = 643;
        public bool isEngMode, isCapsLock;
        private string GetForegroundProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();

            // The foreground window can be NULL in certain circumstances, 
            // such as when a window is losing activation.
            if (hwnd == null)
                return "Unknown";

            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);

            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.Id == pid)
                    return p.ProcessName;
            }
            return "Unknown";
        }

        //check english/korean mode
        public bool GetIME()
        {
            string FocusedProcess = GetForegroundProcessName();
            Process p = Process.GetProcessesByName(FocusedProcess).FirstOrDefault();

            if (p == null)
                return false;

            IntPtr hwnd = p.MainWindowHandle;
            IntPtr hime = ImmGetDefaultIMEWnd(hwnd);
            IntPtr status = SendMessage(hime, WM_IME_CONTROL, new IntPtr(0x5), new IntPtr(0));

            if (status.ToInt32() != 0)
                return true;  //korean
            else
                return false;
        }
        #endregion

        #region Low Level Keyboard hook
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern ushort GetAsyncKeyState(Int32 vKey);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        const int WH_KEYBOARD_LL = 13;
        const int WH_MOUSE_LL = 14;
        const int WM_KEYDOWN = 0x100; 

        private LowLevelKeyboardProc _proc = hookProc;
        private LowLevelKeyboardProc _proc2 = hookProcM;

        private static IntPtr hhook = IntPtr.Zero;
        private static IntPtr hhook2 = IntPtr.Zero;
        public void SetHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hInstance, 0);
            hhook2 = SetWindowsHookEx(WH_MOUSE_LL, _proc2, hInstance, 0);
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(hhook);
            UnhookWindowsHookEx(hhook2);
        }
        public static IntPtr hookProcM(int code, IntPtr wParam, IntPtr lParam)
        {
            if(code >= 0)
            {

            }
            Console.WriteLine($"{code}, {wParam}, {lParam}");
            return CallNextHookEx(hhook, code, (int)wParam, lParam);
        }
        public static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                    Console.WriteLine($"{vkCode:X2}");
                if (wParam == (IntPtr)WM_KEYDOWN)
                { 
                    try
                    {
                        dicBn[$"0x{vkCode:X2}"].Background = Btn_Clicked;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        dicBn[$"0x{vkCode:X2}"].Background = Btn_Normal;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                return CallNextHookEx(hhook, code, (int)wParam, lParam);;
            }
            else
                return CallNextHookEx(hhook, code, (int)wParam, lParam);
        }
        #endregion


        [DllImport("user32.dll")]
        static extern void keybd_event(byte vk, byte scan, int flags, int externinfo);

        KeyValue _keyvalue = new KeyValue();
        bool bShift = false;
        bool bCtrl = false;
        bool bAlt = false;
        bool bWin = false;
        bool bCaps = false;
        bool bNumlock = false;
        bool bScrollLock = false;

        byte keydownMode = 0;
        const byte _KeyDown = 0x00;
        const byte _KeyUp = 0x02;

        static SolidColorBrush Btn_Normal = new SolidColorBrush(Color.FromRgb(0xF7, 0xF7, 0xF7));
        static SolidColorBrush Btn_Clicked = new SolidColorBrush(Color.FromRgb(0x42, 0xA2, 0xE7));

        Queue<byte[]> queue = new Queue<byte[]>();
        Thread _thInput;
        static Dictionary<string, UserButton> dicBn = new Dictionary<string, UserButton>();
        public Keyboard()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //SetHook();
            _thInput = new Thread(new ThreadStart(fn_InputThread));
            _thInput.Start();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(_thInput != null)
                _thInput.Abort();
            UnHook();
        }

        private void fn_InputThread()
        {
            while(true)
            {
                Thread.Sleep(100);
                if(queue.Count > 0)
                {
                    byte[] data = queue.Dequeue();
                    keybd_event(data[0], 0, data[1], 0);
                }
            }
        }

        private void KeyDown_Ascii(object sender, RoutedEventArgs e)
        {
            UserButton bn = sender as UserButton;
            if (bn != null)
            {
                byte[] data = new byte[2];
                try
                {
                    string strValue = bn.CommandParameter as string;
                    byte key = Convert.ToByte(strValue.Substring(2,2), 16);
                    keydownMode = _KeyDown;
                    
                    data[0] = key;
                    data[1] = keydownMode;
                    queue.Enqueue(data);
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// 다른 EXE 에서 포커스 유지.
        /// </summary>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            //Set the window style to noactivate.
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SetWindowLong(helper.Handle, GWL_EXSTYLE,
                GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE| WS_EX_TOPMOST);

            if (helper.Handle != null)
            {
                HwndSource source = HwndSource.FromHwnd(helper.Handle);
                source.AddHook(new HwndSourceHook(WndProc));
            }

        }

        /// <summary>
        /// 같은 EXE 안에서 포커스 유지.
        /// </summary>
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int WS_EX_TOPMOST    = 0x00000008;

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);



        private const int MA_NOACTIVATE = 3;
        private const int WM_MOUSEACTIVATE = 0x0021;

        private void Label_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void bn_Close_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            this.Hide();
        }

        private void bn_Loaded(object sender, RoutedEventArgs e)
        {
            UserButton bn = sender as UserButton;
            if (bn != null)
            {
                dicBn.Add(bn.CommandParameter as string, bn);
            }
        }

        private void cb_Topmost_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = cb_Topmost.IsChecked == true? true : false;
        }

        private void cb_Hooking_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb != null)
            {
                if (cb.IsChecked == true)
                    SetHook();
                else
                    UnHook();
            }
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //  do stuff
            if(msg == WM_MOUSEACTIVATE)
            {
                handled = true;
                return (IntPtr)MA_NOACTIVATE;
            }
            return IntPtr.Zero;
        }
    }
}
