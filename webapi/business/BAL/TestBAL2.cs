using System;
using Microsoft.Extensions.Configuration;
using webapi.common;
using webapi.data.DataAccess;
using webapi.data.DataModel;

namespace webapi.business.BAL
{
    public  class TestBAL2{
        protected Configuration Configuration;
        public TestBAL2(Configuration conStr)
        {
            this.Configuration =    conStr;
        }

        public AlipayData GetAlipayData(int ReservationNo)
        {
            DALTest DALTest = new DALTest(Configuration.ConnectionStrings.ReservationEntities.ConnectionStrings);
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