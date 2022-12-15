using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TorHelper.Util
{
    public static class Util
    {
        /// <summary>
        /// 抓取网页,支持gzip
        /// </summary>
        /// <param name="sUrl"></param>
        /// <returns></returns>
        public static string GetWebContent(string sUrl)
        {
            string strResult = "";
            string charset = "UTF-8";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sUrl);
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
                //声明一个HttpWebRequest请求
                request.Timeout = 60000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                request.Headers.Set("Accept-Encoding", "gzip");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.CharacterSet != "")
                    charset = response.CharacterSet;
                if (response.ToString() == "")
                    return "";

                if (response.ContentEncoding.ToLower().Contains("gzip") | (response.Headers["Content-Encoding"] != null && response.Headers["Content-Encoding"].Contains("gzip")))
                {
                    using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.GetEncoding(charset)))
                        {
                            strResult = reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    Stream myStream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(myStream, System.Text.Encoding.GetEncoding(charset)))
                    {
                        strResult = reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                strResult = "";
            }
            return strResult;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="filename">本地文件，任意目录均可</param>
        /// <returns></returns>
        public static bool DownloadFile(string url, string filename)
        {
            try
            {
                FileInfo fi = new FileInfo(filename);
                var di = fi.Directory;
                if (!di.Exists)
                    di.Create();

                HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(url);
                Myrq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
                Myrq.Timeout = 10 * 1000;
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                Stream st = myrp.GetResponseStream();
                Stream so = new FileStream(filename, FileMode.Create);
                byte[] by = new byte[10240];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
