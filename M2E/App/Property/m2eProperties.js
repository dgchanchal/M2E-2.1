var logoImage = "../../Template/AdminLTE-master/img/m2eV3.png";
var googleAnalyticsAppID = "UA-51967607-1";
var userSession = {
    username:"sumitchourasia91@gmail.com",
    listOfImgurImages:[],
    keepMeSignedIn:false,
    wysiHtml5UploadedInstructionsImageUrlLink: [],
    imgurImageTemplateModeratingPhotos :[]
};
var ServerContextPah = "";

var appLocation = {
    'common': '../../App/CommonInit',
    'preLogin': '../../App/Pages/PreLoginInit',
    'postLogin': '../../App/Pages/PostLoginInit',
    'userPostLogin': '../../App/Pages/UserPostLoginInit'
};

var mobileDevice = detectmob();
var ipadDevice = detectipad();
var isAndroidDevice = detectAndroid();
function detectmob() {
    return (navigator.userAgent.match(/Android/i) || navigator.userAgent.match(/iPhone/i) || navigator.userAgent.match(/iPod/i) || navigator.userAgent.match(/BlackBerry/i) || navigator.userAgent.match(/Windows Phone/i));
}
function detectipad() {
    return (navigator.userAgent.match(/iPad/i) != null);
}
function detectAndroid() {
    return (navigator.userAgent.match(/Android/i) != null);
}

function replaceImageWithFancyBoxImage(text, smallImage, largeImage) {
    //console.log(text);
    //console.log("<img src=\"" + smallImage + "\" title=\"Image: " + smallImage + "\">");

    text = text.replace("<img title=\"Image: " + smallImage + "\" src=\"" + smallImage + "\">", "<a class='fancybox' href='" + largeImage + "' data-fancybox-group='gallery' title='Personalized Title'><img class='MaxUploadedSmallSized' src='" + smallImage + "' alt=''></a>");
    text = text.replace("<img src=\"" + smallImage + "\" title=\"Image: " + smallImage + "\">", "<a class='fancybox' href='" + largeImage + "' data-fancybox-group='gallery' title='Personalized Title'><img class='MaxUploadedSmallSized' src='" + smallImage + "' alt=''></a>");
    text = text.replace("<img src=\"" + smallImage + "\">", "<a class='fancybox' href='" + largeImage + "' data-fancybox-group='gallery' title='Personalized Title'><img class='MaxUploadedSmallSized' src='" + smallImage + "' alt=''></a>");
    return text;
}