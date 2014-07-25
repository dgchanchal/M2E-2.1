
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 07/21/2014 18:24:49
-- Generated from EDMX file: F:\pcongo\trunk\M2E\M2E\Models\M2EContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Zestork];
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
IF OBJECT_ID(N'[dbo].[CreateTemplateeditableInstructionsLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateTemplateeditableInstructionsLists];
GO
IF OBJECT_ID(N'[dbo].[CreateTemplateListBoxQuestionsLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateTemplateListBoxQuestionsLists];
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
IF OBJECT_ID(N'[dbo].[ForgetPasswords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ForgetPasswords];
GO
IF OBJECT_ID(N'[dbo].[JobDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobDatas];
GO
IF OBJECT_ID(N'[dbo].[LinkedInAuthApiDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LinkedInAuthApiDatas];
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
IF OBJECT_ID(N'[dbo].[UserPageSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserPageSettings];
GO
IF OBJECT_ID(N'[dbo].[UserRecommendations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRecommendations];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UserSkills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSkills];
GO
IF OBJECT_ID(N'[dbo].[ValidateUserKeys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ValidateUserKeys];
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
    [buttonText] nvarchar(max)  NOT NULL,
    [referenceId] nvarchar(max)  NOT NULL,
    [total] nvarchar(max)  NOT NULL,
    [completed] nvarchar(max)  NOT NULL,
    [verified] nvarchar(max)  NOT NULL,
    [creationTime] nvarchar(max)  NOT NULL,
    [title] nvarchar(max)  NOT NULL
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

-- Creating table 'RecommendedBies'
CREATE TABLE [dbo].[RecommendedBies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RecommendedTo] nvarchar(max)  NOT NULL,
    [RecommendedFrom] nvarchar(max)  NOT NULL
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
    [RegistrationTime] nvarchar(max)  NULL
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

-- Creating table 'ValidateUserKeys'
CREATE TABLE [dbo].[ValidateUserKeys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [guid] nvarchar(max)  NOT NULL
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
    [imgurLink] nvarchar(max)  NOT NULL
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

-- Creating primary key on [Id] in table 'CreateTemplateeditableInstructionsLists'
ALTER TABLE [dbo].[CreateTemplateeditableInstructionsLists]
ADD CONSTRAINT [PK_CreateTemplateeditableInstructionsLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CreateTemplateListBoxQuestionsLists'
ALTER TABLE [dbo].[CreateTemplateListBoxQuestionsLists]
ADD CONSTRAINT [PK_CreateTemplateListBoxQuestionsLists]
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

-- Creating primary key on [Id] in table 'ValidateUserKeys'
ALTER TABLE [dbo].[ValidateUserKeys]
ADD CONSTRAINT [PK_ValidateUserKeys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CreateTemplateImgurImagesLists'
ALTER TABLE [dbo].[CreateTemplateImgurImagesLists]
ADD CONSTRAINT [PK_CreateTemplateImgurImagesLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------