using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace webapi.data
{
    public class Class1
    {
        protected static IOptions<MySettingss> _AppSettings;

        public Class1(IOptions<MySettingss> AppSettings)
        {
            _AppSettings = AppSettings;
        }
        public void cls1()
        {
            var a ="";
        }
    }
}