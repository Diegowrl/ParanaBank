using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBank.Infrastructure.Query
{
    internal static class ClientQuery
    {
        public const string SQL_GET_ALL= @"
			INSERT INTO dbo.Schedule
			(
				ScheduleId
			)
			VALUES 
			(
				@ScheduleId
			)
		";

        public const string SQL_GET_BY_EMAIL = @"
			select * from [dbo].[Client] where Email = @Email
		";

        public const string SQL_UPDATE = @"
			INSERT INTO dbo.Schedule
			(
				ScheduleId
			)
			VALUES 
			(
				@ScheduleId
			)
		";

        public const string SQL_DELETE_BY_EMAIL = @"
			INSERT INTO dbo.Schedule
			(
				ScheduleId
			)
			VALUES 
			(
				@ScheduleId
			)
		";
    }
}
