using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Data.SqlClient;
using System.IO;

namespace webapi.data.DataAccess
{      
    public class BaseDAL :IDisposable
    {
        public static IConfiguration Configuration; 
        public BaseDAL()
        {
            // var builder = new ConfigurationBuilder();
            // var path = Directory.GetCurrentDirectory() + "/appsettings.json";
            // builder.Add(new JsonConfigurationSource { Path = path})
            // .Build();
            // Configuration = builder.Build();
        }

        private SqlConnection _sqlConnection;
        private DapperHelper _dapperHelper;

        public SqlConnection  _connection
        {
            get{
                return _sqlConnection;
            }
            set{
                _sqlConnection = value;
            }
        }

        public DapperHelper _DapperHelper { get{ return _dapperHelper; } set {_dapperHelper = value;} }

        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

           if (disposing)
            {
                // Free any other managed objects here.
                if (_DapperHelper != null)
                {
                    _DapperHelper.Dispose();
                    _DapperHelper = null;
                }
            }

           // Free any unmanaged objects here.
            disposed = true;
        }

       ~BaseDAL()
        {
            Dispose(false);
        }
    }
}