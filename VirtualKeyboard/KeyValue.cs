using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard
{
    class KeyValue
    {
        public byte VK_LBUTTON = 0x01; // Left mouse button
        public byte VK_RBUTTON = 0x02; // Right mouse button
        public byte VK_CANCEL = 0x03;// Control-break processing
        public byte VK_MBUTTON = 0x04;// Middle mouse button(three-button mouse)
        public byte VK_XBUTTON1 = 0x05;// X1 mouse button
        public byte VK_XBUTTON2 = 0x06;// X2 mouse button
         // - 0x07; // Undefined
        public byte VK_BACK = 0x08;// BACKSPACE key
        public byte VK_TAB = 0x09;// TAB key
                            // - 0x0A-0B;// Reserved
        public byte VK_CLEAR = 0x0C;// CLEAR key
        public byte VK_RETURN = 0x0D;// ENTER key
                               // - // 0x0E-0F // Undefined
        public byte VK_SHIFT = 0x10;// SHIFT key
        public byte VK_CONTROL = 0x11;// CTRL key
        public byte VK_MENU = 0x12;// ALT key
        public byte VK_PAUSE = 0x13;// PAUSE key
        public byte VK_CAPITAL = 0x14;// CAPS LOCK key
        public byte VK_KANA = 0x15;// IME Kana mode
        public byte VK_HANGUEL = 0x15;// IME Hanguel mode(maintained for compatibility; use VK_HANGUL)
        public byte VK_HANGUL = 0x15;// IME Hangul mode
        public byte VK_IME_ON = 0x16;// IME On
        public byte VK_JUNJA = 0x17;// IME Junja mode
        public byte VK_FINAL = 0x18;// IME final mode
        public byte VK_HANJA = 0x19;// IME Hanja mode
        public byte VK_KANJI = 0x19;// IME Kanji mode
        public byte VK_IME_OFF = 0x1A;// IME Off
        public byte VK_ESCAPE = 0x1B;// ESC key
        public byte VK_CONVERT = 0x1C;// IME convert
        public byte VK_NONCONVERT = 0x1D;// IME nonconvert
        public byte VK_ACCEPT = 0x1E;// IME accept
        public byte VK_MODECHANGE = 0x1F;// IME mode change request
        public byte VK_SPACE = 0x20;// SPACEBAR
        public byte VK_PRIOR = 0x21;// PAGE UP key
        public byte VK_NEXT = 0x22;// PAGE DOWN key
        public byte VK_END = 0x23;// END key
        public byte VK_HOME = 0x24;// HOME key
        public byte VK_LEFT = 0x25;// LEFT ARROW key
        public byte VK_UP = 0x26;// UP ARROW key
        public byte VK_RIGHT = 0x27;// RIGHT ARROW key
        public byte VK_DOWN = 0x28;// DOWN ARROW key
        public byte VK_SELECT = 0x29;// SELECT key
        public byte VK_PRINT = 0x2A;// PRINT key
        public byte VK_EXECUTE = 0x2B;// EXECUTE key
        public byte VK_SNAPSHOT = 0x2C;// PRINT SCREEN key
        public byte VK_INSERT = 0x2D;// INS key
        public byte VK_DELETE = 0x2E;// DEL key
        public byte VK_HELP = 0x2F;// HELP key
        public byte key0 = 0x30;// 0 key
        public byte key1 = 0x31;// 1 key
        public byte key2 = 0x32;// 2 key
        public byte key3 = 0x33;// 3 key
        public byte key4 = 0x34;// 4 key
        public byte key5 = 0x35;// 5 key
        public byte key6 = 0x36;// 6 key
        public byte key7 = 0x37;// 7 key
        public byte key8 = 0x38;// 8 key
        public byte key9 = 0x39;// 9 key
                          // -
                          // 0x3A-40
                          // Undefined
        public byte keyA = 0x41;// A key
        public byte keyB = 0x42;// B key
        public byte keyC = 0x43;// C key
        public byte keyD = 0x44;// D key
        public byte keyE = 0x45;// E key
        public byte keyF = 0x46;// F key
        public byte keyG = 0x47;// G key
        public byte keyH = 0x48;// H key
        public byte keyI = 0x49;// I key
        public byte keyJ = 0x4A;// J key
        public byte keyK = 0x4B;// K key
        public byte keyL = 0x4C;// L key
        public byte keyM = 0x4D;// M key
        public byte keyN = 0x4E;// N key
        public byte keyO = 0x4F;// O key
        public byte keyP = 0x50;// P key
        public byte keyQ = 0x51;// Q key
        public byte keyR = 0x52;// R key
        public byte keyS = 0x53;// S key
        public byte keyT = 0x54;// T key
        public byte keyU = 0x55;// U key
        public byte keyV = 0x56;// V key
        public byte keyW = 0x57;// W key
        public byte keyX = 0x58;// X key
        public byte keyY = 0x59;// Y key
        public byte keyZ = 0x5A;// Z key
        public byte VK_LWIN = 0x5B;// Left Windows key(Natural keyboard)
        public byte VK_RWIN = 0x5C;// Right Windows key(Natural keyboard)
        public byte VK_APPS = 0x5D;// Applications key(Natural keyboard)
                             // -
                             // 0x5E
                             // Reserved
        public byte VK_SLEEP = 0x5F;// Computer Sleep key
        public byte VK_NUMPAD0 = 0x60;// Numeric keypad 0 key
        public byte VK_NUMPAD1 = 0x61;// Numeric keypad 1 key
        public byte VK_NUMPAD2 = 0x62;// Numeric keypad 2 key
        public byte VK_NUMPAD3 = 0x63;// Numeric keypad 3 key
        public byte VK_NUMPAD4 = 0x64;// Numeric keypad 4 key
        public byte VK_NUMPAD5 = 0x65;// Numeric keypad 5 key
        public byte VK_NUMPAD6 = 0x66;// Numeric keypad 6 key
        public byte VK_NUMPAD7 = 0x67;// Numeric keypad 7 key
        public byte VK_NUMPAD8 = 0x68;// Numeric keypad 8 key
        public byte VK_NUMPAD9 = 0x69;// Numeric keypad 9 key
        public byte VK_MULTIPLY = 0x6A;// Multiply key
        public byte VK_ADD = 0x6B;// Add key
        public byte VK_SEPARATOR = 0x6C;// Separator key
        public byte VK_SUBTRACT = 0x6D;// Subtract key
        public byte VK_DECIMAL = 0x6E;// Decimal key
        public byte VK_DIVIDE = 0x6F;// Divide key
        public byte VK_F1 = 0x70;// F1 key
        public byte VK_F2 = 0x71;// F2 key
        public byte VK_F3 = 0x72;// F3 key
        public byte VK_F4 = 0x73;// F4 key
        public byte VK_F5 = 0x74;// F5 key
        public byte VK_F6 = 0x75;// F6 key
        public byte VK_F7 = 0x76;// F7 key
        public byte VK_F8 = 0x77;// F8 key
        public byte VK_F9 = 0x78;// F9 key
        public byte VK_F10 = 0x79;// F10 key
        public byte VK_F11 = 0x7A;// F11 key
        public byte VK_F12 = 0x7B;// F12 key
        public byte VK_F13 = 0x7C;// F13 key
        public byte VK_F14 = 0x7D;// F14 key
        public byte VK_F15 = 0x7E;// F15 key
        public byte VK_F16 = 0x7F;// F16 key
        public byte VK_F17 = 0x80;// F17 key
        public byte VK_F18 = 0x81;// F18 key
        public byte VK_F19 = 0x82;// F19 key
        public byte VK_F20 = 0x83;// F20 key
        public byte VK_F21 = 0x84;// F21 key
        public byte VK_F22 = 0x85;// F22 key
        public byte VK_F23 = 0x86;// F23 key
        public byte VK_F24 = 0x87;// F24 key
                            // -
                            // 0x88-8F
                            // Unassigned
        public byte VK_NUMLOCK = 0x90;// NUM LOCK key
        public byte VK_SCROLL = 0x91;// SCROLL LOCK key
                               // 0x92-96
                               // OEM specific
                               // -
                               // 0x97-9F
                               // Unassigned
        public byte VK_LSHIFT = 0xA0;// Left SHIFT key
        public byte VK_RSHIFT = 0xA1;// Right SHIFT key
        public byte VK_LCONTROL = 0xA2;// Left CONTROL key
        public byte VK_RCONTROL = 0xA3;// Right CONTROL key
        public byte VK_LMENU = 0xA4;// Left MENU key
        public byte VK_RMENU = 0xA5;// Right MENU key
        public byte VK_BROWSER_BACK = 0xA6;// Browser Back key
        public byte VK_BROWSER_FORWARD = 0xA7;// Browser Forward key
        public byte VK_BROWSER_REFRESH = 0xA8;// Browser Refresh key
        public byte VK_BROWSER_STOP = 0xA9;// Browser Stop key
        public byte VK_BROWSER_SEARCH = 0xAA;// Browser Search key
        public byte VK_BROWSER_FAVORITES = 0xAB;// Browser Favorites key
        public byte VK_BROWSER_HOME = 0xAC;// Browser Start and Home key
        public byte VK_VOLUME_MUTE = 0xAD;// Volume Mute key
        public byte VK_VOLUME_DOWN = 0xAE;// Volume Down key
        public byte VK_VOLUME_UP = 0xAF;// Volume Up key
        public byte VK_MEDIA_NEXT_TRACK = 0xB0;// Next Track key
        public byte VK_MEDIA_PREV_TRACK = 0xB1;// Previous Track key
        public byte VK_MEDIA_STOP = 0xB2;// Stop Media key
        public byte VK_MEDIA_PLAY_PAUSE = 0xB3;// Play/Pause Media key
        public byte VK_LAUNCH_MAIL = 0xB4;// Start Mail key
        public byte VK_LAUNCH_MEDIA_SELECT = 0xB5;// Select Media key
        public byte VK_LAUNCH_APP1 = 0xB6;// Start Application 1 key
        public byte VK_LAUNCH_APP2 = 0xB7;// Start Application 2 key
                                    // -
                                    // 0xB8-B9
                                    // Reserved
        public byte VK_OEM_1 = 0xBA;// Used for miscellaneous characters; it can vary by keyboard.
                              // For the US standard keyboard, the ';:' key
        public byte VK_OEM_PLUS = 0xBB;// For any country/region, the '+' key
        public byte VK_OEM_COMMA = 0xBC;// For any country/region, the ',' key
        public byte VK_OEM_MINUS = 0xBD;// For any country/region, the '-' key
        public byte VK_OEM_PERIOD = 0xBE;// For any country/region, the '.' key
        public byte VK_OEM_2 = 0xBF;// Used for miscellaneous characters; it can vary by keyboard.
                              // For the US standard keyboard, the '/?' key
        public byte VK_OEM_3 = 0xC0;// Used for miscellaneous characters; it can vary by keyboard.
                              // For the US standard keyboard, the '`~' key
                              // -
                              // 0xC1-D7
                              // Reserved
                              // -
                              // 0xD8-DA
                              // Unassigned
        public byte VK_OEM_4 = 0xDB;// Used for miscellaneous characters; it can vary by keyboard.
                              // For the US standard keyboard, the '[{' key
        public byte VK_OEM_5 = 0xDC;// Used for miscellaneous characters; it can vary by keyboard.
                              // For the US standard keyboard, the '\|' key
        public byte VK_OEM_6 = 0xDD;// Used for miscellaneous characters; it can vary by keyboard.
                              // For the US standard keyboard, the ']}' key
        public byte VK_OEM_7 = 0xDE;// Used for miscellaneous characters; it can vary by keyboard.
                              // For the US standard keyboard, the 'single-quote/double-quote' key
        public byte VK_OEM_8 = 0xDF;// Used for miscellaneous characters; it can vary by keyboard.
                              // -
                              // 0xE0
                              // Reserved
                              // 0xE1
                              // OEM specific
        public byte VK_OEM_102 = 0xE2;// Either the angle bracket key or the backslash key on the RT 102-key keyboard
                                // 0xE3-E4
                                // OEM specific
        public byte VK_PROCESSKEY = 0xE5;// IME PROCESS key
                                   // 0xE6
                                   // OEM specific
        public byte VK_PACKET = 0xE7;// Used to pass Unicode characters as if they were keystrokes.The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods.For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
                               // -
                               // 0xE8
                               // Unassigned
                               // 0xE9-F5
                               // OEM specific
        public byte VK_ATTN = 0xF6;// Attn key
        public byte VK_CRSEL = 0xF7;// CrSel key
        public byte VK_EXSEL = 0xF8;// ExSel key
        public byte VK_EREOF = 0xF9;// Erase EOF key
        public byte VK_PLAY = 0xFA;// Play key
        public byte VK_ZOOM = 0xFB;// Zoom key
        public byte VK_NONAME = 0xFC;// Reserved
        public byte VK_PA1 = 0xFD;// PA1 key
        public byte VK_OEM_CLEAR = 0xFE;// Clear key
    }
}
