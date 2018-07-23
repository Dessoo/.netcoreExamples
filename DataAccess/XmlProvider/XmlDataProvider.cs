
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataAccess.XmlProvider
{
    /// <summary>
    /// Please use this class like a single instance via IoC 
    /// </summary>
    public class XmlDataProvider<TEntity> : IXmlDataProvider<TEntity> where TEntity : class
    {
        private readonly string  _fileLocation;
        private readonly string _fileName;
        private readonly string _folderName;
        private readonly string _fullPath;

        public XmlDataProvider(string fileLocation, string fileName, string folderName)
        {
            this._fileLocation = fileLocation;
            this._fileName = fileName;
            this._folderName = folderName;
            this._fullPath = $"{this._fileLocation}\\{this._folderName}\\{this._fileName}_{Guid.NewGuid()}.xml";

            bool exists = Directory.Exists(this._fileLocation + "\\" + this._folderName);
            if (!exists)
            {
                Directory.CreateDirectory(this._fileLocation + "\\" + this._folderName);
            }

            if (!File.Exists(this._fullPath))
            {
                new StreamWriter(this._fullPath).Dispose();
            }         
        }

        public void Add(TEntity entity)
        {      
            XmlSerializer xs = new XmlSerializer(typeof(List<TEntity>));
            List<TEntity> list = new List<TEntity>();

            var sr = new StreamReader(_fullPath);

            if (sr.BaseStream.Length > 0)
            {
                list = (List<TEntity>)xs.Deserialize(sr);
            }

            list.Add(entity);
            sr.Dispose();

            TextWriter tw = new StreamWriter(_fullPath);    
            xs.Serialize(tw, list);

            tw.Dispose();
        }
    }
}
