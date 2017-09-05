using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog.Extensions.Logging;

namespace webapi.common.Logging
{
    public class Logging
    {
        public string SessionID { get; set; }

        private string _loggingType;
        private string _loggingName;

        public Logging(string loggingType, string loggingName = "")
        {
            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("nlog.config");
            LogManager.GetCurrentClassLogger();
            SessionID = "asdas";//LoggingID.GetID();
            _loggingType = loggingType;

            if (loggingName.ToLower() == "hotel") { _loggingName = "HotelLog"; }
            else if (loggingName.ToLower() == "flight") { _loggingName = "FlightLog"; }
            else if (loggingName.ToLower() == "searchflight") { _loggingName = "SearchFlight"; }
            else if (loggingName.ToLower() == "bookingflight") { _loggingName = "BookingFlight"; }
            else if (loggingName.ToLower() == "payment") { _loggingName = "PaymentLog"; }
            else { _loggingName = "CommonLog"; }
        }

        public void Debug(string message, string section = "")
        {
            var loggingType = (string.IsNullOrWhiteSpace(section)) ? _loggingType : _loggingType + "." + section;
            var logger = LogManager.GetLogger(_loggingName);
            var logEvent = new LogEventInfo(LogLevel.Debug, loggingType, message);

            logEvent.Properties["SessionID"] = SessionID;

            logger.Log(logEvent);
        }

        public void Info(string message, string section = "")
        {
            var loggingType = (string.IsNullOrWhiteSpace(section)) ? _loggingType : _loggingType + "." + section;
            var logger = LogManager.GetLogger(_loggingName);
            var logEvent = new LogEventInfo(LogLevel.Info, loggingType, message);

            logEvent.Properties["SessionID"] = SessionID;

            logger.Log(logEvent);
        }

        public void Error(Exception e)
        {
            var loggingType = _loggingType + ".error";
            var logger = LogManager.GetLogger(_loggingName);
            var logEvent = new LogEventInfo();

            logEvent.Level = LogLevel.Error;
            logEvent.LoggerName = loggingType;
            logEvent.Exception = e;
            logEvent.Properties["SessionID"] = SessionID;

            logger.Log(logEvent);
        }
    }
}
