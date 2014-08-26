var logoImage = "../../Template/AdminLTE-master/img/m2eV3.png";
var googleAnalyticsAppID = "UA-51967607-1";
var userSession = {
    username: "sumitchourasia91@gmail.com",
    guid:"",
    listOfImgurImages:[],
    keepMeSignedIn:false,
    wysiHtml5UploadedInstructionsImageUrlLink: [],
    imgurImageTemplateModeratingPhotos :[],
    imgurImageTranscriptionTemplate: [] 
};
var TemplateInfoModel = {
               
        type_survey : "survey",
        subType_productSurvey : "productSurvey",
        subType_surveyLink : "surveyLink",
        
        type_dataEntry : "dataEntry",
        subType_Transcription : "Transcription",        
        subType_dataCollection : "dataCollection",
        subType_taggingImage : "taggingImage",
        subType_transcribeAV : "transcribeAV",        

        type_contentWritting : "contentWritting",        
        subType_articleWritting : "articleWritting",

        type_moderation : "moderation",        
        subType_imageModeration : "moderatingPhotos"                      
};

var clientConstants = {
    name: "Crowd Automation Requester",
    name_abb: "Requeater"
};
var companyConstants = {
    name: "cautom",
    fullName: "Crowd Automation",
    supportEmail: "support@cautom.com"
};
var userConstants = {
    name: "Crowd Automation Accepter",
    name_abb: "Accepter",
    task: "Crowd Individual Task",
    task_abb: "CIT",
    batch: "batch",
    Batch: "Batch",
    Reputation: "Reputation",
    reputation: "reputation",
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

function getParameterByName(name) {
    var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);
    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
}