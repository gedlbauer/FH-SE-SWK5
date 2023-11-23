using Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAdmin.Dal.Ado
{
    public class MySQLPersonDao : AdoPersonDao
    {
        public MySQLPersonDao(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        protected override string LastInsertedIdQuery => "SELECT CAST(last_insert_id() AS int)";
    }
}
