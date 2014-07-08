IF NOT exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		CREATE TABLE [dbo].[Users] (
			[User_ID] INT IDENTITY (1, 1) NOT NULL CONSTRAINT [PK_Users] PRIMARY KEY,
			[SID] VARCHAR(50) NOT NULL,
			[Name] VARCHAR(255) NOT NULL
		)
	END
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[GetUser]
GO
CREATE PROCEDURE [dbo].[GetUser]
@User_ID	INT OUTPUT,
@SID	VARCHAR(50),
@Name	VARCHAR(255)
AS
	BEGIN
		SELECT @User_ID = [User_ID] FROM [Users] WHERE [SID] = @SID;
		IF @User_ID IS NULL
			BEGIN
				INSERT INTO [Users]( [SID], [Name] ) VALUES( @SID, @Name );
				SELECT @User_ID = @@IDENTITY;
			END
		SELECT Users.*, Teams.Name AS TeamName, Teams.Team_ID FROM Users
		LEFT JOIN [UserTeams] ON [UserTeams].[User_ID] = [Users].[User_ID]
		LEFT JOIN [Teams] ON [Teams].[Team_ID] = [UserTeams].[Team_ID];
	END
GO