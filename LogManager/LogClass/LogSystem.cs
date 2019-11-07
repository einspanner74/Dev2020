using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogMessageManager
{
    class LogSystem : ILogInterface
    {
        public LogSystem(string _LogDefaultPath = @"D:\")
        {
            base.LogDefaultPath = _LogDefaultPath;
        }

        public override void InitializeLogger()
        {
            base.InitializeLogger();
        }

        public override void AddLogMessage(CLogManager.LOG_TYPE _Type, string _ErrMessage)
        {
            base.AddLogMessage(_Type, _ErrMessage);
        }
    }
}
