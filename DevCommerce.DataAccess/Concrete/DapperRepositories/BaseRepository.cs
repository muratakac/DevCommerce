using Dapper;
using DevCommerce.Core.Extensions;
using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories
{
    //TODO => try catch blokları düzenlenecek. 
    //TODO => Connection Open Close blokları düzenlenecek
    public class BaseRepository<TObject> : IRepository<TObject>

          where TObject : class
    {
        private readonly IDbConnection connection;
        public BaseRepository(IConnectionFactory connectionFactory)
        {
            connection = connectionFactory.GetConnection;
        }

        public IQueryable<TObject> All()
        {
            IEnumerable<TObject> result;
            try
            {
                string sqlCommand = this.CreateCommand(QueryType.SelectAll, typeof(TObject).Name.GetPluralize());
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                result = connection.Query<TObject>(sqlCommand);
            }
            finally
            {
                connection.Close();
            }

            return result.AsQueryable();
        }

        public TObject Find(IDictionary<string, string> keyValues)
        {
            TObject result;
            try
            {
                string sqlCommand = this.CreateCommand(QueryType.SelectByCriteria, typeof(TObject).Name.GetPluralize(), keyValues);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                result = connection.QuerySingle<TObject>(sqlCommand);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public TObject Create(TObject entity)
        {
            int affectedRow;
            int counter = 1;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var dynamicParameters = new DynamicParameters();
            foreach (var property in entity.GetType().GetProperties())
            {
                if (!property.GetGetMethod().IsVirtual && !property.GetGetMethod().IsGenericMethod)
                {
                    parameters.Add(property.Name, property.GetValue(this, null).ToString());
                    dynamicParameters.Add($"@p{counter}", property.GetValue(this, null));
                    counter++;
                }
            }

            try
            {
                string sqlCommand = this.CreateCommand(QueryType.Insert, typeof(TObject).Name.GetPluralize(), parameters);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                affectedRow = connection.Execute(sqlCommand, dynamicParameters);
            }
            finally
            {
                connection.Close();
            }

            return entity;
        }


        private string CreateCommand(QueryType queryType, string tableName, IDictionary<string, string> keyValues = null)
        {
            string sqlCommand = string.Empty;

            switch (queryType)
            {
                case QueryType.Insert:

                    int counter = 1;
                    StringBuilder insertColumns = new StringBuilder();
                    StringBuilder insertValues = new StringBuilder();

                    foreach (var keyValue in keyValues)
                    {
                        insertColumns.Append($"{keyValue.Key},");
                        insertValues.Append($"@p{counter},");
                        counter++;
                    }

                    sqlCommand = $"insert into {tableName} {insertColumns.ToString().TrimEnd(',')} values {insertValues.ToString().TrimEnd(',')}";
                    break;
                case QueryType.Update:
                    sqlCommand = $"update {tableName} set {""}";
                    break;
                case QueryType.Delete:
                    sqlCommand = $"delete from {tableName} where {""}";
                    break;
                case QueryType.SelectAll:
                case QueryType.SelectByCriteria:
                    StringBuilder selectColumns = new StringBuilder();
                    foreach (var property in typeof(TObject).GetProperties())
                    {
                        if (!property.GetGetMethod().IsVirtual && !property.GetGetMethod().IsGenericMethod)
                        {
                            selectColumns.AppendFormat("{0},", property.Name);
                        }
                    }

                    if (keyValues != null && keyValues.Count > 0)
                    {
                        StringBuilder searchCriteria = new StringBuilder();
                        foreach (var keyValue in keyValues)
                        {
                            searchCriteria.Append($"{keyValue.Key}={keyValue.Value},");
                        }
                        sqlCommand = $"select {selectColumns.ToString().TrimEnd(',')} from {tableName} where {searchCriteria.ToString().TrimEnd(',')}";
                    }
                    else
                    {
                        sqlCommand = $"select {selectColumns.ToString().TrimEnd(',')} from {tableName}";
                    }

                    break;
            }

            return sqlCommand;
        }


    }
}
