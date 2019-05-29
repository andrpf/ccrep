using System;
using System.Runtime.Serialization;

namespace CcRep.Components.Auth
{
    public class CantGetUserInfo : Exception, ISerializable
    {
        
        public override string Message { get; }
        public CantGetUserInfo(string message)
        {
            Message = message;
        }
    }
}