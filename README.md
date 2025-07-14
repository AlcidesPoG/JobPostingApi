# JobPostingApi

This are the database tables for the project.

CREATE TABLE Companies (
companyId int identity(1,1) primary key,
name nvarchar(100) not null,
email nvarchar(100) not null unique,
photo nvarchar(256) null,
pageUrl nvarchar(256) null,
country nvarchar(100) not null,
state nvarchar(100) not null,
city nvarchar(100) not null,
address nvarchar(250) not null,
description nvarchar(max) null
)
GO
---------------------------------------------------------------------------------

CREATE TABLE Posts (
postId INT IDENTITY PRIMARY KEY,
title NVARCHAR(255) NOT NULL,
location NVARCHAR(255),
type NVARCHAR(50),
category NVARCHAR(100),
description NVARCHAR(MAX),
requirements NVARCHAR(MAX),
salary NVARCHAR(100),
applyUrl NVARCHAR(500),
createdAt DATETIME DEFAULT GETDATE(),
status NVARCHAR(50) DEFAULT 'Active',
companyId int NOt null Foreign key References Companies(companyid)
);

-----------------------------------------------------------------------------------

CREATE TABLE Users (
userId int identity(1,1) primary key,
companyId int not null,
createdAt datetime default getdate(),
name nvarchar(100) not null,
email nvarchar(100) not null unique,
password nvarchar(256) not null,
profileImage nvarchar(256) null,
role nvarchar(50) not null
Foreign key (companyId) References Companies(companyId)
)
GO

--drop table Posts
--drop table Users
--drop table Companies


