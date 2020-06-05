using System;
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

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100; 

        private LowLevelKeyboardProc _proc = hookProc;

        private static IntPtr hhook = IntPtr.Zero;
        public void SetHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hInstance, 0);
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(hhook);
        }

        public static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                
                if (vkCode.ToString() == "162")
                {
                    MessageBox.Show("You pressed a CTR");
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

        SolidColorBrush Btn_Normal = new SolidColorBrush(Color.FromRgb(0xF7, 0xF7, 0xF7));
        SolidColorBrush Btn_Clicked = new SolidColorBrush(Color.FromRgb(0x42, 0xA2, 0xE7));

        Queue<byte[]> queue = new Queue<byte[]>();
        Thread _thInput;
        public Keyboard()
        {
            InitializeComponent();
            this.Topmost = true;
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _thInput = new Thread(new ThreadStart(fn_InputThread));
            _thInput.Start();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _thInput.Abort();
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
                    //string strValue = bn.Content as string;
                    string strValue = bn.CommandParameter as string;
                    byte key = 0x00;
                    keydownMode = _KeyDown;
                    switch (strValue)
                    {
                        case "`":
                            key = _keyvalue.VK_OEM_3;
                            break;
                        case "-":
                            key = _keyvalue.VK_OEM_MINUS;
                            break;
                        case "=":
                            key = _keyvalue.VK_OEM_PLUS;
                            break;
                        case "[":
                            key = _keyvalue.VK_OEM_4;
                            break;
                        case "]":
                            key = _keyvalue.VK_OEM_6;
                            break;
                        case "\\":
                            key = _keyvalue.VK_OEM_5;
                            break;
                        case ";":
                            key = _keyvalue.VK_OEM_1;
                            break;
                        case "'":
                            key = _keyvalue.VK_OEM_7;
                            break;
                        case ",":
                            key = _keyvalue.VK_OEM_COMMA;
                            break;
                        case ".":
                            key = _keyvalue.VK_OEM_PERIOD;
                            break;
                        case "/":
                            key = _keyvalue.VK_DIVIDE;
                            break;
                        case "+":
                            key = _keyvalue.VK_ADD;
                            break;
                        case "*":
                            key = _keyvalue.VK_MULTIPLY;
                            break;
                        case "Menu":
                            key = _keyvalue.VK_MENU;
                            break;
                        case "IME":
                             key = _keyvalue.VK_PROCESSKEY;
                            byte[] ime = new byte[2];
                            ime[0] = key;
                            ime[1] = _KeyDown;
                            queue.Enqueue(ime);
                            keydownMode = _KeyUp;
                            break;
                        default:
                            KeyConverter k = new KeyConverter();
                            Key mykey = (Key)k.ConvertFromString(strValue);
                            int keycode = KeyInterop.VirtualKeyFromKey(mykey);
                            key = Convert.ToByte(keycode);
                            if (mykey == Key.LeftCtrl || mykey == Key.RightCtrl)
                            {
                                if (bCtrl)
                                {
                                    bn.Background = Btn_Normal;
                                    keydownMode = _KeyUp;
                                }
                                else
                                {
                                    bn.Background = Btn_Clicked;
                                    //keydownMode = _KeyDown;
                                }
                                bCtrl = !bCtrl;
                            }
                            if (mykey == Key.LeftShift || mykey == Key.RightShift)
                            {
                                if (bShift)
                                {
                                    bn.Background = Btn_Normal;
                                    keydownMode = _KeyUp;
                                }
                                else
                                {
                                    bn.Background = Btn_Clicked;
                                    //keydownMode = _KeyDown;
                                }
                                bShift = !bShift;
                            }
                            if (mykey == Key.LeftAlt || mykey == Key.RightAlt)
                            {
                                if (bAlt)
                                {
                                    bn.Background = Btn_Normal;
                                    keydownMode = _KeyUp;
                                }
                                else
                                {
                                    bn.Background = Btn_Clicked;
                                    //keydownMode = _KeyDown;
                                }
                                bAlt = !bAlt;
                            }
                            if (mykey == Key.LWin || mykey == Key.RWin)
                            {
                                byte[] ime1 = new byte[2];
                                ime1[0] = key;
                                ime1[1] = _KeyDown;
                                queue.Enqueue(ime1);
                                keydownMode = _KeyUp;
                            }
                            if(mykey == Key.CapsLock)
                            {
                                if(bCaps)
                                {
                                    bn.Background = Btn_Normal;
                                    keydownMode = _KeyUp;
                                }
                                else
                                {
                                    bn.Background = Btn_Clicked;
                                    //keydownMode = _KeyDown;
                                }

                                bCaps = !bCaps;
                            }

                            if (mykey == Key.NumLock)
                            {
                                if (bNumlock)
                                {
                                    bn.Background = Btn_Normal;
                                    keydownMode = _KeyUp;
                                }
                                else
                                {
                                    bn.Background = Btn_Clicked;
                                    //keydownMode = _KeyDown;
                                }

                                bNumlock = !bNumlock;
                            }

                            if (mykey == Key.Scroll)
                            {
                                if (bScrollLock)
                                {
                                    bn.Background = Btn_Normal;
                                    keydownMode = _KeyUp;
                                }
                                else
                                {
                                    bn.Background = Btn_Clicked;
                                    //keydownMode = _KeyDown;
                                }

                                bScrollLock = !bScrollLock;
                            }
                            break;
                    }
                    
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
