var logoImage = "../../Template/AdminLTE-master/img/m2eV3.png";
var googleAnalyticsAppID = "UA-51967607-1";
var facebookAppId = "689268497785729";
var userSession = {
    username: "sumitchourasia91@gmail.com",
    guid:"",
    listOfImgurImages:[],
    keepMeSignedIn:false,
    wysiHtml5UploadedInstructionsImageUrlLink: [],
    imgurImageTemplateModeratingPhotos :[],
    imgurImageTranscriptionTemplate: [] 
};

var popWindow = {
    width:800,
    height:480
};
var userType  = {
    requester: "requester",
    accepter: "accepter"
}

var M2EConstants = {
    userType_user: "user",
    userType_client:"client"
}
var TemplateInfoModel = {
               
        type_survey : "survey",
        subType_productSurvey : "productSurvey",
        subType_surveyLink : "surveyLink",
        subType_userFeedbackSurvey : "userFeedbackSurvey",
        subType_pools : "pools",
        
        type_dataEntry : "dataEntry",
        subType_Transcription : "Transcription",        
        subType_dataCollection : "dataCollection",
        subType_taggingImage : "taggingImage",
        subType_transcribeAV: "transcribeAV",
        subType_searchTheWeb: "searchTheWeb",
        subType_doExcelWork: "doExcelWork",

        type_contentWritting : "contentWriting",        
        subType_articleWritting : "articleWriting",
        subType_blogWriting : "blogWriting",
        subType_copyTyping : "copyTyping",
        subType_powerpoint : "powerpoint",
        subType_shortStories : "shortStories",
        subType_travelWriting : "travelWriting",
        subType_reviews : "reviews",
        subType_productDescriptions : "productDescriptions",

        type_moderation : "moderation",        
        subType_imageModeration : "moderatingPhotos",
        
        type_Ads : "Ads",        
        subType_facebookLike : "facebookLike"                      
};

var clientConstants = {
    name: window.madetoearn.i18n.beforeLoginMasterPageFooterRequester,
    name_abb: "Requester"
};
var companyConstants = {
    name: window.madetoearn.i18n.cautom,
    fullName: window.madetoearn.i18n.crowdAutomation,
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


//faq questions 

var FAQOverviewList = [
            {
                className: "div_heading1",
                id: "whatsCautom",
                question: "What is " + companyConstants.name + "?",
                question_hi: companyConstants.name + "क्या है ?",
                answer: "Cautom stands for <b><i>crowd automation</i></b>. It is an online portal that translates human intelligence into business domain. " +
                "<p><b>" + clientConstants.name + "</b> use the intelligence of a crowd to get his work done in minutes at a reasonable cost. The <b>" + userConstants.name + "</b> get paid for their time." +
                    "</p>"
            },
            {
                className: "div_heading2",
                id: "CITs", question: "What is a " + userConstants.task_abb + "?",
                answer: "Each question " + clientConstants.name + " asks through our application is a <b>" + userConstants.task + "</b>, or <b>" + userConstants.task_abb + "</b>. " +
                "A " + userConstants.task + " contains all of the information an " + userConstants.name_abb + " needs to answer the question, including information about how the question is shown to the " + userConstants.name_abb + " and what " +
                    "kinds of answers would be considered valid. " + "<p class='div_c'>Each " + userConstants.task_abb + " has a reward, an amount of money " + clientConstants.name_abb + " pay to the " + userConstants.name_abb + " that successfully completes " +
                    "the " + userConstants.task_abb + ". " + clientConstants.name_abb + " can request that more than one " + userConstants.name_abb + " to complete a " + userConstants.task_abb + " by specifying a quantity of Workforce " +
                    "required while creating " + userConstants.task_abb + ".</p>"
            },
            {
                className: "div_heading3",
                id: "Requester", question: "Who is a " + clientConstants.name + "?",
                answer: "A " + clientConstants.name + " is anyone (or company or organization) who uploads a task as to harness the crowd potential. <p class='div_c'>As a " + clientConstants.name + ", " +
                    "you use our Interface to submit your questions, retrieve answers, and perform other automated tasks. You can send a mail on " + companyConstants.supportEmail + " to automate your bulk requirement. We will integrate your requirement with our API." +
                    "<p>To " + userConstants.name + ", you are known as the creator of your " + userConstants.task_abb + "s, and as the creator and maintainer of your " + userConstants.Batch + " types.</p>"
            },
            {
                className: "div_heading4",
                id: "Accepter", question: "Who is a " + userConstants.name + "?",
                answer: "A " + userConstants.name + " is a person who answers questions for Crowd Automation. A " + userConstants.name + " uses the " + companyConstants.fullName + " website" +
                    " (<a class='undeline_anchor' href='http://cautom.com'>http://cautom.com</a>) to find questions, submit answers, and manage his or her account." + "<p class='div_c'>To " + clientConstants.name + "s, " +
                    "a " + userConstants.name + " is known as the submitter of a " + userConstants.task_abb + " assignment. You can see the " + userConstants.name + "'s " + userConstants.Batch + ". </p>" +
                    "<p>" + userConstants.Batch + " represent the " + userConstants.name + "'s reputation and abilities in a particular domain. A " + userConstants.name + "'s " + userConstants.Batch + "es " +
                    "are matched against a " + userConstants.task_abb + "'s " + userConstants.Batch + " requirements to allow or disallow the " + userConstants.name + " to accept the " + userConstants.task_abb + ". " +
                    "A " + userConstants.name + "'s " + userConstants.Batch + " cannot be accessed directly by other users.</p>"
            },
            {
                className: "div_heading5",
                id: "joinCautom",
                question: "How can I join " + companyConstants.name + "?",
                question_hi: "मैं " + companyConstants.name + " में कैसे शामिल हो सकता हूँ?",
                answer: "It's pretty simple. Go to www.cautom.com, now if you are a new " + clientConstants.name +
                    ", please visit <a class='undeline_anchor' href='#/signup/client'>Crowd Automation Requester</a> section, like millions of college students or users want to register please visit <a class='undeline_anchor' href='#/signup/user'>Crowd Automation Accepter</a> section." +
                        "<p>Now if you are already a " + clientConstants.name + " or " + userConstants.name_abb + " please visit <a class='undeline_anchor' href='#/login'>login</a> section.</p>"
            }
];

var FAQGeneralList = [
            { className: "div_heading6", id: "CostToJoinCautom", question: "Does it cost to join " + companyConstants.name + "?", answer: "No, it does not cost anything. We won't ever charge you for our services. So smile!" },
            { className: "div_heading7", id: "lockMyAccount", question: "Can I lock my account? ", answer: "Mostly you never will! Still if you ever want to lock an account click on the 'lock account' button in edit profile section. If you are unable to do this send us an email at " + companyConstants.supportEmail + "." },
            { className: "div_heading8", id: "TrackingSystem", question: "Do you have a tracking system?", answer: "We have a very sophisticated tracking system that monitors your work performance. Based on the rating our system gives you more jobs will be assigned." },
            { className: "div_heading9", id: "CustomerServiceNumber", question: "Do you have customer service number?", answer: "Sorry, we don't have a phone number for customer service. Simply because we don't want you to stay in a long queue and listen to some cranky music. Just fill the contact form with your question and send us. We will email you our response. If it's not solved in an email conversation. We will call you at our expense!" },
            { className: "div_heading10", id: "HowToContactUs", question: "How to conatct us?", answer: "Sorry, we don't have a phone number for customer service. Simply because we don't want you to stay in a long queue and listen to some cranky music. Just fill the contact form (<a class='undeline_anchor' href='#/contactus'>Contact form</a>) with your question and send us. We will email you our response. If it's not solved in an email conversation. We will call you at our expense!" }
];


var FAQRequesterGeneralPoliciesList = [
    {
        className: "div_heading1",
        id: "OffensiveContent", question: "Can I create a " + userConstants.task_abb + " that may contain explicit or offensive content?",
        answer: "<p>If your " + userConstants.task_abb + " could contain explicit or offensive content, for instance, nudity, make sure to do the following:</p>" +
        "<ol> <li>Include the following tag in your " + userConstants.task_abb + " title: '(WARNING: This " + userConstants.task_abb + " may contain adult content. " + userConstants.name_abb + " discretion is advised.)' </li>" +
        "<li> Require that " + userConstants.name_abb + " have the Adult Content batch in order to preview the " + userConstants.task_abb + " by checking the 'Adult Batch Required' box in the " + userConstants.task_abb + " template Design step on the Requester.</li></ol>"
    },
    {
        className: "div_heading2",
        id: "restrictions", question: "Are there restrictions over uses of " + companyConstants.fullName + "?",
        answer: "You may not use " + companyConstants.fullName + " for illegal or prohibited activities. Here are some examples those activities:" +
        "<ul><li>personal identifiable information</li>" +
        "<li>provoking any religious sentiments</li>" +
        "<li>interrupting or disrupting the operation of Crowd Automation, any website or internet service</li>" +
        "<li>direct marketing of any product or service</li>" +
        "<li>spamming related activities</li>" +
        "<li>unsolicited contacting of " + userConstants.name_abb + "</li>" +
        "<li>unauthorized advertising or marketing activities</li>" +
        "<li>damaging or undermine the rights of any third party</li>" +
        "<li>posting illegal or objectionable content</li>" +
        "<li>breaching  a security of " + companyConstants.fullName + ", any " + companyConstants.fullName + " user, or any third party</li>" +
        "<li>Terrorism related activities </li>" +
        "<li>fraud activities</li>"
    },
    {
        className: "div_heading3",
        id: "CostToJoinM2E", question: "What are some specific examples of " + userConstants.task_abb + "s that violate Crowd Automation policies?",
        answer: "<ul><li>CITs requiring disclosure of the " + userConstants.name_abb + "'s personal identity or Email address, either directly or indirectly</li>" +
            "<li>" + userConstants.task_abb + "s requiring signup at another website or group</li>" +
            "<li>" + userConstants.task_abb + "s that directly or indirectly promote a site, service, or opinion</li>" +
            "<li>" + userConstants.task_abb + "s that violate the terms and conditions of an activity or website</li>" +
            "<li>" + userConstants.task_abb + "s that have explicit or offensive content, for instance, nudity, but do not have the Adult Content Batch</li>" +
            "<li>" + userConstants.task_abb + "s asking " + userConstants.name_abb + "s to solicit third parties</li>" +
            "<li>" + userConstants.task_abb + "s that generate 'referred' site visits or click-through traffic</li>" +
            "<li>" + userConstants.task_abb + "s that ask " + userConstants.name_abb + "s to take action to manipulate a website's behavior or results</li>" +
            "<li>" + userConstants.task_abb + "s that violate intellectual property rights of any party</li>" +
            "<li>" + userConstants.task_abb + "s that require " + userConstants.name_abb + "s to download software</li></ul>"
    }
];

var FAQRequesterPaymentList = [
            {
                className: "div_heading1",
                id: "payToAccepter", question: "How do I pay for " + userConstants.task_abb + "s on " + companyConstants.fullName + "?",
                answer: "After getting satisfied over results submitted by " + userConstants.name_abb + "s, you will pay against bill. "
            },
            {
                className: "div_heading2",
                id: "feelCalculation", question: "How are the fees calculated?",
                answer: "" + companyConstants.fullName + " collects a 10% commission on every overall bill. For example, if a " + userConstants.task_abb + " reward is set to $0.30, Crowd Automation collects $0.03 for thread (each copy of " + userConstants.task_abb + "s). The minimum commission charged is $0.003 per thread (each copy of " + userConstants.task_abb + "s). When you grant a bonus, " + companyConstants.fullName + " collects 10% of the bonus amount, or a minimum of $0.004 per bonus payment."
            },
            {
                className: "div_heading3",
                id: "accepterPay", question: "How do " + userConstants.name_abb + "s get paid?",
                answer: "" + userConstants.name_abb + "s will be paid and " + companyConstants.fullName + " fees will be charged when you approve submitted work. If you reject the work, the " + userConstants.name_abb + " is not paid and you are not charged the " + companyConstants.fullName + " fees."
            }
];

var FAQRequesterCITRelatedtList = [
            {
                className: "div_heading1",
                id: "requesterStart", question: "How do I get started as a " + clientConstants.name_abb + "? ",
                answer: "The three steps to getting started are 1) signing up on cautom, 2) design your " + userConstants.task_abb + "s, and 3) publishing a " + userConstants.task_abb + ". So, if you haven't already, sign up as a " + companyConstants.name + " -be sure to add your details and a payment method." +
                    "<p>Now it's time to categorize your " + userConstants.task_abb + "s. Decide your project-category and pick a suitable sub-categorize to your project. Outline your goals in instructions textbox, add the skills you're looking for in Batch, and your desired deadline in deadline box. By thinking through and articulating your needs, you make it easier to design an effective " + userConstants.task_abb + " and explain your " + userConstants.task_abb + " to others.</p>" +
                    "<p>Every sub-categorize of " + userConstants.task_abb + "s has its own help section. This help section will explain how to design, publish, and manage your " + userConstants.task_abb + "s. Help section contains instructions, guidelines and videos which can help you design an effective " + userConstants.task_abb + "s. </p>" +
                    "<p>Now that you know what you need, publish a " + userConstants.task_abb + "! Be sure to include as much detail as possible. </p>" +
                    "Next steps:" +
                    "<ul><li>Before publishing the " + userConstants.task_abb + ", must review you " + userConstants.task_abb + "s preview</li>" +
                    "<li>Publish the " + userConstants.task_abb + " (After publishing you wouldn't modify it.)</li>" +
                    "<li>Track your " + userConstants.task_abb + "s in Dashboard</li></ul>"
                    
            },
            {
                className: "div_heading2",
                id: "citStart", question: "How do I create a " + userConstants.task_abb + "?",
                answer: "Every sub-categorize of " + userConstants.task_abb + "s has its own help section. This help section will explain how to design, publish, and manage your " + userConstants.task_abb + "s. Help section contains instructions, guidelines and videos which can help you design an effective " + userConstants.task_abb + "s. "
            },
            {
                className: "div_heading3",
                id: "thread", question: "What is a Thread?",
                answer: "<p>You can assign many " + userConstants.name + " to work on the same " + userConstants.task_abb + ", which is useful way of getting general views on a Subject by many " + userConstants.name + "s giving the same answer or getting different view in case of Survey related " + userConstants.task_abb + "s. An " + userConstants.name + " can only accept a " + userConstants.task_abb + " once and can only submit one thread per " + userConstants.task_abb + ". This guarantees that multiple " + userConstants.name + "s must complete a " + userConstants.task_abb + " that has multiple threads.</p>" +
                "<p>If an " + userConstants.name + " fails to complete an thread before the time frame specified (i.e., the " + userConstants.name + " abandons the " + userConstants.task_abb + "), or if the " + userConstants.name + " chooses not to complete it after accepting it (i.e., the " + userConstants.name + " returns the " + userConstants.task_abb + "), the thread becomes available for other " + userConstants.name + " to work on.</p>"
            },
            {
                className: "div_heading4",
                id: "rejectCIT", question: "What happens when I reject " + userConstants.task_abb + "?",
                answer: "When you reject a thread, the " + userConstants.name_abb + "? who performed the thread does not get paid, a    nd you are not charged the thread fee for the " + userConstants.task_abb + ". The rejection affects your " + companyConstants.name + " reputation and the reputation of the " + userConstants.name_abb + "? who submitted the results."
            },
            {
                className: "div_heading5",
                id: "accurateResult", question: "How do I get accurate results from " + userConstants.name_abb + "?",
                answer: "Visit " + userConstants.task_abb + "s help section before creating " + userConstants.task_abb + "s. Our system are design in such a way that it select " + userConstants.name_abb + "?s have batch of your " + userConstants.task_abb + "s category (Experience related to that category). Always review the submitted " + userConstants.task_abb + "s. "
            },
            {
                className: "div_heading6",
                id: "citHowLongActive", question: "How long does my " + userConstants.task_abb + "s active?",
                answer: "" + companyConstants.fullName + " will delete " + userConstants.task_abb + "s, " + userConstants.task_abb + " Types, and Batch if they are not used for 90 days. If you require access to CIT data for longer than 90 days, then download the " + userConstants.task_abb + " results and store them locally."
            }
];

var FAQAccepterList = [
            {
                className: "div_heading1",
                id: "HowToJoinCautom", question: "How can I join Crowd Automation?",
                answer: "It's pretty simple. Go to www.cautom.com, now if you are a new Crowd Automation Requester, please visit <a class='undeline_anchor' href='#/signup/client'>Crowd Automation Requester</a> section, like millions of students want to register please visit <a class='undeline_anchor' href='#/signup/user'>Crowd Automation Accepter</a> section." +
                    "<p>now if you are already a Crowd Automation Requester or " + userConstants.name_abb + " please visit <a class='undeline_anchor' href='#/login'>login</a> section.</p>"
            },
            {
                className: "div_heading2",
                id: "MultipleAccounts", question: "Can I have multiple accounts?",
                answer: "The answer is NO! Please don't try to trick the computer as in the later stages your job (once you earn above Rs1000) we will ask you PAN card and other identity related information. If we found any discrepancy we will disable your account and money will forfeited."
            },
            {
                className: "div_heading3",
                id: "completeCIT", question: "How do I complete a " + userConstants.task_abb + "? ",
                answer: "<p>After login as a " + userConstants.name_abb + ", you will get all CITs at your " + userConstants.name_abb + " homepage. Click on paticular " + userConstants.task_abb + " link to see a details about that " + userConstants.task_abb + ".Preview the " + userConstants.task_abb + " before you decide to accept it.</p>" +
                    "<p>If you want to complete the " + userConstants.task_abb + ", click on 'Accept " + userConstants.task_abb + "' button. Complete the " + userConstants.task_abb + " according to the Requester instructions, and select the 'Submit HIT'' button. After you submit your results for the HIT, another similar HIT will be presented for you to accept.</p>"
            },
            {
                className: "div_heading4",
                id: "rejectedWork", question: "My submitted work was rejected, what can I do? ",
                answer: "<p>A " + clientConstants.name_abb + " may reject your submitted work if they believe the answer is wrong, the " + userConstants.task_abb + " was not completed correctly, or that the instructions were not followed.</p>" +
                    "<p>If you believe that your work was rejected in error, you may report us directly by clicking on 'report the CIT' link.</p>"
            },
            {
                className: "div_heading5",
                id: "bonusPay", question: "What is a bonus reward?",
                answer: "Its an additional grant of money from a " + clientConstants.name_abb + " for " + userConstants.name_abb + "s who have done good work. A " + clientConstants.name_abb + " can opt to pay a bonus reward in addition to the stated money for completing a " + userConstants.task_abb + "."
            },
            {
                className: "div_heading4",
                id: "ForgotMyPassword", question: "What if I forgot my password?",
                answer: "It happens every time for all of us! Just click on the forgot password link on the main page and follow the instructions."
            },
            {
                className: "div_heading5",
                id: "UpdatePersonalInformation", question: "How to update my personal information?",
                answer: "After logging in, in the setting section you can always update any information related to you. Same as you do in all social media sites. "
            },
            {
                className: "div_heading6",
                id: "lockMyAccount", question: "Can I lock my account?",
                answer: "Mostly you never will! Still if you ever want to lock an account click on the 'lock account' button in edit profile section. If you are unable to do this send us an email at " + companyConstants.supportEmail + "."
            }
];


var FAQAccepterPayList = [
            {
                className: "div_heading7",
                id: "HowMuchICanEarnPerDay", question: "How much can I earn per day?",
                answer: "Currently we have set the maximum to Rs200. This is done to ensure that everyone gets equaly opportunity."
            },
            {
                className: "div_heading8",
                id: "TrackingSystem", question: "Do you have a tracking system?",
                answer: "We have a very sophisticated tracking system that monitors your work performance. Based on he rating our system gives you more jobs will be assigned."
            },
            {
                className: "div_heading9",
                id: "HowDoIGetPaid", question: "How do I get paid?",
                answer: "Your submitted task once approves by " + clientConstants.name_abb + ", money will automatically transfer to your earnings balance displayed on both your home page and Get Paid page. " +
                "If you are a new " + userConstants.name_abb + ", first you need to verify your account by submitting government issued identity card. Once your total earning crosses 100 Rs, you can withdraw your earning to your bank account. Below 100 Rs, you can recharge your moblie bills."
            },
            {
                className: "div_heading10",
                id: "MinBalanceForWithdrawal", question: "What are the minimum amount required to withdraw money to a bank account?",
                answer: "Just Rs100. We wanted to keep a lower limit, but that will result in high operating cost for us."
            },
            {
                className: "div_heading1",
                id: "CheckMyBalance", question: "How can I check the amount of money in my account?",
                answer: "Go to Get Paid page. Your earning will be shown there with all transactions and withdrawals. opportunity."
            },
            {
                className: "div_heading2",
                id: "DaysToCreditAmountInMyAccount", question: "Once I complete a job, how many days it will take to credit my account?",
                answer: "Anywhere between 1-7 days. This depends on the client who proposed the job and bank transfer timings. We will be more than happy to credit your account for the work you have done."
            },
            {
                className: "div_heading3",
                id: "DaysToCreditAmountInMyBankAccount", question: "How many days it takes to credit money to my bank account?",
                answer: "Despite a huge ecommerce growth, our banking system is still slow. So it may take anywhere between 3 to 5 days."
            }
];

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

function updateMe( data ) {
    if(data == 'login')
        location.href="/"+$.cookie('loginType');   
    else if(data == 'fblikepage')
        location.href="/user#/facebookLikePage";
    else if(data == 'error')
        alert("Internal Server Error Occured !! Try Again");
}

function detectIfUserLoggedIn(){
        var headers = {
                        'Content-Type': 'application/json',
						'UTMZT': $.cookie('utmzt'),
						'UTMZK': $.cookie('utmzk'),
						'UTMZV': $.cookie('utmzv')                       
                    };
         if($.cookie('utmzt') != null && $.cookie('utmzt') != "" && $.cookie('loginType') != null && $.cookie('loginType') != "")
         {
            var  url = ServerContextPah + '/Auth/IsValidSession';
                 $.ajax({
						url: url,
						type: "POST",
                        headers: headers
						}).done(function(data,status) {
							console.log(data);
                            if(data == true)
                                location.href="/"+$.cookie('loginType');
                            else
                            {
                                $.removeCookie('utmzt', { path: '/' });
                                $.removeCookie('utmzk', { path: '/' });
                                $.removeCookie('utmzv', { path: '/' });
                                $.removeCookie('utime', { path: '/' });
                                $.removeCookie('kmsi', { path: '/' });
                                // will first fade out the loading animation
                                jQuery("#status").fadeOut();
                                // will fade out the whole DIV that covers the website.
                                jQuery("#preloader").delay(1000).fadeOut("medium");                                                           
                            }					
						});
         }
         else
         {
                 $.removeCookie('utmzt', { path: '/' });
                 $.removeCookie('utmzk', { path: '/' });
                 $.removeCookie('utmzv', { path: '/' });
                 $.removeCookie('utime', { path: '/' });
                 $.removeCookie('kmsi', { path: '/' });
                // will first fade out the loading animation
                jQuery("#status").fadeOut();
                // will fade out the whole DIV that covers the website.
                jQuery("#preloader").delay(1000).fadeOut("medium");
         }
					
}


function logout(){
    var headers = {
                        'Content-Type': 'application/json',
						'UTMZT': $.cookie('utmzt'),
						'UTMZK': $.cookie('utmzk'),
						'UTMZV': $.cookie('utmzv')                       
                    };
         if($.cookie('utmzt') != null && $.cookie('utmzt') != "")
         {
            var  url = ServerContextPah + '/Auth/Logout';
//                 $.ajax({
//						url: url,
//						type: "POST",
//                        headers: headers
//						}).done(function(data,status) {							                                                                              
//                           					
//						});
//                

                    $.ajax({
                       type: "POST",
                       url: url,
                       headers: headers,                       
                       success: function(result){
                            $.removeCookie('utmzt', { path: '/' });
                            $.removeCookie('utmzk', { path: '/' });
                            $.removeCookie('utmzv', { path: '/' });
                            $.removeCookie('utime', { path: '/' });
                            $.removeCookie('kmsi', { path: '/' });
                            location.href = "/";
                       },
                       error: function(request,status,errorThrown) {
                            $.removeCookie('utmzt', { path: '/' });
                            $.removeCookie('utmzk', { path: '/' });
                            $.removeCookie('utmzv', { path: '/' });
                            $.removeCookie('utime', { path: '/' });
                            $.removeCookie('kmsi', { path: '/' });
                            location.href = "/";
                       }
                     });


         }
         else
         {
            location.href = "/";
         }
                     
}