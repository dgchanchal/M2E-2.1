﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace M2E.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class M2EContext : DbContext
    {
        public M2EContext()
            : base("name=M2EContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ClientDetail> ClientDetails { get; set; }
        public DbSet<CreateTemplateeditableInstructionsList> CreateTemplateeditableInstructionsLists { get; set; }
        public DbSet<CreateTemplateListBoxQuestionsList> CreateTemplateListBoxQuestionsLists { get; set; }
        public DbSet<CreateTemplateMultipleQuestionsList> CreateTemplateMultipleQuestionsLists { get; set; }
        public DbSet<CreateTemplateQuestionInfo> CreateTemplateQuestionInfoes { get; set; }
        public DbSet<CreateTemplateSingleQuestionsList> CreateTemplateSingleQuestionsLists { get; set; }
        public DbSet<CreateTemplateTextBoxQuestionsList> CreateTemplateTextBoxQuestionsLists { get; set; }
        public DbSet<ForgetPassword> ForgetPasswords { get; set; }
        public DbSet<JobData> JobDatas { get; set; }
        public DbSet<LinkedInAuthApiData> LinkedInAuthApiDatas { get; set; }
        public DbSet<RecommendedBy> RecommendedBies { get; set; }
        public DbSet<ThirdPartyLogin> ThirdPartyLogins { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserPageSetting> UserPageSettings { get; set; }
        public DbSet<UserRecommendation> UserRecommendations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<ValidateUserKey> ValidateUserKeys { get; set; }
        public DbSet<CreateTemplateImgurImagesList> CreateTemplateImgurImagesLists { get; set; }
        public DbSet<UserSurveyResultToBeReviewed> UserSurveyResultToBeRevieweds1 { get; set; }
        public DbSet<UserSurveyResult> UserSurveyResults { get; set; }
        public DbSet<UserJobMapping> UserJobMappings { get; set; }
        public DbSet<UserMultipleJobMapping> UserMultipleJobMappings { get; set; }
        public DbSet<CreateTemplateFacebookLike> CreateTemplateFacebookLikes { get; set; }
        public DbSet<UserFacebookLikeJobMapping> UserFacebookLikeJobMappings { get; set; }
        public DbSet<FacebookAuth> FacebookAuths { get; set; }
    }
}
