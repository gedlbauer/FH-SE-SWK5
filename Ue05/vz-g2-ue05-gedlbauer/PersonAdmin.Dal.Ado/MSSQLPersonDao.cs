using Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAdmin.Dal.Ado
{
    public class MSSQLPersonDao : AdoPersonDao
    {
        public MSSQLPersonDao(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        protected override string LastInsertedIdQuery => "SELECT CAST(scope_identity() AS int)";
    }
}
