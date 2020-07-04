USE [DiaryApp]
GO

/****** Object:  Table [dbo].[TodoLists]    Script Date: 11.06.2020 18:34:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[EventListsTMP](
	[ID] [int] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[PageID] [int] NOT NULL,
	[DesiresAreaID] [int] NULL,
	[IdeasAreaID] [int] NULL,
 CONSTRAINT [PK_EventListsTMP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO EventListsTMP (ID, Title,PageID, DesiresAreaID, IdeasAreaID)
SELECT ID, Title,PageID, DesiresAreaID, IdeasAreaID FROM EventLists

CREATE TABLE [dbo].[EventsTMP](
	[ID] [int] NOT NULL,
	[Url] [nvarchar](max) NULL,
	[Subject] [nvarchar](200) NULL,
	[OwnerID] [int] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_EventsTMP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT INTO [EventsTMP] (ID, Url, Subject, OwnerID, Date, Description)
SELECT ID, Url, Subject, OwnerID, Date, Description FROM Events

INSERT INTO CommonLists (Title,PageID)
SELECT Title, PageID FROM EventListsTMP

INSERT INTO ListItems (Url, Subject, OwnerID, Date, Discriminator)
SELECT Url, Subject, OwnerID, Date, 'EventItem' FROM EventsTMP

UPDATE ListItems 
SET Discriminator = 'TodoItem'
WHERE Discriminator <> 'TodoItem'

DROP TABLE EventsTMP;
DROP TABLE EventListsTMP;