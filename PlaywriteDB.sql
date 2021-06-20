CREATE DATABASE PlaywriteDB


--Admin and Therapist login details 
CREATE TABLE users(
uID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
firstName VARCHAR(50)  NOT NULL,
lastName VARCHAR(50)  NOT NULL,
email VARCHAR(50)  NOT NULL,
uPassword varbinary(MAX)  NOT NULL,
uSalt varbinary(MAX)  NOT NULL,
uRole BIT  NOT NULL
);


--Caretaker newsletter subscriptions (mailing list)
CREATE TABLE subscriptions(
subID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
email VARCHAR(50)
);

--Physical Newsletters storage
CREATE TABLE newsletters(
nID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
publishedDate DateTime,
cloudID VARCHAR(256),
cloudURL VARCHAR(MAX),
);

--Physical Newsletters storage
CREATE TABLE media(
mID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
publishedDate DateTime,
title VARCHAR(256)
cloudID VARCHAR(256),
cloudURL VARCHAR(MAX),
);


INSERT INTO orders VALUES(1,3,'10/06/2021');


SELECT * FROM users;
SELECT * FROM newsletters;
SELECT * FROM subscriptions;
SELECT * FROM media;