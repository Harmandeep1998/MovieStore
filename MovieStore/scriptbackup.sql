CREATE DATABASE [moviestore]
GO
USE [moviestore]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 8/8/2021 5:49:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[cust_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](100) NULL,
	[last_name] [varchar](100) NULL,
	[address] [varchar](500) NULL,
	[phone_no] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[cust_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[viewCustomer]    Script Date: 8/8/2021 5:49:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[viewCustomer] as
	select cust_id , first_name + ' ' + last_name as name from customer
GO
/****** Object:  Table [dbo].[movies]    Script Date: 8/8/2021 5:49:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movies](
	[movie_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](100) NULL,
	[rating] [float] NULL,
	[release_year] [int] NULL,
	[genre] [varchar](50) NULL,
	[rental_cost] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[movie_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[viewMovie]    Script Date: 8/8/2021 5:49:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[viewMovie] as
	select movie_id , title + ' [ $' + CAST(rental_cost as varchar(10)) + ' ]' as title from movies
GO
/****** Object:  Table [dbo].[rented_movies]    Script Date: 8/8/2021 5:49:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rented_movies](
	[rmid] [int] IDENTITY(1,1) NOT NULL,
	[movie_id_fk] [int] NULL,
	[cust_id_fk] [int] NULL,
	[date_rented] [datetime] NULL,
	[date_returned] [datetime] NULL,
	[rented_cost] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[rmid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[customer] ON 
GO
INSERT [dbo].[customer] ([cust_id], [first_name], [last_name], [address], [phone_no]) VALUES (1, N'Clifton', N'Shelton', N'336 Academy Street', N'6125896324')
GO
INSERT [dbo].[customer] ([cust_id], [first_name], [last_name], [address], [phone_no]) VALUES (2, N'Therese', N'Shepherd', N'225 Route 17 Auckland', N'6125746324')
GO
INSERT [dbo].[customer] ([cust_id], [first_name], [last_name], [address], [phone_no]) VALUES (3, N'Bobbi', N'Shannon', N'769 Front Street South', N'6125749639')
GO
SET IDENTITY_INSERT [dbo].[customer] OFF
GO
SET IDENTITY_INSERT [dbo].[movies] ON 
GO
INSERT [dbo].[movies] ([movie_id], [title], [rating], [release_year], [genre], [rental_cost]) VALUES (1, N'God Father', 5, 1969, N'Drama', 2)
GO
INSERT [dbo].[movies] ([movie_id], [title], [rating], [release_year], [genre], [rental_cost]) VALUES (2, N'Salt', 4, 2019, N'Thriller', 5)
GO
INSERT [dbo].[movies] ([movie_id], [title], [rating], [release_year], [genre], [rental_cost]) VALUES (3, N'Fault in Our Star', 4, 2016, N'Romantic', 5)
GO
SET IDENTITY_INSERT [dbo].[movies] OFF
GO
SET IDENTITY_INSERT [dbo].[rented_movies] ON 
GO
INSERT [dbo].[rented_movies] ([rmid], [movie_id_fk], [cust_id_fk], [date_rented], [date_returned], [rented_cost]) VALUES (5, 2, 1, CAST(N'2021-06-20T23:49:06.777' AS DateTime), NULL, 5)
GO
INSERT [dbo].[rented_movies] ([rmid], [movie_id_fk], [cust_id_fk], [date_rented], [date_returned], [rented_cost]) VALUES (6, 3, 1, CAST(N'2021-06-20T23:49:06.777' AS DateTime), NULL, 5)
GO
INSERT [dbo].[rented_movies] ([rmid], [movie_id_fk], [cust_id_fk], [date_rented], [date_returned], [rented_cost]) VALUES (7, 3, 3, CAST(N'2021-06-20T23:49:06.777' AS DateTime), CAST(N'2021-06-20T23:50:15.703' AS DateTime), 5)
GO
SET IDENTITY_INSERT [dbo].[rented_movies] OFF
GO
ALTER TABLE [dbo].[rented_movies]  WITH CHECK ADD  CONSTRAINT [fk_cust_id_with_rented_movies] FOREIGN KEY([cust_id_fk])
REFERENCES [dbo].[customer] ([cust_id])
GO
ALTER TABLE [dbo].[rented_movies] CHECK CONSTRAINT [fk_cust_id_with_rented_movies]
GO
ALTER TABLE [dbo].[rented_movies]  WITH CHECK ADD  CONSTRAINT [fk_movie_id_with_rented_movies] FOREIGN KEY([movie_id_fk])
REFERENCES [dbo].[movies] ([movie_id])
GO
ALTER TABLE [dbo].[rented_movies] CHECK CONSTRAINT [fk_movie_id_with_rented_movies]
GO
/****** Object:  StoredProcedure [dbo].[prcShowOutRentedMovies]    Script Date: 8/8/2021 5:49:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[prcShowOutRentedMovies]
as
select rmid, first_name + ' ' + last_name as name,address,phone_no, title , rm.rented_cost , date_rented, date_returned
from rented_movies rm join customer c on rm.cust_id_fk = cust_id
join movies m on rm.movie_id_fk = m.movie_id where date_returned is null;
GO
/****** Object:  StoredProcedure [dbo].[prcShowRentedMovies]    Script Date: 8/8/2021 5:49:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[prcShowRentedMovies]
as
select rmid, first_name + ' ' + last_name as name,address,phone_no, title , rm.rented_cost , date_rented, date_returned
from rented_movies rm join customer c on rm.cust_id_fk = cust_id
join movies m on rm.movie_id_fk = m.movie_id;
GO
