using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCLib.FlotationDevice.Service.Models
{
  public enum LogType { Trace = 1, Debug = 2, Info = 3, Warn = 4, Error = 5, Fatal = 6 }

  public static class NLogWriter
  {
    public static void LogMessage(LogType logType, string message)
    {
      switch (logType)
      {
        case LogType.Debug:
          NLog.LogManager.GetCurrentClassLogger().Debug(message);
          break;
        case LogType.Info:
          NLog.LogManager.GetCurrentClassLogger().Info(message);
          break;
        case LogType.Warn:
          NLog.LogManager.GetCurrentClassLogger().Warn(message);
          break;
        case LogType.Error:
          NLog.LogManager.GetCurrentClassLogger().Error(message);
          break;
        case LogType.Fatal:
          NLog.LogManager.GetCurrentClassLogger().Fatal(message);
          break;
        default:
          NLog.LogManager.GetCurrentClassLogger().Trace(message);
          break;
      }
    }
  }
}