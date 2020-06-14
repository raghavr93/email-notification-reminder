USE [SurveyManagement]
GO

/****** Object:  StoredProcedure [dbo].[getReminderRespondents]    Script Date: 15-06-2020 02:46:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Respondents](
	[RespondentId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](100) NULL,
	[EmailId] [varchar](100) NOT NULL,
	[ResponseFlag] [bit] NULL,
	[RespondedDate] [datetime] NULL,
	[ReminderFlag] [bit] NULL,
	[ReminderDate] [datetime] NULL,
	[ReminderCount] [int] NULL,
	[SentDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RespondentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Respondents] ADD  DEFAULT ((0)) FOR [ResponseFlag]
GO

ALTER TABLE [dbo].[Respondents] ADD  DEFAULT ((0)) FOR [ReminderFlag]
GO

ALTER TABLE [dbo].[Respondents] ADD  DEFAULT ((0)) FOR [ReminderCount]
GO

CREATE Procedure [dbo].[getReminderRespondents]
AS
Begin

	Select RespondentId,EmailId from Respondents where SentDate IS not null and (ReminderFlag = 0 OR ReminderCount < 3)

End
GO

GO

CREATE Procedure [dbo].[getRespondents]
AS
Begin

	Select RespondentId,EmailId from Respondents where ResponseFlag = 0 and ReminderFlag = 0

End
GO
GO

Create Procedure [dbo].[updateReminderCount]
(
@RespondentId int 
)AS
Begin
 
 Update Respondents 
 set ReminderFlag = 1,
 ReminderDate = GETDATE(),
 ReminderCount = ReminderCount + 1
 Where RespondentId = @RespondentId

End
GO


GO

Create Procedure [dbo].[updateSent](
@RespondentId int 
)
As
Begin

Update Respondents
Set SentDate = GETDATE()
Where RespondentId = @RespondentId

End
GO


CREATE Procedure [dbo].[updateResponse]
(
@RespondentId int 
)AS
Begin
 
 Update Respondents 
 set ResponseFlag = 1,
 RespondedDate = GETDATE()
 Where RespondentId = @RespondentId

 Select RespondentId,EmailId from Respondents where RespondentId = @RespondentId

End

