//(function () {   
//    (function (i, s, o, g, r, a, m) {
//        i['GoogleAnalyticsObject'] = r;
//        i[r] = i[r] || function () { (i[r].q = i[r].q || []).push(arguments); },
//                i[r].l = 1 * new Date();
//        a = s.createElement(o), m = s.getElementsByTagName(o)[0];
//        a.async = 1;
//        a.src = g;
//        m.parentNode.insertBefore(a, m);
//    })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
//    ga('create', googleAnalyticsAppID, 'auto', { 'cookieDomain': 'none' });
//    ga('require', 'linkid', 'linkid.js');
//})();

(function () {
(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
    (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
    m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
})(window,document,'script','//www.google-analytics.com/analytics.js','ga');
ga('create', googleAnalyticsAppID, 'auto', { 'cookieDomain': 'none' });
ga('require', 'displayfeatures');
ga('send', 'pageview');
})();

window.onload = function () {
    $('.googleAnalytics').click(function () {
        var event = this.getAttribute("ga-event");
        var category = this.getAttribute("ga-category");
        var label = this.getAttribute("ga-label");        
        gaWeb(category, event, label);        
    });
};


function gaWeb(category, event, label) {
    ga('send', 'event', category, event, label);
}

function gaPageView(page, title) {
    ga('send', 'pageview', {
        'page': page,
        'title': title
    });
}