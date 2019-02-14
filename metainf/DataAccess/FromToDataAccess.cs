using metainf.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public FromToDataAccess(MainContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string GetTables(int connectionId)
        {
            Connection connection = _context.Connection.Where(x => x.Id.Equals(connectionId)).FirstOrDefault();

            switch (_configuration.GetValue<string>("MetaDB"))
            {
                case "SqlServer":
                    return GetTablesSqlServer(connection);
                case "Sqlite":
                    return GetTablesSqlite(connection);
                case "MySql":
                    return GetTablesMySql(connection);
                case "Postgres":
                    return GetTablesPostgres(connection);
            }

            return String.Empty;
        }

        public string GetColumns(int connectionId, string table)
        {
            Connection connection = _context.Connection.Where(x => x.Id.Equals(connectionId)).FirstOrDefault();

            switch (_configuration.GetValue<string>("MetaDB"))
            {
                case "SqlServer":
                    return GetColumnsSqlServer(connection, table);
                case "Sqlite":
                    return GetColumnsSqlite(connection, table);
                case "MySql":
                    return GetColumnsMySql(connection, table);
                case "Postgres":
                    return GetColumnsPostgres(connection, table);
            }

            return String.Empty;
        }

        public string GetTablesSqlServer(Connection connection)
        {
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

        public string GetTablesSqlite(Connection connection)
        {
            StringBuilder query = new StringBuilder();
            StringBuilder tables = new StringBuilder();

            query.Append("SELECT ");
            query.Append("  name ");
            query.Append("FROM ");
            query.Append("  sqlite_master ");
            query.Append("WHERE ");
            query.Append("  type='table' ");

            using (SqliteConnection conn = new SqliteConnection("data source=" + connection.Host + @"\" + connection.Database + ".db"))
            {
                conn.Open();
                using (SqliteCommand cmd = new SqliteCommand(query.ToString(), conn))
                {
                    using (SqliteDataReader dr = cmd.ExecuteReader())
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

        public string GetTablesMySql(Connection connection)
        {
            //StringBuilder query = new StringBuilder();
            //StringBuilder tables = new StringBuilder();

            //query.Append("select ");
            //query.Append("  t2.name + '.' + t1.name ");
            //query.Append("from ");
            //query.Append("  sys.tables t1 ");
            //query.Append("  inner join sys.schemas t2 on t2.schema_id = t1.schema_id ");
            //query.Append("order by ");
            //query.Append("  t2.name, ");
            //query.Append("  t1.name ");

            //using (SqlConnection conn = new SqlConnection("data source=" + connection.Host + ";user id=" + connection.Login + ";password=" + connection.Password + ";initial catalog=" + connection.Database + ";"))
            //{
            //    conn.Open();
            //    using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
            //    {
            //        using (SqlDataReader dr = cmd.ExecuteReader())
            //        {
            //            while (dr.Read())
            //            {
            //                tables.Append("<option id=\"" + dr[0].ToString() + "\">" + dr[0].ToString() + "</option>");
            //            }
            //        }
            //    }
            //}

            //return tables.ToString();
            return String.Empty;
        }

        public string GetTablesPostgres(Connection connection)
        {
            //StringBuilder query = new StringBuilder();
            //StringBuilder tables = new StringBuilder();

            //query.Append("select ");
            //query.Append("  t2.name + '.' + t1.name ");
            //query.Append("from ");
            //query.Append("  sys.tables t1 ");
            //query.Append("  inner join sys.schemas t2 on t2.schema_id = t1.schema_id ");
            //query.Append("order by ");
            //query.Append("  t2.name, ");
            //query.Append("  t1.name ");

            //using (SqlConnection conn = new SqlConnection("data source=" + connection.Host + ";user id=" + connection.Login + ";password=" + connection.Password + ";initial catalog=" + connection.Database + ";"))
            //{
            //    conn.Open();
            //    using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
            //    {
            //        using (SqlDataReader dr = cmd.ExecuteReader())
            //        {
            //            while (dr.Read())
            //            {
            //                tables.Append("<option id=\"" + dr[0].ToString() + "\">" + dr[0].ToString() + "</option>");
            //            }
            //        }
            //    }
            //}

            //return tables.ToString();
            return String.Empty;
        }

        public string GetColumnsSqlServer(Connection connection, string table)
        {
            StringBuilder query = new StringBuilder();
            StringBuilder columns = new StringBuilder();

            query.Append("select ");
            query.Append("    t3.name + '|' + t4.name + '|' + cast(t3.max_length as VARCHAR) + ',' + cast(t3.precision as VARCHAR) + case t3.is_nullable when 1 then '|nullable' else '|not null' end ");
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

        public string GetColumnsSqlite(Connection connection, string table)
        {
            StringBuilder query = new StringBuilder();
            StringBuilder columns = new StringBuilder();

            query.Append("select ");
            query.Append("  name || ");
            query.Append("  '|' || ");
            query.Append("  case when substr(type, 0, instr(type, '(') - 1) = '' then type else substr(type, 0, instr(type, '(') - 1) end || ");
            query.Append("  '|' || ");
            query.Append("  case when REPLACE(REPLACE(REPLACE(substr(type, instr(type, '('),  instr(type, ')')), '(', ''), ')', ''), ' ', '') = '' then 'no length' else REPLACE(REPLACE(REPLACE(substr(type, instr(type, '('), instr(type, ')')), '(', ''), ')', ''), ' ', '') end || ");
            query.Append("  '|' || ");
            query.Append("  case when[notnull] = 0 then 'nullable' else 'not null' end ");
            query.Append("from ");
            query.Append("  pragma_table_info('" + table + "') ");

            using (SqliteConnection conn = new SqliteConnection("data source=" + connection.Host + @"\" + connection.Database + ".db"))
            {
                conn.Open();
                using (SqliteCommand cmd = new SqliteCommand(query.ToString(), conn))
                {
                    using (SqliteDataReader dr = cmd.ExecuteReader())
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

        public string GetColumnsMySql(Connection connection, string table)
        {
            //StringBuilder query = new StringBuilder();
            //StringBuilder columns = new StringBuilder();

            //query.Append("select ");
            //query.Append("    t3.name + '|' + t4.name + '|' + cast(t3.max_length as VARCHAR) + ',' + cast(t3.precision as VARCHAR) + case t3.is_nullable when 1 then '|nullable' else '|not nullable' end ");
            //query.Append("from ");
            //query.Append("    sys.tables t1 ");
            //query.Append("    inner join sys.schemas t2 on t2.schema_id = t1.schema_id ");
            //query.Append("    inner join sys.columns t3 on t3.object_id = t1.object_id ");
            //query.Append("    inner join sys.types t4 on t4.system_type_id = t3.system_type_id ");
            //query.Append("where ");
            //query.Append("    t1.name = '" + table.Split('.')[1] + "' and ");
            //query.Append("    t2.name = '" + table.Split('.')[0] + "' ");
            //query.Append("order BY ");
            //query.Append("    t3.name ");

            //using (SqlConnection conn = new SqlConnection("data source=" + connection.Host + ";user id=" + connection.Login + ";password=" + connection.Password + ";initial catalog=" + connection.Database + ";"))
            //{
            //    conn.Open();
            //    using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
            //    {
            //        using (SqlDataReader dr = cmd.ExecuteReader())
            //        {
            //            while (dr.Read())
            //            {
            //                columns.Append("<option id=\"" + dr[0].ToString() + "\"data-content=\"" + dr[0].ToString().Split('|')[0] + " <span class='badge badge-secondary'>" + dr[0].ToString().Split('|')[1] + "</span> <span class='badge badge-secondary'>" + dr[0].ToString().Split('|')[2] + "</span> <span class='badge badge-secondary'>" + dr[0].ToString().Split('|')[3] + "</span>\">" + dr[0].ToString() + "</option>");
            //            }
            //        }
            //    }
            //}

            //return columns.ToString();
            return String.Empty;
        }

        public string GetColumnsPostgres(Connection connection, string table)
        {
            //StringBuilder query = new StringBuilder();
            //StringBuilder columns = new StringBuilder();

            //query.Append("select ");
            //query.Append("    t3.name + '|' + t4.name + '|' + cast(t3.max_length as VARCHAR) + ',' + cast(t3.precision as VARCHAR) + case t3.is_nullable when 1 then '|nullable' else '|not nullable' end ");
            //query.Append("from ");
            //query.Append("    sys.tables t1 ");
            //query.Append("    inner join sys.schemas t2 on t2.schema_id = t1.schema_id ");
            //query.Append("    inner join sys.columns t3 on t3.object_id = t1.object_id ");
            //query.Append("    inner join sys.types t4 on t4.system_type_id = t3.system_type_id ");
            //query.Append("where ");
            //query.Append("    t1.name = '" + table.Split('.')[1] + "' and ");
            //query.Append("    t2.name = '" + table.Split('.')[0] + "' ");
            //query.Append("order BY ");
            //query.Append("    t3.name ");

            //using (SqlConnection conn = new SqlConnection("data source=" + connection.Host + ";user id=" + connection.Login + ";password=" + connection.Password + ";initial catalog=" + connection.Database + ";"))
            //{
            //    conn.Open();
            //    using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
            //    {
            //        using (SqlDataReader dr = cmd.ExecuteReader())
            //        {
            //            while (dr.Read())
            //            {
            //                columns.Append("<option id=\"" + dr[0].ToString() + "\"data-content=\"" + dr[0].ToString().Split('|')[0] + " <span class='badge badge-secondary'>" + dr[0].ToString().Split('|')[1] + "</span> <span class='badge badge-secondary'>" + dr[0].ToString().Split('|')[2] + "</span> <span class='badge badge-secondary'>" + dr[0].ToString().Split('|')[3] + "</span>\">" + dr[0].ToString() + "</option>");
            //            }
            //        }
            //    }
            //}

            //return columns.ToString();
            return String.Empty;
        }
    }
}