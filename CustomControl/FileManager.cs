using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CustomControl
{
    /// <summary>
    /// File Write/Read 용 class
    /// </summary>
    public class FileManager
    {
        #region txt File Write/Read

        //LDH, txt 파일 전체 쓰기
        public void txtFileWrite(string FilePath, List<string> WriteList)
        {
            if (Path.GetDirectoryName(FilePath) == null) return;

            DirectoryInfo FolderInfo = new DirectoryInfo(Path.GetDirectoryName(FilePath));
            if (!FolderInfo.Exists) FolderInfo.Create();

            StreamWriter FileWriter = new StreamWriter(FilePath);

            foreach (string ContentsLine in WriteList)
            {
                FileWriter.WriteLine(ContentsLine);
            }

            FileWriter.Close();
        }

        //LDH, txt 파일 전체 읽기
        public List<string> txtFileRead(string FilePath)
        {
            List<string> ReadList = new List<string>();

            if (File.Exists(FilePath))
            {
                StreamReader FileReader = new StreamReader(FilePath, Encoding.Default);

                foreach (string ReadString in FileReader.ReadToEnd().Split('\n'))
                {
                    ReadList.Add(ReadString.Replace("\r", ""));
                }
            }
            else
            {
                ReadList = null;
            }

            return ReadList;
        }
        #endregion txt File Write/Read

        public void iniFileWrite(string FilePath)
        {

        }

        public void iniFileReae(string FilePath)
        {

        }
    }
}
