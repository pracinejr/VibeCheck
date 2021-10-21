USE [master]
GO

IF db_id('VibeCheck') IS NULL
	CREATE DATABASE VibeCheck
GO

USE [VibeCheck]
GO




DROP TABLE IF EXISTS [user];
DROP TABLE IF EXISTS [venue];
DROP TABLE IF EXISTS [bandMember];
DROP TABLE IF EXISTS [band];
DROP TABLE IF EXISTS [connection];
GO




CREATE TABLE [user] (
  [id] integer PRIMARY KEY IDENTITY,
  [FirebaseUserId] nvarchar(28) NOT NULL,
  [name] nvarchar(50) NOT NULL,
  [email] nvarchar(255) NOT NULL,
  [imageLocation] nvarchar(255)
)
GO

CREATE TABLE [venue] (
  [id] integer PRIMARY KEY IDENTITY,
  [name] nvarchar(50) NOT NULL
)
GO

CREATE TABLE [bandMember] (
  [id] integer PRIMARY KEY IDENTITY,
  [bandId] integer NOT NULL,
  [userId] integer NOT NULL
)
GO

CREATE TABLE [band] (
  [id] integer PRIMARY KEY IDENTITY,
  [name] nvarchar(50) NOT NULL
)
GO

CREATE TABLE [connection] (
  [id] integer PRIMARY KEY IDENTITY,
  [userId] integer NOT NULL,
  [venueId] integer NOT NULL,
  [mutualFriendId] integer,
  [acquaintanceId] integer NOT NULL,
  [dateCreated] datetime NOT NULL,
  [notes] nvarchar(MAX)
)
GO

ALTER TABLE [user] ADD CONSTRAINT ak_user_fbUserId UNIQUE ([FirebaseUserId])
GO

ALTER TABLE [connection] ADD FOREIGN KEY ([venueId]) REFERENCES [venue] ([id])
GO

ALTER TABLE [connection] ADD FOREIGN KEY ([userId]) REFERENCES [user] ([id])
GO

ALTER TABLE [connection] ADD FOREIGN KEY ([acquaintanceId]) REFERENCES [user] ([id])
GO

ALTER TABLE [bandMember] ADD CONSTRAINT fk_bandMember_band FOREIGN KEY ([bandId]) REFERENCES [band] ([id])
GO

ALTER TABLE [bandMember] ADD CONSTRAINT fk_bandMember_user FOREIGN KEY ([userId]) REFERENCES [user] ([id])
GO

ALTER TABLE [connection] ADD FOREIGN KEY ([mutualFriendId]) REFERENCES [user] ([id])
GO

ALTER TABLE bandMember
DROP CONSTRAINT fk_bandMember_band
ALTER TABLE bandMember
ADD CONSTRAINT fk_bandMember_band
FOREIGN KEY (bandId)
REFERENCES band(id)
ON DELETE CASCADE;

ALTER TABLE bandMember
DROP CONSTRAINT fk_bandMember_user
ALTER TABLE bandMember
ADD CONSTRAINT fk_bandMember_user
FOREIGN KEY (userId)
REFERENCES [user](id)
ON DELETE CASCADE;