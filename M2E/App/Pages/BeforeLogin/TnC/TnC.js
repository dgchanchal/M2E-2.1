'use strict';
define([appLocation.preLogin], function (app) {
    app.controller('termsPrivacyController', function ($scope, $http, $rootScope,Restangular, CookieUtil) {
        $('title').html("index"); //TODO: change the title so cann't be tracked in log
        
        $scope.isAllExpanded = true;
        $scope.tncContents = [            
            {
                headerClass: "div_heading1",
                RightBoxClass: "tr_Right_Nav_Header1",
                headerLabel: "Cookies",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: false,
                                text: "" + companyConstants.fullName + " website uses cookies', which are small pieces of data that are stored on your computer, mobile phone or other device. For instance, we use cookies to store your preferences and settings; help with sign-in; provide targeted ads; combat fraud; and analyze site operations."
                            },
                            {
                                isUnderline: false,
                                text: "We use this technology to do things like:"
                            }
                        ],
                        dataList: [
                            {
                                text: "make " + companyConstants.fullName + " site easier or faster to use;"
                            },
                            {
                                text: "enable features and store information about you (including on your device or in your browser cache) and your use of " + companyConstants.fullName + " site;"
                            },
                            {
                                text: "deliver, understand and improve advertising;"
                            },
                            {
                                text: "monitor and understand the use of our products and services; and,"
                            },
                            {
                                text: "protect you, others and " + companyConstants.fullName + " site."
                            }
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading2",
                RightBoxClass: "tr_Right_Nav_Header2",
                headerLabel: "Participants Criteria",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: false,
                                text: "By Registering for and using the " + companyConstants.fullName + " site:"
                            }
                        ],
                        dataList: [
                            {
                                text: "All participants should be at-least 18 years old to receive a payment from the " + companyConstants.fullName + "."
                            },
                            {
                                text: "All transactions involving higher amounts( as set by the " + companyConstants.fullName + ") will require proper tax verification(for Indians, not limited to PAN card alone)"
                            },
                            {
                                text: "You should not be banned/restricted by Indian Government to use our services."
                            },
                            {
                                text: "All " + clientConstants.name + " and " + userConstants.name + " should abide to our code of conduct and terms & conditions."
                            },
                            {
                                text: "You should not use this service as a way to trick people or get jobs/actions done that are prohibited by Indian law."
                            },
                            {
                                text: "" + userConstants.name + " should be proud to be an independent earner!"
                            }
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading3",
                RightBoxClass: "tr_Right_Nav_Header3",
                headerLabel: "Privacy Concerns",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: false,
                                text: "Your privacy is very important to us. We encourage you to read the Data Use Policy, and to use it to help you make informed decisions."
                            }
                        ],
                        dataList: [
                            {
                                text: "By accessing/registering on  " + companyConstants.fullName + " site, you agree to all policies laid down by the website and should be aware that we form time to time change these policies to keep the website running."
                            },
                            {
                                text: "Your email ID, name and phone numbers will be used for promotional/third party services for receiving monetary benefits. However you can always stop this by mailing us at help@cautom.com"
                            },
                            {
                                text: "We legitimately will be using your personal information to provide you with better suited CITs, decided by our algorithms."
                            }
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading4",
                RightBoxClass: "tr_Right_Nav_Header4",
                headerLabel: "Payment Related Service",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: false,
                                text: "" + companyConstants.fullName + " or its affiliates will process all payments made by " + clientConstants.name_abb + " to Providers (the 'Payment Service'). Requester payments made through the Payment Service are received by Crowd Automation or its Affiliates on behalf of Providers, and may be disbursed only in accordance with the terms outlined below."
                            }
                        ],
                        dataList: [
                            {
                                text: "All financial transactions will be dealt by the organization. We reserve all rights to withhold or even cancel the pay for a user at our discretion, provided he/she is not agreeing to our terms and conditions."
                            },
                            {
                                text: "Also the rates per job and the time schedule for payment can be varied at our sole discretion."
                            },
                            {
                                text: "All trials in this regard will be dealt in the Bangalore High court, India."
                            },
                            {
                                text: "However our customer care support will try to solve any payment related issues without any partiality."
                            }
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading5",
                RightBoxClass: "tr_Right_Nav_Header5",
                headerLabel: "Data Storage & encryption",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: false,
                                text: "" + companyConstants.fullName + " site is committed to protecting the security of your personal information. We use a variety of security technologies and procedures to help protect your personal information from unauthorized access, use or disclosure. For example, we store the personal information you provide on computer systems that have limited access and are in controlled facilities. When we transmit highly confidential information (such as a credit card number or password) over the Internet, we protect it through the use of encryption, such as the Secure Socket Layer (SSL) protocol."
                            }
                        ],
                        dataList: [
                            {
                                text: "All your data is stored in highly secured encrypted servers."
                            },
                            {
                                text: "In-case of a security breach beyond our reach, we won't be responsible for the resulting data misuse. However we will ensure that such activities never occur."
                            },
                            {
                                text: "All our servers are maintained by third parties, which are highly secure. But any major misuse from their part won't be our liability."
                            }
                        ]
                    },
                    {
                        paragraphList: [
                            {
                                isUnderline: true,
                                text: "Third Party data usage Policies:"
                            }
                        ],
                        dataList: [
                            {
                                text: "All third party services we use have a similar or even strong privacy policy. Your data will be handled with at-most caution."
                            },
                            {
                                text: "Any damages- direct or indirect caused by the third parties won't be our liability in any case."
                            }
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading6",
                RightBoxClass: "tr_Right_Nav_Header6",
                headerLabel: "Fees  & Services",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: false,
                                text: "This is defined by company and changes from time to time. Typically we take 10% of the total cost involved from the " + companyConstants.fullName + ". The " + userConstants.name + " is never charged."
                            }
                        ],
                        dataList: [
                            {
                                text: "We are not responsible for any conversion/processing charges that your bank/financial institution charges for any transaction with us."
                            },
                            {
                                text: "For direct deposit of money to the " + userConstants.name_abb + " bank account, the " + userConstants.name_abb + " to furnish all details accurately. In-case he/she fails to do so we, will charge a 10% fee for all extra attempts to be done."
                            },
                            {
                                text: "The fees to be paid by " + clientConstants.name_abb + " are described by Individuals within our company, concentrating on fair trade practices."
                            }
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading7",
                RightBoxClass: "tr_Right_Nav_Header7",
                headerLabel: "Advertising & Promotion",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: false,
                                text: "Advertising & PromotionWe may display your logo, company name or details for promotion purposes. But all these actions will be informed to you through email. You are free to opt-out at any time from this action by just sending us an email."
                            },
                            {
                                isUnderline: false,
                                text: "Advertising point of view,"
                            }
                        ],
                        dataList: [
                            {
                                text: "You are not supposed to advertise any services/website as a part of usage on " + companyConstants.fullName + " site. However promotion of company videos/logo/ teaser can be done as a part of our paid services. But you should be a registered " + clientConstants.name + " or " + userConstants.name_abb + " and these facts should be clearly stated during the submission of work proposal."
                            },
                            {
                                text: "Brand creation/promotion is accepted as a part of our paid services and should be stated clearly during submission of our work."
                            }
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading8",
                RightBoxClass: "tr_Right_Nav_Header8",
                headerLabel: "Content Uploading Policies",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: false,
                                text: "When you give us content, you grant us a non-exclusive, worldwide, perpetual, irrevocable, royalty-free, sub-licensable (through multiple tiers) right to exercise any and all copyright, trademark, publicity, and database rights (but no other rights) you have in the content, in any media known now or in the future."
                            }
                        ],
                        dataList: [
                            {
                                text: "Any serious infringement related aspects will be under the jurisdiction of Supreme Court of India and the Bangalore High Court."
                            },
                            {
                                text: "In-case the content hurts the sentiments of an entire community or a country, you can request us to take it down, but subject to mutual agreement."
                            }
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading9",
                RightBoxClass: "tr_Right_Nav_Header9",
                headerLabel: "Fraud & illegal service prevention",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            
                        ],
                        dataList: [
                            {
                                text: "The website reserves all rights, un-conditionally to take down any CITs/stop payments/delete a " + clientConstants.name_abb + " or " + userConstants.name_abb + " account without any notice, in-case we find any illegal activities associated with your account. However all hearing in this regard will be dealt in the Bangalore high court, India."
                            },
                            {
                                text: "We will be terminating a " + userConstants.name_abb + "/" + clientConstants.name_abb + " account or payment if a law enforcement agency asks us to do so."
                            }
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading10",
                RightBoxClass: "tr_Right_Nav_Header10",
                headerLabel: "Disputes",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: false,
                                text: "As we try to ensure fair trade, any disputes that arise will be solved within the company. Any " + userConstants.name_abb + "/" + clientConstants.name_abb + " can mail help@cautom.com to do so. We will get back to you in 24h and will try to resolve the issue."
                            },
                            {
                                isUnderline: false,
                                text: "If it involves a payment/law enforcement agency, this will be subjects to Indian law system."
                            }
                        ],
                        dataList: [
                           
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading1",
                RightBoxClass: "tr_Right_Nav_Header1",
                headerLabel: "No Insurance or Warranty",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: false,
                                text: "" + companyConstants.fullName + " site do not offer any form of insurance, or other Buyer or Seller protection."
                            }
                        ],
                        dataList: [
                            
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading2",
                RightBoxClass: "tr_Right_Nav_Header2",
                headerLabel: "Right to review",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: false,
                                text: "" + companyConstants.fullName + " site has unconditional rights to access/review all materials uploaded and written on our website by any party involved."
                            }
                        ],
                        dataList: [
                            
                        ]
                    }
                ]
            },
            {
                headerClass: "div_heading3",
                RightBoxClass: "tr_Right_Nav_Header3",
                headerLabel: "Other Information",
                showDiv: true,
                ContentList: [
                    {
                        paragraphList: [
                            {
                                isUnderline: true,
                                text: "In-active accounts"
                            }
                        ],
                        dataList: [
                            {
                                text: "All accounts inactive for at least 3 months will be taken down without any notice. You can reinstate the service by emailing at help@cautom.com"
                            }
                        ]
                    },
                    {
                        paragraphList: [
                            {
                                isUnderline: true,
                                text: "Disclaimer of warranties"
                            }
                        ],
                        dataList: [
                            {
                                text: "" + companyConstants.fullName + " site shall not be liable to any damages, direct, indirect, incidental, and consequential caused due to usage of our website."
                            }
                        ]
                    }
                ]
            }
        ];
        $scope.divTo = function (id) {
            console.log(id);
            var i = 0;
            if (id != -1) {
                for (i = 0; i < 13; i++) {
                    $scope.tncContents[i].showDiv = false;
                }
                $scope.isAllExpanded = false;
                $scope.tncContents[id].showDiv = true;
            } else {
                for (i = 0; i < 13; i++) {
                    $scope.tncContents[i].showDiv = true;
                }
                $scope.isAllExpanded = true;
            }            
        }

    });
});



			

