USE [WeatherObservations]
GO

CREATE TABLE dbo.Observations
(
	ObservationId int identity (1,1) not null,
	ObsTime varchar(50) null,
	Temp_f float null,
	Temp_c float null,
	RelHum float null,
	Wind_mph float null,
	Pressure_mb float null,
	Dewpt_f float null,
 CONSTRAINT [ObservationId_pk] PRIMARY KEY CLUSTERED 
(
	ObservationId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO


CREATE TABLE [dbo].[AppErrors](
	[ErrorID] [int] IDENTITY(1,1) NOT NULL,
	[ErrorSrc] [varchar](50) NULL,
	[ErrorMsg] [varchar](50) NULL,
	[ClassName] [varchar](50) NULL,
	[MethodName] [varchar](50) NULL,
	[PostDate] [datetime] NULL,
 CONSTRAINT [PK_AppErrors] PRIMARY KEY CLUSTERED 
(
	[ErrorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO