using metainf.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metainf.DataAccess
{
    public class FromToDataAccess
    {
        private readonly MainContext _context;

        public FromToDataAccess(MainContext context)
        {
            _context = context;
        }

        public string GetTables(int connectionId)
        {
            Connection connection = _context.Connection.Where(x => x.Id.Equals(connectionId)).FirstOrDefault();
            StringBuilder query = new StringBuilder();
            StringBuilder tables = new StringBuilder();

            query.Append("select ");
            query.Append("  t2.name + '.' + t1.name ");
            query.Append("from ");
            query.Append("  sys.tables t1 ");
            query.Append("  inner join sys.schemas t2 on t2.schema_id = t1.schema_id ");
            query.Append("order by ");
            query.Append("  t2.name, ");
            query.Append("  t1.name ");

            using (SqlConnection conn = new SqlConnection("data source=" + connection.Host + ";user id=" + connection.Login + ";password=" + connection.Password + ";initial catalog=" + connection.Database + ";"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            tables.Append("<option id=\"" + dr[0].ToString() + "\">" + dr[0].ToString() + "</option>");
                        }
                    }
                }
            }

            return tables.ToString();
        }

        public string GetColumns(int connectionId, string table)
        {
            Connection connection = _context.Connection.Where(x => x.Id.Equals(connectionId)).FirstOrDefault();
            StringBuilder query = new StringBuilder();
            StringBuilder columns = new StringBuilder();

            query.Append("select ");
            query.Append("    t3.name + '|' + t4.name + '|' + cast(t3.max_length as VARCHAR) + ',' + cast(t3.precision as VARCHAR) + case t3.is_nullable when 1 then '|nullable' else '|not nullable' end ");
            query.Append("from ");
            query.Append("    sys.tables t1 ");
            query.Append("    inner join sys.schemas t2 on t2.schema_id = t1.schema_id ");
            query.Append("    inner join sys.columns t3 on t3.object_id = t1.object_id ");
            query.Append("    inner join sys.types t4 on t4.system_type_id = t3.system_type_id ");
            query.Append("where ");
            query.Append("    t1.name = '" + table.Split('.')[1] + "' and ");
            query.Append("    t2.name = '" + table.Split('.')[0] + "' ");
            query.Append("order BY ");
            query.Append("    t3.name ");

            using (SqlConnection conn = new SqlConnection("data source=" + connection.Host + ";user id=" + connection.Login + ";password=" + connection.Password + ";initial catalog=" + connection.Database + ";"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            columns.Append("<option id=\"" + dr[0].ToString() + "\"data-content=\"" + dr[0].ToString().Split('|')[0] + " <span class='badge badge-secondary'>" + dr[0].ToString().Split('|')[1] + "</span> <span class='badge badge-secondary'>" + dr[0].ToString().Split('|')[2] + "</span> <span class='badge badge-secondary'>" + dr[0].ToString().Split('|')[3] + "</span>\">" + dr[0].ToString() + "</option>");
                        }
                    }
                }
            }

            return columns.ToString();
        }
    }
}