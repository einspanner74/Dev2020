using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading;

using LogMessageManager;

namespace HistoryManager
{
    public class SqliteManager
    {
        private static List<string[]> list;
        private static string connStr = @"Data Source=";
        private static string connStrFolderPath = @"D:\VisionInspectionData\KPINTVisionInspection\HistoryData";
        public static DataSet HistoryDataSet = new DataSet();
        private static bool RecipeFlag = false;
        private static bool ResultFlag = false;
        private static bool NGTypeFlag = false;
        private static bool CamTypeFlag = false;
        private static bool ModelNameFlag = false;
        private static List<string> RecipeList = new List<string>();
        private static List<string> ResultList = new List<string>();
        private static List<string> NGTypeList = new List<string>();

        private static SQLiteDataReader SQLData;

        #region Tranjection
        /// <summary>
        /// 트랜잭션 시작
        /// </summary>
        /// <param name="sqlConn">Connection한 객체명</param>
        private static void BeginTran(SQLiteConnection sqlConn)
        {
            using (SQLiteCommand SQLiteCommand = new SQLiteCommand("Begin", sqlConn))
            {
                SQLiteCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 트랜잭션 시작
        /// </summary>
        /// <param name="sqlConn">Connection한 객체명</param>
        private static void CommitTran(SQLiteConnection sqlConn)
        {
            using (SQLiteCommand SQLiteCommand = new SQLiteCommand("Commit", sqlConn))
            {
                SQLiteCommand.ExecuteNonQuery();
            }
        }
        #endregion Tranjection

        #region "List 형태로 반환"
        /// <summary>
        /// Select List로 반환 형
        /// </summary>
        /// <param name="_sql">완성된 Select 문</param>
        /// <returns>List<string[]></returns>
        public static List<string[]> SqlSelect(string SqlQuery)
        {
            list = new List<string[]>();

            using (SQLiteConnection SQLiteConnection = new SQLiteConnection(SqlQuery))
            {
                SQLiteConnection.Open();
                SQLiteCommand _SQLiteCommand = new SQLiteCommand(SqlQuery, SQLiteConnection);
                SQLiteDataReader _SQLiteDataReader = null;
                try
                {
                    _SQLiteDataReader = _SQLiteCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    int _No = _SQLiteDataReader.FieldCount;
                    while (_SQLiteDataReader.Read())
                    {
                        string[] _rowValue = new string[_No];
                        for (int i = 0; i < _No; i++)
                        {
                            _rowValue[i] = _SQLiteDataReader[i].ToString();
                        }
                        list.Add(_rowValue);
                    }
                }
                catch (Exception _e)
                {
                    CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "SqliteManager SqlSelect Exception!!", CLogManager.LOG_LEVEL.LOW);
                }
                finally
                {
                    _SQLiteDataReader.Close();
                }
                SQLiteConnection.Close();
            }

            return list;
        }
        #endregion

        #region "Insert,update,delete single row"
        /// <summary>
        /// single Insert,update,delete
        /// </summary>
        /// <param name="_sql">완성된 Insert,update,delete 문</param>
        /// <returns></returns>
        public static int SqlExecute(string _SqlQuery, bool _CreateTable, string _CreateComm = "")
        {
            try
            {
                string StrFilePath = String.Format(@"{0}\History.db", connStrFolderPath);
                StrFilePath = String.Format(@"{0}{1}; PRAGMA Journa_Mode=WAL", connStr, StrFilePath);
                using (SQLiteConnection SQLiteConnection = new SQLiteConnection(StrFilePath))
                {
                    SQLiteConnection.Open();
                    SQLiteCommand SQLiteComm;
                    int iRet = 0;

                    if (_CreateTable == true)
                    {
                        //    string CreateComm = "";
                        //    CreateComm = string.Format("{0} (Date Datetime, RecipeName char, Result char, ResultX char, ResultTheta char, InspResult char, SendResult char, InspImagePath char);", SqlDefine.CREATE_TABLE);
                        SQLiteComm = new SQLiteCommand(_CreateComm, SQLiteConnection);
                        iRet = SQLiteComm.ExecuteNonQuery();
                    }

                    SQLiteComm = new SQLiteCommand(_SqlQuery, SQLiteConnection);
                    iRet = SQLiteComm.ExecuteNonQuery();
                    SQLiteConnection.Close();
                    return iRet;
                }
            }
            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "SqliteManager SqlExecute Exception!!", CLogManager.LOG_LEVEL.LOW);
                return 0;
            }
        }
        #endregion "Insert,update,delete single row"

