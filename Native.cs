using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AsyncPluggableProtocol
{
    /// <summary>
    /// Contains the security descriptor of an object and specifies whether the handle retrieved by specifying this structure is inheritable.
    /// </summary>
    [ComConversionLoss]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SECURITY_ATTRIBUTES
    {
        /// <summary>
        /// The size, in bytes, of this structure. Set this value to the size of this structure.
        /// </summary>
        public uint nLength;
        /// <summary>
        /// A pointer to a SECURITY_DESCRIPTOR structure that controls access to the object. If the value of this member is null, the object is assigned the default security descriptor associated with the access token of the calling process. This is not the same as granting access to everyone by assigning a null discretionary access control list (DACL). The default DACL in the access token of a process allows access only to the user represented by the access token.
        /// </summary>
        [ComConversionLoss]
        public IntPtr lpSecurityDescriptor;
        /// <summary>
        /// A boolean value that specifies whether the returned handle is inherited when a new process is created. If this field is true, the new process inherits the handle.
        /// </summary>
        public int bInheritHandle;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct STGMEDIUM
    {
        public uint tymed;
        public IntPtr unionmember;
        [MarshalAs(UnmanagedType.IUnknown)]
        public object pUnkForRelease;
    }

    /// <summary>
    /// Contains additional information on the requested binding operation.  The meaning of this structure is specific to the type of asynchronous moniker.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct BINDINFO
    {
        /// <summary>
        /// Indicates the size of the structure in bytes.
        /// </summary>
        public uint cbSize;
        /// <summary>
        /// The behavior of this field is moniker-specific.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szExtraInfo;
        /// <summary>
        /// Defines the data to be used in a PUT or POST operation specified by <see cref="F:Microsoft.VisualStudio.OLE.Interop.BINDINFO.dwBindVerb"/>.
        /// </summary>
        public STGMEDIUM stgmedData;
        /// <summary>
        /// Indicates the flag from the <see cref="F:Microsoft.VisualStudio.OLE.Interop.BINDINFOF"/> enumeration that determines the use of URL encoding during the binding operation.  This member is specific to URL monikers.
        /// </summary>
        public uint grfBindInfoF;
        /// <summary>
        /// Indicates the value from the <see cref="T:Microsoft.VisualStudio.OLE.Interop.BINDVERB"/> enumeration specifying an action to be performed during the bind operation.
        /// </summary>
        public uint dwBindVerb;
        /// <summary>
        /// Represents the BSTR specifying a protocol-specific custom action to be performed during the bind operation (only if <see cref="F:Microsoft.VisualStudio.OLE.Interop.BINDINFO.dwBindVerb"/> is set to BINDVERB_CUSTOM).
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szCustomVerb;
        /// <summary>
        /// Indicates the size of the data provided in the <see cref="F:Microsoft.VisualStudio.OLE.Interop.BINDINFO.stgmedData"/> member.
        /// </summary>
        public uint cbstgmedData;
        /// <summary>
        /// Reserved. Must be set to 0.
        /// </summary>
        public uint dwOptions;
        /// <summary>
        /// Reserved. Must be set to 0.
        /// </summary>
        public uint dwOptionsFlags;
        /// <summary>
        /// Represents an unsigned long integer value that contains the code page used to perform the conversion.
        /// </summary>
        public uint dwCodePage;
        /// <summary>
        /// Represents the <see cref="F:Microsoft.VisualStudio.OLE.Interop.SECUTIRY_ATTRIBUTES"/> structure that contains the descriptor for the object being bound to and indicates whether the handle retrieved by specifying this structure is inheritable.
        /// </summary>
        public SECURITY_ATTRIBUTES securityAttributes;
        /// <summary>
        /// Indicates the interface identifier of the IUnknown interface referred to by <see cref="F:Microsoft.VisualStudio.OLE.Interop.BINDINFO.pUnk"/>.
        /// </summary>
        public Guid iid;
        /// <summary>
        /// Point to the IUnknown (COM) interface.
        /// </summary>
        [MarshalAs(UnmanagedType.IUnknown)]
        public object punk;
        /// <summary>
        /// Reserved. Must be set to 0.
        /// </summary>
        public uint dwReserved;
    }

    [ComImport]
    [Guid("00000001-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IClassFactory
    {
        [PreserveSig]
        int CreateInstance([In] IntPtr pUnkOuter, [In] ref Guid riid, [Out] out IntPtr ppvObject); 
        void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock);
    }

    [ComImport]
    [Guid("79EAC9E1-BAF9-11CE-8C82-00AA004BA90B"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInternetBindInfo
    {
        void GetBindInfo(out uint grfBINDF, [In, Out] ref BINDINFO pbindinfo);
        void GetBindString(uint ulStringType, [MarshalAs(UnmanagedType.LPWStr)] ref string ppwzStr, uint cEl, ref uint pcElFetched);
    }

    /// <summary>
    /// Indicates the type of data that is available when passed to the client in IBindStatusCallback::OnDataAvailable.
    /// </summary>
    public enum BSCF
    {
        BSCF_FIRSTDATANOTIFICATION = 1,
        BSCF_INTERMEDIATEDATANOTIFICATION = 2,
        BSCF_LASTDATANOTIFICATION = 4,
        BSCF_DATAFULLYAVAILABLE = 8,
        BSCF_AVAILABLEDATASIZEUNKNOWN = 16,
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("79EAC9E5-BAF9-11CE-8C82-00AA004BA90B")]
    public interface IInternetProtocolSink
    {
        void Switch(ref PROTOCOLDATA pProtocolData);
        void ReportProgress(uint ulStatusCode, [MarshalAs(UnmanagedType.LPWStr)] string szStatusText);
        void ReportData(BSCF grfBSCF, uint ulProgress, uint ulProgressMax);
        void ReportResult(int hrResult, uint dwError, [MarshalAs(UnmanagedType.LPWStr)] string szResult);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROTOCOLDATA
    {
        uint grfFlags;
        uint dwState;
        IntPtr pData;
        ulong cbData;
    }

    /// <summary>
    /// Represents a 64-bit signed integer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct LARGE_INTEGER
    {
        /// <summary>
        /// Represents a 64-bit signed integer.
        /// </summary>
        public long QuadPart;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ULARGE_INTEGER
    {
        public ulong QuadPart;
    }

    [Flags]
    public enum PI_FLAGS
    {
        PI_PARSE_URL = 1,
        PI_FILTER_MODE = 2,
        PI_FORCE_ASYNC = 4,
        PI_USE_WORKERTHREAD = 8,
        PI_MIMEVERIFICATION = 16,
        PI_CLSIDLOOKUP = 32,
        PI_DATAPROGRESS = 64,
        PI_SYNCHRONOUS = 128,
        PI_APARTMENTTHREADED = 256,
        PI_CLASSINSTALL = 512,
        PI_PASSONBINDCTX = 8192,
        PI_NOMIMEHANDLER = 32768,
        PI_LOADAPPDIRECT = 16384,
        PD_FORCE_SWITCH = 65536,
        PI_PREFERDEFAULTHANDLER = 131072,
    }

    /// <summary>This interface is used to control the operation of an asynchronous pluggable protocol handler. </summary>
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("79eac9e3-baf9-11ce-8c82-00aa004ba90b")]
    public interface IInternetProtocolRoot
    {
        /// <summary>Starts the operation. </summary>
        /// <remarks>URL Moniker Error Codes can be returned only by a pluggable namespace handler or MIME filter. Only a single, permanently registered asynchronous pluggable protocol handler can be assigned to a particular scheme (such as FTP), so there are no other handlers to default to.</remarks>
        /// <param name="szUrl">
        /// Address of a string value that contains the URL. For a pluggable MIME filter, this parameter contains the MIME type.</param>
        /// <param name="pOIProtSink">
        /// Address of the protocol sink provided by the client.</param>
        /// <param name="pOIBindInfo">
        /// Address of the IInternetBindInfo interface from which the protocol gets download-specific information.</param>
        /// <param name="grfPI">
        /// Unsigned long integer value that contains the flags that determine if the method only parses or if it parses and downloads the URL. This can be one of the PI_FLAGS values.</param>
        /// <param name="dwReserved">
        /// For pluggable MIME filters, contains the address of a PROTOCOLFILTERDATA structure. Otherwise, it is reserved and must be set to NULL.</param>
        void Start(
            [MarshalAs(UnmanagedType.LPWStr)]
                [In] string szUrl,
                [MarshalAs(UnmanagedType.Interface)]
                [In] IInternetProtocolSink pOIProtSink,
                [MarshalAs(UnmanagedType.Interface)]
                [In] IInternetBindInfo pOIBindInfo,
                [In] PI_FLAGS grfPI,
                [In] int dwReserved);

        /// <summary>Allows the pluggable protocol handler to continue processing data on the apartment thread. </summary>
        /// <remarks>This method is called in response to a call to the IInternetProtocolSink::Switch method. </remarks>
        /// <param name="pProtocolData">
        /// Address of the PROTOCOLDATA structure data passed to IInternetProtocolSink::Switch.</param>
        void Continue(
            [In] ref PROTOCOLDATA pProtocolData);

        /// <summary>Cancels an operation that is in progress. </summary>
        /// <param name="hrReason">
        /// HRESULT value that contains the reason for canceling the operation. This is the HRESULT that is reported by the pluggable protocol if it successfully canceled the binding. The pluggable protocol passes this HRESULT to urlmon.dll using the IInternetProtocolSink::ReportResult method. Urlmon.dll then passes this HRESULT to the host using IBindStatusCallback::OnStopBinding.</param>
        /// <param name="dwOptions">
        /// Reserved. Must be set to 0.</param>
        void Abort(
            [In] int hrReason,
            [In] int dwOptions);

        /// <summary>Releases the resources used by the pluggable protocol handler. </summary>
        /// <remarks>Note to implementers
        /// Urlmon.dll will not call this method until your asynchronous pluggable protocol handler calls the Urlmon.dll IInternetProtocolSink::ReportResult method. When your IInternetProtocolRoot::Terminate method is called, your asynchronous pluggable protocol handler should free all resources it has allocated.
        /// Note to callers
        /// This method should be called after receiving a call to your IInternetProtocolSink::ReportResult method and after the protocol handler's IInternetProtocol::LockRequest method has been called. </remarks>
        /// <param name="dwOptions">
        /// Reserved. Must be set to 0.</param>
        void Terminate(
            [In] int dwOptions);

        /// <summary>Not currently implemented.</summary>
        void Suspend();

        /// <summary>Not currently implemented. </summary>
        void Resume();
    }

    /// <summary>This is the main interface exposed by an asynchronous pluggable protocol. This interface and the IInternetProtocolSink interface communicate with each other very closely during download operations. </summary>
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("79eac9e4-baf9-11ce-8c82-00aa004ba90b")]
    public interface IInternetProtocol
    {
        /// <summary>Starts the operation. </summary>
        /// <remarks>URL Moniker Error Codes can be returned only by a pluggable namespace handler or MIME filter. Only a single, permanently registered asynchronous pluggable protocol handler can be assigned to a particular scheme (such as FTP), so there are no other handlers to default to.</remarks>
        /// <param name="szUrl">
        /// Address of a string value that contains the URL. For a pluggable MIME filter, this parameter contains the MIME type.</param>
        /// <param name="pOIProtSink">
        /// Address of the protocol sink provided by the client.</param>
        /// <param name="pOIBindInfo">
        /// Address of the IInternetBindInfo interface from which the protocol gets download-specific information.</param>
        /// <param name="grfPI">
        /// Unsigned long integer value that contains the flags that determine if the method only parses or if it parses and downloads the URL. This can be one of the PI_FLAGS values.</param>
        /// <param name="dwReserved">
        /// For pluggable MIME filters, contains the address of a PROTOCOLFILTERDATA structure. Otherwise, it is reserved and must be set to NULL.</param>
        void Start(
            [MarshalAs(UnmanagedType.LPWStr)]
                [In] string szUrl,
                [MarshalAs(UnmanagedType.Interface)]
                [In] IInternetProtocolSink pOIProtSink,
                [MarshalAs(UnmanagedType.Interface)]
                [In] IInternetBindInfo pOIBindInfo,
                [In] PI_FLAGS grfPI,
                [In] int dwReserved);

        /// <summary>Allows the pluggable protocol handler to continue processing data on the apartment thread. </summary>
        /// <remarks>This method is called in response to a call to the IInternetProtocolSink::Switch method. </remarks>
        /// <param name="pProtocolData">
        /// Address of the PROTOCOLDATA structure data passed to IInternetProtocolSink::Switch.</param>
        void Continue(
            [In] ref PROTOCOLDATA pProtocolData);

        /// <summary>Cancels an operation that is in progress. </summary>
        /// <param name="hrReason">
        /// HRESULT value that contains the reason for canceling the operation. This is the HRESULT that is reported by the pluggable protocol if it successfully canceled the binding. The pluggable protocol passes this HRESULT to urlmon.dll using the IInternetProtocolSink::ReportResult method. Urlmon.dll then passes this HRESULT to the host using IBindStatusCallback::OnStopBinding.</param>
        /// <param name="dwOptions">
        /// Reserved. Must be set to 0.</param>
        void Abort(
            [In] int hrReason,
            [In] int dwOptions);

        /// <summary>Releases the resources used by the pluggable protocol handler. </summary>
        /// <remarks>Note to implementers
        /// Urlmon.dll will not call this method until your asynchronous pluggable protocol handler calls the Urlmon.dll IInternetProtocolSink::ReportResult method. When your IInternetProtocolRoot::Terminate method is called, your asynchronous pluggable protocol handler should free all resources it has allocated.
        /// Note to callers
        /// This method should be called after receiving a call to your IInternetProtocolSink::ReportResult method and after the protocol handler's IInternetProtocol::LockRequest method has been called. </remarks>
        /// <param name="dwOptions">
        /// Reserved. Must be set to 0.</param>
        void Terminate(
            [In] int dwOptions);

        /// <summary>Not currently implemented.</summary>
        void Suspend();

        /// <summary>Not currently implemented.</summary>
        void Resume();

        /// <summary>Reads data retrieved by the pluggable protocol handler. </summary>
        /// <remarks>Developers who are implementing an asynchronous pluggable protocol must be prepared to have their implementation of IInternetProtocol::Read continue to be called a few extra times after it has returned S_FALSE. </remarks>
        /// <param name="pv">
        /// Address of the buffer where the information will be stored.</param>
        /// <param name="cb">
        /// Value that indicates the size of the buffer.</param>
        /// <param name="pcbRead">
        /// Address of a value that indicates the amount of data stored in the buffer.</param>
        [PreserveSig]
        int Read(
            [In, Out] IntPtr pv,
            [In] int cb,
            [Out] out int pcbRead);

        /// <summary>Moves the current seek offset.</summary>
        /// <param name="dlibMove">
        /// Large integer value that indicates how far to move the offset.</param>
        /// <param name="dwOrigin">
        /// DWORD value that indicates where the move should begin.
        /// FILE_BEGIN : Starting point is zero or the beginning of the file. If FILE_BEGIN is specified, dlibMove is interpreted as an unsigned location for the new file pointer.
        /// FILE_CURRENT : Current value of the file pointer is the starting point.
        /// FILE_END : Current end-of-file position is the starting point. This method fails if the content length is unknown.</param>
        /// <param name="plibNewPosition">
        /// Address of an unsigned long integer value that indicates the new offset.</param>
        void Seek(
            [In] long dlibMove,
            [In] int dwOrigin,
            [Out] out long plibNewPosition);

        /// <summary>Locks the requested resource so that the IInternetProtocolRoot::Terminate method can be called and the remaining data can be read. </summary>
        /// <remarks>For asynchronous pluggable protocols that do not need to lock a request, the method should return S_OK.</remarks>
        /// <param name="dwOptions">
        /// Reserved. Must be set to 0.</param>
        void LockRequest(
            [In] int dwOptions);

        /// <summary>Frees any resources associated with a lock. </summary>
        /// <remarks>This method is called only if IInternetProtocol::LockRequest was called. </remarks>
        void UnlockRequest();
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("79eac9e7-baf9-11ce-8c82-00aa004ba90b")]
    public interface IInternetSession
    {
        void RegisterNameSpace(
            [MarshalAs(UnmanagedType.Interface)]
                IClassFactory pCF,
            //IntPtr pCF,
            [In] ref Guid rclsid,
            [MarshalAs(UnmanagedType.LPWStr)] string pwzProtocol,
            int cPatterns,
            [In, MarshalAs(UnmanagedType.LPWStr)] ref string ppwzPatterns,
            int dwReserved);

        void UnregisterNameSpace(
            IClassFactory pCF,
            //IntPtr pCF,
            [MarshalAs(UnmanagedType.LPWStr)] string pszProtocol);

        void RegisterMimeFilter(
            IntPtr pCF,
            [In] ref Guid rclsid,
            [MarshalAs(UnmanagedType.LPWStr)] string pwzType);

        void UnregisterMimeFilter(
            IntPtr pCF,
            [MarshalAs(UnmanagedType.LPWStr)] string pwzType);

        void CreateBinding(
            IntPtr pbc,
            [MarshalAs(UnmanagedType.LPWStr)] string szUrl,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppunk,
            [Out] out IInternetProtocol ppOInetProt,
            int dwOption);

        void SetSessionOption(
            int dwOption,
            [MarshalAs(UnmanagedType.I4)] IntPtr pBuffer,
            int dwBufferLength,
            int dwReserved);

        void GetSessionOption(
            int dwOption,
            [MarshalAs(UnmanagedType.I4)] IntPtr pBuffer,
            [In, Out] ref int pdwBufferLength,
            int dwReserved);
    }

    [ComImport]
    [Guid("79eac9ec-baf9-11ce-8c82-00aa004ba90b")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInternetProtocolInfo
    {
        [PreserveSig]
        int ParseUrl(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
            [In] PARSEACTION ParseAction,
            [In] [MarshalAs(UnmanagedType.U4)] uint dwParseFlags,
            [In] IntPtr pwzResult,
            [In] [MarshalAs(UnmanagedType.U4)] uint cchResult,
            [Out] [MarshalAs(UnmanagedType.U4)] out uint pcchResult,
            [In] [MarshalAs(UnmanagedType.U4)] uint dwReserved);

        [PreserveSig]
        int CombineUrl(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pwzBaseUrl,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pwzRelativeUrl,
            [In] [MarshalAs(UnmanagedType.U4)] uint dwCombineFlags,
            [In] IntPtr pwzResult,
            [In] [MarshalAs(UnmanagedType.U4)] uint cchResult,
            [Out] [MarshalAs(UnmanagedType.U4)] out uint pcchResult,
            [In] [MarshalAs(UnmanagedType.U4)] uint dwReserved);

        [PreserveSig]
        int CompareUrl(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl1,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl2,
            [In] [MarshalAs(UnmanagedType.U4)] uint dwCompareFlags);

        [PreserveSig]
        int QueryInfo(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
            [In] QUERYOPTION OueryOption,
            [In] [MarshalAs(UnmanagedType.U4)] uint dwQueryFlags,
            [In] IntPtr pBuffer,
            [In] [MarshalAs(UnmanagedType.U4)] uint cbBuffer,
            [In, Out] [MarshalAs(UnmanagedType.U4)] ref uint pcbBuf,
            [In] [MarshalAs(UnmanagedType.U4)] uint dwReserved);
    }
    
    public enum PARSEACTION
    {
        PARSE_CANONICALIZE = 1,
        PARSE_FRIENDLY,
        PARSE_SECURITY_URL,
        PARSE_ROOTDOCUMENT,
        PARSE_DOCUMENT,
        PARSE_ANCHOR,
        PARSE_ENCODE,
        PARSE_DECODE,
        PARSE_PATH_FROM_URL,
        PARSE_URL_FROM_PATH,
        PARSE_MIME,
        PARSE_SERVER,
        PARSE_SCHEMA,
        PARSE_SITE,
        PARSE_DOMAIN,
        PARSE_LOCATION,
        PARSE_SECURITY_DOMAIN,
        PARSE_ESCAPE,
        PARSE_UNESCAPE
    }

    public enum QUERYOPTION
    {
        QUERY_EXPIRATION_DATE = 1,
        QUERY_TIME_OF_LAST_CHANGE,
        QUERY_CONTENT_ENCODING,
        QUERY_CONTENT_TYPE,
        QUERY_REFRESH,
        QUERY_RECOMBINE,
        QUERY_CAN_NAVIGATE,
        QUERY_USES_NETWORK,
        QUERY_IS_CACHED,
        QUERY_IS_INSTALLEDENTRY,
        QUERY_IS_CACHED_OR_MAPPED,
        QUERY_USES_CACHE,
        QUERY_IS_SECURE,
        QUERY_IS_SAFE
    }

    public enum URLZONE
    {
        URLZONE_INVALID = -1,
        URLZONE_PREDEFINED_MIN = 0,
        URLZONE_LOCAL_MACHINE = 0,
        URLZONE_INTRANET,
        URLZONE_TRUSTED,
        URLZONE_INTERNET,
        URLZONE_UNTRUSTED,
        URLZONE_PREDEFINED_MAX = 999,
        URLZONE_USER_MIN = 1000,
        URLZONE_USER_MAX = 10000
    }

    public enum GetWindow_Cmd : uint
    {
        GW_HWNDFIRST = 0,
        GW_HWNDLAST = 1,
        GW_HWNDNEXT = 2,
        GW_HWNDPREV = 3,
        GW_OWNER = 4,
        GW_CHILD = 5,
        GW_ENABLEDPOPUP = 6
    }

    public enum HitTestValues
    {
        HTERROR = -2,
        HTTRANSPARENT = -1,
        HTNOWHERE = 0,
        HTCLIENT = 1,
        HTCAPTION = 2,
        HTSYSMENU = 3,
        HTGROWBOX = 4,
        HTMENU = 5,
        HTHSCROLL = 6,
        HTVSCROLL = 7,
        HTMINBUTTON = 8,
        HTMAXBUTTON = 9,
        HTLEFT = 10,
        HTRIGHT = 11,
        HTTOP = 12,
        HTTOPLEFT = 13,
        HTTOPRIGHT = 14,
        HTBOTTOM = 15,
        HTBOTTOMLEFT = 16,
        HTBOTTOMRIGHT = 17,
        HTBORDER = 18,
        HTOBJECT = 19,
        HTCLOSE = 20,
        HTHELP = 21
    }

    public enum WindowMessages
    {
        WM_NULL = 0x0000,
        WM_CREATE = 0x0001,
        WM_DESTROY = 0x0002,
        WM_MOVE = 0x0003,
        WM_SIZE = 0x0005,
        WM_ACTIVATE = 0x0006,
        WM_SETFOCUS = 0x0007,
        WM_KILLFOCUS = 0x0008,
        WM_ENABLE = 0x000A,
        WM_SETREDRAW = 0x000B,
        WM_SETTEXT = 0x000C,
        WM_GETTEXT = 0x000D,
        WM_GETTEXTLENGTH = 0x000E,
        WM_PAINT = 0x000F,
        WM_CLOSE = 0x0010,

        WM_QUIT = 0x0012,
        WM_ERASEBKGND = 0x0014,
        WM_SYSCOLORCHANGE = 0x0015,
        WM_SHOWWINDOW = 0x0018,

        WM_ACTIVATEAPP = 0x001C,

        WM_SETCURSOR = 0x0020,
        WM_MOUSEACTIVATE = 0x0021,
        WM_GETMINMAXINFO = 0x24,
        WM_WINDOWPOSCHANGING = 0x0046,
        WM_WINDOWPOSCHANGED = 0x0047,

        WM_CONTEXTMENU = 0x007B,
        WM_STYLECHANGING = 0x007C,
        WM_STYLECHANGED = 0x007D,
        WM_DISPLAYCHANGE = 0x007E,
        WM_GETICON = 0x007F,
        WM_SETICON = 0x0080,

        // non client area
        WM_NCCREATE = 0x0081,
        WM_NCDESTROY = 0x0082,
        WM_NCCALCSIZE = 0x0083,
        WM_NCHITTEST = 0x84,
        WM_NCPAINT = 0x0085,
        WM_NCACTIVATE = 0x0086,

        WM_GETDLGCODE = 0x0087,

        WM_SYNCPAINT = 0x0088,

        // non client mouse
        WM_NCMOUSEMOVE = 0x00A0,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_NCLBUTTONUP = 0x00A2,
        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_NCRBUTTONDOWN = 0x00A4,
        WM_NCRBUTTONUP = 0x00A5,
        WM_NCRBUTTONDBLCLK = 0x00A6,
        WM_NCMBUTTONDOWN = 0x00A7,
        WM_NCMBUTTONUP = 0x00A8,
        WM_NCMBUTTONDBLCLK = 0x00A9,

        // keyboard
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_CHAR = 0x0102,

        WM_SYSCOMMAND = 0x0112,

        // menu
        WM_INITMENU = 0x0116,
        WM_INITMENUPOPUP = 0x0117,
        WM_MENUSELECT = 0x011F,
        WM_MENUCHAR = 0x0120,
        WM_ENTERIDLE = 0x0121,
        WM_MENURBUTTONUP = 0x0122,
        WM_MENUDRAG = 0x0123,
        WM_MENUGETOBJECT = 0x0124,
        WM_UNINITMENUPOPUP = 0x0125,
        WM_MENUCOMMAND = 0x0126,

        WM_CHANGEUISTATE = 0x0127,
        WM_UPDATEUISTATE = 0x0128,
        WM_QUERYUISTATE = 0x0129,

        // mouse
        WM_MOUSEFIRST = 0x0200,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_MOUSEWHEEL = 0x020A,
        WM_MOUSELAST = 0x020D,

        WM_PARENTNOTIFY = 0x0210,
        WM_ENTERMENULOOP = 0x0211,
        WM_EXITMENULOOP = 0x0212,

        WM_NEXTMENU = 0x0213,
        WM_SIZING = 0x0214,
        WM_CAPTURECHANGED = 0x0215,
        WM_MOVING = 0x0216,

        WM_ENTERSIZEMOVE = 0x0231,
        WM_EXITSIZEMOVE = 0x0232,

        WM_MOUSELEAVE = 0x02A3,
        WM_MOUSEHOVER = 0x02A1,
        WM_NCMOUSEHOVER = 0x02A0,
        WM_NCMOUSELEAVE = 0x02A2,

        WM_MDIACTIVATE = 0x0222,
        WM_HSCROLL = 0x0114,
        WM_VSCROLL = 0x0115,

        WM_PRINT = 0x0317,
        WM_PRINTCLIENT = 0x0318,
    }

    public enum SystemCommands
    {
        SC_SIZE = 0xF000,
        SC_MOVE = 0xF010,
        SC_MINIMIZE = 0xF020,
        SC_MAXIMIZE = 0xF030,
        SC_MAXIMIZE2 = 0xF032,	// fired from double-click on caption
        SC_NEXTWINDOW = 0xF040,
        SC_PREVWINDOW = 0xF050,
        SC_CLOSE = 0xF060,
        SC_VSCROLL = 0xF070,
        SC_HSCROLL = 0xF080,
        SC_MOUSEMENU = 0xF090,
        SC_KEYMENU = 0xF100,
        SC_ARRANGE = 0xF110,
        SC_RESTORE = 0xF120,
        SC_RESTORE2 = 0xF122,	// fired from double-click on caption
        SC_TASKLIST = 0xF130,
        SC_SCREENSAVE = 0xF140,
        SC_HOTKEY = 0xF150,

        SC_DEFAULT = 0xF160,
        SC_MONITORPOWER = 0xF170,
        SC_CONTEXTHELP = 0xF180,
        SC_SEPARATOR = 0xF00F
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public static RECT FromXYWH(int x, int y, int width, int height)
        {
            return new RECT(x,
                            y,
                            x + width,
                            y + height);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWPOS
    {
        internal IntPtr hwnd;
        internal IntPtr hWndInsertAfter;
        internal int x;
        internal int y;
        internal int cx;
        internal int cy;
        internal uint flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINTS
    {
        public short X;
        public short Y;
    }

    [Flags]
    public enum WindowStyle
    {
        WS_OVERLAPPED = 0x00000000,
        WS_POPUP = -2147483648, //0x80000000,
        WS_CHILD = 0x40000000,
        WS_MINIMIZE = 0x20000000,
        WS_VISIBLE = 0x10000000,
        WS_DISABLED = 0x08000000,
        WS_CLIPSIBLINGS = 0x04000000,
        WS_CLIPCHILDREN = 0x02000000,
        WS_MAXIMIZE = 0x01000000,
        WS_CAPTION = 0x00C00000,
        WS_BORDER = 0x00800000,
        WS_DLGFRAME = 0x00400000,
        WS_VSCROLL = 0x00200000,
        WS_HSCROLL = 0x00100000,
        WS_SYSMENU = 0x00080000,
        WS_THICKFRAME = 0x00040000,
        WS_GROUP = 0x00020000,
        WS_TABSTOP = 0x00010000,
        WS_MINIMIZEBOX = 0x00020000,
        WS_MAXIMIZEBOX = 0x00010000,
        WS_TILED = WS_OVERLAPPED,
        WS_ICONIC = WS_MINIMIZE,
        WS_SIZEBOX = WS_THICKFRAME,
        WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
        WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU |
                                WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),
        WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU),
        WS_CHILDWINDOW = (WS_CHILD)
    }

    public static class NativeMethods
    {
        [DllImport("urlmon.dll")]
        internal static extern int CoInternetGetSession(
            int dwSessionMode,
            out IInternetSession ppIInternetSession,
            int dwReserved);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        
        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetCapture();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);
        
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wparam, int lparam);

        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hwnd, int msg, int wparam, int lparam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y,
           IntPtr hwnd, IntPtr lptpm);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wparam, POINTS pos);

        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hwnd, int msg, int wparam, POINTS pos);

        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
    
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("user32.dll")]
        public static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        [DllImport("gdi32.dll")]
        public static extern int GetRgnBox(IntPtr hrgn, out RECT lprc);

        [DllImport("user32.dll")]
        public static extern Int32 GetWindowLong(IntPtr hWnd, Int32 Offset);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int smIndex);
    
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
    }

    public static class NativeConstants
    {
        public const int SM_CXSIZEFRAME = 32;
        public const int SM_CYSIZEFRAME = 33;
        public const int SM_CXPADDEDBORDER = 92;
        
        public const int GWL_ID = (-12);
        public const int GWL_STYLE = (-16);
        public const int GWL_EXSTYLE = (-20);

        public static readonly Guid IID_IDispatch = new Guid("{00020400-0000-0000-C000-000000000046}");
        public static readonly Guid IID_IDispatchEx = new Guid("{a6ef9860-c720-11d0-9337-00a0c90dcaa9}");
        public static readonly Guid IID_IPersistStorage = new Guid("{0000010A-0000-0000-C000-000000000046}");
        public static readonly Guid IID_IPersistStream = new Guid("{00000109-0000-0000-C000-000000000046}");
        public static readonly Guid IID_IPersistPropertyBag = new Guid("{37D84F60-42CB-11CE-8135-00AA004BB851}");

        public const int INTERFACESAFE_FOR_UNTRUSTED_CALLER = 0x00000001;
        public const int INTERFACESAFE_FOR_UNTRUSTED_DATA = 0x00000002;

        public const int INET_E_DEFAULT_ACTION = unchecked((int)0x800C0011);
        public const int INET_E_INVALID_URL = unchecked((int)0x800C0002);
        public const int INET_E_DATA_NOT_AVAILABLE = unchecked((int)0x800C0007);

        public const int STG_E_FILENOTFOUND = unchecked((int)0x80030002);

        public const int S_OK = 0;
        public const int S_FALSE = 1;

        public const int E_PENDING = unchecked((int)0x8000000A);
        public const int E_FAIL = unchecked((int)0x80004005);
        public const int E_NOTIMPL = unchecked((int)0x80004001);
        public const int E_NOINTERFACE = unchecked((int)0x80004002);
        public const int CLASS_E_NOAGGREGATION = unchecked((int)0x80040110);

        public const int MAX_PATH = 260;

        public const int NORMAL_CACHE_ENTRY = 1;

        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCRBUTTONUP = 0x00A5;

        public const uint TPM_LEFTBUTTON = 0x0000;
        public const uint TPM_RIGHTBUTTON = 0x0002;
        public const uint TPM_RETURNCMD = 0x0100;

        public static readonly IntPtr TRUE = new IntPtr(1);
        public static readonly IntPtr FALSE = new IntPtr(0);
    }
}
