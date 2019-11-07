using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CustomControl
{
    #region CSVWrite 일반
    public class CSVManager
    {
        StreamWriter CSVWrite;

        public void Initialize(string FileName)
        {
            CSVWrite = new StreamWriter(FileName);
        }

        public void DeInitialize()
        {
            CSVWrite.Flush();
            CSVWrite.Close();
        }

        public void WriteCSVFile(string FileName, string[] RowData)
        {   
            for (int iLoopCount = 0; iLoopCount < RowData.Count(); iLoopCount++)
            {
                CSVWrite.Write(RowData[iLoopCount]);
                if (iLoopCount != RowData.Count()) { CSVWrite.Write(","); }
            }

            CSVWrite.Write(CSVWrite.NewLine);         
        }
    }
    #endregion

    //LDH, Gridview 전체 저장용
    public class CSVManagerSaveAll
    {
        public void SaveGridViewAll(DataGridView SaveGridView, string SaveFilePath)
        {
            //FileStream GridViewStream = new FileStream(SaveFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //StreamWriter GridViewWriter = new StreamWriter(GridViewStream, System.Text.Encoding.Default);
            StreamWriter GridViewWriter = new StreamWriter(SaveFilePath, true, System.Text.Encoding.Default);

            if (SaveGridView.Rows.Count == 0) return;

            for (int iLoopcount = 0; iLoopcount < SaveGridView.Columns.Count; iLoopcount++)
            {
                GridViewWriter.Write(SaveGridView.Columns[iLoopcount].HeaderText);

                if (iLoopcount != SaveGridView.Columns.Count - 1) { GridViewWriter.Write(","); }
            }

            GridViewWriter.Write(GridViewWriter.NewLine);

            foreach (DataGridViewRow GridRow in SaveGridView.Rows)
            {
                if (!GridRow.IsNewRow)
                {
                    for (int iLoopCount = 0; iLoopCount < SaveGridView.Columns.Count; iLoopCount++)
                    {
                        GridViewWriter.Write(GridRow.Cells[iLoopCount].Value);

                        if (iLoopCount != SaveGridView.Columns.Count - 1) { GridViewWriter.Write(","); }
                    }
                    GridViewWriter.Write(GridViewWriter.NewLine);
                }
            }

            GridViewWriter.Flush();
            GridViewWriter.Close();
            //GridViewStream.Close();
        }
    }

    public class CSVManagerStringArr
    {
        public void SaveStringArrAll(string[] _HeaderString, string[] _SaveString, string _SaveFilePath)
        {
            bool _HeaderWriteFlag = false;
            if (false == File.Exists(_SaveFilePath)) { File.Create(_SaveFilePath).Close(); _HeaderWriteFlag = true; }

            //FileStream StringArrStream = new FileStream(_SaveFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //StreamWriter StringArrWriter = new StreamWriter(StringArrStream, System.Text.Encoding.Default);
            StreamWriter StringArrWriter = new StreamWriter(_SaveFilePath, true, System.Text.Encoding.Default);

            if (_HeaderWriteFlag)
            {
                for(int iLoopCount = 0; iLoopCount < _HeaderString.Count(); iLoopCount++)
                {
                    StringArrWriter.Write(_HeaderString[iLoopCount]);
                    if (iLoopCount != _HeaderString.Length - 1) { StringArrWriter.Write(","); }
                }
                StringArrWriter.Write(StringArrWriter.NewLine);
            }

            if (_SaveString.Length == 0) return;
            
            for (int iLoopCount = 0; iLoopCount < _SaveString.Length; iLoopCount++)
            {
                StringArrWriter.Write(_SaveString[iLoopCount]);
                if (iLoopCount != _SaveString.Length - 1) { StringArrWriter.Write(","); }
            }
            StringArrWriter.Write(StringArrWriter.NewLine);

            StringArrWriter.Flush();
            StringArrWriter.Close();
            //StringArrStream.Close();
        }

        public void SaveStringArrAll(string[,] SaveString, int ColumnCount, string SaveFilePath)
        {
            FileStream StringArrStream = new FileStream(SaveFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter StringArrWriter = new StreamWriter(StringArrStream, System.Text.Encoding.Default);

            if (SaveString.Length == 0) return;

            for (int iLoopCount = 0; iLoopCount < SaveString.Length / ColumnCount; iLoopCount++)
            {
                for (int jLoopCount = 0; jLoopCount < ColumnCount; jLoopCount++)
                {
                    StringArrWriter.Write(SaveString[iLoopCount, jLoopCount]);
                    if (jLoopCount != ColumnCount - 1) { StringArrWriter.Write(","); }
                }
                
                StringArrWriter.Write(StringArrWriter.NewLine);
            }

            StringArrWriter.Flush();
            StringArrWriter.Close();
            StringArrStream.Close();
        }
    }


    public class CSVManagerStream : StreamWriter
    {
        public CSVManagerStream(Stream stream)
            : base(stream)
        {
        }

        public CSVManagerStream(string FileName)
            : base(FileName, true, System.Text.Encoding.Default)
        {
        }

        public void WriteCSVHeader(string[] HeaderData)
        {
            WriteCSVFile(HeaderData);
        }

        public void WriteCSVFile(string[] RowData)
        {
            StringBuilder builder = new StringBuilder();
            bool firstColumn = true;

            foreach (string Value in RowData)
            {
                if (!firstColumn) builder.Append(',');

                if (Value.IndexOfAny(new char[] { '"', ',' }) != -1)
                    builder.AppendFormat("\"{0}\"", Value.Replace("\"", "\"\""));
                else
                    builder.Append(Value);
                firstColumn = false;
            }            
            WriteLine(builder.ToString());
        }
    }


    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }

    public class CsvFileReader : StreamReader
    {
        public CsvFileReader(string filename, Encoding en)
            : base(filename, en)
        {
        }

        public CsvFileReader(string filename)
            : base(filename)
        {
            
        }       

        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool ReadRow(CsvRow row)
        {
            row.LineText = ReadLine();
            if (String.IsNullOrEmpty(row.LineText))
                return false;

            int pos = 0;
            int rows = 0;

            while (pos < row.LineText.Length)
            {
                string value;

                // Special handling for quoted field
                if (row.LineText[pos] == '"')
                {
                    // Skip initial quote
                    pos++;

                    // Parse quoted value
                    int start = pos;
                    while (pos < row.LineText.Length)
                    {
                        // Test for quote character
                        if (row.LineText[pos] == '"')
                        {
                            // Found one
                            pos++;

                            // If two quotes together, keep one
                            // Otherwise, indicates end of value
                            if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                            {
                                pos--;
                                break;
                            }
                        }
                        pos++;
                    }
                    value = row.LineText.Substring(start, pos - start);
                    value = value.Replace("\"\"", "\"");
                }
                else
                {
                    // Parse unquoted value
                    int start = pos;
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    value = row.LineText.Substring(start, pos - start);
                }

                // Add field to list
                if (rows < row.Count)
                    row[rows] = value;
                else
                    row.Add(value);
                rows++;

                // Eat up to and including next comma
                while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    pos++;
                if (pos < row.LineText.Length)
                    pos++;
            }
            // Delete any unused items
            while (row.Count > rows)
                row.RemoveAt(rows);

            // Return true if any columns read
            return (row.Count > 0);
        }
    }
}
