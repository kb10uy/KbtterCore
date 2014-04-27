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
        /// <summary>
        /// ホームタイムラインの最新つぶやきを取得します。
        /// </summary>
        /// <param name="count">件数</param>
        /// <returns>取得したつぶやき</returns>
        public IEnumerable<TwitterStatus> GetHomeTimelineStatuses(int count)
        {
            return Service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions { Count = count, IncludeEntities = true });
        }

        /// <summary>
        /// ホームタイムラインの最新つぶやきを取得します。
        /// </summary>
        /// <param name="count">件数</param>
        /// <returns>取得したつぶやき</returns>
        public Task<IEnumerable<TwitterStatus>> GetHomeTimelineStatusesTaskAsync(int count)
        {
            return Task<IEnumerable<TwitterStatus>>.Run(() =>
            {
                return Service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions { Count = count, IncludeEntities = true });
            });
        }

        /// <summary>
        /// ホームタイムラインの最新つぶやきを取得します。非同期で実行されます。
        /// </summary>
        /// <param name="count">件数</param>
        /// <param name="act">動作</param>
        public void GetHomeTimelineStatusesAsync(int count,Action<IEnumerable<TwitterStatus>,TwitterResponse> act)
        {
            Service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions { Count = count, IncludeEntities = true }, act);
        }

    }
}
