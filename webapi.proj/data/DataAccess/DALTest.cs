using System;
using System.Data.SqlClient;
using webapi.data.DataModel;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace webapi.data.DataAccess
{
    public class DALTest : BaseDAL
    {
        protected static IConfiguration Configuration;
        public DALTest(string connection)
        {
            _connection = new SqlConnection(connection);
            this._DapperHelper = new DapperHelper(connection, true, false);
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

        }

        public AlipayData GetAlipayOrderData(int ReservationNo)
        {
            try
            {
                _DapperHelper.Connection.Open();
                _DapperHelper.ClearParameter();
                _DapperHelper.AddParameter("@ReservationNo", ReservationNo, DbType.Int32, ParameterDirection.Input);

                return _DapperHelper.QuerySingle<AlipayData>(
                    "RESERVATION.dbo.usp_Payment_Check_Alipay_Order",
                    _DapperHelper.Params,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _DapperHelper.Connection.Close();
            }
        }

        public void getConfig()
        {

        }
    }
}