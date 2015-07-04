using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using CafeF.Redis.BO;
using ServiceStack.Redis;

namespace CafeF.Redis.Page
{
    /// <summary>
    /// Update image to new storage.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class StorageImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            var land = (context.Request["land"] ?? "").ToLower() == "true";
            var image = context.Request["image"] ?? "";
            var width = context.Request["width"] ?? "0";
            var height = context.Request["height"] ?? "0";
            var redis = new RedisClient(ConfigurationManager.AppSettings["ServerRedisMaster"] ?? "", int.Parse(ConfigurationManager.AppSettings["PortRedisMaster"] ?? "0"));
            
            var key = string.Format("storage:imagehandler:{0}", image);
            var done = (redis.ContainsKey(key) && !string.IsNullOrEmpty(redis.Get<string>(key) ?? ""));
            var noimage = ConfigurationManager.AppSettings["StorageNoImage"] ?? "Common/CEO/noimage.jpg";
            //var file = ConfigurationManager.AppSettings["StorageNoImage"] ?? "Common/CEO/noimage.jpg";
            var imgPath = ConfigurationManager.AppSettings["imgPath"] ?? "http://images1.cafef.vn/";
            var storageServer = ConfigurationManager.AppSettings["StorageServer"] ?? "http://testcafef.vcmedia.vn/";
            string imageSrc = storageServer + noimage;
            if (!done)
            {

                if (!string.IsNullOrEmpty(image))
                {
                    imgPath += (land ? "batdongsan/" : "");
                    if (image.StartsWith(imgPath) && StorageUtils.Utils.checkImageExtension(image))
                    {
                        if (StorageUtils.Utils.UploadSiteImage(image, imgPath, (land ? "Common/BDS/" : "")) == "Storage : OK")
                        {
                            //file = image.Replace(imgPath, "");

                        }
                    }
                }

                int iw, ih;
                if (!int.TryParse(width, out iw)) iw = 0;
                if (!int.TryParse(height, out ih)) ih = 0;

                imageSrc = image.Contains(imgPath) ? image : (imgPath + image); //GetStorageImage(file, iw, ih, land);

                if (!CheckFileExist(imageSrc))
                {
                    imageSrc = storageServer + noimage;
                }
                if (redis.ContainsKey(key))
                    redis.Set(key, imageSrc, new TimeSpan(0, 5, 0));
                else
                    redis.Add(key, imageSrc, new TimeSpan(0, 5, 0));
            }
            else
            {
                imageSrc = redis.Get<string>(key) ?? (storageServer + noimage);
            }

            context.Response.ContentType = StorageUtils.Utils.MimeType(imageSrc);
            context.Response.Redirect(imageSrc);
            //context.Response.BinaryWrite(StorageUtils.Utils.GetFileBinary("", imageSrc));
        }
        private bool CheckFileExist(string src)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(src);
                var res = (HttpWebResponse)req.GetResponse();
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    res.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        private string GetStorageImage(string imagePath, int imageWidth, int imageHeight, bool land)
        {
            var storageServer = ConfigurationManager.AppSettings["StorageServer"] ?? "http://testcafef.vcmedia.vn/";
            var folder = (land ? "Common/BDS/" : "");
            if (imageWidth == 0 && imageHeight == 0)
            {
                return storageServer + folder + imagePath;
            }
            else if (imageWidth > 0 && imageHeight > 0)
            {
                return storageServer + "zoom/" + imageWidth + "_" + imageHeight + "/" + folder + imagePath;
            }
            else if (imageWidth == 0)
            {
                return storageServer + "thumb_h/" + imageHeight + "/" + folder + imagePath;
            }
            else if (imageHeight == 0)
            {
                return storageServer + "thumb_w/" + imageWidth + "/" + folder + imagePath;
            }
            return storageServer + folder + imagePath;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
