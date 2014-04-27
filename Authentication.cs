using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using Kb10uy.Extension;
using Kb10uy.Scripting;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.IO;

namespace Kbtter
{
    public partial class KbtterCore
    {

        #region 認証系メソッド
        /// <summary>
        /// OAuthで認証します。
        /// トークンを取得していない場合、
        /// GetRequestURLなどを用いて取得できます。
        /// </summary>
        /// <param name="ut">User Token</param>
        /// <param name="uts">User Token Secret</param>
        public void Authenticate(string ut, string uts)
        {
            UserToken = ut;
            UserTokenSecret = uts;
            Service.AuthenticateWith(ut, uts);
        }

        /// <summary>
        /// OAuthで認証します。
        /// トークンを取得していない場合、
        /// GetRequestURLなどを用いて取得できます。
        /// </summary>
        /// <param name="at">OAuthAccessToken</param>
        public void Authenticate(OAuthAccessToken at)
        {
            UserToken = at.Token;
            UserTokenSecret = at.TokenSecret;
            Service.AuthenticateWith(at.Token, at.TokenSecret);
        }

        /// <summary>
        /// OAuthで認証します。
        /// トークンを取得していない場合、
        /// GetRequestURLなどを用いて取得できます。
        /// </summary>
        /// <param name="ut">User Token</param>
        /// <param name="uts">User Token Secret</param>
        public Task AuthenticateAsync(string ut, string uts)
        {
            return Task.Run(() =>
            {
                UserToken = ut;
                UserTokenSecret = uts;
                Service.AuthenticateWith(ut, uts);
            });
        }

        /// <summary>
        /// OAuthで認証します。
        /// トークンを取得していない場合、
        /// GetRequestURLなどを用いて取得できます。
        /// </summary>
        /// <param name="at">OAuthAccessToken</param>
        public Task AuthenticateAsync(OAuthAccessToken at)
        {
            return Task.Run(() =>
            {
                UserToken = at.Token;
                UserTokenSecret = at.TokenSecret;
                Service.AuthenticateWith(at.Token, at.TokenSecret);
            });
        }



        /// <summary>
        /// AccessTokenを取得するための認証Uriを生成します。
        /// </summary>
        /// <returns>認証用のKbtterRequest</returns>
        public KbtterRequest GetRequest()
        {
            var reqt = Service.GetRequestToken();
            return new KbtterRequest(Service.GetAuthorizationUri(reqt), reqt);
        }

        /// <summary>
        /// AccessTokenを取得するための認証Uriを生成します。
        /// </summary>
        /// <returns>認証用のKbtterRequest</returns>
        public Task<KbtterRequest> GetRequestAsync()
        {
            return Task<KbtterRequest>.Run(() =>
            {
                var reqt = Service.GetRequestToken();
                return new KbtterRequest(Service.GetAuthorizationUri(reqt), reqt);
            });
        }



        /// <summary>
        /// 取得したKbtterRequestを使用して、
        /// AccessTokenを取得します。
        /// </summary>
        /// <remarks>KbtterRequest.PinCodeに対応する値が格納されている必要があります。</remarks>
        /// <param name="req">GetRequestなどで取得したKbtterRequest</param>
        /// <returns>新しいAccessToken</returns>
        public OAuthAccessToken GetAccessToken(KbtterRequest req)
        {
            return Service.GetAccessToken(req.Request, req.PinCode);
        }

        /// <summary>
        /// 取得したKbtterRequestを使用して、
        /// AccessTokenを取得します。
        /// </summary>
        /// <remarks>KbtterRequest.PinCodeに対応する値が格納されている必要があります。</remarks>
        /// <param name="req">GetRequestなどで取得したKbtterRequest</param>
        /// <returns>新しいAccessToken</returns>
        public Task<OAuthAccessToken> GetAccessTokenAsync(KbtterRequest req)
        {
            return Task<OAuthAccessToken>.Run(() =>
            {
                return Service.GetAccessToken(req.Request, req.PinCode);
            });
        }
        #endregion
    }

    /// <summary>
    /// Kbtterの認証要求関連のクラスです。
    /// </summary>
    public class KbtterRequest
    {
        /// <summary>
        /// 認証のUri
        /// </summary>
        public Uri RequestUri { get; private set; }

        internal OAuthRequestToken Request { get; set; }

        /// <summary>
        /// PINコード。
        /// KbtterCore.GetAccessTokenの引数に渡す前に、
        /// このメンバーに設定する必要があります。
        /// </summary>
        public string PinCode { get; set; }

        private KbtterRequest()
        {

        }

        internal KbtterRequest(Uri r, OAuthRequestToken rt)
        {
            RequestUri = r;
            Request = rt;
            PinCode = "";
        }

        /// <summary>
        /// Process.Startを用いて、RequestUriを表示します。
        /// </summary>
        public void ShowWithBrowser()
        {
            Process.Start(RequestUri.ToString());
        }
    }
}
