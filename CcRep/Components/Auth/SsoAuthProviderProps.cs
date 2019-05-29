using System;

namespace CcRep.Components.Auth
{
    public enum StateHashSource
    {
        GenerateNew,
        FromRequest
    }
    public partial class SsoAuthProvider
    {
        public string AuthServHost { get; set; }
        public string AuthServPath { get; set; }
        public string AuthEndpointTokenPath { get; set; }
        public string AuthEndpointProfilePath { get; set; }
        public string AuthEndpointUserFindPath { get; set; }
        public string AppName { get; set; }
        public string AppKey { get; set; }

        public string redirectAfterAuthPath { get; set; }
        public string RespType { get; set; }
        public string StateHash { get; set; }

        private Uri CurrentUrl { get; set; }
    }
}