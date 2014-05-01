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
        #region プロパティ
        /// <summary>
        /// Streaming APIで使用するIObservable
        /// </summary>
        internal IObservable<TwitterStreamArtifact> Stream { get; set; }
        #endregion

        #region イベント
        /// <summary>
        /// Streaming有効時にツイートされた時に発生するイベント。
        /// </summary>
        public event Action<TwitterUserStreamStatus> StreamingStatus;

        /// <summary>
        /// Streaming有効時にイベントが発生した時に発生するイベント。
        /// </summary>
        public event Action<TwitterUserStreamEvent> StreamingEvent;

        /// <summary>
        /// Streaming有効時にダイレクトメッセージに更新があった時に発生するイベント。
        /// </summary>
        public event Action<TwitterUserStreamDirectMessage> StreamingDirectMessage;

        /// <summary>
        /// Streaming有効時にツイートが削除された時に発生するイベント。
        /// </summary>
        public event Action<TwitterUserStreamDeleteStatus> StreamingDeleteStatus;

        /// <summary>
        /// Streaming有効時にダイレクトメッセージが削除された時に発生するイベント。
        /// </summary>
        public event Action<TwitterUserStreamDeleteDirectMessage> StreamingDeleteDirectMessage;

        /// <summary>
        /// Streaming有効時にユーザー関係でなにかあった時に発生するイベント。
        /// </summary>
        public event Action<TwitterUserStreamFriends> StreamingFriends;

        /// <summary>
        /// Streaming有効時にユーザー関係のイベントが発生した時に発生するイベント。
        /// </summary>
        public event Action<TwitterUserStreamUserEvent> StreamingUserEvent;
        #endregion

        #region メソッド
        /// <summary>
        /// Streaming APIを初期化して、ストリーミングを開始します。
        /// </summary>
        public void StartStreaming()
        {
            Service.IncludeEntities = true;
            Service.IncludeRetweets = true;
            Stream = Service.StreamUser();
            //各種
            Stream.OfType<TwitterUserStreamStatus>().Subscribe((p) =>
            {
                if (StreamingStatus != null) StreamingStatus(p);
            });
            Stream.OfType<TwitterUserStreamEvent>().Subscribe((p) =>
            {
                if (StreamingEvent != null) StreamingEvent(p);
            });
            Stream.OfType<TwitterUserStreamUserEvent>().Subscribe((p) =>
            {
                if (StreamingUserEvent != null) StreamingUserEvent(p);
            });
            Stream.OfType<TwitterUserStreamDirectMessage>().Subscribe((p) =>
            {
                if (StreamingDirectMessage != null) StreamingDirectMessage(p);
            });
            Stream.OfType<TwitterUserStreamFriends>().Subscribe((p) =>
            {
                if (StreamingFriends != null) StreamingFriends(p);
            });
            Stream.OfType<TwitterUserStreamDeleteStatus>().Subscribe((p) =>
            {
                if (StreamingDeleteStatus != null) StreamingDeleteStatus(p);
            });
            Stream.OfType<TwitterUserStreamDeleteDirectMessage>().Subscribe((p) =>
            {
                if (StreamingDeleteDirectMessage != null) StreamingDeleteDirectMessage(p);
            });
        }

        /// <summary>
        /// ストリーミングを終了します。
        /// </summary>
        public void StopStreaming()
        {
            Service.CancelStreaming();
            Stream = null;
        }
        #endregion
    }
}
