'use strict';
define([appLocation.preLogin], function (app) {
    app.controller('beforeLoginFAQ', function ($scope, $location, $http, $rootScope, CookieUtil, $anchorScroll, $sce) {
        $scope.scrollTo = function (id) {
            $anchorScroll();
            $('#' + id).click();
            $location.hash(id);
            //$('#' + id).click();        
        };

        $scope.FAQGeneralList = [
            {
                className: "div_heading1",
                id: "whatsCautom", question: "What is " + companyConstants.name + "?",
                answer: $sce.trustAsHtml("Cautom stands for <b><i>crowd automation</i></b>. It is an online portal that translates human intelligence into business domain. " +
                "<p><b>" + clientConstants.name + "</b> use the intelligence of a crowd to get his work done in minutes at a reasonable cost. The <b>" + userConstants.name + "</b> get paid for their time." +
                    "</p>")
            },
            {
                className: "div_heading2",
                id: "CITs", question: "What is a " + userConstants.task_abb + "?",
                answer: $sce.trustAsHtml("Each question " + clientConstants.name + " asks through our application is a <b>" + userConstants.task + "</b>, or <b>" + userConstants.task_abb + "</b>. " +
                "A "+userConstants.task+" contains all of the information an Accepter needs to answer the question, including information about how the question is shown to the Accepter and what " +
                    "kinds of answers would be considered valid. " + "<p class='div_c'>Each " + userConstants.task_abb + " has a reward, an amount of money you pay to the Accepter that successfully completes " +
                    "the " + companyConstants.task_abb + ". Requester can request that more than one Accepter to complete a " + companyConstants.task_abb + " by specifying a quantity of Workforce " +
                    "required while creating " + companyConstants.task_abb + ".</p>")
            },
            {
                className: "div_heading3",
                id: "Requester", question: "Who is a " + clientConstants.name + "?",
                answer: $sce.trustAsHtml("A " + clientConstants.name + " is anyone (or company or organization) who uploads a task as to harness the crowd potential. <p class='div_c'>As a " + clientConstants.name + ", " +
                    "you use our Interface to submit your questions, retrieve answers, and perform other automated tasks. You can send a mail on "+ companyConstants.supportEmail +" to automate your bulk requirement. We will integrate your requirement with our API." +
                    "<p>To "+ userConstants.name +", you are known as the creator of your " + userConstants.task_abb + "s, and as the creator and maintainer of your " + userConstants.Batch + " types.</p>")
            },
            {
                className: "div_heading4",
                id: "Accepter", question: "Who is a " + userConstants.name + "?",
                answer: $sce.trustAsHtml("A " + userConstants.name + " is a person who answers questions for Crowd Automation. A " + userConstants.name + " uses the " + companyConstants.fullName + " website" +
                    " (<a class='undeline_anchor' href='http://cautom.com'>http://cautom.com</a>) to find questions, submit answers, and manage his or her account." + "<p class='div_c'>To " + clientConstants.name + "s, " +
                    "a " + userConstants.name + " is known as the submitter of a " + userConstants.task_abb + " assignment. You can see the " + userConstants.name + "'s " + userConstants.Batch + ". </p>" +
                    "<p>" + userConstants.Batch + "es represent the " + userConstants.name + "'s reputation and abilities in a particular domain. A " + userConstants.name + "'s " + userConstants.Batch + "es " +
                    "are matched against a " + userConstants.task_abb + "'s " + userConstants.Batch + " requirements to allow or disallow the " + userConstants.name + " to accept the " + userConstants.task_abb + ". " +
                    "A " + userConstants.name + "'s " + userConstants.Batch + " cannot be accessed directly by other users.</p>")
            },
            {
                className: "div_heading5", id: "Accepter", question: "How can I join Crowd Automation?", answer: $sce.trustAsHtml("It's pretty simple. Go to www.cautom.com, now if you are a new " + clientConstants.name + ", please visit <a class='undeline_anchor' href='#/signup/client'>Crowd Automation Requester</a> section, like millions of students want to register please visit <a class='undeline_anchor' href='#/signup/user'>Crowd Automation Accepter</a> section." +
                      "<p>now if you are already a " + clientConstants.name + " or Accepter please visit <a class='undeline_anchor' href='#/login'>login</a> section.</p>")
            },
            { className: "div_heading6", id: "DeleteMyAccount", question: "Can I delete my account? ", answer: "Mostly you never will! Still if you ever want to delete an account click on the delete section in settings section. If you are unable to do this send us an email at email@madetoearn.com." },
            { className: "div_heading7", id: "HowMuchICanEarnPerDay", question: "How much can I earn per day?", answer: "Currently we have set the maximum to Rs200. This is done to ensure that everyone gets equaly opportunity." },
            { className: "div_heading8", id: "TrackingSystem", question: "Do you have a tracking system?", answer: "We have a very sophisticated tracking system that monitors your work performance. Based on he rating our system gives you more jobs will be assigned." },
            { className: "div_heading9", id: "HowDoIGetPaid", question: "How do I get paid?", answer: "Currently we have set the maximum to Rs200. This is done to ensure that everyone gets equaly opportunity." },
            { className: "div_heading10", id: "MinBalanceForWithdrawal", question: "What are the minimum amount required to withdraw money to a bank account?", answer: "Just Rs100. We wanted to keep a lower limit, but that will result in high operating cost for us." },
            { className: "div_heading1", id: "CheckMyBalance", question: "How can I check the amount of money in my account?", answer: "Go to dashboard. Your earning will be shown there with all transactions and withdrawals. opportunity." },
            { className: "div_heading2", id: "DaysToCreditAmountInMyAccount", question: "Once I complete a job, how many days it will take to credit my account?", answer: "Anywhere between 1-7 days. This depends on the client who proposed the job and bank transfer timings. We will be more than happy to credit your account for the work you have done." },
            { className: "div_heading3", id: "DaysToCreditAmountInMyBankAccount", question: "How many days it takes to credit money to my bank account?", answer: "Despite a huge ecommerce growth, our banking system is still slow. So it may take anywhere between 3 to 5 days." },
            { className: "div_heading4", id: "CustomerServiceNumber", question: "Do you have customer service number?", answer: "Sorry, we don't have a phone number for customer service. Simply because we don't want you to stay in a long queue and listen to some cranky music. Just fill the contact form with your question and send us. We will email you our response. If it's not solved in an email conversation. We will call you at our expense!" },
            { className: "div_heading5", id: "HowToContactUs", question: "How to conatct us?", answer: "Sorry, we don't have a phone number for customer service. Simply because we don't want you to stay in a long queue and listen to some cranky music. Just fill the contact form with your question and send us. We will email you our response. If it's not solved in an email conversation. We will call you at our expense!" }
        ];

        $scope.FAQRequesterList = [
            {
                className: "div_heading1",
                id: "HowToJoinCautom", question: "How can I join Crowd Automation?",
                answer: $sce.trustAsHtml("It's pretty simple. Go to www.cautom.com, now if you are a new Crowd Automation Requester, please visit <a class='undeline_anchor' href='#/signup/client'>Crowd Automation Requester</a> section, like millions of students want to register please visit <a class='undeline_anchor' href='#/signup/user'>Crowd Automation Accepter</a> section." +
                    "<p>now if you are already a Crowd Automation Requester or Accepter please visit <a class='undeline_anchor' href='#/login'>login</a> section.</p>")
            },
            { className: "div_heading2", id: "MultipleAccounts", question: "Can I have multiple accounts?", answer: "The answer is NO! Please don't try to trick the computer as in the later stages your job (once you earn above Rs1000) we will ask you PAN card and other identity related information. If we found any discrepancy we will disable your account and money will forfeited." },
            { className: "div_heading3", id: "CostToJoinM2E", question: "Does it cost to join M2E?", answer: "No, it does not cost anything. We won't ever charge you for our services. So smile!" },
            { className: "div_heading4", id: "ForgotMyPassword", question: "What if I forgot my password?", answer: "It happens every time for all of us! Just click on the forgot password link on the main page and follow the instructions." },
            { className: "div_heading5", id: "UpdatePersonalInformation", question: "How to update my personal information?", answer: "After logging in, in the setting section you can always update any information related to you. Same as you do in all social media sites. " },
            { className: "div_heading6", id: "DeleteMyAccount", question: "Can I delete my account? ", answer: "Mostly you never will! Still if you ever want to delete an account click on the delete section in settings section. If you are unable to do this send us an email at email@madetoearn.com." },
            { className: "div_heading7", id: "HowMuchICanEarnPerDay", question: "How much can I earn per day?", answer: "Currently we have set the maximum to Rs200. This is done to ensure that everyone gets equaly opportunity." },
            { className: "div_heading8", id: "TrackingSystem", question: "Do you have a tracking system?", answer: "We have a very sophisticated tracking system that monitors your work performance. Based on he rating our system gives you more jobs will be assigned." },
            { className: "div_heading9", id: "HowDoIGetPaid", question: "How do I get paid?", answer: "Currently we have set the maximum to Rs200. This is done to ensure that everyone gets equaly opportunity." },
            { className: "div_heading10", id: "MinBalanceForWithdrawal", question: "What are the minimum amount required to withdraw money to a bank account?", answer: "Just Rs100. We wanted to keep a lower limit, but that will result in high operating cost for us." },
            { className: "div_heading1", id: "CheckMyBalance", question: "How can I check the amount of money in my account?", answer: "Go to dashboard. Your earning will be shown there with all transactions and withdrawals. opportunity." },
            { className: "div_heading2", id: "DaysToCreditAmountInMyAccount", question: "Once I complete a job, how many days it will take to credit my account?", answer: "Anywhere between 1-7 days. This depends on the client who proposed the job and bank transfer timings. We will be more than happy to credit your account for the work you have done." },
            { className: "div_heading3", id: "DaysToCreditAmountInMyBankAccount", question: "How many days it takes to credit money to my bank account?", answer: "Despite a huge ecommerce growth, our banking system is still slow. So it may take anywhere between 3 to 5 days." },
            { className: "div_heading4", id: "CustomerServiceNumber", question: "Do you have customer service number?", answer: "Sorry, we don't have a phone number for customer service. Simply because we don't want you to stay in a long queue and listen to some cranky music. Just fill the contact form with your question and send us. We will email you our response. If it's not solved in an email conversation. We will call you at our expense!" },
            { className: "div_heading5", id: "HowToContactUs", question: "How to conatct us?", answer: "Sorry, we don't have a phone number for customer service. Simply because we don't want you to stay in a long queue and listen to some cranky music. Just fill the contact form with your question and send us. We will email you our response. If it's not solved in an email conversation. We will call you at our expense!" }
        ];
        $scope.FAQAccepterList = [
            {
                className: "div_heading1", id: "HowToJoinCautom", question: "How can I join Crowd Automation?",
                answer: $sce.trustAsHtml("It's pretty simple. Go to www.cautom.com, now if you are a new Crowd Automation Requester, please visit <a class='undeline_anchor' href='#/signup/client'>Crowd Automation Requester</a> section, like millions of students want to register please visit <a class='undeline_anchor' href='#/signup/user'>Crowd Automation Accepter</a> section." +
                    "<p>now if you are already a Crowd Automation Requester or Accepter please visit <a class='undeline_anchor' href='#/login'>login</a> section.</p>")
            },
            { className: "div_heading2", id: "MultipleAccounts", question: "Can I have multiple accounts?", answer: "The answer is NO! Please don't try to trick the computer as in the later stages your job (once you earn above Rs1000) we will ask you PAN card and other identity related information. If we found any discrepancy we will disable your account and money will forfeited." },
            { className: "div_heading3", id: "CostToJoinM2E", question: "Does it cost to join M2E?", answer: "No, it does not cost anything. We won't ever charge you for our services. So smile!" },
            { className: "div_heading4", id: "ForgotMyPassword", question: "What if I forgot my password?", answer: "It happens every time for all of us! Just click on the forgot password link on the main page and follow the instructions." },
            { className: "div_heading5", id: "UpdatePersonalInformation", question: "How to update my personal information?", answer: "After logging in, in the setting section you can always update any information related to you. Same as you do in all social media sites. " },
            { className: "div_heading6", id: "DeleteMyAccount", question: "Can I delete my account? ", answer: "Mostly you never will! Still if you ever want to delete an account click on the delete section in settings section. If you are unable to do this send us an email at email@madetoearn.com." },
            { className: "div_heading7", id: "HowMuchICanEarnPerDay", question: "How much can I earn per day?", answer: "Currently we have set the maximum to Rs200. This is done to ensure that everyone gets equaly opportunity." },
            { className: "div_heading8", id: "TrackingSystem", question: "Do you have a tracking system?", answer: "We have a very sophisticated tracking system that monitors your work performance. Based on he rating our system gives you more jobs will be assigned." },
            { className: "div_heading9", id: "HowDoIGetPaid", question: "How do I get paid?", answer: "Currently we have set the maximum to Rs200. This is done to ensure that everyone gets equaly opportunity." },
            { className: "div_heading10", id: "MinBalanceForWithdrawal", question: "What are the minimum amount required to withdraw money to a bank account?", answer: "Just Rs100. We wanted to keep a lower limit, but that will result in high operating cost for us." },
            { className: "div_heading1", id: "CheckMyBalance", question: "How can I check the amount of money in my account?", answer: "Go to dashboard. Your earning will be shown there with all transactions and withdrawals. opportunity." },
            { className: "div_heading2", id: "DaysToCreditAmountInMyAccount", question: "Once I complete a job, how many days it will take to credit my account?", answer: "Anywhere between 1-7 days. This depends on the client who proposed the job and bank transfer timings. We will be more than happy to credit your account for the work you have done." },
            { className: "div_heading3", id: "DaysToCreditAmountInMyBankAccount", question: "How many days it takes to credit money to my bank account?", answer: "Despite a huge ecommerce growth, our banking system is still slow. So it may take anywhere between 3 to 5 days." },
            { className: "div_heading4", id: "CustomerServiceNumber", question: "Do you have customer service number?", answer: "Sorry, we don't have a phone number for customer service. Simply because we don't want you to stay in a long queue and listen to some cranky music. Just fill the contact form with your question and send us. We will email you our response. If it's not solved in an email conversation. We will call you at our expense!" },
            { className: "div_heading5", id: "HowToContactUs", question: "How to conatct us?", answer: "Sorry, we don't have a phone number for customer service. Simply because we don't want you to stay in a long queue and listen to some cranky music. Just fill the contact form with your question and send us. We will email you our response. If it's not solved in an email conversation. We will call you at our expense!" }
         ];
    });
});

