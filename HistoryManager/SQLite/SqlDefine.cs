using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HistoryManager
{
    public class SqlDefine
    {
        string InspType = "";
        internal const string CREATE_TABLE = "CREATE TABLE HistoryFile";
        internal const string SELECT = "SELECT * FROM HistoryFile";
        internal const string SEARCH_DATE = "SELECT * FROM HistoryFile WHERE";
        internal const string SEARCH_RECIPENAME = "RecipeName in";
        internal const string SEARCH_RESULTNAME = "LastResult in";
        internal const string SEARCH_NGTYPENAME = "ResultType in";
        internal const string SEARCH_CAMTYPENAME = "Cam in";
        internal const string SEARCH_MODELNAME = "ModelName in";
        internal const string DELETE_RECORD = "DELETE FROM HistoryFile WHERE";
        internal const string GET_IMAGEPATH = "SELECT InspImagePath FROM HistoryFile LIMIT 1";
    }
}