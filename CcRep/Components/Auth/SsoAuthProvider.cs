using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace CcRep.Components.Auth
{
    public partial class SsoAuthProvider
    {
        public SsoAuthProvider()
        {
            CurrentUrl = HttpContext.Current.Request.Url;
            RespType = "code";
        }

        public SsoAuthProvider(StateHashSource hashSource) : this()
        {
            if (hashSource == StateHashSource.GenerateNew)
            {
                StateHash = StateKeyHelper.generateStateKey();
            }
            else if (hashSource == StateHashSource.FromRequest)
            {
                StateHash = StateKeyHelper.getStateKeyFromRequest();
            }
        }

        public void setStateKeyToResponse()
        {
            StateKeyHelper.setStateKeyToResponse(StateHash);
        }

        public async Task<string> requestAccessTokenByAuthCode(string code)
        {
            HttpClient client = new HttpClient();

            string backRedir = HttpContext.Current.Server.UrlEncode(GenerateBackRedirct().ToString());

            UriBuilder uriBuilder = ToAuthUriBuilder;
            uriBuilder.Path = AuthEndpointTokenPath;
            uriBuilder.Query = String.Format("app_name={0}&app_key={1}&redirect_url={2}&code={3}&grant_type={4}",
                AppName, AppKey, backRedir, code, "authorization_code");

            Uri uri = uriBuilder.Uri;

            string token = await getHttpResultString(client, uri, "access_token");

            return token;
        }

        public async Task<ActiveDirctoryUser> RequestUserData(string accessToken)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            UriBuilder uriBuilder = ToAuthUriBuilder;
            uriBuilder.Path = AuthEndpointProfilePath;
            uriBuilder.Query = String.Format("grant_type={0}", "access_token");

            Uri uri = uriBuilder.Uri;

            string userData = await getHttpResultString(client, uri);

            try
            {
                var userDict = DeserializeHttpResult(userData);

                return new ActiveDirctoryUser(userDict["username"], userDict["name"], userDict["email"]);
            }
            catch
            {
                throw new HttpException(404, "HTTP/1.1 404 Not Found");
            }
        }

        public async Task<ActiveDirctoryUser> RequestUserDataByName(string name)
        {
            HttpClient client = new HttpClient();

            UriBuilder uriBuilder = ToAuthUriBuilder;
            uriBuilder.Path = AuthEndpointUserFindPath;
            uriBuilder.Query = String.Format("userName={0}", name);

            Uri uri = uriBuilder.Uri;

            string userData = await getHttpResultString(client, uri);

            if(userData is null)
            {
                return null;
            }
            try
            {
                var userDict = DeserializeHttpResult(userData);

                if(userDict != null)
                {
                    return new ActiveDirctoryUser(userDict["username"], userDict["name"], userDict["email"]);
                }

                return null;
            }
            catch
            {
                throw new CantGetUserInfo("Can't get user");
            }
        }

        public Uri GenerateRedirToAuthUri()
        {
            string backRedir = HttpContext.Current.Server.UrlEncode(GenerateBackRedirct().ToString());

            UriBuilder redirectBuilder = ToAuthUriBuilder;
            redirectBuilder.Path = AuthServPath;

            redirectBuilder.Query = String.Format("app_name={0}&app_key={1}&response_type={2}&redirect_url={3}", AppName, AppKey, RespType, backRedir);

            Uri uri = redirectBuilder.Uri;

            return uri;
        }

        public Uri GenerateBackRedirct()
        {
            UriBuilder backTuAppRedir = new UriBuilder();
            backTuAppRedir.Host = CurrentUrl.Host;
            backTuAppRedir.Port = CurrentUrl.Port;
            backTuAppRedir.Path = redirectAfterAuthPath;
            backTuAppRedir.Query = String.Format("state={0}", StateHash);

            return backTuAppRedir.Uri;
        }

        private async Task<string> getHttpResultString(HttpClient client, Uri uri, string resultJsonKey = null)
        {
            try
            {
                string content = await client.GetStringAsync(uri);

                if (resultJsonKey != null)
                {
                    var response = DeserializeHttpResult(content);
                    var respProp = response[resultJsonKey];

                    return respProp;
                }

                return content;
            }

            catch (Exception)
            {
                return null;
            }
        }

        private Dictionary<string, string> DeserializeHttpResult(string json)
        {
            return new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(json);
        }

        private UriBuilder ToAuthUriBuilder
        {
            get
            {
                UriBuilder builder = new UriBuilder();
                builder.Host = AuthServHost;
                builder.Scheme = CurrentUrl.Scheme;

                return builder;
            }
        }
    }
}