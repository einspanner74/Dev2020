using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HistoryManager
{
    public class SqlQuery
    {
        /// <summary>
        /// Error 코드를 저장
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strErrLevel"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>

        public static int HistoryInsertQuery(string HistoryItem, bool _CreateTable, string _CreateComm = "")
        {            
            return SqliteManager.SqlExecute(HistoryItem, _CreateTable, _CreateComm);
        }
    }
}
