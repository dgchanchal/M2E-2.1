(function () {
    var w = window;
    w.madetoearn = w.madetoearn || {};
    var m2e = w.madetoearn;

    function getParameterByName(name) {
        var match = RegExp('[?&]' + name + '=([^&]*)').exec(w.location.search);
        return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
    }

    function isTagName(elem, name) {
        return (elem.tagName && elem.tagName.toLowerCase() === name) ||
        	   (elem.nodeName && elem.nodeName.toLowerCase() === name);
    }

    function getLanguage() {
        // Add any variations of a language value and map to a supported language.
        var langMap = {
            /*German/Deutsch */
            //"de": "de",
            //"de-at": "de",
            //"de-de": "de",
            //"de-li": "de",
            //"de-lu": "de",
            //"de-ch": "de",
            /*English*/
            "en": "en-us",
            //"en-gb": "en-gb",
            "en-us": "en-us",
            /*French*/
            //"fr": "fr-fr",
            //"fr-fr": "fr-fr",
            //"fr-be": "fr-fr",
            //"fr-ca": "fr-fr",
            //"fr-lu": "fr-fr",
            //"fr-ch": "fr-fr",
            /*Italian*/
            //"it": "it",
            //"it-it": "it",
            //"it-ch": "it",
            /*Hindi*/
            "hi_in": "hi_in",            
        };

        //         Querystring parameter         Mozilla               IE
        var lang = getParameterByName("lang") || navigator.language || navigator.userLanguage;
        //console.log("getParameterByName lang : " + getParameterByName("lang"));
        // Default to en-us
        return (lang && langMap[lang.toLowerCase()]) ? langMap[lang.toLowerCase()] : langMap["en"];
    }

    // Public Functions
    m2e.i18nGetString = function (id) {
        return m2e.i18n[id] || ("" + id + ": String not found.");
    };

    m2e.i18nFormat = function () {
        var argLen = arguments.length;
        var str = m2e.i18nGetString(arguments[0]);
        for (var i = 1; i < argLen; i++) {
            var pattern = "\\{" + (i - 1) + "\\}";
            str = str.replace(new RegExp(pattern, "g"), arguments[i]);
        }
        return str;
    };

    m2e.i18nLocalizePage = function (context) {
        if (m2e.i18n) {
            //alert("testing");
            context = context ? context : "*";
            $.each($('[data-i18n]', $(context)), function (index, item) {
                id = $(item).attr("data-i18n") || "EMPTY_data-i18n";
                id = id.split(";")[0];
                val = m2e.i18nGetString(id);
                if (isTagName(item, 'input') || isTagName(item, 'textarea')) {
                    // Handle placeholder
                    $(item).attr('placeholder', val);
                } else if (isTagName(item, 'img')) {
                    // Handle alt text
                    $(item).attr('alt', val);
                } else {
                    // Handle HTML contents
                    $(item).html(val);
                }
            });
        }
    };

    $.ajax({
        url: "../../App/js/i18n/i18n_" + getLanguage() + ".js",
        dataType: "script",
        async: false
    });

})();
