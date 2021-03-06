using Newbe.Mahua.Commands;
using Newbe.Mahua.Internals;

namespace Newbe.Mahua
{
    public static class PluginLoaderExtensions
    {
        private static TResult ConvertType<TResult>(MahuaCommandResult crossDomainCommandResult)
        {
            var json = GlobalCache.JavaScriptSerializer.Serialize(crossDomainCommandResult);
            var re = GlobalCache.JavaScriptSerializer.Deserialize<TResult>(json);
            return re;
        }

        public static TResult SendCommand<TResult>(this IPluginLoader pluginLoader, MahuaCommand command)
            where TResult : MahuaCommandResult, new()
        {
            MahuaCommandResult result;
            pluginLoader.SendCommandWithResult(command, out result);
            //todo 跨AppDomain时遇到了强制转换失败的问题，因此采用了json序列化的办法
            var re = ConvertType<TResult>(result);
            return re;
        }
    }
}