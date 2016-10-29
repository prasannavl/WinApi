using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace WinApi.Kernel32
{
    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum DayOfWeek
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }

    public enum SystemInvalidResult
    {
        INVALID_ATOM = 0,
        INVALID_OS_COUNT = 0xffff,
        INVALID_FILE_SIZE = unchecked((int) 0xFFFFFFFF),
        INVALID_SET_FILE_POINTER = -1,
        INVALID_FILE_ATTRIBUTES = -1,
        INVALID_HANDLE_VALUE = -1
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

    public enum SecurityImpersonationLevel
    {
        /// <summary>
        ///     The server process cannot obtain identification information about the client, and it cannot impersonate the client.
        ///     It is defined with no value given, and thus, by ANSI C rules, defaults to a value of zero.
        /// </summary>
        SecurityAnonymous,

        /// <summary>
        ///     The server process can obtain information about the client, such as security identifiers and privileges, but it
        ///     cannot impersonate the client. This is useful for servers that export their own objects, for example, database
        ///     products that export tables and views. Using the retrieved client-security information, the server can make
        ///     access-validation decisions without being able to use other services that are using the client's security context.
        /// </summary>
        SecurityIdentification,

        /// <summary>
        ///     The server process can impersonate the client's security context on its local system. The server cannot impersonate
        ///     the client on remote systems.
        /// </summary>
        SecurityImpersonation,

        /// <summary>
        ///     The server process can impersonate the client's security context on remote systems
        /// </summary>
        SecurityDelegation
    }

    public enum FileCreationDisposition
    {
        /// <summary>
        ///     Creates a new file, always.
        ///     If the specified file exists and is writable, the function overwrites the file, the function succeeds, and
        ///     last-error code is set to ERROR_ALREADY_EXISTS (183).
        ///     If the specified file does not exist and is a valid path, a new file is created, the function succeeds, and
        ///     the last-error code is set to zero.
        ///     For more information, see the Remarks section of this topic.
        /// </summary>
        CREATE_ALWAYS = 2,

        /// <summary>
        ///     Creates a new file, only if it does not already exist.
        ///     If the specified file exists, the function fails and the last-error code is set to
        ///     ERROR_FILE_EXISTS (80).
        ///     If the specified file does not exist and is a valid path to a writable location, a new file is created.
        /// </summary>
        CREATE_NEW = 1,

        /// <summary>
        ///     Opens a file, always.
        ///     If the specified file exists, the function succeeds and the last-error code is set to
        ///     ERROR_ALREADY_EXISTS (183).
        ///     If the specified file does not exist and is a valid path to a writable location, the function creates a
        ///     file and the last-error code is set to zero.
        /// </summary>
        OPEN_ALWAYS = 4,

        /// <summary>
        ///     Opens a file or device, only if it exists.
        ///     If the specified file or device does not exist, the function fails and the last-error code is set to
        ///     ERROR_FILE_NOT_FOUND (2).
        ///     For more information about devices, see the Remarks section.
        /// </summary>
        OPEN_EXISTING = 3,

        /// <summary>
        ///     Opens a file and truncates it so that its size is zero bytes, only if it exists.
        ///     If the specified file does not exist, the function fails and the last-error code is set to
        ///     ERROR_FILE_NOT_FOUND (2).
        ///     The calling process must open the file with the GENERIC_WRITE bit set as part of
        ///     the dwDesiredAccess parameter.
        /// </summary>
        TRUNCATE_EXISTING = 5
    }

    public enum FileAttributeInfoLevel
    {
        GetFileExInfoStandard,
        GetFileExMaxInfoLevel
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

    [Flags]
    public enum FileShareMode
    {
        /// <summary>
        ///     Prevents other processes from opening a file or device if they request delete, read, or write access.
        /// </summary>
        FILE_SHARE_NONE = 0x00000000,

        /// <summary>
        ///     Enables subsequent open operations on a file or device to request delete access.
        ///     Otherwise, other processes cannot open the file or device if they request delete access.
        ///     If this flag is not specified, but the file or device has been opened for delete access, the function
        ///     fails.
        ///     Note  Delete access allows both delete and rename operations.
        /// </summary>
        FILE_SHARE_DELETE = 0x00000004,

        /// <summary>
        ///     Enables subsequent open operations on a file or device to request read access.
        ///     Otherwise, other processes cannot open the file or device if they request read access.
        ///     If this flag is not specified, but the file or device has been opened for read access, the function
        ///     fails.
        /// </summary>
        FILE_SHARE_READ = 0x00000001,

        /// <summary>
        ///     Enables subsequent open operations on a file or device to request write access.
        ///     Otherwise, other processes cannot open the file or device if they request write access.
        ///     If this flag is not specified, but the file or device has been opened for write access or has a file mapping
        ///     with write access, the function fails.
        /// </summary>
        FILE_SHARE_WRITE = 0x00000002
    }

    [Flags]
    public enum FileAttributes
    {
        /// <summary>
        ///     A file or directory that is an archive file or directory. Applications typically use this attribute to mark
        ///     files for backup or removal .
        /// </summary>
        FILE_ATTRIBUTE_ARCHIVE = 0x20,

        /// <summary>
        ///     A file or directory that is compressed. For a file, all of the data in the file is compressed. For a
        ///     directory, compression is the default for newly created files and subdirectories.
        /// </summary>
        FILE_ATTRIBUTE_COMPRESSED = 0x800,

        /// <summary>
        ///     This value is reserved for system use.
        /// </summary>
        FILE_ATTRIBUTE_DEVICE = 0x40,

        /// <summary>
        ///     The handle that identifies a directory.
        /// </summary>
        FILE_ATTRIBUTE_DIRECTORY = 0x10,

        /// <summary>
        ///     A file or directory that is encrypted. For a file, all data streams in the file are encrypted. For a
        ///     directory, encryption is the default for newly created files and subdirectories.
        /// </summary>
        FILE_ATTRIBUTE_ENCRYPTED = 0x4000,

        /// <summary>
        ///     The file or directory is hidden. It is not included in an ordinary directory listing.
        /// </summary>
        FILE_ATTRIBUTE_HIDDEN = 0x2,

        /// <summary>
        ///     The directory or user data stream is configured with integrity (only supported on ReFS volumes). It is not
        ///     included in an ordinary directory listing. The integrity setting persists with the file if it's renamed. If a
        ///     file is copied the destination file will have integrity set if either the source file or destination directory
        ///     have integrity set.
        ///     Windows Server 2008 R2, Windows 7, Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This
        ///     flag is not supported until Windows Server 2012.
        /// </summary>
        FILE_ATTRIBUTE_INTEGRITY_STREAM = 0x8000,

        /// <summary>
        ///     A file that does not have other attributes set. This attribute is valid only when used alone.
        /// </summary>
        FILE_ATTRIBUTE_NORMAL = 0x80,

        /// <summary>
        ///     The file or directory is not to be indexed by the content indexing service.
        /// </summary>
        FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x2000,

        /// <summary>
        ///     The user data stream not to be read by the background data integrity scanner (AKA scrubber). When set on a
        ///     directory it only provides inheritance. This
        ///     flag is only supported on Storage Spaces and ReFS volumes. It is not included in an ordinary directory
        ///     listing.
        ///     Windows Server 2008 R2, Windows 7, Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This
        ///     flag is not supported until Windows 8 and Windows Server 2012.
        /// </summary>
        FILE_ATTRIBUTE_NO_SCRUB_DATA = 0x20000,

        /// <summary>
        ///     The data of a file is not available immediately. This attribute indicates that the file data is physically
        ///     moved to offline storage. This attribute is used by Remote Storage, which is the hierarchical storage management
        ///     software. Applications should not arbitrarily change this attribute.
        /// </summary>
        FILE_ATTRIBUTE_OFFLINE = 0x1000,

        /// <summary>
        ///     A file that is read-only. Applications can read the file, but cannot write to it or delete it. This
        ///     attribute is not honored on directories. For more information, see
        ///     You cannot view or change the Read-only or the System attributes of folders in Windows Server 2003, in Windows XP,
        ///     in Windows Vista or in Windows 7.
        /// </summary>
        FILE_ATTRIBUTE_READONLY = 0x1,

        /// <summary>
        ///     A file or directory that has an associated reparse point, or a file that is a symbolic link.
        /// </summary>
        FILE_ATTRIBUTE_REPARSE_POINT = 0x400,

        /// <summary>
        ///     A file that is a sparse file.
        /// </summary>
        FILE_ATTRIBUTE_SPARSE_FILE = 0x200,

        /// <summary>
        ///     A file or directory that the operating system uses a part of, or uses exclusively.
        /// </summary>
        FILE_ATTRIBUTE_SYSTEM = 0x4,

        /// <summary>
        ///     A file that is being used for temporary storage. File systems avoid writing data back to mass storage if
        ///     sufficient cache memory is available, because typically, an application deletes a temporary file after the handle
        ///     is closed. In that scenario, the system can entirely avoid writing the data. Otherwise, the data is written after
        ///     the handle is closed.
        /// </summary>
        FILE_ATTRIBUTE_TEMPORARY = 0x100,

        /// <summary>
        ///     This value is reserved for system use.
        /// </summary>
        FILE_ATTRIBUTE_VIRTUAL = 0x10000
    }
}