using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace LogMessageManager
{
    class ILogInterface
    {
        private readonly ILog FileLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string LogFolderPath;
        public string LogDefaultPath = @"D:\";

        private Object Lock_AddMessage = new object();

        public ILogInterface(string _LogDefaultPath = @"D:\")
        {
            //LogDefaultPath = _LogDefaultPath;
            //InitializeLogger();
        }

        public virtual void InitializeLogger()
        {
            
            DateTime _DateTime = DateTime.Now;
            string _LoggerFolderPath = String.Format(@"{0}\{1:D4}\{2:D2}\{3:D2}\", LogDefaultPath, _DateTime.Year, _DateTime.Month, _DateTime.Day);

            log4net.Repository.Hierarchy.Hierarchy _Hierarchy = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
            _Hierarchy.Configured = true;

            log4net.Appender.RollingFileAppender _RollingAppender = new log4net.Appender.RollingFileAppender();
            _RollingAppender.File = LogDefaultPath + @"\";
            _RollingAppender.AppendToFile = true;
            _RollingAppender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;
            _RollingAppender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
            _RollingAppender.StaticLogFileName = false;
            _RollingAppender.DatePattern = "yyyy\\\\MM\\\\dd\\\\\"LogFile_\"yyyyMMdd\".log\""; // 날짜가 지나간 경우 이전 로그에 붙을 이름 구성  
            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout("%d{yyyy/MM/dd-HH:mm:ss:ffff} [%-5level] : %message%newline");
            _RollingAppender.Layout = layout;
            _RollingAppender.ActivateOptions();

            _Hierarchy.Root.RemoveAllAppenders();
            _Hierarchy.Root.AddAppender(_RollingAppender);
            _Hierarchy.Root.Level = log4net.Core.Level.All;

            LogFolderPath = LogDefaultPath;
        }

        public virtual void AddLogMessage(CLogManager.LOG_TYPE _Type, string _ErrMessage)
        {
            lock (Lock_AddMessage)
            {
                InitializeLogger();
                switch (_Type)
                {
                    case CLogManager.LOG_TYPE.INFO: FileLogger.Info(_ErrMessage); break;
                    case CLogManager.LOG_TYPE.WARN: FileLogger.Warn(_ErrMessage); break;
                    case CLogManager.LOG_TYPE.ERR: FileLogger.Error(_ErrMessage); break;
                }
            }
        }
    }
}
