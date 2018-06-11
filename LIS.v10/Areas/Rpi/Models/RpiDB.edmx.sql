
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/15/2018 15:14:06
-- Generated from EDMX file: C:\Users\VILLOSA\Documents\GithubClassic\Lis.2018.03\LIS.v10\Areas\Rpi\Models\RpiDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-LIS.v10-20170509105835];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RpiDeviceRpiDatalog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RpiDatalogs] DROP CONSTRAINT [FK_RpiDeviceRpiDatalog];
GO
IF OBJECT_ID(N'[dbo].[FK_RpiDeviceRpiControl]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RpiControls] DROP CONSTRAINT [FK_RpiDeviceRpiControl];
GO
IF OBJECT_ID(N'[dbo].[FK_RpiDeviceRpiVersion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RpiVersions] DROP CONSTRAINT [FK_RpiDeviceRpiVersion];
GO
IF OBJECT_ID(N'[dbo].[FK_RpiComponentRpiVersionMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RpiVersionMaps] DROP CONSTRAINT [FK_RpiComponentRpiVersionMap];
GO
IF OBJECT_ID(N'[dbo].[FK_RpiVersionRpiVersionMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RpiVersionMaps] DROP CONSTRAINT [FK_RpiVersionRpiVersionMap];
GO
IF OBJECT_ID(N'[dbo].[FK_RpiComponentRpiDataType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RpiDataTypes] DROP CONSTRAINT [FK_RpiComponentRpiDataType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[RpiDevices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RpiDevices];
GO
IF OBJECT_ID(N'[dbo].[RpiDatalogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RpiDatalogs];
GO
IF OBJECT_ID(N'[dbo].[RpiControls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RpiControls];
GO
IF OBJECT_ID(N'[dbo].[RpiVersions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RpiVersions];
GO
IF OBJECT_ID(N'[dbo].[RpiVersionMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RpiVersionMaps];
GO
IF OBJECT_ID(N'[dbo].[RpiComponents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RpiComponents];
GO
IF OBJECT_ID(N'[dbo].[RpiDataTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RpiDataTypes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'RpiDevices'
CREATE TABLE [dbo].[RpiDevices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [RpiVersionId] int  NOT NULL
);
GO

-- Creating table 'RpiDatalogs'
CREATE TABLE [dbo].[RpiDatalogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DtRead] nvarchar(max)  NOT NULL,
    [DataRead] nvarchar(max)  NOT NULL,
    [RpiDeviceId] int  NOT NULL
);
GO

-- Creating table 'RpiControls'
CREATE TABLE [dbo].[RpiControls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DtSchedule] nvarchar(max)  NOT NULL,
    [Data] nvarchar(max)  NOT NULL,
    [RpiDeviceId] int  NOT NULL
);
GO

-- Creating table 'RpiVersions'
CREATE TABLE [dbo].[RpiVersions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [VersionNo] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RpiVersionMaps'
CREATE TABLE [dbo].[RpiVersionMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameMap] nvarchar(max)  NOT NULL,
    [PinNo] nvarchar(max)  NOT NULL,
    [RpiComponentId] int  NOT NULL,
    [RpiVersionId] int  NOT NULL
);
GO

-- Creating table 'RpiComponents'
CREATE TABLE [dbo].[RpiComponents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ComponentName] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [RpiDataTypeId] int  NOT NULL
);
GO

-- Creating table 'RpiDataTypes'
CREATE TABLE [dbo].[RpiDataTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'RpiDevices'
ALTER TABLE [dbo].[RpiDevices]
ADD CONSTRAINT [PK_RpiDevices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RpiDatalogs'
ALTER TABLE [dbo].[RpiDatalogs]
ADD CONSTRAINT [PK_RpiDatalogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RpiControls'
ALTER TABLE [dbo].[RpiControls]
ADD CONSTRAINT [PK_RpiControls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RpiVersions'
ALTER TABLE [dbo].[RpiVersions]
ADD CONSTRAINT [PK_RpiVersions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RpiVersionMaps'
ALTER TABLE [dbo].[RpiVersionMaps]
ADD CONSTRAINT [PK_RpiVersionMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RpiComponents'
ALTER TABLE [dbo].[RpiComponents]
ADD CONSTRAINT [PK_RpiComponents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RpiDataTypes'
ALTER TABLE [dbo].[RpiDataTypes]
ADD CONSTRAINT [PK_RpiDataTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RpiDeviceId] in table 'RpiDatalogs'
ALTER TABLE [dbo].[RpiDatalogs]
ADD CONSTRAINT [FK_RpiDeviceRpiDatalog]
    FOREIGN KEY ([RpiDeviceId])
    REFERENCES [dbo].[RpiDevices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RpiDeviceRpiDatalog'
CREATE INDEX [IX_FK_RpiDeviceRpiDatalog]
ON [dbo].[RpiDatalogs]
    ([RpiDeviceId]);
GO

-- Creating foreign key on [RpiDeviceId] in table 'RpiControls'
ALTER TABLE [dbo].[RpiControls]
ADD CONSTRAINT [FK_RpiDeviceRpiControl]
    FOREIGN KEY ([RpiDeviceId])
    REFERENCES [dbo].[RpiDevices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RpiDeviceRpiControl'
CREATE INDEX [IX_FK_RpiDeviceRpiControl]
ON [dbo].[RpiControls]
    ([RpiDeviceId]);
GO

-- Creating foreign key on [RpiComponentId] in table 'RpiVersionMaps'
ALTER TABLE [dbo].[RpiVersionMaps]
ADD CONSTRAINT [FK_RpiComponentRpiVersionMap]
    FOREIGN KEY ([RpiComponentId])
    REFERENCES [dbo].[RpiComponents]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RpiComponentRpiVersionMap'
CREATE INDEX [IX_FK_RpiComponentRpiVersionMap]
ON [dbo].[RpiVersionMaps]
    ([RpiComponentId]);
GO

-- Creating foreign key on [RpiDataTypeId] in table 'RpiComponents'
ALTER TABLE [dbo].[RpiComponents]
ADD CONSTRAINT [FK_RpiDataTypeRpiComponent]
    FOREIGN KEY ([RpiDataTypeId])
    REFERENCES [dbo].[RpiDataTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RpiDataTypeRpiComponent'
CREATE INDEX [IX_FK_RpiDataTypeRpiComponent]
ON [dbo].[RpiComponents]
    ([RpiDataTypeId]);
GO

-- Creating foreign key on [RpiVersionId] in table 'RpiDevices'
ALTER TABLE [dbo].[RpiDevices]
ADD CONSTRAINT [FK_RpiVersionRpiDevice]
    FOREIGN KEY ([RpiVersionId])
    REFERENCES [dbo].[RpiVersions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RpiVersionRpiDevice'
CREATE INDEX [IX_FK_RpiVersionRpiDevice]
ON [dbo].[RpiDevices]
    ([RpiVersionId]);
GO

-- Creating foreign key on [RpiVersionId] in table 'RpiVersionMaps'
ALTER TABLE [dbo].[RpiVersionMaps]
ADD CONSTRAINT [FK_RpiVersionRpiVersionMap]
    FOREIGN KEY ([RpiVersionId])
    REFERENCES [dbo].[RpiVersions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RpiVersionRpiVersionMap'
CREATE INDEX [IX_FK_RpiVersionRpiVersionMap]
ON [dbo].[RpiVersionMaps]
    ([RpiVersionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------