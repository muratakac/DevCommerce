using DevCommerce.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using DapperExtensions;
using System.Linq;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories
{
    public class BaseRepository<TObject>

          where TObject : class
    {
        private readonly IDbConnection connection;
        public BaseRepository(IConnectionFactory connectionFactory)
        {
            connection = connectionFactory.GetConnection;
        }


        public IQueryable<TObject> All()
        {
            connection.Query<TObject>("").AsQueryable();

            throw new NotImplementedException();
        }


        public string CreateCommand(QueryType queryType, string tableName, params string[] parameters)
        {
            StringBuilder columns = new StringBuilder();
            foreach (var property in typeof(TObject).GetProperties())
            {
                columns.AppendFormat("{0},", property.Name);
            }

            string sqlCommand = string.Empty;

            switch (queryType)
            {
                case QueryType.Insert:
                    sqlCommand = $"insert into {tableName} {""} values {""}";
                    break;
                case QueryType.Update:
                    sqlCommand = $"update {tableName} set {""}";
                    break;
                case QueryType.Delete:
                    sqlCommand = $"delete from {tableName} where {""}";
                    break;
                case QueryType.SelectAll:
                    sqlCommand = $"select {columns.ToString().TrimEnd(',')} from {tableName}";
                    break;
                case QueryType.Select:
                    sqlCommand = $"select {columns.ToString().TrimEnd(',')} from {tableName} where {""}";
                    break;
            }

            return sqlCommand;
        }

    }
}
