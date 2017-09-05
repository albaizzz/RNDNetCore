using System;
using webapi.data.DataModel;
using Microsoft.Extensions.Configuration;
using webapi.data.DataAccess;

namespace webapi.business.BAL
{
    public class TestBAL : BaseBAL
    {
        
        // public TestBAL(IConfiguration configuration) :base(configuration)
        // {

        // }
        public AlipayData GetAlipayData(int ReservationNo)
        {
            DALTest DALTest = new DALTest(Configuration["ConnectionStrings:ReservationEntities:ConnectionStrings"]);
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