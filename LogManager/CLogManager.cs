using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogMessageManager
{
    public class CLogManager
    {
        public enum LOG_TYPE { INFO = 0, WARN, ERR }

        public enum LOG_LEVEL { HIGH = 0, MID = 1, LOW = 2 }
        public static int LogLevel = 0;

        private static LogSystem     LogSystemTool;
        private static LogInspection LogInspectionTool;

        #region Log Window Variable
        private static LogWindowSE LogWnd = new LogWindowSE();
        public bool IsShowLogWindow = false;
        #endregion Log Window Variable

        #region Initialize
        public CLogManager(string _ProjectName)
        {
            LogWnd.Initialize(_ProjectName);
            LogWnd.LogWindoCloserEvent += new LogWindowSE.LogWindoCloserwHandler(LogWindowCloseEventFunction);
        }
        #endregion Initialize

        #region Log Window Show / Hide
        public void ShowLogWindow(bool _IsShow)
        {
            IsShowLogWindow = _IsShow;

            if (true == _IsShow) LogWnd.Show();
            else if (false == _IsShow) LogWnd.Hide();
        }

        private void LogWindowCloseEventFunction()
        {
            ShowLogWindow(false);
        }
        #endregion Log Window Show / Hide

        #region System Log
        public static void LogSystemSetting(string _LogDefaultPath)
        {
            LogSystemTool = new LogSystem(_LogDefaultPath);
        }

        public static void AddSystemLog(LOG_TYPE _Type, string _ErrMessage, LOG_LEVEL _AddLogLevel = LOG_LEVEL.LOW)
        {
            if (null == LogSystemTool) return;

            if ((int)_AddLogLevel >= LogLevel)
            {
                LogSystemTool.AddLogMessage(_Type, _ErrMessage);

                DateTime _NowDate = DateTime.Now;
                string _NowDateFormat = _NowDate.ToString("yyyy-MM-dd HH:mm:ss.ffff");
                string _LogMessage = String.Format(@"[SYS] {0} {1} : {2}", _NowDateFormat, _Type.ToString(), _ErrMessage);
                LogWnd.AddLogMessage(_LogMessage);
            }
        }
        #endregion System Log

        #region Inspection Log
        public static void LogInspectionSetting(string _LogDefaultPath)
        {
            LogInspectionTool = new LogInspection(_LogDefaultPath);
        }

        public static void AddInspectionLog(LOG_TYPE _Type, string _ErrMessage, LOG_LEVEL _AddLogLevel = LOG_LEVEL.LOW)
        {
            if ((int)_AddLogLevel >= LogLevel)
            {
                LogInspectionTool.AddLogMessage(_Type, _ErrMessage);

                DateTime _NowDate = DateTime.Now;
                string _NowDateFormat = _NowDate.ToString("yyyy-MM-dd HH:mm:ss.ffff");
                string _LogMessage = String.Format(@"[INS] {0} {1} : {2}", _NowDateFormat, _Type.ToString(), _ErrMessage);
                LogWnd.AddLogMessage(_LogMessage);
            }
        }
        #endregion Inspection Log

        public static void SetLogLevel(int _LogLevel)
        {
            LogLevel = _LogLevel;
        }
    }
}
