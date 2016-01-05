
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/05/2016 13:46:52
-- Generated from EDMX file: C:\Users\Paweł\Desktop\ugotuj.to\application\Web\Web\Models\KsiazkaKucharskaModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [bpit21_ugotuj];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserRecipe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecipeSet] DROP CONSTRAINT [FK_UserRecipe];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser_Photo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_PhotoSet] DROP CONSTRAINT [FK_UserUser_Photo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserFavorite_Recipe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Favorite_RecipeSet] DROP CONSTRAINT [FK_UserFavorite_Recipe];
GO
IF OBJECT_ID(N'[dbo].[FK_Favorite_RecipeRecipe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Favorite_RecipeSet] DROP CONSTRAINT [FK_Favorite_RecipeRecipe];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProfile_Picture]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Profile_PictureSet] DROP CONSTRAINT [FK_UserProfile_Picture];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PasswordReminderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PasswordReminderSet];
GO
IF OBJECT_ID(N'[dbo].[RecipeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecipeSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[User_PhotoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User_PhotoSet];
GO
IF OBJECT_ID(N'[dbo].[Favorite_RecipeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Favorite_RecipeSet];
GO
IF OBJECT_ID(N'[dbo].[Profile_PictureSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profile_PictureSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PasswordReminderSet'
CREATE TABLE [dbo].[PasswordReminderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [hash_string] nvarchar(max)  NOT NULL,
    [creation_date] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'RecipeSet'
CREATE TABLE [dbo].[RecipeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Preparation] nvarchar(max)  NOT NULL,
    [Photo] nvarchar(max)  NULL,
    [Video] nvarchar(max)  NULL,
    [Creation_Date] datetime  NULL,
    [Preparation_Time] int  NOT NULL,
    [Dificult] int  NULL,
    [Category] nvarchar(max)  NOT NULL,
    [Ingredients] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Salt] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Token] nvarchar(max)  NULL
);
GO

-- Creating table 'User_PhotoSet'
CREATE TABLE [dbo].[User_PhotoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Favorite_RecipeSet'
CREATE TABLE [dbo].[Favorite_RecipeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [RecipeId] int  NOT NULL
);
GO

-- Creating table 'Profile_PictureSet'
CREATE TABLE [dbo].[Profile_PictureSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PasswordReminderSet'
ALTER TABLE [dbo].[PasswordReminderSet]
ADD CONSTRAINT [PK_PasswordReminderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecipeSet'
ALTER TABLE [dbo].[RecipeSet]
ADD CONSTRAINT [PK_RecipeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User_PhotoSet'
ALTER TABLE [dbo].[User_PhotoSet]
ADD CONSTRAINT [PK_User_PhotoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Favorite_RecipeSet'
ALTER TABLE [dbo].[Favorite_RecipeSet]
ADD CONSTRAINT [PK_Favorite_RecipeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Profile_PictureSet'
ALTER TABLE [dbo].[Profile_PictureSet]
ADD CONSTRAINT [PK_Profile_PictureSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'RecipeSet'
ALTER TABLE [dbo].[RecipeSet]
ADD CONSTRAINT [FK_UserRecipe]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRecipe'
CREATE INDEX [IX_FK_UserRecipe]
ON [dbo].[RecipeSet]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'User_PhotoSet'
ALTER TABLE [dbo].[User_PhotoSet]
ADD CONSTRAINT [FK_UserUser_Photo]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser_Photo'
CREATE INDEX [IX_FK_UserUser_Photo]
ON [dbo].[User_PhotoSet]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Favorite_RecipeSet'
ALTER TABLE [dbo].[Favorite_RecipeSet]
ADD CONSTRAINT [FK_UserFavorite_Recipe]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserFavorite_Recipe'
CREATE INDEX [IX_FK_UserFavorite_Recipe]
ON [dbo].[Favorite_RecipeSet]
    ([UserId]);
GO

-- Creating foreign key on [RecipeId] in table 'Favorite_RecipeSet'
ALTER TABLE [dbo].[Favorite_RecipeSet]
ADD CONSTRAINT [FK_Favorite_RecipeRecipe]
    FOREIGN KEY ([RecipeId])
    REFERENCES [dbo].[RecipeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Favorite_RecipeRecipe'
CREATE INDEX [IX_FK_Favorite_RecipeRecipe]
ON [dbo].[Favorite_RecipeSet]
    ([RecipeId]);
GO

-- Creating foreign key on [UserId] in table 'Profile_PictureSet'
ALTER TABLE [dbo].[Profile_PictureSet]
ADD CONSTRAINT [FK_UserProfile_Picture]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProfile_Picture'
CREATE INDEX [IX_FK_UserProfile_Picture]
ON [dbo].[Profile_PictureSet]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'PasswordReminderSet'
ALTER TABLE [dbo].[PasswordReminderSet]
ADD CONSTRAINT [FK_UserPasswordReminder]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPasswordReminder'
CREATE INDEX [IX_FK_UserPasswordReminder]
ON [dbo].[PasswordReminderSet]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------