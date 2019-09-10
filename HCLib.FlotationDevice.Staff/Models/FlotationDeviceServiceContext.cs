using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using HCLib.ApiKey;

namespace HCLib.FlotationDevice.Staff.Models
{
  public class FlotationDeviceServiceContext
  {
    //read config values
    private static string serviceURI = Properties.Settings.Default["FlotationDeviceServiceURI"].ToString();
    private static string apiKey = Properties.Settings.Default["FlotationDeviceApiKey"].ToString();
    private static int serviceTimeout = Int32.Parse(Properties.Settings.Default["FlotationDeviceServiceTimeout"].ToString());
    private static string appID = Properties.Settings.Default["FlotationDeviceAppID"].ToString();

    internal static Default.Container Context()
    {
      string LogMsg = MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().ToString() + " :: ";

      try
      {
        var container = new Default.Container(new Uri(serviceURI));
        container.Timeout = serviceTimeout;

        container.BuildingRequest += (sender, eventArgs) =>
        {
          //attach header information required to use ApiKey 
          var hmac = new HMAC()
          {
            TimeStamp = DateTime.UtcNow.ToString(),
            Uri = eventArgs.RequestUri.ToString(),
            Method = eventArgs.Method.ToString(),
            AppID = appID,
            ApiKey = apiKey
          };
          hmac.SetNonce();
          eventArgs.Headers.Add("HMAC", hmac.BuildHeader());
          eventArgs.Headers.Add("Hash", hmac.Hash());
        };
        return container;
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception calling Flotation Device Service Application :: " + ex.ToString());
        throw;
      }
    }
  }
}