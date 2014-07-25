using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json;
using M2E.Models.DataResponse;

namespace M2E.Service.UploadImages
{
    public class imgurService
    {
        public string CreateImgurAlbum()
        {
            byte[] response;
            using (var w = new WebClient())
            {
                var values = new NameValueCollection
                {
                {"ids[]", "5FBgDJI"},
                {"cover", "5FBgDJI"},
                {"layout", "grid"},
                {"title", "Title"},
                {"description", "This is the caption of the image"}
            };

                w.Headers.Add("Authorization", "Client-ID dac37a6b08b4974");
                response = w.UploadValues("https://api.imgur.com/3/album/{0}", values);                
                
            }
            System.Text.Encoding enc = System.Text.Encoding.ASCII;
            string myString = enc.GetString(response);
            return ("uploaded : " + myString);
        }

        public string GetImgurAlbumDetails(string albumId)
        {

            string myString=string.Empty;
            using (var w = new WebClient())
            {
                var values = new NameValueCollection
                {
                {"id", albumId}
                
            };
                w.Headers.Add("Authorization", "Client-ID dac37a6b08b4974");
                byte[] response = w.UploadValues("https://api.imgur.com/3/album/{0}", values);
                System.Text.Encoding enc = System.Text.Encoding.ASCII;
                myString = enc.GetString(response);                
            }
            return ("uploaded : " + myString);
        }

        public imgurUploadImageResponse UploadMultipleImagesToImgur(IEnumerable<HttpPostedFileBase> files, string albumid)
        {           
            System.Text.Encoding enc = System.Text.Encoding.ASCII;
            imgurUploadImageResponse imgurImageResponseData = new imgurUploadImageResponse();
            foreach (HttpPostedFileBase file in files)
            {                
                using (var w = new WebClient())
                {
                    byte[] binaryData;
                    binaryData = new Byte[file.InputStream.Length];
                    long bytesRead = file.InputStream.Read(binaryData, 0, (int)file.InputStream.Length);
                    file.InputStream.Close();
                    string base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);

                    var values = new NameValueCollection
                    {
                        {"image",  base64String},
                        {"album", albumid}
                    };

                    w.Headers.Add("Authorization", "Client-ID dac37a6b08b4974");
                    byte[] response = w.UploadValues("https://api.imgur.com/3/upload", values);
                    
                    imgurImageResponseData = JsonConvert.DeserializeObject<imgurUploadImageResponse>(enc.GetString(response));                    
                }
            }
            return imgurImageResponseData;
        }
    }
}