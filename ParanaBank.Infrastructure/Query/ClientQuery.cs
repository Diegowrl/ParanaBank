using System.Diagnostics.CodeAnalysis;

namespace ParanaBank.Infrastructure.Query
{
    [ExcludeFromCodeCoverage]
    internal static class ClientQuery
    {

        public const string SQL_ADD = @"
			INSERT INTO [dbo].[Client]
			   (Id
			   ,UserName
			   ,Email
			   ,CreatedAt)
		 VALUES
			   (@Id
			   ,@User
			   ,@Email
			   ,@CreatedAt)
		";

        public const string SQL_GET_ALL = @"
			SELECT
				Id,
				UserName,
				Email,
				CreatedAt,
				UpdateAt 
			FROM [dbo].[Client]
		";

        public const string SQL_GET_BY_EMAIL = @"
			SELECT
				Id,
				UserName,
				Email,
				CreatedAt,
				UpdateAt 
			FROM [dbo].[Client]
			WHERE Email = @Email
		";

        public const string SQL_GET_BY_EMAIL_AND_USER = @"
			SELECT
				Id,
				UserName,
				Email,
				CreatedAt,
				UpdateAt 
			FROM [dbo].[Client]
			WHERE Email = @Email and UserName = @User
		";

        public const string SQL_GET_BY_EMAIL_OR_USER = @"
			SELECT
				Id,
				UserName,
				Email,
				CreatedAt,
				UpdateAt 
			FROM [dbo].[Client]
			WHERE Email = @Email or UserName = @User
		";


        public const string SQL_UPDATE = @"
			UPDATE [dbo].[Client]
			SET   UserName = @User,
				  Email =  @Email,
				  UpdateAt = @UpdatedAt
			WHERE Email = @Email or UserName = @User
		";

        public const string SQL_DELETE_BY_EMAIL = @"
			DELETE 
			FROM [dbo].[Client]
			WHERE Email = @Email
		";
    }
}
