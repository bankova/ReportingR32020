using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Bellatrix.Utilities;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ReportViewer.Tests.Common
{
    internal static class FileUtilities
    {
        internal static string GetDownloadsFolderPath()
        {
            string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";

            return dirPath;
        }

        internal static void AssertFileNotExistInDownloadsPath(string fileName)
        {
            string dirPath = GetDownloadsFolderPath();
            string filePath = dirPath + fileName;

            AssertFileNotExistInFolder(dirPath, fileName);
        }

        internal static void AssertFileExistInDownloadsPath(string fileName)
        {
            string dirPath = GetDownloadsFolderPath();
            string filePath = dirPath + fileName;

            Wait.Until(() => System.IO.File.Exists(filePath), 20);
        }

        private static void AssertFileNotExistInFolder(string downloadsPath, string fileName)
        {
            string fileNameWithWildCard = fileName.Replace(".", "*.");
            var files = Directory.GetFiles(downloadsPath, fileNameWithWildCard);
            if (files.Length > 0)
            {
                foreach (var file in files)
                {
                    File.Delete(Path.Combine(downloadsPath, file));
                }
            }

            Assert.IsFalse(System.IO.File.Exists(downloadsPath + fileName), "File exists but should not exist.");
        }
    }
}
