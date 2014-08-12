'use strict';
define([appLocation.preLogin], function (app) {
    app.controller('beforeLoginFAQ', function ($scope, $location, $http, $rootScope, CookieUtil, $anchorScroll) {
        $scope.scrollTo = function (id) {
            $anchorScroll();
            $('#' + id).click();
            $location.hash(id);
            //$('#' + id).click();        
        };

        $scope.FAQquestionsList = [
            { id: "HowToJoinM2E", question: "How can I join M2E?", answer: "It's pretty simple. Go to www.madetoearn.com, now if you are client click on the client login button and if you, like millions of students want to earn click on the worker login section." },
            { id: "MultipleAccounts", question: "Can I have multiple accounts?", answer: "The answer is NO! Please don't try to trick the computer as in the later stages your job (once you earn above Rs1000) we will ask you PAN card and other identity related information. If we found any discrepancy we will disable your account and money will forfeited." },
            { id: "CostToJoinM2E", question: "Does it cost to join M2E?", answer: "No, it does not cost anything. We won't ever charge you for our services. So smile!" },
            { id: "ForgotMyPassword", question: "What if I forgot my password?", answer: "It happens every time for all of us! Just click on the forgot password link on the main page and follow the instructions." },
            { id: "UpdatePersonalInformation", question: "How to update my personal information?", answer: "After logging in, in the setting section you can always update any information related to you. Same as you do in all social media sites. " },
            { id: "DeleteMyAccount", question: "Can I delete my account? ", answer: "Mostly you never will! Still if you ever want to delete an account click on the delete section in settings section. If you are unable to do this send us an email at email@madetoearn.com." },
            { id: "HowMuchICanEarnPerDay", question: "How much can I earn per day?", answer: "Currently we have set the maximum to Rs200. This is done to ensure that everyone gets equaly opportunity." },
            { id: "TrackingSystem", question: "Do you have a tracking system?", answer: "We have a very sophisticated tracking system that monitors your work performance. Based on he rating our system gives you more jobs will be assigned." },
            { id: "HowDoIGetPaid", question: "How do I get paid?", answer: "Currently we have set the maximum to Rs200. This is done to ensure that everyone gets equaly opportunity." },
            { id: "MinBalanceForWithdrawal", question: "What are the minimum amount required to withdraw money to a bank account?", answer: "Just Rs100. We wanted to keep a lower limit, but that will result in high operating cost for us." },
            { id: "CheckMyBalance", question: "How can I check the amount of money in my account?", answer: "Go to dashboard. Your earning will be shown there with all transactions and withdrawals. opportunity." },
            { id: "DaysToCreditAmountInMyAccount", question: "Once I complete a job, how many days it will take to credit my account?", answer: "Anywhere between 1-7 days. This depends on the client who proposed the job and bank transfer timings. We will be more than happy to credit your account for the work you have done." },
            { id: "DaysToCreditAmountInMyBankAccount", question: "How many days it takes to credit money to my bank account?", answer: "Despite a huge ecommerce growth, our banking system is still slow. So it may take anywhere between 3 to 5 days." },
            { id: "CustomerServiceNumber", question: "Do you have customer service number?", answer: "Sorry, we don't have a phone number for customer service. Simply because we don't want you to stay in a long queue and listen to some cranky music. Just fill the contact form with your question and send us. We will email you our response. If it's not solved in an email conversation. We will call you at our expense!" }
        ];
    });
});

