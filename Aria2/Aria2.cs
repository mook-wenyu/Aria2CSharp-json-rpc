using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Aria2
{
    public static class Aria2
    {
        public static CookieContainer aria2CookieContainer = new CookieContainer();

        public static string jsonrpcInterfaceHttp = "http://token:simple_download_manager@localhost:6800/jsonrpc";
        public static string jsonrpcToken = "simple_download_manager";

        /// <summary>
        /// 线程数
        /// </summary>
        public static string split = "256";
        /// <summary>
        /// 连接数
        /// </summary>
        public static string max_connection_per_server = "8";


        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="method"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public static string RequestData(string method, string @params)
        {
            string data = "{\"jsonrpc\":\"2.0\",\"id\":\"qwer\",\"method\":\"aria2." + method + "\",\"params\": [" + @params + "]}";
            return data;
        }

        /// <summary>
        /// 添加新的下载
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string AddUri(string uri, string baiduCookie = "")
        {
            var @params = "\"token:simple_download_manager\",";
            @params += "[\"" + uri + "\"],";
            @params += "{";
            @params += "\"split\":\"" + split + "\",\"max-connection-per-server\":\"" + max_connection_per_server + "\",\"max-connection-per-server\":\"" + max_connection_per_server + "\"";
            if (!string.IsNullOrWhiteSpace(baiduCookie))
            {
                @params += ",\"header\":\"Cookie:" + baiduCookie + "\"";
            }
            @params += "}";

            var data = RequestData("addUri", @params);

            string str = Aria2Post(jsonrpcInterfaceHttp, data);
            return str;
        }


        /// <summary>
        /// 上传“.torrent”文件添加BitTorrent下载
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /*public static async Task<string> AddTorrent(string path)
        {
            string str = "";
            string fsbase64 = "";
            byte[] fs = File.ReadAllBytes(path);
            fsbase64 = Convert.ToBase64String(fs);  //转为Base64编码

            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.addTorrent";
            List<string> paramslist = new List<string>();
            //添加“.torrent”文件本地地址
            paramslist.Add(fsbase64);
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 删除已经停止的任务，强制删除请使用ForceRemove方法
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> Remove(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.remove";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 强制删除
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> ForceRemove(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.forceRemove";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 暂停下载,强制暂停请使用ForcePause方法
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> Pause(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.pause";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 暂停全部任务
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> PauseAll()
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.pauseAll";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add("");
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 强制暂停下载
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> ForcePause(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.forcePause";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 强制暂停全部下载
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> ForcePauseAll()
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.forcePauseAll";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add("");
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 把正在下载的任务状态改为等待下载
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> PauseToWaiting(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.unpause";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 把全部在下载的任务状态改为等待下载
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> PauseToWaitingAll()
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.unpauseAll";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add("");
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 返回下载进度
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> TellStatus(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.tellStatus";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            paramslist.Add("completedLength\", \"totalLength\",\"downloadSpeed");

            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 返回全局统计信息，例如整体下载和上载速度
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> GetGlobalStat(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.getGlobalStat";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 返回由gid（字符串）表示的下载中使用的URI 。响应是一个结构数组
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> GetUris(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.getUris";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 返回由gid（字符串）表示的下载文件列表
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> GetFiles(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.jsonrpc = "2.0";
            json.id = "qwer";
            json.method = "aria2.getFiles";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.@params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }*/










        /// <summary>
        /// Aria2Post
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public static string Aria2Post(string Url, string postDataStr)
        {
            string retString = string.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.CookieContainer = aria2CookieContainer;
                request.Timeout = 100000;//设置超时时间
                //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                Stream requestStream = request.GetRequestStream();
                StreamWriter streamWriter = new StreamWriter(requestStream);
                streamWriter.Write(postDataStr);
                streamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                retString = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(WebException))//捕获400错误
                {
                    var response = ((WebException)ex).Response;
                    Stream responseStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream);
                    retString = streamReader.ReadToEnd();
                    streamReader.Close();
                    responseStream.Close();
                }
                else
                {
                    retString = ex.ToString();
                }

                retString = ex.ToString();
            }

            return retString;
        }
    }
}
