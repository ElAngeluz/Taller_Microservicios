using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Collections;

namespace microscore.infrastructure.extentions
{
    public static class EnvironmentVariableExtentions
    {
        public static WebApplicationBuilder ConfigureEnvironmentVariable(this WebApplicationBuilder builder)
        {
            try
            {
                var _appname = "BGMICROCONTRATOSVariables";
                var values = new Dictionary<string, string?>();
                foreach (DictionaryEntry e in Environment.GetEnvironmentVariables())
                {
                    if (e.Key != null)
                    {
                        var _key = e.Key.ToString()?.Trim();

                        if (_key != null && _key.StartsWith(_appname + "_"))
                        {
                            var _name = _key.Replace(_appname + "_", "");
                            try
                            {
                                if (e.Value != null)
                                    values.Add(_name, e.Value.ToString());
                            }
                            catch (Exception) { }
                        }
                    }

                }
                builder.Host.ConfigureAppConfiguration((ctx, _builder) =>
                {
                    _builder.AddInMemoryCollection(values);
                });

            }
            catch (Exception) { }
            return builder;
        }
    }
}
