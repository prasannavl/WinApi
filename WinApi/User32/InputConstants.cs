using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace WinApi.User32
{
    [Flags]
    public enum KeyModifierFlags
    {
        /// <summary>
        ///     Either ALT key must be held down.
        /// </summary>
        MOD_ALT = 0x0001,

        /// <summary>
        ///     Either CTRL key must be held down.
        /// </summary>
        MOD_CONTROL = 0x0002,

        /// <summary>
        ///     Changes the hotkey behavior so that the keyboard auto-repeat does not yield multiple hotkey notifications.
        ///     Windows Vista:  This flag is not supported.
        /// </summary>
        MOD_NOREPEAT = 0x4000,

        /// <summary>
        ///     Either SHIFT key must be held down.
        /// </summary>
        MOD_SHIFT = 0x0004,

        /// <summary>
        ///     Either WINDOWS key was held down. These keys are labeled with the Windows logo. Keyboard shortcuts that involve the
        ///     WINDOWS key are reserved for use by the operating system.
        /// </summary>
        MOD_WIN = 0x0008
    }

    [Flags]
    public enum MouseInputKeyStateFlags
    {
        /// <summary>
        ///     The CTRL key is down.
        /// </summary>
        MK_CONTROL = 0x0008,

        /// <summary>
        ///     The left mouse button is down.
        /// </summary>
        MK_LBUTTON = 0x0001,

        /// <summary>
        ///     The middle mouse button is down.
        /// </summary>
        MK_MBUTTON = 0x0010,

        /// <summary>
        ///     The right mouse button is down.
        /// </summary>
        MK_RBUTTON = 0x0002,

        /// <summary>
        ///     The SHIFT key is down.
        /// </summary>
        MK_SHIFT = 0x0004,

        /// <summary>
        ///     The first X button is down.
        /// </summary>
        MK_XBUTTON1 = 0x0020,

        /// <summary>
        ///     The second X button is down.
        /// </summary>
        MK_XBUTTON2 = 0x0040
    }

    [Flags]
    public enum HotKeyInputState
    {
        /// <summary>
        ///     Either ALT key was held down.
        /// </summary>
        MOD_ALT = 0x0001,

        /// <summary>
        ///     Either CTRL key was held down.
        /// </summary>
        MOD_CONTROL = 0x0002,

        /// <summary>
        ///     Either SHIFT key was held down.
        /// </summary>
        MOD_SHIFT = 0x0004,

        /// <summary>
        ///     Either WINDOWS key was held down. These keys are labeled with the Windows logo. Hotkeys that involve the Windows
        ///     key are reserved for use by the operating system.
        /// </summary>
        MOD_WIN = 0x0008
    }

    [Flags]
    public enum MouseInputXButtonFlag
    {
        /// <summary>
        ///     The first X button was clicked.
        /// </summary>
        XBUTTON1 = 0x0001,

        /// <summary>
        ///     The second X button was clicked.
        /// </summary>
        XBUTTON2 = 0x0002
    }

    [Flags]
    public enum MouseInputFlags
    {
        /// <summary>
        ///     The dx and dy members contain normalized absolute coordinates. If the flag is not set, dxand dy contain relative
        ///     data (the change in position since the last reported position). This flag can be set, or not set, regardless of
        ///     what kind of mouse or other pointing device, if any, is connected to the system. For further information about
        ///     relative mouse motion, see the following Remarks section.
        /// </summary>
        MOUSEEVENTF_ABSOLUTE = 0x8000,

        /// <summary>
        ///     The wheel was moved horizontally, if the mouse has a wheel. The amount of movement is specified in mouseData.
        ///     Windows XP/2000:  This value is not supported.
        /// </summary>
        MOUSEEVENTF_HWHEEL = 0x01000,

        /// <summary>
        ///     Movement occurred.
        /// </summary>
        MOUSEEVENTF_MOVE = 0x0001,

        /// <summary>
        ///     The WM_MOUSEMOVE messages will not be coalesced. The default behavior is to coalesce WM_MOUSEMOVE messages.
        ///     Windows XP/2000:  This value is not supported.
        /// </summary>
        MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000,

        /// <summary>
        ///     The left button was pressed.
        /// </summary>
        MOUSEEVENTF_LEFTDOWN = 0x0002,

        /// <summary>
        ///     The left button was released.
        /// </summary>
        MOUSEEVENTF_LEFTUP = 0x0004,

        /// <summary>
        ///     The right button was pressed.
        /// </summary>
        MOUSEEVENTF_RIGHTDOWN = 0x0008,

        /// <summary>
        ///     The right button was released.
        /// </summary>
        MOUSEEVENTF_RIGHTUP = 0x0010,

        /// <summary>
        ///     The middle button was pressed.
        /// </summary>
        MOUSEEVENTF_MIDDLEDOWN = 0x0020,

        /// <summary>
        ///     The middle button was released.
        /// </summary>
        MOUSEEVENTF_MIDDLEUP = 0x0040,

        /// <summary>
        ///     Maps coordinates to the entire desktop. Must be used with MOUSEEVENTF_ABSOLUTE.
        /// </summary>
        MOUSEEVENTF_VIRTUALDESK = 0x4000,

        /// <summary>
        ///     The wheel was moved, if the mouse has a wheel. The amount of movement is specified in mouseData.
        /// </summary>
        MOUSEEVENTF_WHEEL = 0x0800,

        /// <summary>
        ///     An X button was pressed.
        /// </summary>
        MOUSEEVENTF_XDOWN = 0x0080,

        /// <summary>
        ///     An X button was released.
        /// </summary>
        MOUSEEVENTF_XUP = 0x0100
    }

    [Flags]
    public enum KeyboardInputFlags
    {
        /// <summary>
        ///     If specified, the scan code was preceded by a prefix byte that has the value 0xE0 (224).
        /// </summary>
        KEYEVENTF_EXTENDEDKEY = 0x0001,

        /// <summary>
        ///     If specified, the key is being released. If not specified, the key is being pressed.
        /// </summary>
        KEYEVENTF_KEYUP = 0x0002,

        /// <summary>
        ///     If specified, wScan identifies the key and wVk is ignored.
        /// </summary>
        KEYEVENTF_SCANCODE = 0x0008,

        /// <summary>
        ///     If specified, the system synthesizes a VK_PACKET keystroke. The wVk parameter must be zero. This flag can only be
        ///     combined with the KEYEVENTF_KEYUP flag. For more information, see the Remarks section.
        /// </summary>
        KEYEVENTF_UNICODE = 0x0004
    }

    [Flags]
    public enum CursorInfoFlags
    {
        /// <summary>
        ///     The cursor is showing.
        /// </summary>
        CURSOR_SHOWING = 0x00000001,

        /// <summary>
        ///     Windows 8: The cursor is suppressed. This flag indicates that the system is not drawing the cursor because the user
        ///     is providing input through touch or pen instead of the mouse.
        /// </summary>
        CURSOR_SUPPRESSED = 0x00000002
    }

    public enum InputType
    {
        /// <summary>
        ///     The event is a mouse event. Use the mi structure of the union.
        /// </summary>
        INPUT_MOUSE = 0,

        /// <summary>
        ///     The event is a keyboard event. Use the ki structure of the union.
        /// </summary>
        INPUT_KEYBOARD = 1,

        /// <summary>
        ///     The event is a hardware event. Use the hi structure of the union.
        /// </summary>
        INPUT_HARDWARE = 2
    }

    public enum VirtualKeyMapType
    {
        /// <summary>
        ///     The uCode parameter is a virtual-key code and is translated into an unshifted character value in the low order word
        ///     of the return value. Dead keys (diacritics) are indicated by setting the top bit of the return value. If there is
        ///     no translation, the function returns 0.
        /// </summary>
        MAPVK_VK_TO_CHAR = 2,

        /// <summary>
        ///     The uCode parameter is a virtual-key code and is translated into a scan code. If it is a virtual-key code that does
        ///     not distinguish between left- and right-hand keys, the left-hand scan code is returned. If there is no translation,
        ///     the function returns 0.
        /// </summary>
        MAPVK_VK_TO_VSC = 0,

        /// <summary>
        ///     The uCode parameter is a virtual-key code and is translated into a scan code. If it is a virtual-key code that does
        ///     not distinguish between left- and right-hand keys, the left-hand scan code is returned. If the scan code is an
        ///     extended scan code, the high byte of the uCode value can contain either 0xe0 or 0xe1 to specify the extended scan
        ///     code. If there is no translation, the function returns 0.
        /// </summary>
        MAPVK_VK_TO_VSC_EX = 4,

        /// <summary>
        ///     The uCode parameter is a scan code and is translated into a virtual-key code that does not distinguish between
        ///     left- and right-hand keys. If there is no translation, the function returns 0.
        /// </summary>
        MAPVK_VSC_TO_VK = 1,

        /// <summary>
        ///     The uCode parameter is a scan code and is translated into a virtual-key code that distinguishes between left- and
        ///     right-hand keys. If there is no translation, the function returns 0.
        /// </summary>
        MAPVK_VSC_TO_VK_EX = 3
    }

    public enum VirtualKey
    {
        LBUTTON = 0x01,
        RBUTTON = 0x02,
        CANCEL = 0x03,
        MBUTTON = 0x04 /* NOT contiguous with L & RBUTTON */,
        XBUTTON1 = 0x05 /* NOT contiguous with L & RBUTTON */,
        XBUTTON2 = 0x06 /* NOT contiguous with L & RBUTTON */,
        BACK = 0x08,
        TAB = 0x09,
        CLEAR = 0x0C,
        RETURN = 0x0D,
        SHIFT = 0x10,
        CONTROL = 0x11,
        MENU = 0x12,
        PAUSE = 0x13,
        CAPITAL = 0x14,
        KANA = 0x15,
        HANGEUL = 0x15 /* old name - should be here for compatibility */,
        HANGUL = 0x15,
        JUNJA = 0x17,
        FINAL = 0x18,
        HANJA = 0x19,
        KANJI = 0x19,
        ESCAPE = 0x1B,
        CONVERT = 0x1C,
        NONCONVERT = 0x1D,
        ACCEPT = 0x1E,
        MODECHANGE = 0x1F,
        SPACE = 0x20,
        PRIOR = 0x21,
        NEXT = 0x22,
        END = 0x23,
        HOME = 0x24,
        LEFT = 0x25,
        UP = 0x26,
        RIGHT = 0x27,
        DOWN = 0x28,
        SELECT = 0x29,
        PRINT = 0x2A,
        EXECUTE = 0x2B,
        SNAPSHOT = 0x2C,
        INSERT = 0x2D,
        DELETE = 0x2E,
        HELP = 0x2F,
        LWIN = 0x5B,
        RWIN = 0x5C,
        APPS = 0x5D,
        SLEEP = 0x5F,
        NUMPAD0 = 0x60,
        NUMPAD1 = 0x61,
        NUMPAD2 = 0x62,
        NUMPAD3 = 0x63,
        NUMPAD4 = 0x64,
        NUMPAD5 = 0x65,
        NUMPAD6 = 0x66,
        NUMPAD7 = 0x67,
        NUMPAD8 = 0x68,
        NUMPAD9 = 0x69,
        MULTIPLY = 0x6A,
        ADD = 0x6B,
        SEPARATOR = 0x6C,
        SUBTRACT = 0x6D,
        DECIMAL = 0x6E,
        DIVIDE = 0x6F,
        F1 = 0x70,
        F2 = 0x71,
        F3 = 0x72,
        F4 = 0x73,
        F5 = 0x74,
        F6 = 0x75,
        F7 = 0x76,
        F8 = 0x77,
        F9 = 0x78,
        F10 = 0x79,
        F11 = 0x7A,
        F12 = 0x7B,
        F13 = 0x7C,
        F14 = 0x7D,
        F15 = 0x7E,
        F16 = 0x7F,
        F17 = 0x80,
        F18 = 0x81,
        F19 = 0x82,
        F20 = 0x83,
        F21 = 0x84,
        F22 = 0x85,
        F23 = 0x86,
        F24 = 0x87,
        NUMLOCK = 0x90,
        SCROLL = 0x91,
        OEM_NEC_EQUAL = 0x92, // '=' key on numpad,
        OEM_FJ_JISHO = 0x92, // 'Dictionary' key,
        OEM_FJ_MASSHOU = 0x93, // 'Unregister word' key,
        OEM_FJ_TOUROKU = 0x94, // 'Register word' key,
        OEM_FJ_LOYA = 0x95, // 'Left OYAYUBI' key,
        OEM_FJ_ROYA = 0x96, // 'Right OYAYUBI' key,
        LSHIFT = 0xA0,
        RSHIFT = 0xA1,
        LCONTROL = 0xA2,
        RCONTROL = 0xA3,
        LMENU = 0xA4,
        RMENU = 0xA5,
        BROWSER_BACK = 0xA6,
        BROWSER_FORWARD = 0xA7,
        BROWSER_REFRESH = 0xA8,
        BROWSER_STOP = 0xA9,
        BROWSER_SEARCH = 0xAA,
        BROWSER_FAVORITES = 0xAB,
        BROWSER_HOME = 0xAC,
        VOLUME_MUTE = 0xAD,
        VOLUME_DOWN = 0xAE,
        VOLUME_UP = 0xAF,
        MEDIA_NEXT_TRACK = 0xB0,
        MEDIA_PREV_TRACK = 0xB1,
        MEDIA_STOP = 0xB2,
        MEDIA_PLAY_PAUSE = 0xB3,
        LAUNCH_MAIL = 0xB4,
        LAUNCH_MEDIA_SELECT = 0xB5,
        LAUNCH_APP1 = 0xB6,
        LAUNCH_APP2 = 0xB7,
        OEM_1 = 0xBA, // ';:' for US,
        OEM_PLUS = 0xBB, // '+' any country,
        OEM_COMMA = 0xBC, // ',' any country,
        OEM_MINUS = 0xBD, // '-' any country,
        OEM_PERIOD = 0xBE, // '.' any country,
        OEM_2 = 0xBF, // '/?' for US,
        OEM_3 = 0xC0, // '`~' for US,
        OEM_4 = 0xDB, //  '[{' for US,
        OEM_5 = 0xDC, //  '\|' for US,
        OEM_6 = 0xDD, //  ']}' for US,
        OEM_7 = 0xDE, //  ''"' for US,
        OEM_8 = 0xDF,
        OEM_AX = 0xE1, //  'AX' key on Japanese AX kbd,
        OEM_102 = 0xE2, //  "<>" or "\|" on RT 102-key kbd.,
        ICO_HELP = 0xE3, //  Help key on ICO,
        ICO_00 = 0xE4, //  00 key on ICO,
        PROCESSKEY = 0xE5,
        ICO_CLEAR = 0xE6,
        PACKET = 0xE7,
        OEM_RESET = 0xE9,
        OEM_JUMP = 0xEA,
        OEM_PA1 = 0xEB,
        OEM_PA2 = 0xEC,
        OEM_PA3 = 0xED,
        OEM_WSCTRL = 0xEE,
        OEM_CUSEL = 0xEF,
        OEM_ATTN = 0xF0,
        OEM_FINISH = 0xF1,
        OEM_COPY = 0xF2,
        OEM_AUTO = 0xF3,
        OEM_ENLW = 0xF4,
        OEM_BACKTAB = 0xF5,
        ATTN = 0xF6,
        CRSEL = 0xF7,
        EXSEL = 0xF8,
        EREOF = 0xF9,
        PLAY = 0xFA,
        ZOOM = 0xFB,
        NONAME = 0xFC,
        PA1 = 0xFD,
        OEM_CLEAR = 0xFE,
        A = 0x41,
        B = 0x42,
        C = 0x43,
        D = 0x44,
        E = 0x45,
        F = 0x46,
        G = 0x47,
        H = 0x48,
        I = 0x49,
        J = 0x4a,
        K = 0x4b,
        L = 0x4c,
        M = 0x4d,
        N = 0x4e,
        O = 0x4f,
        P = 0x50,
        Q = 0x51,
        R = 0x52,
        S = 0x53,
        T = 0x54,
        U = 0x55,
        V = 0x56,
        W = 0x57,
        X = 0x58,
        Y = 0x59,
        Z = 0x5a,
        D0 = 0x30,
        D1 = 0x31,
        D2 = 0x32,
        D3 = 0x33,
        D4 = 0x34,
        D5 = 0x35,
        D6 = 0x36,
        D7 = 0x37,
        D8 = 0x38,
        D9 = 0x39
    }

    public enum ScanCodes
    {
        LBUTTON = 0,
        RBUTTON = 0,
        CANCEL = 70,
        MBUTTON = 0,
        XBUTTON1 = 0,
        XBUTTON2 = 0,
        BACK = 14,
        TAB = 15,
        CLEAR = 76,
        RETURN = 28,
        SHIFT = 42,
        CONTROL = 29,
        MENU = 56,
        PAUSE = 0,
        CAPITAL = 58,
        KANA = 0,
        HANGUL = 0,
        JUNJA = 0,
        FINAL = 0,
        HANJA = 0,
        KANJI = 0,
        ESCAPE = 1,
        CONVERT = 0,
        NONCONVERT = 0,
        ACCEPT = 0,
        MODECHANGE = 0,
        SPACE = 57,
        PRIOR = 73,
        NEXT = 81,
        END = 79,
        HOME = 71,
        LEFT = 75,
        UP = 72,
        RIGHT = 77,
        DOWN = 80,
        SELECT = 0,
        PRINT = 0,
        EXECUTE = 0,
        SNAPSHOT = 84,
        INSERT = 82,
        DELETE = 83,
        HELP = 99,
        KEY_0 = 11,
        KEY_1 = 2,
        KEY_2 = 3,
        KEY_3 = 4,
        KEY_4 = 5,
        KEY_5 = 6,
        KEY_6 = 7,
        KEY_7 = 8,
        KEY_8 = 9,
        KEY_9 = 10,
        KEY_A = 30,
        KEY_B = 48,
        KEY_C = 46,
        KEY_D = 32,
        KEY_E = 18,
        KEY_F = 33,
        KEY_G = 34,
        KEY_H = 35,
        KEY_I = 23,
        KEY_J = 36,
        KEY_K = 37,
        KEY_L = 38,
        KEY_M = 50,
        KEY_N = 49,
        KEY_O = 24,
        KEY_P = 25,
        KEY_Q = 16,
        KEY_R = 19,
        KEY_S = 31,
        KEY_T = 20,
        KEY_U = 22,
        KEY_V = 47,
        KEY_W = 17,
        KEY_X = 45,
        KEY_Y = 21,
        KEY_Z = 44,
        LWIN = 91,
        RWIN = 92,
        APPS = 93,
        SLEEP = 95,
        NUMPAD0 = 82,
        NUMPAD1 = 79,
        NUMPAD2 = 80,
        NUMPAD3 = 81,
        NUMPAD4 = 75,
        NUMPAD5 = 76,
        NUMPAD6 = 77,
        NUMPAD7 = 71,
        NUMPAD8 = 72,
        NUMPAD9 = 73,
        MULTIPLY = 55,
        ADD = 78,
        SEPARATOR = 0,
        SUBTRACT = 74,
        DECIMAL = 83,
        DIVIDE = 53,
        F1 = 59,
        F2 = 60,
        F3 = 61,
        F4 = 62,
        F5 = 63,
        F6 = 64,
        F7 = 65,
        F8 = 66,
        F9 = 67,
        F10 = 68,
        F11 = 87,
        F12 = 88,
        F13 = 100,
        F14 = 101,
        F15 = 102,
        F16 = 103,
        F17 = 104,
        F18 = 105,
        F19 = 106,
        F20 = 107,
        F21 = 108,
        F22 = 109,
        F23 = 110,
        F24 = 118,
        NUMLOCK = 69,
        SCROLL = 70,
        LSHIFT = 42,
        RSHIFT = 54,
        LCONTROL = 29,
        RCONTROL = 29,
        LMENU = 56,
        RMENU = 56,
        BROWSER_BACK = 106,
        BROWSER_FORWARD = 105,
        BROWSER_REFRESH = 103,
        BROWSER_STOP = 104,
        BROWSER_SEARCH = 101,
        BROWSER_FAVORITES = 102,
        BROWSER_HOME = 50,
        VOLUME_MUTE = 32,
        VOLUME_DOWN = 46,
        VOLUME_UP = 48,
        MEDIA_NEXT_TRACK = 25,
        MEDIA_PREV_TRACK = 16,
        MEDIA_STOP = 36,
        MEDIA_PLAY_PAUSE = 34,
        LAUNCH_MAIL = 108,
        LAUNCH_MEDIA_SELECT = 109,
        LAUNCH_APP1 = 107,
        LAUNCH_APP2 = 33,
        OEM_1 = 39,
        OEM_PLUS = 13,
        OEM_COMMA = 51,
        OEM_MINUS = 12,
        OEM_PERIOD = 52,
        OEM_2 = 53,
        OEM_3 = 41,
        OEM_4 = 26,
        OEM_5 = 43,
        OEM_6 = 27,
        OEM_7 = 40,
        OEM_8 = 0,
        OEM_102 = 86,
        PROCESSKEY = 0,
        PACKET = 0,
        ATTN = 0,
        CRSEL = 0,
        EXSEL = 0,
        EREOF = 93,
        PLAY = 0,
        ZOOM = 98,
        NONAME = 0,
        PA1 = 0,
        OEM_CLEAR = 0
    }
}