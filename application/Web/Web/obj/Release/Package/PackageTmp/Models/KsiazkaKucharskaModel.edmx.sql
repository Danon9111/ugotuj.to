
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/20/2015 21:09:52
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

IF OBJECT_ID(N'[dbo].[FK_UzytkownikPasswordReminder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PasswordReminderSet] DROP CONSTRAINT [FK_UzytkownikPasswordReminder];
GO
IF OBJECT_ID(N'[dbo].[FK_UzytkownikPrzepis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PrzepisSet] DROP CONSTRAINT [FK_UzytkownikPrzepis];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRecipe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecipeSet] DROP CONSTRAINT [FK_UserRecipe];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PrzepisSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PrzepisSet];
GO
IF OBJECT_ID(N'[dbo].[UzytkownikSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UzytkownikSet];
GO
IF OBJECT_ID(N'[dbo].[PasswordReminderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PasswordReminderSet];
GO
IF OBJECT_ID(N'[dbo].[RecipeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecipeSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PrzepisSet'
CREATE TABLE [dbo].[PrzepisSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nazwa] nvarchar(max)  NOT NULL,
    [opis] nvarchar(max)  NOT NULL,
    [przygotowanie] nvarchar(max)  NOT NULL,
    [zdjecie] nvarchar(max)  NULL,
    [film] nvarchar(max)  NULL,
    [data_utworzenia] datetime  NULL,
    [czas_wykonania] int  NOT NULL,
    [trudnosc] int  NULL,
    [kategoria] nvarchar(max)  NOT NULL,
    [skladniki] nvarchar(max)  NOT NULL,
    [UzytkownikId] int  NOT NULL
);
GO

-- Creating table 'UzytkownikSet'
CREATE TABLE [dbo].[UzytkownikSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nazwa] nvarchar(max)  NULL,
    [login] nvarchar(max)  NOT NULL,
    [haslo] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [token] nvarchar(max)  NULL
);
GO

-- Creating table 'PasswordReminderSet'
CREATE TABLE [dbo].[PasswordReminderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [hash_string] nvarchar(max)  NOT NULL,
    [creation_date] nvarchar(max)  NOT NULL,
    [UzytkownikId] int  NOT NULL
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

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PrzepisSet'
ALTER TABLE [dbo].[PrzepisSet]
ADD CONSTRAINT [PK_PrzepisSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UzytkownikSet'
ALTER TABLE [dbo].[UzytkownikSet]
ADD CONSTRAINT [PK_UzytkownikSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

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

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UzytkownikId] in table 'PasswordReminderSet'
ALTER TABLE [dbo].[PasswordReminderSet]
ADD CONSTRAINT [FK_UzytkownikPasswordReminder]
    FOREIGN KEY ([UzytkownikId])
    REFERENCES [dbo].[UzytkownikSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UzytkownikPasswordReminder'
CREATE INDEX [IX_FK_UzytkownikPasswordReminder]
ON [dbo].[PasswordReminderSet]
    ([UzytkownikId]);
GO

-- Creating foreign key on [UzytkownikId] in table 'PrzepisSet'
ALTER TABLE [dbo].[PrzepisSet]
ADD CONSTRAINT [FK_UzytkownikPrzepis]
    FOREIGN KEY ([UzytkownikId])
    REFERENCES [dbo].[UzytkownikSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UzytkownikPrzepis'
CREATE INDEX [IX_FK_UzytkownikPrzepis]
ON [dbo].[PrzepisSet]
    ([UzytkownikId]);
GO

-- Creating foreign key on [UserId] in table 'RecipeSet'
ALTER TABLE [dbo].[RecipeSet]
ADD CONSTRAINT [FK_UserRecipe]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

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
GO

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
GO

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
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Favorite_RecipeRecipe'
CREATE INDEX [IX_FK_Favorite_RecipeRecipe]
ON [dbo].[Favorite_RecipeSet]
    ([RecipeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------