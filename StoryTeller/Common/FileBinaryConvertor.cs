using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StoryTeller.Common
{
    public static class FileBinaryConvertor
    {
        public static byte[] GetFile(string fileName)
        {
            byte[] data = null;
            FileInfo fileInfo = new FileInfo(fileName);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            data = br.ReadBytes((int)imageFileLength);

            return data;
        }
    }
}