USE [WeatherObservations]
GO
/****** Object:  StoredProcedure [dbo].[InsertIntoAppErrors]    Script Date: 11/19/2012 11:29:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[InsertIntoAppErrors]
@ErrorSrc varchar(50),
@ErrorMsg varchar(50),
@ClassName	varchar(50),
@MethodName varchar(50),
@PostDate	datetime
as 
insert into AppErrors 
values (@ErrorSrc, @ErrorMsg,@ClassName,@MethodName, @PostDate)
