SET IDENTITY_INSERT [user] ON
INSERT INTO [user]
  ([id], [FirebaseUserId], [Name], [Email], [imageLocation])
VALUES
  (1, 'KhuRov8X5EZnU0VZCEOrJUguzPr2', 'Peter Racine Jr.', 'peter@racine.com', null),
  (2, 'nBxTzukytwR6QaaO35V5re4CeRO2', 'Marcus Smart', 'marcus@smart.com', null),
  (3, 'IfYKjWLjIPUlVxoPPI9c44XVFyb2', 'Jason Tatum', 'jason@tatum.com', null),
  (4, 'ZOVYbJVLobWX6iv0o9hTrinIdkS2', 'Jaylen Brown', 'jaylen@brown.com', null),
  (5, '7RBLiGVlQBhXHX1CuLIylW3Wi153', 'Payton Pritchard', 'payton@pritchard.com', null),
  (6, 'loXEYfClEOZOXAlAYnKTX6tmZmD2', 'Breanna Stewart', 'breanna@stewart.com', null),
  (7, 'PMXtWaAnhOYM16REMmSBaW89MIC3', 'Jonquel Jones', 'jonquel@jones.com', null),
  (8, 'eIqPWZjtHzYsiTvxNCyvZbe3Ni53', 'Brittney Griner', 'brittney@griner.com', null);
SET IDENTITY_INSERT [user] OFF


SET IDENTITY_INSERT [venue] ON
INSERT INTO [venue]
  ([id], [name])
VALUES
  (1, 'The High Watt'),
  (2, 'The Basement'),
  (3, 'Exit In'),
  (4, 'Preservation Pub');
SET IDENTITY_INSERT [venue] OFF


SET IDENTITY_INSERT [band] ON
INSERT INTO [band]
  ([id], [name])
VALUES
  (1, 'Led Zeppelin'),
  (2, 'The Yardbirds'),
  (3, 'The Honeydrippers'),
  (4, 'Page and Plant');
SET IDENTITY_INSERT [band] OFF


SET IDENTITY_INSERT [bandMember] ON
INSERT INTO [bandMember]
  ([id], [bandId], [userId])
VALUES
  (1, 2, 3),
  (2, 1, 1),
  (3, 1, 2),
  (4, 4, 4),
  (5, 3, 5),
  (6, 4, 1);
SET IDENTITY_INSERT [bandMember] OFF


SET IDENTITY_INSERT [connection] ON
INSERT INTO [connection]
  ([id], [userId], [venueId], [mutualFriendId], [acquaintanceId], [dateCreated], [notes])
VALUES
  (1, 1, 4, 4, 5, SYSDATETIME(), 'We had a lot in common. Said he might have a gig for me later this year.'),
  (2, 2, 2, 4, 6, SYSDATETIME(), 'Cool dive bar. Cave-like vibe. Said her family was from my hometown.'),
  (3, 6, 1, 1, 2, SYSDATETIME(), 'VOLTAGE played a great show. I was shocked. It was electric!'),
  (4, 5, 3, 7, 8, SYSDATETIME(), 'We both like pilsners and the "Chicken Soup for the Soul" books.');
SET IDENTITY_INSERT [venue] OFF