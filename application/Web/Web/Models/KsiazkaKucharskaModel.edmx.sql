
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/18/2015 17:46:50
-- Generated from EDMX file: C:\Users\Paweł\Documents\GitHubVisualStudio\ugotuj.to\application\Web\Web\Models\KsiazkaKucharskaModel.edmx
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
    [skladniki] nvarchar(max)  NOT NULL
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------