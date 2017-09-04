using System;
using webapi.data.DataModel;
using Microsoft.Extensions.Configuration;
using webapi.data.DataAccess;

namespace bal.business
{
    public class TestBAL : BaseBAL
    {
        
        // public TestBAL(IConfiguration configuration) :base(configuration)
        // {

        // }
        public AlipayData GetAlipayData(int ReservationNo)
        {
            DALTest DALTest = new DALTest(Configuration.GetSection("ConnectionStrings:ReservationEntities:ConnectionStrings").ToString());
            AlipayData data = null;
            try
            {
                data = DALTest.GetAlipayOrderData(ReservationNo);
            }
            catch(Exception ex)
            {

            }
            return data;
        }
    }
}