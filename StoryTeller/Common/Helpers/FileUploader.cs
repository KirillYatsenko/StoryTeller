using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StoryTeller.Common
{
    public static class  FileUploader
    {
        public static byte[] GetFile(string fileName, HttpRequestBase request)
        {
            byte[] file = null;

            if (request.Files.Count > 0)
            {
                HttpPostedFileBase poImgFile = request.Files[fileName];

                using (var binary = new BinaryReader(poImgFile.InputStream))
                {
                    file = binary.ReadBytes(poImgFile.ContentLength);
                }
            }

            return file;
        }
    }
}