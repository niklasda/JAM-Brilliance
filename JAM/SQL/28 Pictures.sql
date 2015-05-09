/*
CREATE TABLE dbo.Pictures 
(
	PictureId	INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	SurveyId	INT NOT NULL,
	IsMain		BIT NOT NULL DEFAULT(0),
	IsApproved	BIT NOT NULL DEFAULT(0),
	Picture		varbinary(max) NULL,
	ContentType nvarchar(30) NULL,
	UploadDate  datetime NOT NULL DEFAULT(getdate()),

	CONSTRAINT FK_MPictures_SurveyId_Surveys_SurveyId FOREIGN KEY (SurveyId) REFERENCES dbo.Surveys (SurveyId)
)

*/