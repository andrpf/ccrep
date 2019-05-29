using System;
using System.Web;

namespace CcRep.Components.Auth
{
    public static class StateKeyHelper
    {
        const string STATE_HASH_NAME = "_sso_state";

        /** state time */
        const int STATE_COOKIE_LIFETIME = 5 * 60;

        public static string generateStateKey(string pref = "pref_")
        {
            Guid g = Guid.NewGuid();
            string GuidString = pref + Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return GuidString;
        }

        public static string getStateKeyFromRequest()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[STATE_HASH_NAME];

            return cookie == null ? null : cookie.Value;
        }

        public static void setStateKeyToResponse(string stateKey)
        {

            unsetStateKey();

            HttpCookie stateCookie = new HttpCookie(STATE_HASH_NAME);
            stateCookie.Value = stateKey;
            stateCookie.Expires = DateTime.Now.AddHours(1);

            HttpContext.Current.Response.Cookies.Add(stateCookie);
        }

        public static void unsetStateKey()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[STATE_HASH_NAME];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static bool verifyStateKey(string stateKey)
        {
            return getStateKeyFromRequest() == stateKey;
        }
    }
}