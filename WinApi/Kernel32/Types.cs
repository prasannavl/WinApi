using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace WinApi.Kernel32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ShortPoint
    {
        public ShortPoint(short x, short y)
        {
            X = x;
            Y = y;
        }

        public short X, Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ShortRectangle
    {
        public ShortRectangle(short left = 0, short top = 0, short right = 0, short bottom = 0)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public short Left, Top, Right, Bottom;

        public short Width
        {
            get { return (short) (Right - Left); }
            set { Right = (short) (Left + value); }
        }

        public short Height
        {
            get { return (short) (Bottom - Top); }
            set { Bottom = (short) (Top + value); }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SystemInfo
    {
        public ushort ProcessorArchitecture;
        readonly ushort Reserved;
        public uint PageSize;
        public IntPtr MinimumApplicationAddress;
        public IntPtr MaximumApplicationAddress;
        public IntPtr ActiveProcessorMask;
        public uint NumberOfProcessors;
        public uint ProcessorType;
        public uint AllocationGranularity;
        public ushort ProcessorLevel;
        public ushort ProcessorRevision;
        public uint OemId => ((uint) ProcessorArchitecture << 8) | Reserved;
    }

    public enum ProcessArchitecture
    {
        PROCESSOR_ARCHITECTURE_INTEL = 0,
        PROCESSOR_ARCHITECTURE_MIPS = 1,
        PROCESSOR_ARCHITECTURE_ALPHA = 2,
        PROCESSOR_ARCHITECTURE_PPC = 3,
        PROCESSOR_ARCHITECTURE_SHX = 4,
        PROCESSOR_ARCHITECTURE_ARM = 5,
        PROCESSOR_ARCHITECTURE_IA64 = 6,
        PROCESSOR_ARCHITECTURE_ALPHA64 = 7,
        PROCESSOR_ARCHITECTURE_MSIL = 8,
        PROCESSOR_ARCHITECTURE_AMD64 = 9,
        PROCESSOR_ARCHITECTURE_IA32_ON_WIN64 = 10,
        PROCESSOR_ARCHITECTURE_NEUTRAL = 11,
        PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF
    }

    public enum StdHandle
    {
        /// <summary>
        ///     The standard input device. Initially, this is the console input buffer, CONIN$.
        /// </summary>
        STD_INPUT_HANDLE = unchecked((int) (uint) -10),

        /// <summary>
        ///     The standard output device. Initially, this is the active console screen buffer, CONOUT$.
        /// </summary>
        STD_OUTPUT_HANDLE = unchecked((int) (uint) -11),

        /// <summary>
        ///     The standard error device. Initially, this is the active console screen buffer, CONOUT$.
        /// </summary>
        STD_ERROR_HANDLE = unchecked((int) (uint) -12)
    }

    [Flags]
    public enum LoadLibraryFlags
    {
        /// <summary>
        ///     If this value is used, and the executable module is a DLL, the system does not call
        ///     DllMain for process and thread initialization and
        ///     termination. Also, the system does not load additional executable modules that are referenced by the
        ///     specified module.
        ///     Note  Do not use this value; it is provided only for backward compatibility. If you are planning to access
        ///     only data or resources in the DLL, use LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE or
        ///     LOAD_LIBRARY_AS_IMAGE_RESOURCE or both. Otherwise, load the library as a DLL or
        ///     executable module using the LoadLibrary
        ///     function.
        /// </summary>
        DONT_RESOLVE_DLL_REFERENCES = 0x00000001,

        /// <summary>
        ///     If this value is used, the system does not check
        ///     AppLocker rules or apply
        ///     Software Restriction Policies
        ///     for the DLL. This action applies only to the DLL being loaded and not to its dependencies. This value is
        ///     recommended for use in setup programs that must run extracted DLLs during installation.
        ///     Windows Server 2008 R2 and Windows 7:  On systems with KB2532445 installed, the caller must be running as
        ///     "LocalSystem" or
        ///     "TrustedInstaller"; otherwise the system ignores this flag. For more information, see
        ///     "You can circumvent AppLocker rules by using an Office macro on a computer that is running Windows 7 or Windows
        ///     Server 2008 R2"
        ///     in the Help and Support Knowledge Base at
        ///     http://support.microsoft.com/kb/2532445.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP:  AppLocker was introduced in Windows 7 and
        ///     Windows Server 2008 R2.
        /// </summary>
        LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,

        /// <summary>
        ///     If this value is used, the system maps the file into the calling process's virtual address space as if it
        ///     were a data file. Nothing is done to execute or prepare to execute the mapped file. Therefore, you cannot
        ///     call functions like GetModuleFileName,
        ///     GetModuleHandle or
        ///     GetProcAddress with this DLL. Using this value
        ///     causes writes to read-only memory to raise an access violation. Use this flag when you want to load a DLL
        ///     only to extract messages or resources from it.
        ///     This value can be used with LOAD_LIBRARY_AS_IMAGE_RESOURCE. For more information,
        ///     see Remarks.
        /// </summary>
        LOAD_LIBRARY_AS_DATAFILE = 0x00000002,

        /// <summary>
        ///     Similar to LOAD_LIBRARY_AS_DATAFILE, except that the DLL file is opened with
        ///     exclusive write access for the calling process. Other processes cannot open the DLL file for write access
        ///     while it is in use. However, the DLL can still be opened by other processes.
        ///     This value can be used with LOAD_LIBRARY_AS_IMAGE_RESOURCE. For more information,
        ///     see Remarks.
        ///     Windows Server 2003 and Windows XP:  This value is not supported until Windows Vista.
        /// </summary>
        LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,

        /// <summary>
        ///     If this value is used, the system maps the file into the process's virtual address space as an image file.
        ///     However, the loader does not load the static imports or perform the other usual initialization steps. Use
        ///     this flag when you want to load a DLL only to extract messages or resources from it.
        ///     Unless the application depends on the file having the in-memory layout of an image, this value should be used with
        ///     either
        ///     LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE or
        ///     LOAD_LIBRARY_AS_DATAFILE. For more information, see the Remarks section.
        ///     Windows Server 2003 and Windows XP:  This value is not supported  until Windows Vista.
        /// </summary>
        LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,

        /// <summary>
        ///     If this value is used, the application's installation directory is searched for the DLL and its
        ///     dependencies. Directories in the standard search path are not searched. This value cannot be combined with
        ///     LOAD_WITH_ALTERED_SEARCH_PATH.
        ///     Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires
        ///     KB2533623 to be
        ///     installed.
        ///     Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,

        /// <summary>
        ///     This value is a combination of LOAD_LIBRARY_SEARCH_APPLICATION_DIR,
        ///     LOAD_LIBRARY_SEARCH_SYSTEM32, and
        ///     LOAD_LIBRARY_SEARCH_USER_DIRS. Directories in the standard search path are not
        ///     searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        ///     This value represents the recommended maximum number of directories an application should include in its
        ///     DLL search path.
        ///     Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires
        ///     KB2533623 to be
        ///     installed.
        ///     Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,

        /// <summary>
        ///     If this value is used, the directory that contains the DLL is temporarily added to the beginning of the
        ///     list of directories that are searched for the DLL's dependencies.  Directories in the standard search path
        ///     are not searched.
        ///     The lpFileName parameter must specify a fully qualified path. This value cannot
        ///     be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        ///     For example, if Lib2.dll is a dependency of C:\Dir1\Lib1.dll, loading
        ///     Lib1.dll  with this value causes the system to search for Lib2.dll only in
        ///     C:\Dir1. To search for Lib2.dll in C:\Dir1 and all of the directories
        ///     in the DLL search path, combine this value with LOAD_LIBRARY_DEFAULT_DIRS.
        ///     Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires
        ///     KB2533623 to be
        ///     installed.
        ///     Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,

        /// <summary>
        ///     If this value is used, %windows%\system32 is searched for the DLL and its dependencies.
        ///     Directories in the standard search path are not searched. This value cannot be combined with
        ///     LOAD_WITH_ALTERED_SEARCH_PATH.
        ///     Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires
        ///     KB2533623 to be
        ///     installed.
        ///     Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,

        /// <summary>
        ///     If this value is used, directories added using the
        ///     AddDllDirectory or the
        ///     SetDllDirectory function are searched for the DLL
        ///     and its dependencies. If more than one directory has been added, the order in which the directories are
        ///     searched is unspecified. Directories in the standard search path are not searched. This value cannot be
        ///     combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        ///     Windows 7, Windows Server 2008 R2, Windows Vista, and Windows Server 2008:  This value requires
        ///     KB2533623 to be
        ///     installed.
        ///     Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,

        /// <summary>
        ///     If this value is used and lpFileName specifies an absolute path, the system uses
        ///     the alternate file search strategy discussed in the Remarks section to find associated executable modules
        ///     that the specified module causes to be loaded. If this value is used and lpFileName
        ///     specifies a relative path, the behavior is undefined.
        ///     If this value is not used, or if lpFileName does not specify a path, the system
        ///     uses the standard search strategy discussed in the Remarks section to find associated executable modules that
        ///     the specified module causes to be loaded.
        ///     This value cannot be combined with any LOAD_LIBRARY_SEARCH flag.
        /// </summary>
        LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
    }

    [Flags]
    public enum LibrarySearchFlags
    {
        /// <summary>
        ///     If this value is used, the application's installation directory is searched.
        /// </summary>
        LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,

        /// <summary>
        ///     This value is a combination of LOAD_LIBRARY_SEARCH_APPLICATION_DIR,
        ///     LOAD_LIBRARY_SEARCH_SYSTEM32, and
        ///     LOAD_LIBRARY_SEARCH_USER_DIRS.
        ///     This value represents the recommended maximum number of directories an application should include in its
        ///     DLL search path.
        /// </summary>
        LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,

        /// <summary>
        ///     If this value is used, %windows%\system32 is searched.
        /// </summary>
        LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,

        /// <summary>
        ///     If this value is used, any path explicitly added using the
        ///     AddDllDirectory or
        ///     SetDllDirectory function is searched. If more than
        ///     one directory has been added, the order in which those directories are searched is unspecified.
        /// </summary>
        LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400
    }

    [Flags]
    public enum DuplicateHandleFlags
    {
        /// <summary>
        ///     Closes the source handle. This occurs regardless of any error status returned.
        /// </summary>
        DUPLICATE_CLOSE_SOURCE = 0x00000001,

        /// <summary>
        ///     Ignores the dwDesiredAccess parameter. The duplicate handle has the same access as the source handle.
        /// </summary>
        DUPLICATE_SAME_ACCESS = 0x00000002
    }

    [Flags]
    public enum HandleInfoFlags
    {
        /// <summary>
        ///     If this flag is set, a child process created with the bInheritHandles parameter of
        ///     CreateProcess set to TRUE will inherit the object handle.
        /// </summary>
        HANDLE_FLAG_INHERIT = 0x00000001,

        /// <summary>
        ///     If this flag is set, calling the
        ///     CloseHandle function will not close the object handle.
        /// </summary>
        HANDLE_FLAG_PROTECT_FROM_CLOSE = 0x00000002
    }

    [Flags]
    public enum GetModuleHandleFlags
    {
        /// <summary>
        ///     The lpModuleName parameter is an address in the module.
        /// </summary>
        GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS = 0x00000004,

        /// <summary>
        ///     The module stays loaded until the process is terminated, no matter how many times FreeLibrary is called.
        ///     This option cannot be used with GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT.
        /// </summary>
        GET_MODULE_HANDLE_EX_FLAG_PIN = 0x00000001,

        /// <summary>
        ///     The reference count for the module is not incremented. This option is equivalent to the behavior of
        ///     GetModuleHandle. Do not pass the retrieved module handle to the FreeLibrary function; doing so can cause the DLL to
        ///     be unmapped prematurely. For more information, see Remarks. This option cannot be used with
        ///     GET_MODULE_HANDLE_EX_FLAG_PIN.
        /// </summary>
        GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT = 0x00000002
    }
}