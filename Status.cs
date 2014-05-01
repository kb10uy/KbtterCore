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
        #region REST API系メソッド
        /// <summary>
        /// つぶやきます。
        /// </summary>
        /// <param name="text">内容</param>
        /// <returns>投稿されたつぶやき</returns>
        public TwitterStatus Tweet(string text)
        {
            return Service.SendTweet(new SendTweetOptions { Status = text });
        }

        /// <summary>
        /// つぶやきます。
        /// </summary>
        /// <param name="text">内容</param>
        /// <returns>投稿されたつぶやき</returns>
        public Task<TwitterStatus> TweetTaskAsync(string text)
        {
            return Task<TwitterStatus>.Run(() =>
            {
                return Service.SendTweet(new SendTweetOptions { Status = text });
            });
        }

        /// <summary>
        /// つぶやきます。非同期で実行されます。
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="act">つぶやいた後に実行されるAction</param>
        public void TweetAsync(string text, Action<TwitterStatus, TwitterResponse> act)
        {
            Service.SendTweet(new SendTweetOptions { Status = text }, act);
        }



        /// <summary>
        /// 画像付きでつぶやきます。
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="imagepath">画像のパス(複数可能)</param>
        /// <returns>投稿されたつぶやき</returns>
        public TwitterStatus TweetWithMedia(string text, IEnumerable<string> imagepath)
        {
            var d = new Dictionary<string, Stream>();
            foreach (var i in imagepath)
            {
                var fs = new FileStream(i, FileMode.Open);
                d.Add("image", fs);
            }
            var st = Service.SendTweetWithMedia(new SendTweetWithMediaOptions { Status = text, Images = d });
            foreach (var kv in d) kv.Value.Dispose();   //念のため解放
            return st;
        }

        /// <summary>
        /// 画像付きでつぶやきます。
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="imagepath">画像のパス(複数可能)</param>
        /// <returns>投稿されたつぶやき</returns>
        public Task<TwitterStatus> TweetWithMediaTaskAsync(string text, IEnumerable<string> imagepath)
        {
            return Task<TwitterStatus>.Run(() =>
            {
                var d = new Dictionary<string, Stream>();
                foreach (var i in imagepath)
                {
                    var fs = new FileStream(i, FileMode.Open);
                    d.Add("image", fs);
                }
                var st = Service.SendTweetWithMedia(new SendTweetWithMediaOptions { Status = text, Images = d });
                foreach (var kv in d) kv.Value.Dispose();   //念のため解放
                return st;
            });
        }



        /// <summary>
        /// 返信します。
        /// </summary>
        /// <param name="id">返信先ツイートID</param>
        /// <param name="text">内容</param>
        /// <returns>投稿されたつぶやき</returns>
        public TwitterStatus Reply(long id, string text)
        {
            return Service.SendTweet(new SendTweetOptions { Status = text, InReplyToStatusId = id });
        }

        /// <summary>
        /// 返信します。
        /// </summary>
        /// <param name="id">返信先ツイートID</param>
        /// <param name="text">内容</param>
        /// <returns>投稿されたつぶやき</returns>
        public Task<TwitterStatus> ReplyAsync(long id, string text)
        {
            return Task<TwitterStatus>.Run(() =>
            {
                return Service.SendTweet(new SendTweetOptions { Status = text, InReplyToStatusId = id });
            });
        }

        /// <summary>
        /// 返信します。非同期で実行されます。
        /// </summary>
        /// <param name="id">返信先ツイートID</param>
        /// <param name="text">内容</param>
        /// <param name="act">つぶやいた後に実行されるAction</param>
        public void ReplyAsync(long id, string text, Action<TwitterStatus, TwitterResponse> act)
        {
            Service.SendTweet(new SendTweetOptions { Status = text, InReplyToStatusId = id }, act);
        }



        /// <summary>
        /// 画像付きで返信します。
        /// </summary>
        /// <param name="id">返信先ツイートID</param>
        /// <param name="text">内容</param>
        /// <param name="imagepath">画像のパス(複数可能)</param>
        /// <returns>投稿されたつぶやき</returns>
        public TwitterStatus ReplyWithMedia(long id, string text, IEnumerable<string> imagepath)
        {
            var d = new Dictionary<string, Stream>();
            foreach (var i in imagepath)
            {
                var fs = new FileStream(i, FileMode.Open);
                d.Add("image", fs);
            }
            var st = Service.SendTweetWithMedia(new SendTweetWithMediaOptions { Status = text, Images = d, InReplyToStatusId = id });
            foreach (var kv in d) kv.Value.Dispose();   //念のため解放
            return st;
        }

        /// <summary>
        /// 画像付きで返信します。
        /// </summary>
        /// <param name="id">返信先ツイートID</param>
        /// <param name="text">内容</param>
        /// <param name="imagepath">画像のパス(複数可能)</param>
        /// <returns>投稿されたつぶやき</returns>
        public Task<TwitterStatus> ReplyWithMediaTaskAsync(long id, string text, IEnumerable<string> imagepath)
        {
            return Task<TwitterStatus>.Run(() =>
            {
                var d = new Dictionary<string, Stream>();
                foreach (var i in imagepath)
                {
                    var fs = new FileStream(i, FileMode.Open);
                    d.Add("image", fs);
                }
                var st = Service.SendTweetWithMedia(new SendTweetWithMediaOptions { Status = text, Images = d, InReplyToStatusId = id });
                foreach (var kv in d) kv.Value.Dispose();   //念のため解放
                return st;
            });
        }



        /// <summary>
        /// ふぁぼります。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>ふぁぼったやつ</returns>
        public TwitterStatus Favorite(long id)
        {
            return Service.FavoriteTweet(new FavoriteTweetOptions { Id = id });
        }

        /// <summary>
        /// ふぁぼります。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>ふぁぼったやつ</returns>
        public Task<TwitterStatus> FavoriteTaskAsync(long id)
        {
            return Task<TwitterStatus>.Run(() => Service.FavoriteTweet(new FavoriteTweetOptions { Id = id }));
        }

        /// <summary>
        /// ふぁぼります。非同期で実行されます。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="act">ふぁぼった後の動作</param>
        public void FavoriteAsync(long id, Action<TwitterStatus, TwitterResponse> act)
        {
            Service.FavoriteTweet(new FavoriteTweetOptions { Id = id }, act);
        }




        /// <summary>
        /// あんふぁぼります。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>あんふぁぼったやつ</returns>
        public TwitterStatus Unfavorite(long id)
        {
            return Service.FavoriteTweet(new FavoriteTweetOptions { Id = id });
        }

        /// <summary>
        /// あんふぁぼります。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>あんふぁぼったやつ</returns>
        public Task<TwitterStatus> UnfavoriteTaskAsync(long id)
        {
            return Task<TwitterStatus>.Run(() => Service.UnfavoriteTweet(new UnfavoriteTweetOptions { Id = id }));
        }

        /// <summary>
        /// あんふぁぼります。非同期で実行されます。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="act">あんふぁぼった後の動作</param>
        public void UnfavoriteAsync(long id, Action<TwitterStatus, TwitterResponse> act)
        {
            Service.UnfavoriteTweet(new UnfavoriteTweetOptions { Id = id }, act);
        }




        /// <summary>
        /// リツイートします。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>RTしたやつ</returns>
        public TwitterStatus Retweet(long id)
        {
            return Service.Retweet(new RetweetOptions { Id = id });
        }

        /// <summary>
        /// リツイートします。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>RTしたやつ</returns>
        public Task<TwitterStatus> RetweetTaskAsync(long id)
        {
            return Task<TwitterStatus>.Run(() => Service.Retweet(new RetweetOptions { Id = id }));
        }

        /// <summary>
        /// リツイートします。非同期で実行されます。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="act">リツイートした後の動作</param>
        public void RetweetAsync(long id, Action<TwitterStatus, TwitterResponse> act)
        {
            Service.Retweet(new RetweetOptions { Id = id }, act);
        }



        /// <summary>
        /// ツイートを削除します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>削除したやつ</returns>
        public TwitterStatus DeleteTweet(long id)
        {
            return Service.DeleteTweet(new DeleteTweetOptions { Id = id });
        }

        /// <summary>
        /// ツイートを削除します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>削除したやつ</returns>
        public TwitterStatus DeleteTweetTaskAsync(long id)
        {
            return Service.DeleteTweet(new DeleteTweetOptions { Id = id });
        }

        /// <summary>
        /// ツイートを削除します。非同期で実行されます。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="act">削除した後の動作</param>
        public void DeleteTweetAsync(long id, Action<TwitterStatus, TwitterResponse> act)
        {
            Service.DeleteTweet(new DeleteTweetOptions { Id = id }, act);
        }
        #endregion
    }
}
