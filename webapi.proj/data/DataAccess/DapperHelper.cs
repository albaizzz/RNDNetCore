using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace webapi.Data.DataAccess
{
    public class DapperHelper : IDisposable
    {
        #region [Constant]

        public static IConfiguration Configuration;
        
        const string DefaultConnectionString = "DefaultConnection";
        #endregion

        #region [Properties]
        private DbProviderFactory _provider;
        private DbConnection _connection;
        private DynamicParameters _params;
        public DbProviderFactory Provider
        {
            get
            {
                return _provider;
            }
        }
        public DbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public object Params
        {
            get
            {
                return _params;
            }
        }

        private bool _handleErrors = false;
        private string _lstError = "";

        public bool HandleExceptions
        {
            get { return _handleErrors; }
            set { _handleErrors = value; }
        }

        public string LastError
        {
            get { return _lstError; }
        }
        #endregion

        #region [Constructors]
        public DapperHelper()
        {
            var builder = new ConfigurationBuilder();
            builder.Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
            Configuration = builder.Build();
            CreateConnection(DefaultConnectionString);
        }

        public DapperHelper(string connectionString)
        {
            var builder = new ConfigurationBuilder();
            builder.Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
            Configuration = builder.Build();
            
            CreateConnection(connectionString);
        }

        public DapperHelper(string connectionString, bool bWebConfig)
        {
            var builder = new ConfigurationBuilder();
            builder.Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
            Configuration = builder.Build();
            
            CreateConnection(connectionString, bWebConfig);
        }

        public DapperHelper(string connectionString, bool bWebConfig, bool bConnectionName, bool useConnectionStringAsLiteral = false)
        {
            var builder = new ConfigurationBuilder();
            builder.Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
            Configuration = builder.Build();

            CreateConnection(connectionString, bWebConfig, bConnectionName, useConnectionStringAsLiteral);
        }
        #endregion

        #region [Connection / Close]
        
        private void CreateConnection(string key, bool bWebConfig = false, bool bConnectionName = true, bool useKeyAsConnectionStringLiteral = false)
        {
            // ConnectionStringSettings css;
            // if (bWebConfig && bConnectionName)
            //     css = ConfigurationManager.ConnectionStrings[key];
            // else if (bWebConfig && !bConnectionName)
            //     css = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().FirstOrDefault(o => o.ConnectionString == key);
            // else if (useKeyAsConnectionStringLiteral)
            //     css = new ConnectionStringSettings("name", key, "System.Data.SqlClient");
            // else
            //     css = new ConnectionStringSettings("name", DBConfigSettings.ConnectionStrings.GetConnectionString(key), DBConfigSettings.ConnectionStrings.GetProviderName(key));


            // if (css == null)
            //     throw new ArgumentException("Invalid or missing connection string . Check if it exists in configuration file.");
            var conStr = Configuration.GetConnectionString("ReservationEntities");
            var prodiverName = Configuration.GetConnectionString("ConnectionStrings:ReservationEntities:ProviderName");
            try
            {
                // _provider = DbProviderFactories.GetFactory(css.ProviderName);

                _connection = _provider.CreateConnection();
                _connection.ConnectionString = conStr;
            }
            catch (Exception ex)
            {
                throw ex;
                // throw new CustomException("DB", "DB Conn Error");
            }
        }

        /// <summary>
        /// DB 연결 닫기
        /// </summary>
        private void Close()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
                _provider = null;
            }
        }
        #endregion

        #region [Parameter]

        public void AddParameter(string name, object value, DbType? dbtype, ParameterDirection? direction, int? size)
        {
            if (_params == null) { _params = new DynamicParameters(); }

            _params.Add(name, value, dbType: dbtype, direction: direction, size: size);
        }

        public void AddParameter(string name, object value = null, DbType? dbtype = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
        {
            if (_params == null) { _params = new DynamicParameters(); }

            _params.Add(name, value: value, dbType: dbtype, direction: direction, size: size, precision: precision, scale: scale);
        }

        public T GetParameter<T>(string name)
        {
            return _params.Get<T>(name);
        }

        /// <summary>
        /// Celaer DynamicParameters 
        /// </summary>
        public void ClearParameter()
        {
            if (_params == null) { return; }

            _params = new DynamicParameters();
        }
        #endregion

        #region [Execute]
        
        public int Execute(string sql, object param = null, CommandType? commandType = default(CommandType?), bool IsTransRequired = false, int? commandTimeout = default(int?))
        {
            int affectedRowsCnt = 0;
            IDbTransaction transaction = null;
            try
            {
                if (IsTransRequired)
                {
                    _connection.Open();
                    transaction = _connection.BeginTransaction();
                }
                affectedRowsCnt = _connection.Execute(sql, param, transaction, commandTimeout, commandType);

                if (IsTransRequired) transaction.Commit();
            }
            catch (Exception ex)
            {
                if (IsTransRequired) transaction.Rollback();

                if (_handleErrors) _lstError = ex.Message;
                else throw ex; // CustomException System, ex
            }
            return affectedRowsCnt;
        }

        public object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            object obj = null;
            try
            {
                obj = _connection.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                if (_handleErrors) _lstError = ex.Message;
                else throw ex; // CustomException System, ex
            }
            return obj;
        }
        #endregion

        #region [Query]
        public IEnumerable<dynamic> Query(string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            try
            {
                return _connection.Query(sql, param, commandType: commandType ?? CommandType.Text);
            }
            catch (Exception ex)
            {
                if (_handleErrors) _lstError = ex.Message;
                else throw ex; // CustomException System, ex
            }
            return null;
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            try
            {
                return _connection.Query<T>(sql, param, commandType: commandType ?? CommandType.Text);
            }
            catch (Exception ex)
            {
                if (_handleErrors) _lstError = ex.Message;
                else throw ex; // CustomException System, ex
            }
            return null;
        }
        #endregion

        #region [QuerySingle]
        
        public dynamic QuerySingle(string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            try
            {
                return _connection.Query(sql, param, commandType: commandType ?? CommandType.Text).FirstOrDefault();
            }
            catch (Exception ex)
            {
                if (_handleErrors) _lstError = ex.Message;
                else throw ex; // CustomException System, ex
            }
            return null;
        }

        public T QuerySingle<T>(string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            try
            {
                return _connection.Query<T>(sql, param, commandType: commandType ?? CommandType.Text).FirstOrDefault();
            }
            catch (Exception ex)
            {
                if (_handleErrors) _lstError = ex.Message;
                else throw ex; // CustomException System, ex
            }
            return default(T);
        }
        #endregion

        #region [IDisposable]
        // Flag: Has Dispose already been called?
        bool disposed = false;


        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                _connection.Close();
                _connection.Dispose();
                _connection = null;
                _provider = null;
            }

            // Free any unmanaged objects(all native resources) here.
            //
            disposed = true;
        }

        //// Implement a finalizer by using destructor style syntax
        //~DBHelper2()
        //{
        //    // Call the overridden Dispose method that contains common cleanup code
        //    // Pass false to indicate the it is not called from Dispose
        //    Dispose(false);
        //}
        #endregion
    }
}