        #region "Insert,update,delete multi Row"
        /// <summary>
        /// multi Insert,update,delete
        /// </summary>
        /// <param name="_sql">완성된 Insert,update,delete 문</param>
        /// <returns></returns>
        public static int SqlFastExecute(string SqlQuery)
        {
            try
            {
                using (SQLiteConnection SQLiteConnection = new SQLiteConnection(SqlQuery))
                {
                    SQLiteConnection.Open();
                    BeginTran(SQLiteConnection);
                    SQLiteCommand SQLiteCommand = new SQLiteCommand(SqlQuery, SQLiteConnection);
                    int iRet = SQLiteCommand.ExecuteNonQuery();
                    CommitTran(SQLiteConnection);
                    SQLiteConnection.Close();
                    return iRet;
                }
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }
        #endregion "Insert,update,delete multi Row"

        public static DataSet SqlOpen(bool SelectDate, string SelectDateFrom, string SelectDateTo)
        {
            if (CheckDirectory() == null) return HistoryDataSet = null;

            using (SQLiteConnection SQLiteConnection = new SQLiteConnection(CheckDirectory()))
            {
                SQLiteConnection.Open();
                
                string strSQL;

                if (SelectDate == true)
                {
                    strSQL = String.Format("{0} ( Date BETWEEN '{1} 00:00:00' AND '{2} 23:59:59')", SqlDefine.SEARCH_DATE, SelectDateFrom, SelectDateTo);
                    strSQL = CheckSearchOption(strSQL);
                }
                else
                {
                    if (RecipeFlag == true || ResultFlag == true || NGTypeFlag == true || CamTypeFlag == true || ModelNameFlag == true) 
                    {
                        strSQL = String.Format(" {0} ", SqlDefine.SEARCH_DATE);
                        strSQL = CheckSearchOption(strSQL, true);
                    }
                    else strSQL = SqlDefine.SELECT;
                }

                HistoryDataSet.Clear();
                
                SQLiteDataAdapter adaqtar = new SQLiteDataAdapter(strSQL, SQLiteConnection);               

                adaqtar = new SQLiteDataAdapter(strSQL, SQLiteConnection);

                adaqtar.Fill(HistoryDataSet);
                SQLiteConnection.Close();

                return HistoryDataSet;
            }
        }

        public static DataSet SqlDelete(bool SelectDate, string SelectDateFrom, string SelectDateTo)
        {
            if (CheckDirectory() == null) return null;

            using (SQLiteConnection SQLiteConnection = new SQLiteConnection(CheckDirectory()))
            {
                SQLiteConnection.Open();

                string strSQL;
                if (SelectDate == true)
                {
                    //strSQL = String.Format("{0} ( Date BETWEEN '{1} 00:00:00' AND '{2} 23:59:59')", SqlDefine.DELETE_RECORD, SelectDateFrom, SelectDateTo);
                    strSQL = String.Format("{0} ( Date <= '{1} 23:59:59')", SqlDefine.DELETE_RECORD, SelectDateTo);
                    if (RecipeFlag == true) { strSQL = String.Format("{0} AND {1} ({2})", strSQL, SqlDefine.SEARCH_RECIPENAME, SetRecipeNameList()); }
                    if (ModelNameFlag == true) { strSQL = String.Format("{0} AND {1} ({2})", strSQL, SqlDefine.SEARCH_MODELNAME, SetRecipeNameList()); }
                }
                else
                {
                    strSQL = "DELETE FROM HistoryFile";
                    if (RecipeFlag == true) strSQL = String.Format("{0} WHERE {1} ({2})", strSQL, SqlDefine.SEARCH_RECIPENAME, SetRecipeNameList());
                    if (ModelNameFlag == true) strSQL = String.Format("{0} WHERE {1} ({2})", strSQL, SqlDefine.SEARCH_MODELNAME, SetRecipeNameList());
                }

                HistoryDataSet.Clear();
                
                SQLiteDataAdapter adaqtar = new SQLiteDataAdapter(strSQL, SQLiteConnection);
                adaqtar.Fill(HistoryDataSet);

                HistoryDataSet.Clear();

                if (RecipeFlag == true) strSQL = String.Format(" {0} {1} ({2})", SqlDefine.SEARCH_DATE, SqlDefine.SEARCH_RECIPENAME, SetRecipeNameList());
                if (ModelNameFlag == true) strSQL = String.Format(" {0} {1} ({2})", SqlDefine.SEARCH_DATE, SqlDefine.SEARCH_MODELNAME, SetRecipeNameList());
                else strSQL = SqlDefine.SELECT;

                adaqtar = new SQLiteDataAdapter(strSQL, SQLiteConnection);

                adaqtar.Fill(HistoryDataSet);

                SQLiteConnection.Close();

                return HistoryDataSet;
            }
        }

        public static void SqlDistinct(ref List<string> _DataList, string _SelectColumn)
        {
            if (CheckDirectory() == null) _DataList = null;

            using (SQLiteConnection SQLiteConnection = new SQLiteConnection(CheckDirectory()))
            {                
                string strSQL = "SELECT distinct " + _SelectColumn + " FROM HistoryFile";

                SQLiteCommand _SQLCommand = new SQLiteCommand(strSQL, SQLiteConnection);

                SQLiteConnection.Open();

                SQLiteDataReader _SQLReader = _SQLCommand.ExecuteReader();
                
                while(_SQLReader.Read())
                {
                    _DataList.Add(_SQLReader.GetString(0));
                }

                //_DataList.Clear();

                //SQLiteDataAdapter adaqtar = new SQLiteDataAdapter(strSQL, SQLiteConnection);

                //adaqtar = new SQLiteDataAdapter(strSQL, SQLiteConnection);

                //adaqtar.Fill(_DataList, "DSTables");
                _SQLReader.Close();
                SQLiteConnection.Close();
            }
        }

        public static string SqlGetScreenshotPath()
        {
            if (CheckDirectory() == null) return null;

            using (SQLiteConnection SQLiteConnection = new SQLiteConnection(CheckDirectory()))
            {
                SQLiteConnection.Open();
                SQLiteCommand SQLiteComm;
                string ScreenshotPath;
                int iRet;

                SQLiteComm = new SQLiteCommand(SqlDefine.GET_IMAGEPATH, SQLiteConnection);
                iRet = SQLiteComm.ExecuteNonQuery();

                if (SQLiteComm.ExecuteScalar() == null) return null;

                ScreenshotPath = SQLiteComm.ExecuteScalar().ToString();

                SQLiteConnection.Close();

                return ScreenshotPath;
            }
        }

        public static void SqlSetSearchCondition(string _ConditionName, bool _SetFlag, List<string> _ConditionList)
        {
            switch (_ConditionName)
            {
                case "Recipe": { RecipeFlag = _SetFlag; RecipeList = _ConditionList; } break;
                case "Result": { ResultFlag = _SetFlag; ResultList = _ConditionList; } break;
                case "NGType": { NGTypeFlag = _SetFlag; NGTypeList = _ConditionList; } break;
                case "CamType": { CamTypeFlag = _SetFlag; ResultList = _ConditionList; } break;
                case "ModelName": { ModelNameFlag = _SetFlag; RecipeList = _ConditionList; } break;
            }
        }

        public static void SqlSetSearchConditionClear(string _ConditionName, bool _SetFlag)
        {
            switch (_ConditionName)
            {
                case "Recipe": RecipeFlag = _SetFlag; break;
                case "Result": ResultFlag = _SetFlag; break;
                case "NGType": NGTypeFlag = _SetFlag; break;
                case "CamType": CamTypeFlag = _SetFlag; break;
                case "ModelName": ModelNameFlag = _SetFlag; break;
            }
        }

        public static string CheckDirectory()
        {
            DateTime TimeNow = DateTime.Now;

            if (false == File.Exists(String.Format(@"{0}\History.db", connStrFolderPath))) { return null; }

            string connStrFolder;
            connStrFolder = String.Format(@"{0}{1}\History.db; PRAGMA Journa_Mode=WAL", connStr, connStrFolderPath, TimeNow.Year, TimeNow.Month, TimeNow.Day);

            return connStrFolder;
        }

        private static string CheckSearchOption(string _strSQL, bool multiOptionFlag = false)
        {
            if (RecipeFlag)
            {
                if (multiOptionFlag == false) _strSQL = String.Format("{0} AND {1} ({2})", _strSQL, SqlDefine.SEARCH_RECIPENAME, SetRecipeNameList());
                else
                {
                    _strSQL = String.Format("{0} {1} ({2})", _strSQL, SqlDefine.SEARCH_RECIPENAME, SetRecipeNameList());
                    multiOptionFlag = false;
                }
            }

            if (ResultFlag)
            {
                string strResultList = String.Format("'{0}'", ResultList[0]);
                if (multiOptionFlag == false) _strSQL = String.Format("{0} AND {1} ({2})", _strSQL, SqlDefine.SEARCH_RESULTNAME, strResultList);
                else
                {
                    _strSQL = String.Format("{0} {1} ({2})", _strSQL, SqlDefine.SEARCH_RESULTNAME, strResultList);
                    multiOptionFlag = false;
                }
            }

            if (NGTypeFlag)
            {
                string strNGTypeList = "";

                for (int i = 0; i < NGTypeList.Count; i++)
                {
                    if (strNGTypeList == "") strNGTypeList = String.Format("'{0}'", NGTypeList[i]);
                    else strNGTypeList = String.Format("{0}, '{1}'", strNGTypeList, NGTypeList[i]);
                }

                if (multiOptionFlag == false) _strSQL = String.Format("{0} AND {1} ({2})", _strSQL, SqlDefine.SEARCH_NGTYPENAME, strNGTypeList);
                else
                {
                    _strSQL = String.Format("{0} {1} ({2})", _strSQL, SqlDefine.SEARCH_NGTYPENAME, strNGTypeList);
                    multiOptionFlag = false;
                }
            }

            if (CamTypeFlag)
            {
                string strResultList = String.Format("'{0}'", ResultList[0]);
                if (multiOptionFlag == false) _strSQL = String.Format("{0} AND {1} ({2})", _strSQL, SqlDefine.SEARCH_CAMTYPENAME, strResultList);
                else
                {
                    _strSQL = String.Format("{0} {1} ({2})", _strSQL, SqlDefine.SEARCH_CAMTYPENAME, strResultList);
                    multiOptionFlag = false;
                }
            }

            if(ModelNameFlag)
            {
                if (multiOptionFlag == false) _strSQL = String.Format("{0} AND {1} ({2})", _strSQL, SqlDefine.SEARCH_MODELNAME, SetRecipeNameList());
                else
                {
                    _strSQL = String.Format("{0} {1} ({2})", _strSQL, SqlDefine.SEARCH_MODELNAME, SetRecipeNameList());
                    multiOptionFlag = false;
                }
            }


            return _strSQL;
        }

        private static string SetRecipeNameList()
        {
            string _strRecipeList = "";

            for (int i = 0; i < RecipeList.Count; i++)
            {
                if (_strRecipeList == "") _strRecipeList = String.Format("'{0}'", RecipeList[i]);
                else _strRecipeList = String.Format("{0}, '{1}'", _strRecipeList, RecipeList[i]);
            }

            return _strRecipeList;
        }

        public static void SetHistoryFolderPath(string _ProjectName)
        {
            connStrFolderPath = String.Format(@"D:\VisionInspectionData\{0}\HistoryData", _ProjectName);
        }
    }
}
