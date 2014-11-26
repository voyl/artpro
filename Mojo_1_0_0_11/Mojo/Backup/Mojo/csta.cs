using System;
using System.Runtime.InteropServices;

public static class Csta
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Nulltype
    {
        // null
    }
    [StructLayout(LayoutKind.Sequential, Pack = 4)]    
    public struct DeviceID_t
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public char [] device;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ServerID_t
    {
        public char [] server;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AppName_t
    {
        public char [] appName;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Version_t
    {
        public char [] version;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct LoginID_t
    {
        public char [] login;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AgentID_t
    {
        public char [] agent;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AgentPassword_t
    {
        public char [] password;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AccountInfo_t
    {
        public char [] account;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AuthCode_t
    {
        public char [] code;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct WinNTPipe_t
    {
        public char [] pipe;
    }

public const int ACS_OPEN_STREAM = 1;
public const int ACS_OPEN_STREAM_CONF = 2;
public const int ACS_CLOSE_STREAM = 3;
public const int ACS_CLOSE_STREAM_CONF = 4;
public const int ACS_ABORT_STREAM = 5;
public const int ACS_UNIVERSAL_FAILURE_CONF = 6;
public const int ACS_UNIVERSAL_FAILURE = 7;
public const int ACS_KEY_REQUEST = 8;
public const int ACS_KEY_REPLY = 9;
public const int ACS_NAME_SRV_REQUEST = 10;
public const int ACS_NAME_SRV_REPLY = 11;
public const int ACS_AUTH_REPLY = 12;
public const int ACS_AUTH_REPLY_TWO = 13;
public enum StreamType_t
{
   ST_CSTA = 1,
   ST_OAM = 2,
   ST_DIRECTORY = 3,
   ST_NMSRV = 4,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CryptPasswd_t
{
   public short length;
   [ MarshalAs( UnmanagedType.ByValArray, SizeConst=47)]
   public char [] value;
};

public enum Level_t
{
   ACS_LEVEL1 = 1,
   ACS_LEVEL2 = 2,
   ACS_LEVEL3 = 3,
   ACS_LEVEL4 = 4,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSOpenStream_t
{
   public StreamType_t streamType;
   public ServerID_t serverID;
   public LoginID_t loginID;
   public CryptPasswd_t cryptPass;
   public AppName_t applicationName;
   public Level_t level;
   public Version_t apiVer;
   public Version_t libVer;
   public Version_t tsrvVer;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSOpenStreamConfEvent_t
{
   public Version_t apiVer;
   public Version_t libVer;
   public Version_t tsrvVer;
   public Version_t drvrVer;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSCloseStream_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSCloseStreamConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSAbortStream_t
{
   public Nulltype nil;
};

public enum ACSUniversalFailure_t
{
   TSERVER_STREAM_FAILED = 0,
   TSERVER_NO_THREAD = 1,
   TSERVER_BAD_DRIVER_ID = 2,
   TSERVER_DEAD_DRIVER = 3,
   TSERVER_MESSAGE_HIGH_WATER_MARK = 4,
   TSERVER_FREE_BUFFER_FAILED = 5,
   TSERVER_SEND_TO_DRIVER = 6,
   TSERVER_RECEIVE_FROM_DRIVER = 7,
   TSERVER_REGISTRATION_FAILED = 8,
   TSERVER_SPX_FAILED = 9,
   TSERVER_TRACE = 10,
   TSERVER_NO_MEMORY = 11,
   TSERVER_ENCODE_FAILED = 12,
   TSERVER_DECODE_FAILED = 13,
   TSERVER_BAD_CONNECTION = 14,
   TSERVER_BAD_PDU = 15,
   TSERVER_NO_VERSION = 16,
   TSERVER_ECB_MAX_EXCEEDED = 17,
   TSERVER_NO_ECBS = 18,
   TSERVER_NO_SDB = 19,
   TSERVER_NO_SDB_CHECK_NEEDED = 20,
   TSERVER_SDB_CHECK_NEEDED = 21,
   TSERVER_BAD_SDB_LEVEL = 22,
   TSERVER_BAD_SERVERID = 23,
   TSERVER_BAD_STREAM_TYPE = 24,
   TSERVER_BAD_PASSWORD_OR_LOGIN = 25,
   TSERVER_NO_USER_RECORD = 26,
   TSERVER_NO_DEVICE_RECORD = 27,
   TSERVER_DEVICE_NOT_ON_LIST = 28,
   TSERVER_USERS_RESTRICTED_HOME = 30,
   TSERVER_NO_AWAYPERMISSION = 31,
   TSERVER_NO_HOMEPERMISSION = 32,
   TSERVER_NO_AWAY_WORKTOP = 33,
   TSERVER_BAD_DEVICE_RECORD = 34,
   TSERVER_DEVICE_NOT_SUPPORTED = 35,
   TSERVER_INSUFFICIENT_PERMISSION = 36,
   TSERVER_NO_RESOURCE_TAG = 37,
   TSERVER_INVALID_MESSAGE = 38,
   TSERVER_EXCEPTION_LIST = 39,
   TSERVER_NOT_ON_OAM_LIST = 40,
   TSERVER_PBX_ID_NOT_IN_SDB = 41,
   TSERVER_USER_LICENSES_EXCEEDED = 42,
   TSERVER_OAM_DROP_CONNECTION = 43,
   TSERVER_NO_VERSION_RECORD = 44,
   TSERVER_OLD_VERSION_RECORD = 45,
   TSERVER_BAD_PACKET = 46,
   TSERVER_OPEN_FAILED = 47,
   TSERVER_OAM_IN_USE = 48,
   TSERVER_DEVICE_NOT_ON_HOME_LIST = 49,
   TSERVER_DEVICE_NOT_ON_CALL_CONTROL_LIST = 50,
   TSERVER_DEVICE_NOT_ON_AWAY_LIST = 51,
   TSERVER_DEVICE_NOT_ON_ROUTE_LIST = 52,
   TSERVER_DEVICE_NOT_ON_MONITOR_DEVICE_LIST = 53,
   TSERVER_DEVICE_NOT_ON_MONITOR_CALL_DEVICE_LIST = 54,
   TSERVER_NO_CALL_CALL_MONITOR_PERMISSION = 55,
   TSERVER_HOME_DEVICE_LIST_EMPTY = 56,
   TSERVER_CALL_CONTROL_LIST_EMPTY = 57,
   TSERVER_AWAY_LIST_EMPTY = 58,
   TSERVER_ROUTE_LIST_EMPTY = 59,
   TSERVER_MONITOR_DEVICE_LIST_EMPTY = 60,
   TSERVER_MONITOR_CALL_DEVICE_LIST_EMPTY = 61,
   TSERVER_USER_AT_HOME_WORKTOP = 62,
   TSERVER_DEVICE_LIST_EMPTY = 63,
   TSERVER_BAD_GET_DEVICE_LEVEL = 64,
   TSERVER_DRIVER_UNREGISTERED = 65,
   TSERVER_NO_ACS_STREAM = 66,
   TSERVER_DROP_OAM = 67,
   TSERVER_ECB_TIMEOUT = 68,
   TSERVER_BAD_ECB = 69,
   TSERVER_ADVERTISE_FAILED = 70,
   TSERVER_NETWARE_FAILURE = 71,
   TSERVER_TDI_QUEUE_FAULT = 72,
   TSERVER_DRIVER_CONGESTION = 73,
   TSERVER_NO_TDI_BUFFERS = 74,
   TSERVER_OLD_INVOKEID = 75,
   TSERVER_HWMARK_TO_LARGE = 76,
   TSERVER_SET_ECB_TO_LOW = 77,
   TSERVER_NO_RECORD_IN_FILE = 78,
   TSERVER_ECB_OVERDUE = 79,
   TSERVER_BAD_PW_ENCRYPTION = 80,
   TSERVER_BAD_TSERV_PROTOCOL = 81,
   TSERVER_BAD_DRIVER_PROTOCOL = 82,
   TSERVER_BAD_TRANSPORT_TYPE = 83,
   TSERVER_PDU_VERSION_MISMATCH = 84,
   TSERVER_VERSION_MISMATCH = 85,
   TSERVER_LICENSE_MISMATCH = 86,
   TSERVER_BAD_ATTRIBUTE_LIST = 87,
   TSERVER_BAD_TLIST_TYPE = 88,
   TSERVER_BAD_PROTOCOL_FORMAT = 89,
   TSERVER_OLD_TSLIB = 90,
   TSERVER_BAD_LICENSE_FILE = 91,
   TSERVER_NO_PATCHES = 92,
   TSERVER_SYSTEM_ERROR = 93,
   TSERVER_OAM_LIST_EMPTY = 94,
   TSERVER_TCP_FAILED = 95,
   TSERVER_SPX_DISABLED = 96,
   TSERVER_TCP_DISABLED = 97,
   TSERVER_REQUIRED_MODULES_NOT_LOADED = 98,
   TSERVER_TRANSPORT_IN_USE_BY_OAM = 99,
   TSERVER_NO_NDS_OAM_PERMISSION = 100,
   TSERVER_OPEN_SDB_LOG_FAILED = 101,
   TSERVER_INVALID_LOG_SIZE = 102,
   TSERVER_WRITE_SDB_LOG_FAILED = 103,
   TSERVER_NT_FAILURE = 104,
   TSERVER_LOAD_LIB_FAILED = 105,
   TSERVER_INVALID_DRIVER = 106,
   TSERVER_REGISTRY_ERROR = 107,
   TSERVER_DUPLICATE_ENTRY = 108,
   TSERVER_DRIVER_LOADED = 109,
   TSERVER_DRIVER_NOT_LOADED = 110,
   TSERVER_NO_LOGON_PERMISSION = 111,
   TSERVER_ACCOUNT_DISABLED = 112,
   TSERVER_NO_NETLOGON = 113,
   TSERVER_ACCT_RESTRICTED = 114,
   TSERVER_INVALID_LOGON_TIME = 115,
   TSERVER_INVALID_WORKSTATION = 116,
   TSERVER_ACCT_LOCKED_OUT = 117,
   TSERVER_PASSWORD_EXPIRED = 118,
   DRIVER_DUPLICATE_ACSHANDLE = 1000,
   DRIVER_INVALID_ACS_REQUEST = 1001,
   DRIVER_ACS_HANDLE_REJECTION = 1002,
   DRIVER_INVALID_CLASS_REJECTION = 1003,
   DRIVER_GENERIC_REJECTION = 1004,
   DRIVER_RESOURCE_LIMITATION = 1005,
   DRIVER_ACSHANDLE_TERMINATION = 1006,
   DRIVER_LINK_UNAVAILABLE = 1007,
   DRIVER_OAM_IN_USE = 1008,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSUniversalFailureConfEvent_t
{
   public ACSUniversalFailure_t error;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSUniversalFailureEvent_t
{
   public ACSUniversalFailure_t error;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ChallengeKey_t
{
   public short length;
   [ MarshalAs( UnmanagedType.ByValArray, SizeConst=8)]
   public char [] value;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSKeyRequest_t
{
   public LoginID_t loginID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSKeyReply_t
{
   public int objectID;
   public ChallengeKey_t key;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSNameSrvRequest_t
{
   public StreamType_t streamType;
};

// *****************************
// * Nested elements not supported
// *****************************
   // typedef struct ACSNameAddr_t {
   //     char            FAR *serverName;
   //     struct {
   //         short           length;
   //         unsigned char   FAR *value;
   //     } serverAddr;
   // } ACSNameAddr_t;
// *****************************

// *****************************
// * Nested elements not supported
// *****************************
   // typedef struct ACSNameSrvReply_t {
   //     Boolean         more;
   //     struct {
   //         short           count;
   //         ACSNameAddr_t   FAR *nameAddr;
   //     } list;
   // } ACSNameSrvReply_t;
// *****************************

public enum ACSAuthType_t
{
   REQUIRES_EXTERNAL_AUTH = -1,
   AUTH_LOGIN_ID_ONLY = 0,
   AUTH_LOGIN_ID_IS_DEFAULT = 1,
   NEED_LOGIN_ID_AND_PASSWD = 2,
   ANY_LOGIN_ID = 3,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSAuthInfo_t
{
   public ACSAuthType_t authType;
   public LoginID_t authLoginID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSAuthReply_t
{
   public int objectID;
   public ChallengeKey_t key;
   public ACSAuthInfo_t authInfo;
};

public enum ACSEncodeType_t
{
   CAN_USE_BINDERY_ENCRYPTION = 1,
   NDS_AUTH_CONNID = 2,
   WIN_NT_LOCAL = 3,
   WIN_NT_NAMED_PIPE = 4,
   WIN_NT_WRITE_DATA = 5,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSAuthReplyTwo_t
{
   public int objectID;
   public ChallengeKey_t key;
   public ACSAuthInfo_t authInfo;
   public ACSEncodeType_t encodeType;
   public WinNTPipe_t pipe;
};

// Generated by PInvoke Wizard (v 1.3) from The Paul Yao Company http://www.paulyao.com 

public const int CSTA_ALTERNATE_CALL = 1;
public const int CSTA_ALTERNATE_CALL_CONF = 2;
public const int CSTA_ANSWER_CALL = 3;
public const int CSTA_ANSWER_CALL_CONF = 4;
public const int CSTA_CALL_COMPLETION = 5;
public const int CSTA_CALL_COMPLETION_CONF = 6;
public const int CSTA_CLEAR_CALL = 7;
public const int CSTA_CLEAR_CALL_CONF = 8;
public const int CSTA_CLEAR_CONNECTION = 9;
public const int CSTA_CLEAR_CONNECTION_CONF = 10;
public const int CSTA_CONFERENCE_CALL = 11;
public const int CSTA_CONFERENCE_CALL_CONF = 12;
public const int CSTA_CONSULTATION_CALL = 13;
public const int CSTA_CONSULTATION_CALL_CONF = 14;
public const int CSTA_DEFLECT_CALL = 15;
public const int CSTA_DEFLECT_CALL_CONF = 16;
public const int CSTA_PICKUP_CALL = 17;
public const int CSTA_PICKUP_CALL_CONF = 18;
public const int CSTA_GROUP_PICKUP_CALL = 19;
public const int CSTA_GROUP_PICKUP_CALL_CONF = 20;
public const int CSTA_HOLD_CALL = 21;
public const int CSTA_HOLD_CALL_CONF = 22;
public const int CSTA_MAKE_CALL = 23;
public const int CSTA_MAKE_CALL_CONF = 24;
public const int CSTA_MAKE_PREDICTIVE_CALL = 25;
public const int CSTA_MAKE_PREDICTIVE_CALL_CONF = 26;
public const int CSTA_QUERY_MWI = 27;
public const int CSTA_QUERY_MWI_CONF = 28;
public const int CSTA_QUERY_DND = 29;
public const int CSTA_QUERY_DND_CONF = 30;
public const int CSTA_QUERY_FWD = 31;
public const int CSTA_QUERY_FWD_CONF = 32;
public const int CSTA_QUERY_AGENT_STATE = 33;
public const int CSTA_QUERY_AGENT_STATE_CONF = 34;
public const int CSTA_QUERY_LAST_NUMBER = 35;
public const int CSTA_QUERY_LAST_NUMBER_CONF = 36;
public const int CSTA_QUERY_DEVICE_INFO = 37;
public const int CSTA_QUERY_DEVICE_INFO_CONF = 38;
public const int CSTA_RECONNECT_CALL = 39;
public const int CSTA_RECONNECT_CALL_CONF = 40;
public const int CSTA_RETRIEVE_CALL = 41;
public const int CSTA_RETRIEVE_CALL_CONF = 42;
public const int CSTA_SET_MWI = 43;
public const int CSTA_SET_MWI_CONF = 44;
public const int CSTA_SET_DND = 45;
public const int CSTA_SET_DND_CONF = 46;
public const int CSTA_SET_FWD = 47;
public const int CSTA_SET_FWD_CONF = 48;
public const int CSTA_SET_AGENT_STATE = 49;
public const int CSTA_SET_AGENT_STATE_CONF = 50;
public const int CSTA_TRANSFER_CALL = 51;
public const int CSTA_TRANSFER_CALL_CONF = 52;
public const int CSTA_UNIVERSAL_FAILURE_CONF = 53;
public const int CSTA_CALL_CLEARED = 54;
public const int CSTA_CONFERENCED = 55;
public const int CSTA_CONNECTION_CLEARED = 56;
public const int CSTA_DELIVERED = 57;
public const int CSTA_DIVERTED = 58;
public const int CSTA_ESTABLISHED = 59;
public const int CSTA_FAILED = 60;
public const int CSTA_HELD = 61;
public const int CSTA_NETWORK_REACHED = 62;
public const int CSTA_ORIGINATED = 63;
public const int CSTA_QUEUED = 64;
public const int CSTA_RETRIEVED = 65;
public const int CSTA_SERVICE_INITIATED = 66;
public const int CSTA_TRANSFERRED = 67;
public const int CSTA_CALL_INFORMATION = 68;
public const int CSTA_DO_NOT_DISTURB = 69;
public const int CSTA_FORWARDING = 70;
public const int CSTA_MESSAGE_WAITING = 71;
public const int CSTA_LOGGED_ON = 72;
public const int CSTA_LOGGED_OFF = 73;
public const int CSTA_NOT_READY = 74;
public const int CSTA_READY = 75;
public const int CSTA_WORK_NOT_READY = 76;
public const int CSTA_WORK_READY = 77;
public const int CSTA_ROUTE_REGISTER_REQ = 78;
public const int CSTA_ROUTE_REGISTER_REQ_CONF = 79;
public const int CSTA_ROUTE_REGISTER_CANCEL = 80;
public const int CSTA_ROUTE_REGISTER_CANCEL_CONF = 81;
public const int CSTA_ROUTE_REGISTER_ABORT = 82;
public const int CSTA_ROUTE_REQUEST = 83;
public const int CSTA_ROUTE_SELECT_REQUEST = 84;
public const int CSTA_RE_ROUTE_REQUEST = 85;
public const int CSTA_ROUTE_USED = 86;
public const int CSTA_ROUTE_END = 87;
public const int CSTA_ROUTE_END_REQUEST = 88;
public const int CSTA_ESCAPE_SVC = 89;
public const int CSTA_ESCAPE_SVC_CONF = 90;
public const int CSTA_ESCAPE_SVC_REQ = 91;
public const int CSTA_ESCAPE_SVC_REQ_CONF = 92;
public const int CSTA_PRIVATE = 93;
public const int CSTA_PRIVATE_STATUS = 94;
public const int CSTA_SEND_PRIVATE = 95;
public const int CSTA_BACK_IN_SERVICE = 96;
public const int CSTA_OUT_OF_SERVICE = 97;
public const int CSTA_REQ_SYS_STAT = 98;
public const int CSTA_SYS_STAT_REQ_CONF = 99;
public const int CSTA_SYS_STAT_START = 100;
public const int CSTA_SYS_STAT_START_CONF = 101;
public const int CSTA_SYS_STAT_STOP = 102;
public const int CSTA_SYS_STAT_STOP_CONF = 103;
public const int CSTA_CHANGE_SYS_STAT_FILTER = 104;
public const int CSTA_CHANGE_SYS_STAT_FILTER_CONF = 105;
public const int CSTA_SYS_STAT = 106;
public const int CSTA_SYS_STAT_ENDED = 107;
public const int CSTA_SYS_STAT_REQ = 108;
public const int CSTA_REQ_SYS_STAT_CONF = 109;
public const int CSTA_SYS_STAT_EVENT_SEND = 110;
public const int CSTA_MONITOR_DEVICE = 111;
public const int CSTA_MONITOR_CALL = 112;
public const int CSTA_MONITOR_CALLS_VIA_DEVICE = 113;
public const int CSTA_MONITOR_CONF = 114;
public const int CSTA_CHANGE_MONITOR_FILTER = 115;
public const int CSTA_CHANGE_MONITOR_FILTER_CONF = 116;
public const int CSTA_MONITOR_STOP = 117;
public const int CSTA_MONITOR_STOP_CONF = 118;
public const int CSTA_MONITOR_ENDED = 119;
public const int CSTA_SNAPSHOT_CALL = 120;
public const int CSTA_SNAPSHOT_CALL_CONF = 121;
public const int CSTA_SNAPSHOT_DEVICE = 122;
public const int CSTA_SNAPSHOT_DEVICE_CONF = 123;
public const int CSTA_GETAPI_CAPS = 124;
public const int CSTA_GETAPI_CAPS_CONF = 125;
public const int CSTA_GET_DEVICE_LIST = 126;
public const int CSTA_GET_DEVICE_LIST_CONF = 127;
public const int CSTA_QUERY_CALL_MONITOR = 128;
public const int CSTA_QUERY_CALL_MONITOR_CONF = 129;
public const int CSTA_ROUTE_REQUEST_EXT = 130;
public const int CSTA_ROUTE_USED_EXT = 131;
public const int CSTA_ROUTE_SELECT_INV_REQUEST = 132;
public const int CSTA_ROUTE_END_INV_REQUEST = 133;
public enum CSTAUniversalFailure_t
{
   GENERIC_UNSPECIFIED = 0,
   GENERIC_OPERATION = 1,
   REQUEST_INCOMPATIBLE_WITH_OBJECT = 2,
   VALUE_OUT_OF_RANGE = 3,
   OBJECT_NOT_KNOWN = 4,
   INVALID_CALLING_DEVICE = 5,
   INVALID_CALLED_DEVICE = 6,
   INVALID_FORWARDING_DESTINATION = 7,
   PRIVILEGE_VIOLATION_ON_SPECIFIED_DEVICE = 8,
   PRIVILEGE_VIOLATION_ON_CALLED_DEVICE = 9,
   PRIVILEGE_VIOLATION_ON_CALLING_DEVICE = 10,
   INVALID_CSTA_CALL_IDENTIFIER = 11,
   INVALID_CSTA_DEVICE_IDENTIFIER = 12,
   INVALID_CSTA_CONNECTION_IDENTIFIER = 13,
   INVALID_DESTINATION = 14,
   INVALID_FEATURE = 15,
   INVALID_ALLOCATION_STATE = 16,
   INVALID_CROSS_REF_ID = 17,
   INVALID_OBJECT_TYPE = 18,
   SECURITY_VIOLATION = 19,
   GENERIC_STATE_INCOMPATIBILITY = 21,
   INVALID_OBJECT_STATE = 22,
   INVALID_CONNECTION_ID_FOR_ACTIVE_CALL = 23,
   NO_ACTIVE_CALL = 24,
   NO_HELD_CALL = 25,
   NO_CALL_TO_CLEAR = 26,
   NO_CONNECTION_TO_CLEAR = 27,
   NO_CALL_TO_ANSWER = 28,
   NO_CALL_TO_COMPLETE = 29,
   GENERIC_SYSTEM_RESOURCE_AVAILABILITY = 31,
   SERVICE_BUSY = 32,
   RESOURCE_BUSY = 33,
   RESOURCE_OUT_OF_SERVICE = 34,
   NETWORK_BUSY = 35,
   NETWORK_OUT_OF_SERVICE = 36,
   OVERALL_MONITOR_LIMIT_EXCEEDED = 37,
   CONFERENCE_MEMBER_LIMIT_EXCEEDED = 38,
   GENERIC_SUBSCRIBED_RESOURCE_AVAILABILITY = 41,
   OBJECT_MONITOR_LIMIT_EXCEEDED = 42,
   EXTERNAL_TRUNK_LIMIT_EXCEEDED = 43,
   OUTSTANDING_REQUEST_LIMIT_EXCEEDED = 44,
   GENERIC_PERFORMANCE_MANAGEMENT = 51,
   PERFORMANCE_LIMIT_EXCEEDED = 52,
   UNSPECIFIED_SECURITY_ERROR = 60,
   SEQUENCE_NUMBER_VIOLATED = 61,
   TIME_STAMP_VIOLATED = 62,
   PAC_VIOLATED = 63,
   SEAL_VIOLATED = 64,
   GENERIC_UNSPECIFIED_REJECTION = 70,
   GENERIC_OPERATION_REJECTION = 71,
   DUPLICATE_INVOCATION_REJECTION = 72,
   UNRECOGNIZED_OPERATION_REJECTION = 73,
   MISTYPED_ARGUMENT_REJECTION = 74,
   RESOURCE_LIMITATION_REJECTION = 75,
   ACS_HANDLE_TERMINATION_REJECTION = 76,
   SERVICE_TERMINATION_REJECTION = 77,
   REQUEST_TIMEOUT_REJECTION = 78,
   REQUESTS_ON_DEVICE_EXCEEDED_REJECTION = 79,
   UNRECOGNIZED_APDU_REJECTION = 80,
   MISTYPED_APDU_REJECTION = 81,
   BADLY_STRUCTURED_APDU_REJECTION = 82,
   INITIATOR_RELEASING_REJECTION = 83,
   UNRECOGNIZED_LINKEDID_REJECTION = 84,
   LINKED_RESPONSE_UNEXPECTED_REJECTION = 85,
   UNEXPECTED_CHILD_OPERATION_REJECTION = 86,
   MISTYPED_RESULT_REJECTION = 87,
   UNRECOGNIZED_ERROR_REJECTION = 88,
   UNEXPECTED_ERROR_REJECTION = 89,
   MISTYPED_PARAMETER_REJECTION = 90,
   NON_STANDARD = 100,
};

public enum CSTAEventCause_t
{
   EC_NONE = -1,
   EC_ACTIVE_MONITOR = 1,
   EC_ALTERNATE = 2,
   EC_BUSY = 3,
   EC_CALL_BACK = 4,
   EC_CALL_CANCELLED = 5,
   EC_CALL_FORWARD_ALWAYS = 6,
   EC_CALL_FORWARD_BUSY = 7,
   EC_CALL_FORWARD_NO_ANSWER = 8,
   EC_CALL_FORWARD = 9,
   EC_CALL_NOT_ANSWERED = 10,
   EC_CALL_PICKUP = 11,
   EC_CAMP_ON = 12,
   EC_DEST_NOT_OBTAINABLE = 13,
   EC_DO_NOT_DISTURB = 14,
   EC_INCOMPATIBLE_DESTINATION = 15,
   EC_INVALID_ACCOUNT_CODE = 16,
   EC_KEY_CONFERENCE = 17,
   EC_LOCKOUT = 18,
   EC_MAINTENANCE = 19,
   EC_NETWORK_CONGESTION = 20,
   EC_NETWORK_NOT_OBTAINABLE = 21,
   EC_NEW_CALL = 22,
   EC_NO_AVAILABLE_AGENTS = 23,
   EC_OVERRIDE = 24,
   EC_PARK = 25,
   EC_OVERFLOW = 26,
   EC_RECALL = 27,
   EC_REDIRECTED = 28,
   EC_REORDER_TONE = 29,
   EC_RESOURCES_NOT_AVAILABLE = 30,
   EC_SILENT_MONITOR = 31,
   EC_TRANSFER = 32,
   EC_TRUNKS_BUSY = 33,
   EC_VOICE_UNIT_INITIATOR = 34,
};

public enum DeviceIDType_t
{
   DEVICE_IDENTIFIER = 0,
   IMPLICIT_PUBLIC = 20,
   EXPLICIT_PUBLIC_UNKNOWN = 30,
   EXPLICIT_PUBLICintERNATIONAL = 31,
   EXPLICIT_PUBLIC_NATIONAL = 32,
   EXPLICIT_PUBLIC_NETWORK_SPECIFIC = 33,
   EXPLICIT_PUBLIC_SUBSCRIBER = 34,
   EXPLICIT_PUBLIC_ABBREVIATED = 35,
   IMPLICIT_PRIVATE = 40,
   EXPLICIT_PRIVATE_UNKNOWN = 50,
   EXPLICIT_PRIVATE_LEVEL3_REGIONAL_NUMBER = 51,
   EXPLICIT_PRIVATE_LEVEL2_REGIONAL_NUMBER = 52,
   EXPLICIT_PRIVATE_LEVEL1_REGIONAL_NUMBER = 53,
   EXPLICIT_PRIVATE_PTN_SPECIFIC_NUMBER = 54,
   EXPLICIT_PRIVATE_LOCAL_NUMBER = 55,
   EXPLICIT_PRIVATE_ABBREVIATED = 56,
   OTHER_PLAN = 60,
   TRUNK_IDENTIFIER = 70,
   TRUNK_GROUP_IDENTIFIER = 71,
};

public enum DeviceIDStatus_t
{
   ID_PROVIDED = 0,
   ID_NOT_KNOWN = 1,
   ID_NOT_REQUIRED = 2,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ExtendedDeviceID_t
{
   public DeviceID_t deviceID;
   public DeviceIDType_t deviceIDType;
   public DeviceIDStatus_t deviceIDStatus;
};

public enum ConnectionID_Device_t
{
   STATIC_ID = 0,
   DYNAMIC_ID = 1,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ConnectionID_t
{
   public UInt32 callID;
   public DeviceID_t deviceID;
   public ConnectionID_Device_t devIDType;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct Connection_t
{
   public ConnectionID_t party;
   public ExtendedDeviceID_t staticDevice;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ConnectionList_t
{
   public int count;
   public Connection_t connection;
};

public enum LocalConnectionState_t
{
   CS_NONE = -1,
   CS_NULL = 0,
   CS_INITIATE = 1,
   CS_ALERTING = 2,
   CS_CONNECT = 3,
   CS_HOLD = 4,
   CS_QUEUED = 5,
   CS_FAIL = 6,
};

public const int CF_CALL_CLEARED = 0x8000;
public const int CF_CONFERENCED = 0x4000;
public const int CF_CONNECTION_CLEARED = 0x2000;
public const int CF_DELIVERED = 0x1000;
public const int CF_DIVERTED = 0x0800;
public const int CF_ESTABLISHED = 0x0400;
public const int CF_FAILED = 0x0200;
public const int CF_HELD = 0x0100;
public const int CF_NETWORK_REACHED = 0x0080;
public const int CF_ORIGINATED = 0x0040;
public const int CF_QUEUED = 0x0020;
public const int CF_RETRIEVED = 0x0010;
public const int CF_SERVICE_INITIATED = 0x0008;
public const int CF_TRANSFERRED = 0x0004;
public const int FF_CALL_INFORMATION = 0x80;
public const int FF_DO_NOT_DISTURB = 0x40;
public const int FF_FORWARDING = 0x20;
public const int FF_MESSAGE_WAITING = 0x10;
public const int AF_LOGGED_ON = 0x80;
public const int AF_LOGGED_OFF = 0x40;
public const int AF_NOT_READY = 0x20;
public const int AF_READY = 0x10;
public const int AF_WORK_NOT_READY = 0x08;
public const int AF_WORK_READY = 0x04;
public const int MF_BACK_IN_SERVICE = 0x80;
public const int MF_OUT_OF_SERVICE = 0x40;
[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMonitorFilter_t
{
   public ushort call;
   public byte feature;
   public byte agent;
   public byte maintenance;
   public UInt32 privateFilter;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTACallState_t
{
   public int count;
   public LocalConnectionState_t state;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASnapshotDeviceResponseInfo_t
{
   public ConnectionID_t callIdentifier;
   public CSTACallState_t localCallState;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASnapshotDeviceData_t
{
   public int count;
   public CSTASnapshotDeviceResponseInfo_t info;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASnapshotCallResponseInfo_t
{
   public ExtendedDeviceID_t deviceOnCall;
   public ConnectionID_t callIdentifier;
   public LocalConnectionState_t localConnectionState;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASnapshotCallData_t
{
   public int count;
   public CSTASnapshotCallResponseInfo_t info;
};

public enum ForwardingType_t
{
   FWD_IMMEDIATE = 0,
   FWD_BUSY = 1,
   FWD_NO_ANS = 2,
   FWD_BUSYint = 3,
   FWD_BUSY_EXT = 4,
   FWD_NO_ANSint = 5,
   FWD_NO_ANS_EXT = 6,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ForwardingInfo_t
{
   public ForwardingType_t forwardingType;
   public Boolean forwardingOn;
   public DeviceID_t forwardDN;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ListForwardParameters_t
{
   public short count;
   [ MarshalAs( UnmanagedType.ByValArray, SizeConst=7)]
   public ForwardingInfo_t [] param;
};

public enum SelectValue_t
{
   SV_NORMAL = 0,
   SV_LEAST_COST = 1,
   SV_EMERGENCY = 2,
   SV_ACD = 3,
   SV_USER_DEFINED = 4,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct SetUpValues_t
{
   public int length;
   public byte value;
};

public const int noListAvailable = -1;
public const int noCountAvailable = -2;
[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAAlternateCall_t
{
   public ConnectionID_t activeCall;
   public ConnectionID_t otherCall;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAAlternateCallConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAAnswerCall_t
{
   public ConnectionID_t alertingCall;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAAnswerCallConfEvent_t
{
   public Nulltype nil;
};

public enum Feature_t
{
   FT_CAMP_ON = 0,
   FT_CALL_BACK = 1,
   FTintRUDE = 2,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTACallCompletion_t
{
   public Feature_t feature;
   public ConnectionID_t call;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTACallCompletionConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAClearCall_t
{
   public ConnectionID_t call;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAClearCallConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAClearConnection_t
{
   public ConnectionID_t call;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAClearConnectionConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAConferenceCall_t
{
   public ConnectionID_t heldCall;
   public ConnectionID_t activeCall;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAConferenceCallConfEvent_t
{
   public ConnectionID_t newCall;
   public ConnectionList_t connList;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAConsultationCall_t
{
   public ConnectionID_t activeCall;
   public DeviceID_t calledDevice;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAConsultationCallConfEvent_t
{
   public ConnectionID_t newCall;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTADeflectCall_t
{
   public ConnectionID_t deflectCall;
   public DeviceID_t calledDevice;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTADeflectCallConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAPickupCall_t
{
   public ConnectionID_t deflectCall;
   public DeviceID_t calledDevice;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAPickupCallConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAGroupPickupCall_t
{
   public ConnectionID_t deflectCall;
   public DeviceID_t pickupDevice;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAGroupPickupCallConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAHoldCall_t
{
   public ConnectionID_t activeCall;
   public Boolean reservation;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAHoldCallConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMakeCall_t
{
   public DeviceID_t callingDevice;
   public DeviceID_t calledDevice;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMakeCallConfEvent_t
{
   public ConnectionID_t newCall;
};

public enum AllocationState_t
{
   AS_CALL_DELIVERED = 0,
   AS_CALL_ESTABLISHED = 1,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMakePredictiveCall_t
{
   public DeviceID_t callingDevice;
   public DeviceID_t calledDevice;
   public AllocationState_t allocationState;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMakePredictiveCallConfEvent_t
{
   public ConnectionID_t newCall;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryMwi_t
{
   public DeviceID_t device;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryMwiConfEvent_t
{
   public Boolean messages;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryDnd_t
{
   public DeviceID_t device;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryDndConfEvent_t
{
   public Boolean doNotDisturb;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryFwd_t
{
   public DeviceID_t device;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryFwdConfEvent_t
{
   public ListForwardParameters_t forward;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryAgentState_t
{
   public DeviceID_t device;
};

public enum AgentState_t
{
   AG_NOT_READY = 0,
   AG_NULL = 1,
   AG_READY = 2,
   AG_WORK_NOT_READY = 3,
   AG_WORK_READY = 4,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryAgentStateConfEvent_t
{
   public AgentState_t agentState;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryLastNumber_t
{
   public DeviceID_t device;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryLastNumberConfEvent_t
{
   public DeviceID_t lastNumber;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryDeviceInfo_t
{
   public DeviceID_t device;
};

public enum DeviceType_t
{
   DT_STATION = 0,
   DT_LINE = 1,
   DT_BUTTON = 2,
   DT_ACD = 3,
   DT_TRUNK = 4,
   DT_OPERATOR = 5,
   DT_STATION_GROUP = 16,
   DT_LINE_GROUP = 17,
   DT_BUTTON_GROUP = 18,
   DT_ACD_GROUP = 19,
   DT_TRUNK_GROUP = 20,
   DT_OPERATOR_GROUP = 21,
   DT_OTHER = 255,
};

public const int DC_VOICE = 0x80;
public const int DC_DATA = 0x40;
public const int DC_IMAGE = 0x20;
public const int DC_OTHER = 0x10;
[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryDeviceInfoConfEvent_t
{
   public DeviceID_t device;
   public DeviceType_t deviceType;
   public byte deviceClass;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAReconnectCall_t
{
   public ConnectionID_t activeCall;
   public ConnectionID_t heldCall;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAReconnectCallConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARetrieveCall_t
{
   public ConnectionID_t heldCall;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARetrieveCallConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASetMwi_t
{
   public DeviceID_t device;
   public Boolean messages;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASetMwiConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASetDnd_t
{
   public DeviceID_t device;
   public Boolean doNotDisturb;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASetDndConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASetFwd_t
{
   public DeviceID_t device;
   public ForwardingInfo_t forward;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASetFwdConfEvent_t
{
   public Nulltype nil;
};

public enum AgentMode_t
{
   AM_LOG_IN = 0,
   AM_LOG_OUT = 1,
   AM_NOT_READY = 2,
   AM_READY = 3,
   AM_WORK_NOT_READY = 4,
   AM_WORK_READY = 5,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASetAgentState_t
{
   public DeviceID_t device;
   public AgentMode_t agentMode;
   public AgentID_t agentID;
   public DeviceID_t agentGroup;
   public AgentPassword_t agentPassword;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASetAgentStateConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTATransferCall_t
{
   public ConnectionID_t heldCall;
   public ConnectionID_t activeCall;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTATransferCallConfEvent_t
{
   public ConnectionID_t newCall;
   public ConnectionList_t connList;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAUniversalFailureConfEvent_t
{
   public CSTAUniversalFailure_t error;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTACallClearedEvent_t
{
   public ConnectionID_t clearedCall;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAConferencedEvent_t
{
   public ConnectionID_t primaryOldCall;
   public ConnectionID_t secondaryOldCall;
   public ExtendedDeviceID_t confController;
   public ExtendedDeviceID_t addedParty;
   public ConnectionList_t conferenceConnections;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAConnectionClearedEvent_t
{
   public ConnectionID_t droppedConnection;
   public ExtendedDeviceID_t releasingDevice;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTADeliveredEvent_t
{
   public ConnectionID_t connection;
   public ExtendedDeviceID_t alertingDevice;
   public ExtendedDeviceID_t callingDevice;
   public ExtendedDeviceID_t calledDevice;
   public ExtendedDeviceID_t lastRedirectionDevice;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTADivertedEvent_t
{
   public ConnectionID_t connection;
   public ExtendedDeviceID_t divertingDevice;
   public ExtendedDeviceID_t newDestination;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAEstablishedEvent_t
{
   public ConnectionID_t establishedConnection;
   public ExtendedDeviceID_t answeringDevice;
   public ExtendedDeviceID_t callingDevice;
   public ExtendedDeviceID_t calledDevice;
   public ExtendedDeviceID_t lastRedirectionDevice;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAFailedEvent_t
{
   public ConnectionID_t failedConnection;
   public ExtendedDeviceID_t failingDevice;
   public ExtendedDeviceID_t calledDevice;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAHeldEvent_t
{
   public ConnectionID_t heldConnection;
   public ExtendedDeviceID_t holdingDevice;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTANetworkReachedEvent_t
{
   public ConnectionID_t connection;
   public ExtendedDeviceID_t trunkUsed;
   public ExtendedDeviceID_t calledDevice;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAOriginatedEvent_t
{
   public ConnectionID_t originatedConnection;
   public ExtendedDeviceID_t callingDevice;
   public ExtendedDeviceID_t calledDevice;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueuedEvent_t
{
   public ConnectionID_t queuedConnection;
   public ExtendedDeviceID_t queue;
   public ExtendedDeviceID_t callingDevice;
   public ExtendedDeviceID_t calledDevice;
   public ExtendedDeviceID_t lastRedirectionDevice;
   public short numberQueued;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARetrievedEvent_t
{
   public ConnectionID_t retrievedConnection;
   public ExtendedDeviceID_t retrievingDevice;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAServiceInitiatedEvent_t
{
   public ConnectionID_t initiatedConnection;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTATransferredEvent_t
{
   public ConnectionID_t primaryOldCall;
   public ConnectionID_t secondaryOldCall;
   public ExtendedDeviceID_t transferringDevice;
   public ExtendedDeviceID_t transferredDevice;
   public ConnectionList_t transferredConnections;
   public LocalConnectionState_t localConnectionInfo;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTACallInformationEvent_t
{
   public ConnectionID_t connection;
   public ExtendedDeviceID_t device;
   public AccountInfo_t accountInfo;
   public AuthCode_t authorisationCode;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTADoNotDisturbEvent_t
{
   public ExtendedDeviceID_t device;
   public Boolean doNotDisturbOn;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAForwardingEvent_t
{
   public ExtendedDeviceID_t device;
   public ForwardingInfo_t forwardingInformation;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMessageWaitingEvent_t
{
   public ExtendedDeviceID_t deviceForMessage;
   public ExtendedDeviceID_t invokingDevice;
   public Boolean messageWaitingOn;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTALoggedOnEvent_t
{
   public ExtendedDeviceID_t agentDevice;
   public AgentID_t agentID;
   public DeviceID_t agentGroup;
   public AgentPassword_t password;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTALoggedOffEvent_t
{
   public ExtendedDeviceID_t agentDevice;
   public AgentID_t agentID;
   public DeviceID_t agentGroup;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTANotReadyEvent_t
{
   public ExtendedDeviceID_t agentDevice;
   public AgentID_t agentID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAReadyEvent_t
{
   public ExtendedDeviceID_t agentDevice;
   public AgentID_t agentID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAWorkNotReadyEvent_t
{
   public ExtendedDeviceID_t agentDevice;
   public AgentID_t agentID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAWorkReadyEvent_t
{
   public ExtendedDeviceID_t agentDevice;
   public AgentID_t agentID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteRegisterReq_t
{
   public DeviceID_t routingDevice;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteRegisterReqConfEvent_t
{
   public int registerReqID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteRegisterCancel_t
{
   public int routeRegisterReqID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteRegisterCancelConfEvent_t
{
   public int routeRegisterReqID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteRegisterAbortEvent_t
{
   public int routeRegisterReqID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteRequestEvent_t
{
   public int routeRegisterReqID;
   public int routingCrossRefID;
   public DeviceID_t currentRoute;
   public DeviceID_t callingDevice;
   public ConnectionID_t routedCall;
   public SelectValue_t routedSelAlgorithm;
   public Boolean priority;
   public SetUpValues_t setupInformation;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteSelectRequest_t
{
   public int routeRegisterReqID;
   public int routingCrossRefID;
   public DeviceID_t routeSelected;
   public short remainRetry;
   public SetUpValues_t setupInformation;
   public Boolean routeUsedReq;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAReRouteRequest_t
{
   public int routeRegisterReqID;
   public int routingCrossRefID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteUsedEvent_t
{
   public int routeRegisterReqID;
   public int routingCrossRefID;
   public DeviceID_t routeUsed;
   public DeviceID_t callingDevice;
   public Boolean domain;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteEndEvent_t
{
   public int routeRegisterReqID;
   public int routingCrossRefID;
   public CSTAUniversalFailure_t errorValue;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteEndRequest_t
{
   public int routeRegisterReqID;
   public int routingCrossRefID;
   public CSTAUniversalFailure_t errorValue;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAEscapeSvc_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAEscapeSvcConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAEscapeSvcReqEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAEscapeSvcReqConf_t
{
   public CSTAUniversalFailure_t errorValue;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAPrivateEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAPrivateStatusEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASendPrivateEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTABackInServiceEvent_t
{
   public DeviceID_t device;
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAOutOfServiceEvent_t
{
   public DeviceID_t device;
   public CSTAEventCause_t cause;
};

public enum SystemStatus_t
{
   SS_INITIALIZING = 0,
   SS_ENABLED = 1,
   SS_NORMAL = 2,
   SS_MESSAGES_LOST = 3,
   SS_DISABLED = 4,
   SS_OVERLOAD_IMMINENT = 5,
   SS_OVERLOAD_REACHED = 6,
   SS_OVERLOAD_RELIEVED = 7,
};

public const int SF_INITIALIZING = 0x80;
public const int SF_ENABLED = 0x40;
public const int SF_NORMAL = 0x20;
public const int SF_MESSAGES_LOST = 0x10;
public const int SF_DISABLED = 0x08;
public const int SF_OVERLOAD_IMMINENT = 0x04;
public const int SF_OVERLOAD_REACHED = 0x02;
public const int SF_OVERLOAD_RELIEVED = 0x01;
[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAReqSysStat_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASysStatReqConfEvent_t
{
   public SystemStatus_t systemStatus;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASysStatStart_t
{
   public byte statusFilter;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASysStatStartConfEvent_t
{
   public byte statusFilter;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASysStatStop_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASysStatStopConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAChangeSysStatFilter_t
{
   public byte statusFilter;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAChangeSysStatFilterConfEvent_t
{
   public byte statusFilterSelected;
   public byte statusFilterActive;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASysStatEvent_t
{
   public SystemStatus_t systemStatus;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASysStatEndedEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASysStatReqEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAReqSysStatConf_t
{
   public SystemStatus_t systemStatus;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASysStatEventSend_t
{
   public SystemStatus_t systemStatus;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMonitorDevice_t
{
   public DeviceID_t deviceID;
   public CSTAMonitorFilter_t monitorFilter;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMonitorCall_t
{
   public ConnectionID_t call;
   public CSTAMonitorFilter_t monitorFilter;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMonitorCallsViaDevice_t
{
   public DeviceID_t deviceID;
   public CSTAMonitorFilter_t monitorFilter;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMonitorConfEvent_t
{
   public int monitorCrossRefID;
   public CSTAMonitorFilter_t monitorFilter;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAChangeMonitorFilter_t
{
   public int monitorCrossRefID;
   public CSTAMonitorFilter_t monitorFilter;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAChangeMonitorFilterConfEvent_t
{
   public CSTAMonitorFilter_t monitorFilter;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMonitorStop_t
{
   public int monitorCrossRefID;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMonitorStopConfEvent_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAMonitorEndedEvent_t
{
   public CSTAEventCause_t cause;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASnapshotCall_t
{
   public ConnectionID_t snapshotObject;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASnapshotCallConfEvent_t
{
   public CSTASnapshotCallData_t snapshotData;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASnapshotDevice_t
{
   public DeviceID_t snapshotObject;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTASnapshotDeviceConfEvent_t
{
   public CSTASnapshotDeviceData_t snapshotData;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAGetAPICaps_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAGetAPICapsConfEvent_t
{
   public short alternateCall;
   public short answerCall;
   public short callCompletion;
   public short clearCall;
   public short clearConnection;
   public short conferenceCall;
   public short consultationCall;
   public short deflectCall;
   public short pickupCall;
   public short groupPickupCall;
   public short holdCall;
   public short makeCall;
   public short makePredictiveCall;
   public short queryMwi;
   public short queryDnd;
   public short queryFwd;
   public short queryAgentState;
   public short queryLastNumber;
   public short queryDeviceInfo;
   public short reconnectCall;
   public short retrieveCall;
   public short setMwi;
   public short setDnd;
   public short setFwd;
   public short setAgentState;
   public short transferCall;
   public short eventReport;
   public short callClearedEvent;
   public short conferencedEvent;
   public short connectionClearedEvent;
   public short deliveredEvent;
   public short divertedEvent;
   public short establishedEvent;
   public short failedEvent;
   public short heldEvent;
   public short networkReachedEvent;
   public short originatedEvent;
   public short queuedEvent;
   public short retrievedEvent;
   public short serviceInitiatedEvent;
   public short transferredEvent;
   public short callInformationEvent;
   public short doNotDisturbEvent;
   public short forwardingEvent;
   public short messageWaitingEvent;
   public short loggedOnEvent;
   public short loggedOffEvent;
   public short notReadyEvent;
   public short readyEvent;
   public short workNotReadyEvent;
   public short workReadyEvent;
   public short backInServiceEvent;
   public short outOfServiceEvent;
   public short privateEvent;
   public short routeRequestEvent;
   public short reRoute;
   public short routeSelect;
   public short routeUsedEvent;
   public short routeEndEvent;
   public short monitorDevice;
   public short monitorCall;
   public short monitorCallsViaDevice;
   public short changeMonitorFilter;
   public short monitorStop;
   public short monitorEnded;
   public short snapshotDeviceReq;
   public short snapshotCallReq;
   public short escapeService;
   public short privateStatusEvent;
   public short escapeServiceEvent;
   public short escapeServiceConf;
   public short sendPrivateEvent;
   public short sysStatReq;
   public short sysStatStart;
   public short sysStatStop;
   public short changeSysStatFilter;
   public short sysStatReqEvent;
   public short sysStatReqConf;
   public short sysStatEvent;
};

public enum CSTALevel_t
{
   CSTA_HOME_WORK_TOP = 1,
   CSTA_AWAY_WORK_TOP = 2,
   CSTA_DEVICE_DEVICE_MONITOR = 3,
   CSTA_CALL_DEVICE_MONITOR = 4,
   CSTA_CALL_CONTROL = 5,
   CSTA_ROUTING = 6,
   CSTA_CALL_CALL_MONITOR = 7,
};

public enum SDBLevel_t
{
   NO_SDB_CHECKING = -1,
   ACS_ONLY = 1,
   ACS_AND_CSTA_CHECKING = 0,
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAGetDeviceList_t
{
   public int index;
   public CSTALevel_t level;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct DeviceList_t
{
   public short count;
   [ MarshalAs( UnmanagedType.ByValArray, SizeConst=20)]
   public DeviceID_t [] device;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAGetDeviceListConfEvent_t
{
   public SDBLevel_t driverSdbLevel;
   public CSTALevel_t level;
   public int index;
   public DeviceList_t devList;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryCallMonitor_t
{
   public Nulltype nil;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTAQueryCallMonitorConfEvent_t
{
   public Boolean callMonitor;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteRequestExtEvent_t
{
   public int routeRegisterReqID;
   public int routingCrossRefID;
   public ExtendedDeviceID_t currentRoute;
   public ExtendedDeviceID_t callingDevice;
   public ConnectionID_t routedCall;
   public SelectValue_t routedSelAlgorithm;
   public Boolean priority;
   public SetUpValues_t setupInformation;
};

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct CSTARouteUsedExtEvent_t
{
   public int routeRegisterReqID;
   public int routingCrossRefID;
   public ExtendedDeviceID_t routeUsed;
   public ExtendedDeviceID_t callingDevice;
   public Boolean domain;
};

// Generated by PInvoke Wizard (v 1.3) from The Paul Yao Company http://www.paulyao.com 

public const int TSERV_SAP_CSTA = 0x0559;
public const int CLIENT_SAP_CSTA = 0x5905;
public const int TSERV_SAP_NMSRV = 0x055B;
public const int CLIENT_SAP_NMSRV = 0x5B05;
public const int ACSPOSITIVE_ACK = 0;
public const int ACSERR_APIVERDENIED = -1;
public const int ACSERR_BADPARAMETER = -2;
public const int ACSERR_DUPSTREAM = -3;
public const int ACSERR_NODRIVER = -4;
public const int ACSERR_NOSERVER = -5;
public const int ACSERR_NORESOURCE = -6;
public const int ACSERR_UBUFSMALL = -7;
public const int ACSERR_NOMESSAGE = -8;
public const int ACSERR_UNKNOWN = -9;
public const int ACSERR_BADHDL = -10;
public const int ACSERR_STREAM_FAILED = -11;
public const int ACSERR_NOBUFFERS = -12;
public const int ACSERR_QUEUE_FULL = -13;
public enum InvokeIDType_t
{
   APP_GEN_ID,
   LIB_GEN_ID,
};

public const int ACSREQUEST = 0;
public const int ACSUNSOLICITED = 1;
public const int ACSCONFIRMATION = 2;
[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct ACSEventHeader_t
{
   public ulong acsHandle;
   public ushort eventClass;
   public ushort eventType;
};

// *****************************
// * Nested elements not supported
// *****************************
   // typedef struct 
   // {
   // 	union 
   // 	{
   // 		ACSUniversalFailureEvent_t	failureEvent;
   // 	} u;
   // } ACSUnsolicitedEvent;
// *****************************

// *****************************
// * Nested elements not supported
// *****************************
   // typedef struct 
   // {
   // 	InvokeID_t	invokeID;
   // 	union 
   // 	{
   // 		ACSOpenStreamConfEvent_t		acsopen;
   // 		ACSCloseStreamConfEvent_t		acsclose;
   // 		ACSUniversalFailureConfEvent_t	failureEvent;
   // 	} u;
   // } ACSConfirmationEvent;
// *****************************

public const int ACS_MAX_HEAP = 1024;
// *****************************
// * Nested elements not supported
// *****************************
   // typedef struct 
   // {
   // 	ACSEventHeader_t	eventHeader;
   // 	union 
   // 	{
   // 		ACSUnsolicitedEvent		acsUnsolicited;
   // 		ACSConfirmationEvent	acsConfirmation;
   // 	} event;
   // 	char	heap[ACS_MAX_HEAP];
   // } ACSEvent_t;
// *****************************

[StructLayout(LayoutKind.Sequential,Pack=4)]
public struct PrivateData_t
{
   [ MarshalAs( UnmanagedType.ByValArray, SizeConst=32)]
   public char [] vendor;
   public ushort length;
   [ MarshalAs( UnmanagedType.ByValArray, SizeConst=1)]
   public char [] data;
};

public struct EventBuf_t
{
    [MarshalAs( UnmanagedType.ByValArray, SizeConst=CSTA_MAX_HEAP)]
    public byte [] data;
};

public const int PRIVATE_DATA_ENCODING = 0;
    [DllImport("csta32.dll")]
    public static extern int acsOpenStream(ref UInt32 acsHandle, int invokeIDType, UInt32 invokeID, int streamType, char[] serverID, char[] loginID, char[] passwd, char[] applicationName, int acsLevelReq, char[] apiVer, ushort sendQSize, ushort sendExtraBufs, ushort recvQSize, ushort recvExtraBufs, ref PrivateData_t priv);

    [DllImport("csta32.dll")]
    // public static extern int acsCloseStream (ACSHandle_t acsHandle, uint invokeID, ref PrivateData_t priv);
    public static extern int acsCloseStream(UInt32 acsHandle, UInt32 invokeID, ref PrivateData_t priv);

    [DllImport("csta32.dll")]
    // public static extern int acsAbortStream (ACSHandle_t acsHandle, ref PrivateData_t priv);
    public static extern int acsAbortStream(UInt32 acsHandle, ref PrivateData_t priv);

    [DllImport("csta32.dll")]
    // public static extern int acsFlushEventQueue (ACSHandle_t acsHandle);
    public static extern int acsFlushEventQueue(UInt32 acsHandle);

    [DllImport("csta32.dll")]
    public static extern int acsGetEventPoll(UInt32 acsHandle, ref EventBuf_t eventBuf, ref ushort eventBufSize, ref PrivateData_t privData, ref ushort numEvents);

    [DllImport("csta32.dll")]
    // public static extern int acsGetEventBlock (ACSHandle_t acsHandle, ref EventBuf_t eventBuf, ref ushort eventBufSize, ref PrivateData_t privData, ref ushort numEvents);
    public static extern int acsGetEventBlock(UInt32 acsHandle, ref EventBuf_t eventBuf, ref ushort eventBufSize, ref PrivateData_t privData, ref ushort numEvents);

    [DllImport("csta32.dll")]
    // public static extern int acsEventNotify (ACSHandle_t acsHandle, IntPtr hwnd, int msg, Boolean notifyAll);
    public static extern int acsEventNotify(UInt32 acsHandle, IntPtr hwnd, int msg, Boolean notifyAll);

    [DllImport("csta32.dll")]
    // public static extern int acsSetESR (ACSHandle_t acsHandle, EventBuf_t param1);
    public static extern int acsSetESR(UInt32 acsHandle, EventBuf_t param1);

    // [DllImport("csta32.dll")]
    // public static extern int acsEventNotify (ACSHandle_t acsHandle, IntPtr hwnd, int msg, Boolean notifyAll);
    // public static extern int acsEventNotify(ref IntPtr acsHandle, IntPtr hwnd, int msg, Boolean notifyAll);

    // [DllImport("csta32.dll")]
    // public static extern int acsSetESR (ACSHandle_t acsHandle, void param1);

    // [DllImport("csta32.dll")]
    // public static extern int acsSetESR (ACSHandle_t acsHandle, void param1);

    [DllImport("csta32.dll")]
    // public static extern int acsEventNotify (ACSHandle_t acsHandle, IntPtr hwnd, int msg, byte notifyAll);
    public static extern int acsEventNotify(UInt32 acsHandle, IntPtr hwnd, int msg, byte notifyAll);

    // [DllImport("csta32.dll")]
    // public static extern int acsSetESR (ACSHandle_t acsHandle, void param1);

    // public const int kTSAPIEventClass = 'Csta';
    // public const int kTSAPIEventArrived = 'NuEv';
    // public const int keyTSAPIEventClass = 'clas';
    // public const int keyTSAPIEventType = 'type';
    // public const int keyStreamHandle = 'strm';
    
    // [DllImport("csta32.dll")]
    // public static extern int acsEventNotify (ACSHandle_t acsHandle, ref AEAddressDesc targetAddr, Boolean notifyAll);
    // public static extern int acsEventNotify(ref IntPtr acsHandle, ref AEAddressDesc targetAddr, Boolean notifyAll);

    // [DllImport("csta32.dll")]
    // public static extern int acsSetESR (ACSHandle_t acsHandle, UniversalProcPtr esr, uint esrParam, Boolean notifyAll);
    // public static extern int acsSetESR(ref IntPtr acsHandle, UniversalProcPtr esr, uint esrParam, Boolean notifyAll);

// public const int kTSAPIEventClass = 'Csta';
// public const int kTSAPIEventArrived = 'NuEv';
// public const int keyTSAPIEventClass = 'clas';
// public const int keyTSAPIEventType = 'type';
// public const int keyStreamHandle = 'strm';

    // [DllImport("csta32.dll")]
    // public static extern int acsEventNotify (ACSHandle_t acsHandle, ref AEAddressDesc targetAddr, Boolean notifyAll);

    // [DllImport("csta32.dll")]
    // public static extern uppEsrFuncProcInfo RESULT_SIZE (SIZE_CODE param0);

    // [DllImport("csta32.dll")]
    // public static extern int acsSetESR (ACSHandle_t acsHandle, UniversalProcPtr esr, uint esrParam, Boolean notifyAll);

    // [DllImport("csta32.dll")]
    // public static extern uppEnumServerNamesProcInfo RESULT_SIZE (SIZE_CODE param0);

    [DllImport("csta32.dll")]
    public static extern int acsGetFile (UInt32 acsHandle);

    // [DllImport("csta32.dll")]
    // public static extern int acsGetFile (ACSHandle_t acsHandle);

    // [DllImport("csta32.dll")]
    // public static extern int acsEnumServerNames (StreamType_t streamType, UniversalProcPtr userCB, uint lParam);

    // [DllImport("csta32.dll")]
    // public static extern int acsQueryAuthInfo (ref ServerID_t serverID, ref ACSAuthInfo_t authInfo);

public const string CSTA_API_VERSION = "TS2";
public const int CSTAREQUEST = 3;
public const int CSTAUNSOLICITED = 4;
public const int CSTACONFIRMATION = 5;
public const int CSTAEVENTREPORT = 6;
public const int CSTA_MAX_GET_DEVICE = 20;
// *****************************
// * Nested elements not supported
// *****************************
   // typedef struct 
   // {
   // 	InvokeID_t	invokeID;
   // 	union 
   // 	{
   // 		CSTARouteRequestEvent_t		routeRequest;
   // 		CSTARouteRequestExtEvent_t	routeRequestExt;
   // 		CSTAReRouteRequest_t		reRouteRequest;
   // 		CSTAEscapeSvcReqEvent_t		escapeSvcReqeust;
   // 		CSTASysStatReqEvent_t		sysStatRequest;
   // 	} u;
   // } CSTARequestEvent;
// *****************************

// *****************************
// * Nested elements not supported
// *****************************
   // typedef struct
   // {
   // 	union
   // 	{
   // 		CSTARouteRegisterAbortEvent_t   registerAbort;
   // 		CSTARouteUsedEvent_t			routeUsed;
   // 		CSTARouteUsedExtEvent_t			routeUsedExt;
   // 		CSTARouteEndEvent_t				routeEnd;
   // 		CSTAPrivateEvent_t				privateEvent;
   // 		CSTASysStatEvent_t				sysStat;
   // 		CSTASysStatEndedEvent_t			sysStatEnded;
   // 	}u;
   // } CSTAEventReport;
// *****************************

// *****************************
// * Nested elements not supported
// *****************************
   // typedef struct 
   // {
   // 	CSTAMonitorCrossRefID_t		monitorCrossRefId;
   // 	union 
   // 	{
   // 		CSTACallClearedEvent_t			callCleared;
   // 		CSTAConferencedEvent_t			conferenced;
   // 		CSTAConnectionClearedEvent_t	connectionCleared;
   // 		CSTADeliveredEvent_t			delivered;
   // 		CSTADivertedEvent_t				diverted;
   // 		CSTAEstablishedEvent_t			established;
   // 		CSTAFailedEvent_t				failed;
   // 		CSTAHeldEvent_t					held;
   // 		CSTANetworkReachedEvent_t		networkReached;
   // 		CSTAOriginatedEvent_t			originated;
   // 		CSTAQueuedEvent_t				queued;
   // 		CSTARetrievedEvent_t			retrieved;
   // 		CSTAServiceInitiatedEvent_t		serviceInitiated;
   // 		CSTATransferredEvent_t			transferred;
   // 		CSTACallInformationEvent_t		callInformation;
   // 		CSTADoNotDisturbEvent_t			doNotDisturb;
   // 		CSTAForwardingEvent_t			forwarding;
   // 		CSTAMessageWaitingEvent_t		messageWaiting;
   // 		CSTALoggedOnEvent_t				loggedOn;
   // 		CSTALoggedOffEvent_t			loggedOff;
   // 		CSTANotReadyEvent_t				notReady;
   // 		CSTAReadyEvent_t				ready;
   // 		CSTAWorkNotReadyEvent_t			workNotReady;
   // 		CSTAWorkReadyEvent_t			workReady;
   // 		CSTABackInServiceEvent_t		backInService;
   // 		CSTAOutOfServiceEvent_t			outOfService;
   // 		CSTAPrivateStatusEvent_t		privateStatus;
   // 		CSTAMonitorEndedEvent_t  		monitorEnded;
   // 	} u;
   // } CSTAUnsolicitedEvent;
// *****************************

// *****************************
// * Nested elements not supported
// *****************************
   // typedef struct 
   // {
   // 	InvokeID_t	invokeID;
   // 	union 
   // 	{
   // 		CSTAAlternateCallConfEvent_t		alternateCall;
   // 		CSTAAnswerCallConfEvent_t			answerCall;
   // 		CSTACallCompletionConfEvent_t		callCompletion;
   // 		CSTAClearCallConfEvent_t			clearCall;
   // 		CSTAClearConnectionConfEvent_t    	clearConnection;
   // 		CSTAConferenceCallConfEvent_t		conferenceCall;
   // 		CSTAConsultationCallConfEvent_t		consultationCall;
   // 		CSTADeflectCallConfEvent_t			deflectCall;
   // 		CSTAPickupCallConfEvent_t			pickupCall;
   // 		CSTAGroupPickupCallConfEvent_t		groupPickupCall;
   // 		CSTAHoldCallConfEvent_t				holdCall;
   // 		CSTAMakeCallConfEvent_t				makeCall;
   // 		CSTAMakePredictiveCallConfEvent_t 	makePredictiveCall;
   // 		CSTAQueryMwiConfEvent_t				queryMwi;
   // 		CSTAQueryDndConfEvent_t				queryDnd;
   // 		CSTAQueryFwdConfEvent_t				queryFwd;
   // 		CSTAQueryAgentStateConfEvent_t		queryAgentState;
   // 		CSTAQueryLastNumberConfEvent_t		queryLastNumber;
   // 		CSTAQueryDeviceInfoConfEvent_t		queryDeviceInfo;
   // 		CSTAReconnectCallConfEvent_t		reconnectCall;
   // 		CSTARetrieveCallConfEvent_t			retrieveCall;
   // 		CSTASetMwiConfEvent_t				setMwi;
   // 		CSTASetDndConfEvent_t				setDnd;
   // 		CSTASetFwdConfEvent_t				setFwd;
   // 		CSTASetAgentStateConfEvent_t		setAgentState;
   // 		CSTATransferCallConfEvent_t			transferCall;
   // 		CSTAUniversalFailureConfEvent_t		universalFailure;
   // 		CSTAMonitorConfEvent_t				monitorStart;
   // 		CSTAChangeMonitorFilterConfEvent_t	changeMonitorFilter;
   // 		CSTAMonitorStopConfEvent_t			monitorStop;
   // 		CSTASnapshotDeviceConfEvent_t		snapshotDevice;
   // 		CSTASnapshotCallConfEvent_t			snapshotCall;
   // 		CSTARouteRegisterReqConfEvent_t		routeRegister;
   // 		CSTARouteRegisterCancelConfEvent_t	routeCancel;
   // 		CSTAEscapeSvcConfEvent_t			escapeService;
   // 		CSTASysStatReqConfEvent_t			sysStatReq;
   // 		CSTASysStatStartConfEvent_t			sysStatStart;
   // 		CSTASysStatStopConfEvent_t			sysStatStop;
   // 		CSTAChangeSysStatFilterConfEvent_t	changeSysStatFilter;
   // 		CSTAGetAPICapsConfEvent_t			getAPICaps;
   // 		CSTAGetDeviceListConfEvent_t		getDeviceList;
   // 		CSTAQueryCallMonitorConfEvent_t		queryCallMonitor;
   // 	} u;
   // } CSTAConfirmationEvent;
// *****************************

public const int CSTA_MAX_HEAP = 1024;
// *****************************
// * Nested elements not supported
// *****************************
   // typedef struct 
   // {
   // 	ACSEventHeader_t	eventHeader;
   // 	union 
   // 	{
   // 		ACSUnsolicitedEvent		acsUnsolicited;
   // 		ACSConfirmationEvent	acsConfirmation;
   // 		CSTARequestEvent		cstaRequest;
   // 		CSTAUnsolicitedEvent	cstaUnsolicited;
   // 		CSTAConfirmationEvent	cstaConfirmation;
   // 		CSTAEventReport			cstaEventReport;
   // 	} event;
   // 	char	heap[CSTA_MAX_HEAP];
   // } CSTAEvent_t;
// *****************************

[DllImport("csta32.dll")]
public static extern int cstaAlternateCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t activeCall, ref ConnectionID_t otherCall, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaAnswerCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t alertingCall, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaCallCompletion (UInt32 acsHandle, UInt32 invokeID, Feature_t feature, ref ConnectionID_t call, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaClearCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t call, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaClearConnection(UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t call, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaConferenceCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t heldCall, ref ConnectionID_t activeCall, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaConsultationCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t activeCall, char [] calledDevice, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaDeflectCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t deflectCall, char [] calledDevice, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaGroupPickupCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t deflectCall, char [ ] pickupDevice, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaHoldCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t activeCall, Boolean reservation, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaMakeCall(UInt32 acsHandle, UInt32 invokeID, char [] callingDevice, char [] calledDevice, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaMakePredictiveCall (UInt32 acsHandle, UInt32 invokeID, char [] callingDevice, char [] calledDevice, AllocationState_t allocationState, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaPickupCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t deflectCall, char [] calledDevice, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaReconnectCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t activeCall, ref ConnectionID_t heldCall, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaRetrieveCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t heldCall, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaTransferCall(UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t heldCall, ref ConnectionID_t activeCall, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSetMsgWaitingInd (UInt32 acsHandle, UInt32 invokeID, char [] device, Boolean messages, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSetDoNotDisturb (UInt32 acsHandle, UInt32 invokeID, char [] device, Boolean doNotDisturb, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSetForwarding (UInt32 acsHandle, UInt32 invokeID, char [] device, ForwardingType_t forwardingType, Boolean forwardingOn, char [] forwardingDestination, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSetAgentState(UInt32 acsHandle, UInt32 invokeID, char [] device, AgentMode_t agentMode, char [] agentID, char [] agentGroup, char [] agentPassword, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaQueryMsgWaitingInd (UInt32 acsHandle, UInt32 invokeID, char [] device, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaQueryDoNotDisturb (UInt32 acsHandle, UInt32 invokeID, char [] device, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaQueryForwarding (UInt32 acsHandle, UInt32 invokeID, char [] device, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaQueryAgentState(UInt32 acsHandle, UInt32 invokeID, char [] device, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaQueryLastNumber (UInt32 acsHandle, UInt32 invokeID, char [] device, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaQueryDeviceInfo(UInt32 acsHandle, UInt32 invokeID, char [] device, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaMonitorDevice(UInt32 acsHandle, UInt32 invokeID, char [] deviceID, ref CSTAMonitorFilter_t monitorFilter, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaMonitorCall (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t call, ref CSTAMonitorFilter_t monitorFilter, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaMonitorCallsViaDevice (UInt32 acsHandle, UInt32 invokeID, char [] deviceID, ref CSTAMonitorFilter_t monitorFilter, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaChangeMonitorFilter (UInt32 acsHandle, UInt32 invokeID, int monitorCrossRefID, ref CSTAMonitorFilter_t filterlist, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaMonitorStop (UInt32 acsHandle, UInt32 invokeID, int monitorCrossRefID, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSnapshotCallReq (UInt32 acsHandle, UInt32 invokeID, ref ConnectionID_t snapshotObj, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSnapshotDeviceReq (UInt32 acsHandle, UInt32 invokeID, char [] snapshotObj, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaRouteRegisterReq (UInt32 acsHandle, UInt32 invokeID, char [] routingDevice, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaRouteRegisterCancel (UInt32 acsHandle, UInt32  invokeID, int routeRegisterReqID, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaRouteSelect (UInt32 acsHandle, int routeRegisterReqID, int routingCrossRefID, char [] routeSelected, short remainRetry, ref SetUpValues_t setupInformation, Boolean routeUsedReq, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaRouteEnd (UInt32 acsHandle, int routeRegisterReqID, int routingCrossRefID, CSTAUniversalFailure_t errorValue, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaRouteSelectInv (UInt32 acsHandle, UInt32 invokeID, int routeRegisterReqID, int routingCrossRefID, ref DeviceID_t routeSelected, short remainRetry, ref SetUpValues_t setupInformation, Boolean routeUsedReq, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaRouteEndInv (UInt32 acsHandle, UInt32 invokeID, int routeRegisterReqID, int routingCrossRefID, CSTAUniversalFailure_t errorValue, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaEscapeService (UInt32 acsHandle, UInt32 invokeID, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaEscapeServiceConf (UInt32 acsHandle, UInt32 invokeID, CSTAUniversalFailure_t error, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSendPrivateEvent (UInt32 acsHandle, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSysStatReq (UInt32 acsHandle, UInt32 invokeID, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSysStatStart (UInt32 acsHandle, UInt32 invokeID, byte statusFilter, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSysStatStop (UInt32 acsHandle, UInt32 invokeID, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaChangeSysStatFilter (UInt32 acsHandle, UInt32 invokeID, byte statusFilter, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSysStatReqConf (UInt32 acsHandle, UInt32 invokeID, SystemStatus_t systemStatus, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaSysStatEvent (UInt32 acsHandle, SystemStatus_t systemStatus, ref PrivateData_t privateData);

[DllImport("csta32.dll")]
public static extern int cstaGetAPICaps (UInt32 acsHandle, UInt32 invokeID);

[DllImport("csta32.dll")]
public static extern int cstaGetDeviceList (UInt32 acsHandle, UInt32 invokeID, int index, CSTALevel_t level);

[DllImport("csta32.dll")]
public static extern int cstaQueryCallMonitor (UInt32 acsHandle, UInt32 invokeID);

    
}
