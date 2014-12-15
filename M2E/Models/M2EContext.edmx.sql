
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/15/2014 21:06:12
-- Generated from EDMX file: F:\PcOnGo_SVN\branches\M2E-2.0\M2E\Models\M2EContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Zestork2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ClientDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientDetails];
GO
IF OBJECT_ID(N'[dbo].[ClientWallets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientWallets];
GO
IF OBJECT_ID(N'[dbo].[CreateTemplateeditableInstructionsLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateTemplateeditableInstructionsLists];
GO
IF OBJECT_ID(N'[dbo].[CreateTemplateFacebookLikes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateTemplateFacebookLikes];
GO
IF OBJECT_ID(N'[dbo].[CreateTemplateImgurImagesLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateTemplateImgurImagesLists];
GO
IF OBJECT_ID(N'[dbo].[CreateTemplateListBoxQuestionsLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateTemplateListBoxQuestionsLists];
GO
IF OBJECT_ID(N'[dbo].[CreateTemplateModeratingImagesLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateTemplateModeratingImagesLists];
GO
IF OBJECT_ID(N'[dbo].[CreateTemplateMultipleQuestionsLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateTemplateMultipleQuestionsLists];
GO
IF OBJECT_ID(N'[dbo].[CreateTemplateQuestionInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateTemplateQuestionInfoes];
GO
IF OBJECT_ID(N'[dbo].[CreateTemplateSingleQuestionsLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateTemplateSingleQuestionsLists];
GO
IF OBJECT_ID(N'[dbo].[CreateTemplateTextBoxQuestionsLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateTemplateTextBoxQuestionsLists];
GO
IF OBJECT_ID(N'[dbo].[FacebookAuths]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FacebookAuths];
GO
IF OBJECT_ID(N'[dbo].[facebookPageLikeMappings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[facebookPageLikeMappings];
GO
IF OBJECT_ID(N'[dbo].[ForgetPasswords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ForgetPasswords];
GO
IF OBJECT_ID(N'[dbo].[JobDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobDatas];
GO
IF OBJECT_ID(N'[dbo].[LinkedInAuthApiDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LinkedInAuthApiDatas];
GO
IF OBJECT_ID(N'[dbo].[linkedinAuths]', 'U') IS NOT NULL
    DROP TABLE [dbo].[linkedinAuths];
GO
IF OBJECT_ID(N'[dbo].[RecommendedBies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecommendedBies];
GO
IF OBJECT_ID(N'[dbo].[ThirdPartyLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ThirdPartyLogins];
GO
IF OBJECT_ID(N'[dbo].[UserDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDetails];
GO
IF OBJECT_ID(N'[dbo].[UserEarnings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserEarnings];
GO
IF OBJECT_ID(N'[dbo].[UserFacebookLikeJobMappings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserFacebookLikeJobMappings];
GO
IF OBJECT_ID(N'[dbo].[UserJobMappings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserJobMappings];
GO
IF OBJECT_ID(N'[dbo].[UserMultipleJobMappings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMultipleJobMappings];
GO
IF OBJECT_ID(N'[dbo].[UserPageSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserPageSettings];
GO
IF OBJECT_ID(N'[dbo].[UserRecommendations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRecommendations];
GO
IF OBJECT_ID(N'[dbo].[UserReputationMappings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserReputationMappings];
GO
IF OBJECT_ID(N'[dbo].[UserReputations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserReputations];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UserSkills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSkills];
GO
IF OBJECT_ID(N'[dbo].[UserSurveyResults]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSurveyResults];
GO
IF OBJECT_ID(N'[dbo].[UserSurveyResultToBeRevieweds1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSurveyResultToBeRevieweds1];
GO
IF OBJECT_ID(N'[dbo].[ValidateUserKeys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ValidateUserKeys];
GO
IF OBJECT_ID(N'[dbo].[UserEarningHistories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserEarningHistories];
GO
IF OBJECT_ID(N'[dbo].[UserMessages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMessages];
GO
IF OBJECT_ID(N'[dbo].[UserAlerts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAlerts];
GO
IF OBJECT_ID(N'[dbo].[contactUs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[contactUs];
GO
IF OBJECT_ID(N'[dbo].[MobikwikUserPhoneLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MobikwikUserPhoneLists];
GO
IF OBJECT_ID(N'[dbo].[MobikwikUserHistories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MobikwikUserHistories];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ClientDetails'
CREATE TABLE [dbo].[ClientDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompanyName] nvarchar(max)  NULL,
    [Username] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ClientWallets'
CREATE TABLE [dbo].[ClientWallets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [total] nvarchar(max)  NOT NULL,
    [approved] nvarchar(max)  NOT NULL,
    [pending] nvarchar(max)  NOT NULL,
    [currency] nvarchar(max)  NOT NULL,
    [LastUpdated] datetime  NULL
);
GO

-- Creating table 'CreateTemplateeditableInstructionsLists'
CREATE TABLE [dbo].[CreateTemplateeditableInstructionsLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [verified] nvarchar(max)  NOT NULL,
    [assignedTo] nvarchar(max)  NOT NULL,
    [assignTime] nvarchar(max)  NOT NULL,
    [completedAt] nvarchar(max)  NOT NULL,
    [referenceKey] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CreateTemplateFacebookLikes'
CREATE TABLE [dbo].[CreateTemplateFacebookLikes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [referenceId] nvarchar(max)  NOT NULL,
    [totalThreads] nvarchar(max)  NOT NULL,
    [completed] nvarchar(max)  NOT NULL,
    [verified] nvarchar(max)  NOT NULL,
    [creationTime] nvarchar(max)  NOT NULL,
    [title] nvarchar(max)  NOT NULL,
    [subType] nvarchar(max)  NOT NULL,
    [payPerUser] nvarchar(max)  NOT NULL,
    [DateTime] datetime  NULL,
    [pageId] nvarchar(max)  NOT NULL,
    [pageUrl] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CreateTemplateImgurImagesLists'
CREATE TABLE [dbo].[CreateTemplateImgurImagesLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [verified] nvarchar(max)  NOT NULL,
    [assignedTo] nvarchar(max)  NOT NULL,
    [assignTime] nvarchar(max)  NOT NULL,
    [completedAt] nvarchar(max)  NOT NULL,
    [referenceKey] nvarchar(max)  NOT NULL,
    [imgurId] nvarchar(max)  NOT NULL,
    [imgurDeleteHash] nvarchar(max)  NOT NULL,
    [imgurLink] nvarchar(max)  NOT NULL,
    [alocatedCount] int  NULL
);
GO

-- Creating table 'CreateTemplateListBoxQuestionsLists'
CREATE TABLE [dbo].[CreateTemplateListBoxQuestionsLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [Question] nvarchar(max)  NOT NULL,
    [Options] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [verified] nvarchar(max)  NOT NULL,
    [assignedTo] nvarchar(max)  NOT NULL,
    [assignTime] nvarchar(max)  NOT NULL,
    [completedAt] nvarchar(max)  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [referenceKey] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CreateTemplateModeratingImagesLists'
CREATE TABLE [dbo].[CreateTemplateModeratingImagesLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [assignedTo] nvarchar(max)  NOT NULL,
    [assignTime] nvarchar(max)  NOT NULL,
    [completedAt] nvarchar(max)  NOT NULL,
    [referenceKey] nvarchar(max)  NOT NULL,
    [deletehash] nvarchar(max)  NOT NULL,
    [link] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CreateTemplateMultipleQuestionsLists'
CREATE TABLE [dbo].[CreateTemplateMultipleQuestionsLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [Question] nvarchar(max)  NOT NULL,
    [Options] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [verified] nvarchar(max)  NOT NULL,
    [assignedTo] nvarchar(max)  NOT NULL,
    [assignTime] nvarchar(max)  NOT NULL,
    [completedAt] nvarchar(max)  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [referenceKey] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CreateTemplateQuestionInfoes'
CREATE TABLE [dbo].[CreateTemplateQuestionInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [visible] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [referenceId] nvarchar(max)  NOT NULL,
    [totalThreads] nvarchar(max)  NOT NULL,
    [completed] nvarchar(max)  NOT NULL,
    [verified] nvarchar(max)  NOT NULL,
    [creationTime] nvarchar(max)  NOT NULL,
    [title] nvarchar(max)  NOT NULL,
    [subType] nvarchar(max)  NOT NULL,
    [payPerUser] nvarchar(max)  NOT NULL,
    [DateTime] datetime  NULL
);
GO

-- Creating table 'CreateTemplateSingleQuestionsLists'
CREATE TABLE [dbo].[CreateTemplateSingleQuestionsLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [Question] nvarchar(max)  NOT NULL,
    [Options] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [verified] nvarchar(max)  NOT NULL,
    [assignedTo] nvarchar(max)  NOT NULL,
    [assignTime] nvarchar(max)  NOT NULL,
    [completedAt] nvarchar(max)  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [referenceKey] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CreateTemplateTextBoxQuestionsLists'
CREATE TABLE [dbo].[CreateTemplateTextBoxQuestionsLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [Question] nvarchar(max)  NOT NULL,
    [Options] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [verified] nvarchar(max)  NOT NULL,
    [assignedTo] nvarchar(max)  NOT NULL,
    [assignTime] nvarchar(max)  NOT NULL,
    [completedAt] nvarchar(max)  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [referenceKey] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FacebookAuths'
CREATE TABLE [dbo].[FacebookAuths] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [datetime] nvarchar(max)  NOT NULL,
    [facebookId] nvarchar(max)  NOT NULL,
    [facebookUsername] nvarchar(max)  NOT NULL,
    [AuthToken] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'facebookPageLikeMappings'
CREATE TABLE [dbo].[facebookPageLikeMappings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [refKey] nvarchar(max)  NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [PageId] nvarchar(max)  NOT NULL,
    [UserFacebookId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ForgetPasswords'
CREATE TABLE [dbo].[ForgetPasswords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [guid] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'JobDatas'
CREATE TABLE [dbo].[JobDatas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Category] nvarchar(max)  NOT NULL,
    [SubCategory] nvarchar(max)  NOT NULL,
    [Data] nvarchar(max)  NULL,
    [PostedBy] nvarchar(max)  NOT NULL,
    [StartDate] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [EndDate] nvarchar(max)  NULL
);
GO

-- Creating table 'LinkedInAuthApiDatas'
CREATE TABLE [dbo].[LinkedInAuthApiDatas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [oauth_Token] nvarchar(max)  NOT NULL,
    [oauth_TokenSecret] nvarchar(max)  NOT NULL,
    [oauth_verifier] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'linkedinAuths'
CREATE TABLE [dbo].[linkedinAuths] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [oauth_Token] nvarchar(max)  NOT NULL,
    [oauth_TokenSecret] nvarchar(max)  NOT NULL,
    [oauth_verifier] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RecommendedBies'
CREATE TABLE [dbo].[RecommendedBies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RecommendedTo] nvarchar(max)  NOT NULL,
    [RecommendedFrom] nvarchar(max)  NOT NULL,
    [DateTime] datetime  NULL,
    [isValid] nvarchar(max)  NULL,
    [RecommendedFromUsername] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ThirdPartyLogins'
CREATE TABLE [dbo].[ThirdPartyLogins] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [FacebookId] nvarchar(max)  NOT NULL,
    [FacebookAccessToken] nvarchar(max)  NOT NULL,
    [GoogleId] nvarchar(max)  NOT NULL,
    [GoogleAccessToken] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserDetails'
CREATE TABLE [dbo].[UserDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserEarnings'
CREATE TABLE [dbo].[UserEarnings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [total] nvarchar(max)  NOT NULL,
    [approved] nvarchar(max)  NOT NULL,
    [pending] nvarchar(max)  NOT NULL,
    [currency] nvarchar(max)  NOT NULL,
    [LastUpdated] datetime  NULL,
    [userType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserFacebookLikeJobMappings'
CREATE TABLE [dbo].[UserFacebookLikeJobMappings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [refKey] nvarchar(max)  NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [pageLikeTime] datetime  NULL,
    [type] nvarchar(max)  NOT NULL,
    [subType] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [pageId] nvarchar(max)  NOT NULL,
    [pageUrl] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserJobMappings'
CREATE TABLE [dbo].[UserJobMappings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [refKey] nvarchar(max)  NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [startTime] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [endTime] nvarchar(max)  NOT NULL,
    [expectedDeliveryTime] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserMultipleJobMappings'
CREATE TABLE [dbo].[UserMultipleJobMappings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [refKey] nvarchar(max)  NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [startTime] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [subType] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [endTime] nvarchar(max)  NOT NULL,
    [expectedDelivery] nvarchar(max)  NOT NULL,
    [surveyResult] nvarchar(max)  NOT NULL,
    [imageKey] nvarchar(max)  NOT NULL,
    [isFirst] nvarchar(max)  NULL
);
GO

-- Creating table 'UserPageSettings'
CREATE TABLE [dbo].[UserPageSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [PageThemeColor] nvarchar(max)  NULL,
    [LayoutWidth] nvarchar(max)  NULL,
    [TopBar] nvarchar(max)  NULL,
    [SideBar] nvarchar(max)  NULL
);
GO

-- Creating table 'UserRecommendations'
CREATE TABLE [dbo].[UserRecommendations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [TotalRecommendation] nvarchar(max)  NOT NULL,
    [UsefulRecommendation] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserReputationMappings'
CREATE TABLE [dbo].[UserReputationMappings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [subType] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [DateTime] datetime  NULL,
    [reputation] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserReputations'
CREATE TABLE [dbo].[UserReputations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [ReputationScore] nvarchar(max)  NOT NULL,
    [UserBadge] nvarchar(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [isActive] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Source] nvarchar(max)  NOT NULL,
    [guid] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [ImageUrl] nvarchar(max)  NULL,
    [gender] nvarchar(max)  NULL,
    [Locked] nvarchar(max)  NULL,
    [KeepMeSignedIn] nvarchar(max)  NULL,
    [RegistrationTime] nvarchar(max)  NULL,
    [DateTime] datetime  NULL,
    [FacebookLink] nvarchar(max)  NULL,
    [LinkedinLink] nvarchar(max)  NULL,
    [GoogleLink] nvarchar(max)  NULL,
    [fixedGuid] nvarchar(max)  NULL,
    [isVerified] nvarchar(max)  NULL
);
GO

-- Creating table 'UserSkills'
CREATE TABLE [dbo].[UserSkills] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Skill] nvarchar(max)  NOT NULL,
    [Rating] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserSurveyResults'
CREATE TABLE [dbo].[UserSurveyResults] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [refKey] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [answer] nvarchar(max)  NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [questionId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserSurveyResultToBeRevieweds1'
CREATE TABLE [dbo].[UserSurveyResultToBeRevieweds1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [refKey] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [answer] nvarchar(max)  NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [questionId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ValidateUserKeys'
CREATE TABLE [dbo].[ValidateUserKeys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [guid] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserEarningHistories'
CREATE TABLE [dbo].[UserEarningHistories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [subtype] nvarchar(max)  NOT NULL,
    [paymentMode] nvarchar(max)  NOT NULL,
    [title] nvarchar(max)  NOT NULL,
    [amount] nvarchar(max)  NOT NULL,
    [dateTime] datetime  NOT NULL,
    [userType] nvarchar(max)  NOT NULL,
    [currency] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserMessages'
CREATE TABLE [dbo].[UserMessages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [userType] nvarchar(max)  NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [titleText] nvarchar(max)  NOT NULL,
    [bodyText] nvarchar(max)  NOT NULL,
    [dateTime] datetime  NOT NULL,
    [priority] nvarchar(max)  NOT NULL,
    [iconUrl] nvarchar(max)  NOT NULL,
    [messageFrom] nvarchar(max)  NOT NULL,
    [messageTo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserAlerts'
CREATE TABLE [dbo].[UserAlerts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [userType] nvarchar(max)  NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [titleText] nvarchar(max)  NOT NULL,
    [dateTime] nvarchar(max)  NOT NULL,
    [priority] nvarchar(max)  NOT NULL,
    [iconUrl] nvarchar(max)  NOT NULL,
    [messageFrom] nvarchar(max)  NOT NULL,
    [messageTo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'contactUs'
CREATE TABLE [dbo].[contactUs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [emailId] nvarchar(max)  NOT NULL,
    [heading] nvarchar(max)  NOT NULL,
    [message] nvarchar(max)  NOT NULL,
    [dateTime] datetime  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [ReplyMessage] nvarchar(max)  NOT NULL,
    [RepliedBy] nvarchar(max)  NOT NULL,
    [RepliedDateTime] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MobikwikUserPhoneLists'
CREATE TABLE [dbo].[MobikwikUserPhoneLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [MobileNumber] nvarchar(max)  NOT NULL,
    [operator] nvarchar(max)  NOT NULL,
    [operatorID] nvarchar(max)  NOT NULL,
    [circle] nvarchar(max)  NOT NULL,
    [circleID] nvarchar(max)  NOT NULL,
    [dateTime] datetime  NOT NULL,
    [nickName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MobikwikUserHistories'
CREATE TABLE [dbo].[MobikwikUserHistories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [MobileNumber] nvarchar(max)  NOT NULL,
    [nickName] nvarchar(max)  NOT NULL,
    [amount] nvarchar(max)  NOT NULL,
    [dateTime] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ClientDetails'
ALTER TABLE [dbo].[ClientDetails]
ADD CONSTRAINT [PK_ClientDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientWallets'
ALTER TABLE [dbo].[ClientWallets]
ADD CONSTRAINT [PK_ClientWallets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CreateTemplateeditableInstructionsLists'
ALTER TABLE [dbo].[CreateTemplateeditableInstructionsLists]
ADD CONSTRAINT [PK_CreateTemplateeditableInstructionsLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CreateTemplateFacebookLikes'
ALTER TABLE [dbo].[CreateTemplateFacebookLikes]
ADD CONSTRAINT [PK_CreateTemplateFacebookLikes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CreateTemplateImgurImagesLists'
ALTER TABLE [dbo].[CreateTemplateImgurImagesLists]
ADD CONSTRAINT [PK_CreateTemplateImgurImagesLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CreateTemplateListBoxQuestionsLists'
ALTER TABLE [dbo].[CreateTemplateListBoxQuestionsLists]
ADD CONSTRAINT [PK_CreateTemplateListBoxQuestionsLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CreateTemplateModeratingImagesLists'
ALTER TABLE [dbo].[CreateTemplateModeratingImagesLists]
ADD CONSTRAINT [PK_CreateTemplateModeratingImagesLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CreateTemplateMultipleQuestionsLists'
ALTER TABLE [dbo].[CreateTemplateMultipleQuestionsLists]
ADD CONSTRAINT [PK_CreateTemplateMultipleQuestionsLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CreateTemplateQuestionInfoes'
ALTER TABLE [dbo].[CreateTemplateQuestionInfoes]
ADD CONSTRAINT [PK_CreateTemplateQuestionInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CreateTemplateSingleQuestionsLists'
ALTER TABLE [dbo].[CreateTemplateSingleQuestionsLists]
ADD CONSTRAINT [PK_CreateTemplateSingleQuestionsLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CreateTemplateTextBoxQuestionsLists'
ALTER TABLE [dbo].[CreateTemplateTextBoxQuestionsLists]
ADD CONSTRAINT [PK_CreateTemplateTextBoxQuestionsLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FacebookAuths'
ALTER TABLE [dbo].[FacebookAuths]
ADD CONSTRAINT [PK_FacebookAuths]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'facebookPageLikeMappings'
ALTER TABLE [dbo].[facebookPageLikeMappings]
ADD CONSTRAINT [PK_facebookPageLikeMappings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ForgetPasswords'
ALTER TABLE [dbo].[ForgetPasswords]
ADD CONSTRAINT [PK_ForgetPasswords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobDatas'
ALTER TABLE [dbo].[JobDatas]
ADD CONSTRAINT [PK_JobDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LinkedInAuthApiDatas'
ALTER TABLE [dbo].[LinkedInAuthApiDatas]
ADD CONSTRAINT [PK_LinkedInAuthApiDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'linkedinAuths'
ALTER TABLE [dbo].[linkedinAuths]
ADD CONSTRAINT [PK_linkedinAuths]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecommendedBies'
ALTER TABLE [dbo].[RecommendedBies]
ADD CONSTRAINT [PK_RecommendedBies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ThirdPartyLogins'
ALTER TABLE [dbo].[ThirdPartyLogins]
ADD CONSTRAINT [PK_ThirdPartyLogins]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [PK_UserDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserEarnings'
ALTER TABLE [dbo].[UserEarnings]
ADD CONSTRAINT [PK_UserEarnings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserFacebookLikeJobMappings'
ALTER TABLE [dbo].[UserFacebookLikeJobMappings]
ADD CONSTRAINT [PK_UserFacebookLikeJobMappings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserJobMappings'
ALTER TABLE [dbo].[UserJobMappings]
ADD CONSTRAINT [PK_UserJobMappings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserMultipleJobMappings'
ALTER TABLE [dbo].[UserMultipleJobMappings]
ADD CONSTRAINT [PK_UserMultipleJobMappings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserPageSettings'
ALTER TABLE [dbo].[UserPageSettings]
ADD CONSTRAINT [PK_UserPageSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserRecommendations'
ALTER TABLE [dbo].[UserRecommendations]
ADD CONSTRAINT [PK_UserRecommendations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserReputationMappings'
ALTER TABLE [dbo].[UserReputationMappings]
ADD CONSTRAINT [PK_UserReputationMappings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserReputations'
ALTER TABLE [dbo].[UserReputations]
ADD CONSTRAINT [PK_UserReputations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSkills'
ALTER TABLE [dbo].[UserSkills]
ADD CONSTRAINT [PK_UserSkills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSurveyResults'
ALTER TABLE [dbo].[UserSurveyResults]
ADD CONSTRAINT [PK_UserSurveyResults]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSurveyResultToBeRevieweds1'
ALTER TABLE [dbo].[UserSurveyResultToBeRevieweds1]
ADD CONSTRAINT [PK_UserSurveyResultToBeRevieweds1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ValidateUserKeys'
ALTER TABLE [dbo].[ValidateUserKeys]
ADD CONSTRAINT [PK_ValidateUserKeys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserEarningHistories'
ALTER TABLE [dbo].[UserEarningHistories]
ADD CONSTRAINT [PK_UserEarningHistories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserMessages'
ALTER TABLE [dbo].[UserMessages]
ADD CONSTRAINT [PK_UserMessages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserAlerts'
ALTER TABLE [dbo].[UserAlerts]
ADD CONSTRAINT [PK_UserAlerts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'contactUs'
ALTER TABLE [dbo].[contactUs]
ADD CONSTRAINT [PK_contactUs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MobikwikUserPhoneLists'
ALTER TABLE [dbo].[MobikwikUserPhoneLists]
ADD CONSTRAINT [PK_MobikwikUserPhoneLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MobikwikUserHistories'
ALTER TABLE [dbo].[MobikwikUserHistories]
ADD CONSTRAINT [PK_MobikwikUserHistories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------