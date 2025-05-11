using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Data
{
    public interface ISqlTransaction
    {
        IDbTransaction Transaction { get; }
        IDbTransaction BeginTransaction(IsolationLevel? il = null, IDbConnection dbConnection = null);

        void DoTryCommit();

        void DoTryRollback();

    }
}
