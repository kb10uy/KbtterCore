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
    public partial class KbtterCore
    {
        #region ユーザー関係REST API

        /// <summary>
        /// ユーザー情報を取得します。
        /// </summary>
        /// <param name="sn">ScreenName</param>
        /// <returns>取得したTwitterUser</returns>
        public TwitterUser GetUser(string sn)
        {
            return Service.GetUserProfileFor(new GetUserProfileForOptions { ScreenName = sn, IncludeEntities = true });
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="sn">-</param>
        /// <returns>-</returns>
        public Task<TwitterUser> GetUserTaskAsync(string sn)
        {
            return Task.Run(() => Service.GetUserProfileFor(new GetUserProfileForOptions { ScreenName = sn, IncludeEntities = true }));
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="sn">-</param>
        /// <param name="act">-</param>
        public void GetUserAsync(string sn, Action<TwitterUser, TwitterResponse> act)
        {
            Service.GetUserProfileFor(new GetUserProfileForOptions { ScreenName = sn, IncludeEntities = true }, act);
        }

        #endregion
    }
}
