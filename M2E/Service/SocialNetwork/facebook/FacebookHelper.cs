using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Service.SocialNetwork.facebook
{
    public class FacebookHelper
    {
        public static string Get_Like_Button_Iframe_Standard(string URL, string iFrame_Width)
        {

            return "<iframe src='//www.facebook.com/plugins/like.php?href=" + URL + "&amp;send=false&amp;layout=standard&amp;width=" + iFrame_Width + "&amp;show_faces=true&amp;action=like&amp;colorscheme=light&amp;font&amp;height=80' scrolling='no' frameborder='0' style='border:none; overflow:hidden; width:" + iFrame_Width + "px; height:80px;' allowTransparency='true'></iframe>";

        }



        public static string Get_Like_Button_Iframe_Standard(string iFrame_Width)
        {

            return Get_Like_Button_Iframe_Standard(HttpContext.Current.Request.Url.ToString().ToLower(), iFrame_Width);

        }





        public static string Get_Like_Button_Iframe_Count_Button(string URL, string iFrame_Width)
        {

            return "<iframe src='//www.facebook.com/plugins/like.php?href=" + URL + "&amp;send=false&amp;layout=button_count&amp;width=" + iFrame_Width + "&amp;show_faces=true&amp;action=like&amp;colorscheme=light&amp;font&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:hidden; width:" + iFrame_Width + "px; height:21px;' allowTransparency='true'></iframe>";

        }



        public static string Get_Like_Button_Iframe_Count_Button(string iFrame_Width)
        {

            return Get_Like_Button_Iframe_Count_Button(HttpContext.Current.Request.Url.ToString().ToLower(), iFrame_Width);

        }

        //<iframe src="//www.facebook.com/plugins/likebox.php?href=https%3A%2F%2Fwww.facebook.com%2Fbeststatuslines&amp;width&amp;height=290&amp;colorscheme=light&amp;show_faces=true&amp;header=true&amp;stream=false&amp;show_border=true&amp;appId=689268497785729" scrolling="no" frameborder="0" style="border:none; overflow:hidden; height:290px;" allowTransparency="true"></iframe>
    }
}