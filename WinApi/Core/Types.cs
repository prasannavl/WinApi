using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace WinApi.Core
{
    public enum HResult : uint
    {
        /// <summary>
        ///     Operation successful
        /// </summary>
        S_OK = 0x00000000,

        /// <summary>
        ///     Not implemented
        /// </summary>
        E_NOTIMPL = 0x80004001,

        /// <summary>
        ///     No such interface supported
        /// </summary>
        E_NOINTERFACE = 0x80004002,

        /// <summary>
        ///     Pointer that is not valid
        /// </summary>
        E_POINTER = 0x80004003,

        /// <summary>
        ///     Operation aborted
        /// </summary>
        E_ABORT = 0x80004004,

        /// <summary>
        ///     Unspecified failure
        /// </summary>
        E_FAIL = 0x80004005,

        /// <summary>
        ///     Unexpected failure
        /// </summary>
        E_UNEXPECTED = 0x8000FFFF,

        /// <summary>
        ///     General access denied error
        /// </summary>
        E_ACCESSDENIED = 0x80070005,

        /// <summary>
        ///     Handle that is not valid
        /// </summary>
        E_HANDLE = 0x80070006,

        /// <summary>
        ///     Failed to allocate necessary memory
        /// </summary>
        E_OUTOFMEMORY = 0x8007000E,

        /// <summary>
        ///     One or more arguments are not valid
        /// </summary>
        E_INVALIDARG = 0x80070057
    }
}