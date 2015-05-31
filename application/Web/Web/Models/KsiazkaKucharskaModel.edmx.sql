
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/31/2015 18:48:23
-- Generated from EDMX file: C:\Users\Paweł\Source\Repos\ugotuj.to\application\Web\Web\Models\KsiazkaKucharskaModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ksiazka_kucharska];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PrzepisSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PrzepisSet];
GO
IF OBJECT_ID(N'[dbo].[UzytkownikSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UzytkownikSet];
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

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------