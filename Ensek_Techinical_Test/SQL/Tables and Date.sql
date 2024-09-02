﻿CREATE DATABASE Ensek;
GO

USE Ensek;
GO

CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeterReading]    Script Date: 02/09/2024 16:15:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeterReading](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NULL,
	[MeterReadingDateTime] [datetime] NOT NULL,
	[MeterReadValue] [decimal](8, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1234, N'Freya', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1239, N'Noddy', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1240, N'Archie', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1241, N'Lara', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1242, N'Tim', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1243, N'Graham', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1244, N'Tony', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1245, N'Neville', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1246, N'Jo', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1247, N'Jim', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1248, N'Pam', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2233, N'Barry', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2344, N'Tommy', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2345, N'Jerry', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2346, N'Ollie', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2347, N'Tara', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2348, N'Tammy', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2349, N'Simon', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2350, N'Colin', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2351, N'Gladys', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2352, N'Greg', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2353, N'Tony', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2355, N'Arthur', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2356, N'Craig', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (4534, N'JOSH', N'TEST')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (6776, N'Laura', N'Test')
GO
INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (8766, N'Sally', N'Test')
GO
ALTER TABLE [dbo].[MeterReading]  WITH CHECK ADD FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
