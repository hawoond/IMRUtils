using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using static IMRUtils.OverrideType;

namespace IMRUtils
{
    /// <summary>
    /// TypeParser의 요약 설명입니다.
    /// </summary>
    public static class TypeParser
    {
        /// <summary>
        /// XML을 Dictionary로 변환
        /// </summary>
        /// <param name="sXml"></param>
        /// <returns></returns>
        public static Dictionary<string, string> XmlToDictionary(string sXml)
        {
            XElement xElement = XElement.Parse(sXml);
            Dictionary<string, string> dicResult = new Dictionary<string, string>();

            foreach (var item in xElement.Elements())
            {
                dicResult.Add(item.Name.ToString(), item.Value);
            }

            return dicResult;
        }

        /// <summary>
        /// Xml을 List로 변환
        /// </summary>
        /// <param name="sXml"></param>
        /// <returns></returns>
        public static List<string> XmlToList(string sXml)
        {
            XElement xElement = XElement.Parse(sXml);
            List<string> arrResult = new List<string>();

            foreach (var item in xElement.Elements())
            {
                arrResult.Add(item.Value);
            }

            return arrResult;
        }

        /// <summary>
        /// Json을 Dictionary로 변환
        /// </summary>
        /// <param name="sJson"></param>
        /// <returns></returns>
        public static Dictionary<string, string> JsonToDictionary(string sJson)
        {
            JObject jObject = JObject.Parse(sJson);
            Dictionary<string, string> dicResult = new Dictionary<string, string>();

            foreach (var item in jObject)
            {
                dicResult.Add(item.Key, item.Value.ToString());
            }

            return dicResult;
        }

        /// <summary>
        /// Json을 List로 변환
        /// </summary>
        /// <param name="sJson"></param>
        /// <returns></returns>
        public static List<string> JsonToList(string sJson)
        {
            JObject jObject = JObject.Parse(sJson);
            List<string> arrResult = new List<string>();

            foreach (var item in jObject)
            {
                arrResult.Add(item.Value.ToString());
            }

            return arrResult;
        }

        /// <summary>
        /// Dictionary를 Xml로 변환
        /// </summary>
        /// <param name="dicData"></param>
        /// <returns></returns>
        public static string DictionaryToXml(Dictionary<string, string> dicData)
        {
            XDocument xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            XElement root = new XElement("groot");
            xDocument.Add(root);

            foreach (var item in dicData)
            {
                string sTempValue = item.Value;
                XElement xElement;
                try
                {
                    xElement = new XElement(item.Key);
                    xElement.Add(XElement.Parse(sTempValue));
                }
                catch (Exception ex)
                {
                    xElement = new XElement(item.Key, sTempValue);
                }
                root.Add(xElement);
            }

            return xDocument.ToString();
        }

        /// <summary>
        /// Dictionary를 Xml로 변환
        /// </summary>
        /// <param name="dicData"></param>
        /// <returns></returns>
        public static string DictionaryToNoRootXml(Dictionary<string, string> dicData)
        {
            XDocument xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", null));

            foreach (var item in dicData)
            {
                XElement xElement = new XElement(item.Key, new XCData(item.Value));
                xDocument.Add(xElement);
            }

            return xDocument.ToString();
        }

        /// <summary>
        /// Dictionary를 Json으로 변환
        /// </summary>
        /// <param name="sJson"></param>
        /// <returns></returns>
        public static string DictionaryToJson(Dictionary<string, string> dicData)
        {
            JObject jObject = new JObject();

            foreach (var item in dicData)
            {
                jObject.Add(item.Key, item.Value);
            }

            return jObject.ToString();
        }

        /// <summary>
        /// List를 Xml로 변환
        /// </summary>
        /// <param name="arrData"></param>
        /// <returns></returns>
        public static string ListToXml(List<string> arrData)
        {
            XDocument xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            XElement root = new XElement("groot");
            xDocument.Add(root);

            for (int i = 0; i < arrData.Count; ++i)
            {
                XElement xElement = new XElement("Data" + i, arrData[i]);
                root.Add(xElement);
            }

            return xDocument.ToString();
        }

        /// <summary>
        /// List를 Json으로 변환
        /// </summary>
        /// <param name="arrData"></param>
        /// <returns></returns>
        public static string ListToJson(List<string> arrData)
        {
            JObject jObject = new JObject();

            for (int i = 0; i < arrData.Count; ++i)
            {
                jObject.Add("Data" + i, arrData[i]);
            }

            return jObject.ToString();
        }

        /// <summary>
        /// DataTable을 리스트로 변환
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">데이터 테이블</param>
        /// <returns></returns>
        public static MTObservableCollection<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                MTObservableCollection<T> list = new MTObservableCollection<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 객체를 Dictionary로 변환
        /// </summary>
        /// <param name="data">객체</param>
        /// <returns></returns>
        public static Dictionary<string, string> ClassToDictinary(object data)
        {
            Dictionary<string, string> dicResult = new Dictionary<string, string>();

            foreach (var prop in data.GetType().GetProperties())
            {
                try
                {
                    string value = data.GetType().GetProperty(prop.Name).GetValue(data, null).ToString();

                    if (data.GetType().GetProperty(prop.Name).GetValue(data, null).GetType().Name.Equals(typeof(Dictionary<string, string>).Name))
                    {
                        value = DictionaryToNoRootXml((Dictionary<string, string>)data.GetType().GetProperty(prop.Name).GetValue(data, null));
                    }

                    dicResult.Add(prop.Name, value);
                }
                catch (Exception ex)
                {

                }
            }
            return dicResult;
        }

        /// <summary>
        /// XML 데이터를 DataSet으로 변환
        /// </summary>
        /// <param name="sXml"></param>
        /// <returns></returns>
        public static DataSet XmlToDataSet(string sXml)
        {
            DataSet dsResult = new DataSet();

            XmlReader xmlReader = XDocument.Parse(sXml).CreateReader();

            dsResult.ReadXml(xmlReader);

            return dsResult;
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var stream = new MemoryStream())
            {
                imageIn.Save(stream, imageIn.RawFormat);
                return stream.ToArray();
            }
        }

        public static Image ByteArrayToImage(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes, 0, bytes.Length);
            stream.Write(bytes, 0, bytes.Length);
            return Image.FromStream(stream, true);
        }

        /// <summary>
        /// 문자열을 스트림으로 변환
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Stream StringToStream(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// JsonArray를 List<Dictionary<string, string>>로 변환
        /// </summary>
        /// <param name="sJson">Json string</param>
        /// <returns>List<Dictionary<string, string>></returns>
        public static List<Dictionary<string, string>> JsonArrayToDictionary(string sJson)
        {
            List<Dictionary<string, string>> arrResult = new List<Dictionary<string, string>>();
            Dictionary<string, string> dicResult;

            JArray jArray = JArray.Parse(sJson);

            foreach (JObject item in jArray)
            {
                dicResult = new Dictionary<string, string>();
                foreach (var obejctItem in item)
                {
                    dicResult.Add(obejctItem.Key, obejctItem.Value.ToString());
                }
                arrResult.Add(dicResult);
            }

            return arrResult;
        }
    }

}