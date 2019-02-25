using AppLabs.Dapper.Interfaces;

namespace AppLabs.Dapper.Test
{
    public class LogRepository : Repository<LogMetadata>
    {
        public override string SelectClause => "h.Date, d.ErrorCode, d.Exception, h.ID as Id, h.Level, h.Logger, d.Message, h.RequestId, h.RFC, h.serviceid as ServiceId, d.ElapsedTime as Time";

        public override string FromClause => "Stamping_Header h JOIN Stamping_Detail d ON h.ID = d.ID";

        public override string OrderByClause => "h.ID DESC";

        public LogRepository(IProviderFactory providerFactory) : base(providerFactory)
        {

        }

        public LogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
