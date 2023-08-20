namespace ITPLibrary.Data.DataProvider;

public class SqlConnector
{
    #region Private Fields

    private readonly string _connectionString;

    #endregion Private Fields

    #region Public Constructors

    public SqlConnector(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string cannot be null or empty.", nameof(connectionString));
        }

        _connectionString = connectionString;
    }

    #endregion Public Constructors

    #region Public Methods

    public SqlConnection CreateConnection()
    {
        var sqlConnection = new SqlConnection(_connectionString);

        return sqlConnection;
    }

    public DataSet ExecuteQuery(string sql, params SqlParameter[] sqlParameters)
    {
        using (var sqlConnection = CreateConnection())
        {
            sqlConnection.Open();

            var sqlCommand = GetSqlCommand(sql, sqlParameters, sqlConnection);

            var dataSet = GetDataSet(sqlCommand);

            sqlConnection.Close();

            return dataSet;
        }
    }

    #endregion Public Methods

    #region Private Methods

    private static SqlCommand GetSqlCommand(string sql, SqlParameter[] sqlParameters, SqlConnection sqlConnection)
    {
        var sqlCommand = sqlConnection.CreateCommand();

        sqlCommand.CommandText = sql;

        if (sqlParameters != null && sqlParameters.Length > 0)
        {
            AdjustSqlParameters(sqlParameters);

            sqlCommand.Parameters.AddRange(sqlParameters);
        }

        return sqlCommand;
    }

    private static void AdjustSqlParameters(IEnumerable<SqlParameter> sqlParameters)
    {
        foreach (var sqlParameter in sqlParameters)
        {
            if (sqlParameter.SqlDbType == SqlDbType.NVarChar)
            {
                sqlParameter.SqlDbType = SqlDbType.VarChar;
            }
        }
    }

    private DataSet GetDataSet(SqlCommand sqlCommand)
    {
        var dataSet = new DataSet
        {
            Locale = CultureInfo.InvariantCulture
        };

        using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
        {
            sqlDataAdapter.Fill(dataSet);
        }

        return dataSet;
    }

    #endregion Private Methods
}
