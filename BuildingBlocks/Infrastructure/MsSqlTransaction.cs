using Bigstick.BuildingBlocks.Application.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Infrastructure
{
    public class MsSqlTransaction : ISqlTransaction, IDisposable
    {
        private IDbTransaction dbTransaction;

        private readonly MsSqlConnectionFactory _sqlConnectionFactory;

        public MsSqlTransaction(MsSqlConnectionFactory sqlConnectionFactory) 
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public IDbTransaction Transaction => dbTransaction;

        public IDbTransaction BeginTransaction(IsolationLevel? il = null, IDbConnection dbConnection = null)
        {
            if (dbTransaction != null) 
                throw new Exception("There is a transaction in progress that has not yet been confirmed");
            
            var connection = dbConnection ?? _sqlConnectionFactory.GetOpenConnection();

            dbTransaction = connection.BeginTransaction();

            return dbTransaction;
        }


        public void DoTryCommit()
        {
            var trans =  dbTransaction;
            if (trans != null) trans.Commit();
            dbTransaction = null;
        }

        public void DoTryRollback()
        {
            if (dbTransaction != null) dbTransaction.Rollback();
            dbTransaction = null;
        }

        void IDisposable.Dispose()
        {
            DoTryRollback();
        }
    }
}
