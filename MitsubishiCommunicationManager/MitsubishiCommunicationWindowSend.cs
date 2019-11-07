using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace MitsubishiCommunicationManager
{
    public partial class MitsubishiCommunicationWindow
    {
        private ResultCommunicationData ResultCommData = new ResultCommunicationData();
        private MemoryMappedFile ResultMemMapFile;
        private MemoryMappedViewStream ResultMemMapStream;

        private Thread ThreadReadResultData;
        private bool IsThreadReadResultDataExit;

        private Thread ThreadAliveData;
        private bool IsThreadAliveDataExit;

        private void ResultDataMemoryMapping()
        {
            ResultMemMapStream = ResultMemMapFile.CreateViewStream();
            {
                BinaryFormatter _Serializer = new BinaryFormatter();
                _Serializer.Serialize(ResultMemMapStream, ResultCommData);
                ResultMemMapStream.Seek(0, SeekOrigin.Begin);
            }

            GridViewUpdateVisionData();
        }

        private void ThreadReadResultDataFunc()
        {
            try
            {
                while (false == IsThreadReadResultDataExit)
                {
                    ResultDataMemoryMapping();
                    Thread.Sleep(5);
                }
            }

            catch (Exception ex)
            {

            }
        }

        private void ThreadAliveDataFunc()
        {

        }
    }
}
