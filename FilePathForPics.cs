using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SimpleFileManager
{

    //Source for code from: https://www.youtube.com/watch?v=6ac6Hdn4-p0 ,Code with Huw
    internal class FilePathForPics
    {
        private string xmlpath { get; set; } = @"FileManagerGuide.xml";
        private string abs_xmlpath { get; set; }=string.Empty;
        public FilePathForPics()
        {
            if (!Path.IsPathRooted(xmlpath))
                abs_xmlpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + xmlpath;
        }

        public void SavePathToXML(string pathvalue, string pathdate)
        {
           
            if (File.Exists(abs_xmlpath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(abs_xmlpath);
                XmlNode dirpath = doc.SelectSingleNode("/AllPathData/DirPath");
                dirpath.InnerText = pathvalue;
                //picfilepath = dirpath[0].InnerText;
                XmlNode dirpathdate = doc.SelectSingleNode("/AllPathData/PathDate");
                 dirpathdate.InnerText=pathdate;
                doc.Save(abs_xmlpath);
            }
           
        }

        public FilesClass LoadPathToXML()
        {
            string picfilepath = string.Empty;
            string pathdate = string.Empty;

            if (File.Exists(abs_xmlpath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(abs_xmlpath);
                XmlNode dirpath = doc.SelectSingleNode("/AllPathData/DirPath");
                picfilepath = dirpath.InnerText;
                XmlNode dirpathdate = doc.SelectSingleNode("/AllPathData/PathDate");
                pathdate = dirpathdate.InnerText;
               
            }
          
            FilesClass filesClass = new FilesClass(picfilepath,pathdate);
            return filesClass;

        }
    }
}
