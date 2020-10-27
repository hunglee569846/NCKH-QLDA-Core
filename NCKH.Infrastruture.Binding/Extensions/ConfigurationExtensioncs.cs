//using Microsoft.Extensions.Configuration;
//using NCKH.Infrastruture.Binding.Models;

//namespace NCKH.Infrastruture.Binding.Extensions
//{
//    public static class ConfigurationExtension
//    {
//        public static ApiUrlSettings GetApiUrl(this IConfiguration configuration)
//        {
//            var apiUrlSetting = new ApiUrlSettings();
//            configuration?.GetSection("ApiUrlSettings")?.Bind(apiUrlSetting);
//            return apiUrlSetting;
//        }

//        public static string GetApiUrl(this IConfiguration configuration, string name)
//        {
//            var section = configuration?.GetSection("ApiUrlSettings");
//            return section?[name];
//        }

//        public static string ConfigIdentity(this IConfiguration configuration, string name)
//        {
//            var section = configuration?.GetSection("ConfigIdentity");
//            return section?[name];
//        }

//        public static ApiServiceInfo GetApiServiceInfo(this IConfiguration configuration)
//        {
//            var apiServiceInfo = new ApiServiceInfo();
//            configuration?.GetSection("ApiServiceInfo")?.Bind(apiServiceInfo);
//            return apiServiceInfo;
//        }

//    }
//}
