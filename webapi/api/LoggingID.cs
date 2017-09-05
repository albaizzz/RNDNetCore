// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.Web;

// namespace webapi.common.Logging
// {
//     public class LoggingID
//     {
//         const string TRACKINGNAME = "reservasi-tracking-id";

//         public static string CreateID()
//         {
//             var trackingId = Environment.MachineName + "-" + Guid.NewGuid().ToString("N");

//             HttpContext.Current.Items.Add(TRACKINGNAME, trackingId);

//             return trackingId;
//         }

//         public static string GetID()
//         {
//             if (HttpContext.Current == null || HttpContext.Current.Items.Contains(TRACKINGNAME) == false) return string.Empty;

//             return HttpContext.Current.Items[TRACKINGNAME].ToString();
//         }
//     }
// }
