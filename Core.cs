using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.IO;

namespace Kbtter
{
    /// <summary>
    /// Kbtterのメイン機能を提供します。
    /// TweetSharpをベースとしているので、そこら辺勘弁願いたい
    /// </summary>
    public partial class KbtterCore
    {
        #region キー関連プロパティ
        /// <summary>
        /// Consumer Key
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Consumer Secret
        /// </summary>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// 取得済みのUser Token
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// 取得済みのUser Token Secret
        /// </summary>
        public string UserTokenSecret { get; set; }
        #endregion

        #region TweetSharp関連プロパティ
        /// <summary>
        /// 保持しているTwitterServiceインスタンス
        /// </summary>
        internal TwitterService Service { get; set; }

        /// <summary>
        /// 認証に使用したOAuthAccessToken
        /// </summary>
        public OAuthAccessToken AccessToken { get; protected set; }

        /// <summary>
        /// 認証しているTwitterUser
        /// </summary>
        public TwitterUser User { get; protected set; }

        /// <summary>
        /// 現在のレート制限
        /// </summary>
        public TwitterRateLimitStatus RateLimit
        {
            get
            {
                return Service.Response.RateLimitStatus;
            }
        }

        #endregion

        #region (コン|デ)ストラクタ
        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="ck">Consumer Key</param>
        /// <param name="cs">Consumer Secret</param>
        public KbtterCore(string ck, string cs)
        {
            ConsumerKey = ck;
            ConsumerSecret = cs;
            Service = new TwitterService(ck, cs);
        }

        /// <summary>
        /// デストラクタに説明いるのかよ
        /// </summary>
        ~KbtterCore()
        {
            Service.CancelStreaming();
        }
        #endregion

    }

    internal static class TEx
    {
        public static IObservable<TwitterStreamArtifact> StreamUser(this TwitterService s)
        {
            return Observable.Create<TwitterStreamArtifact>((obs) =>
            {
                s.IncludeEntities = true;
                s.StreamUser((a, r) => obs.OnNext(a));
                return s.CancelStreaming;
            });
        }
    }
}
