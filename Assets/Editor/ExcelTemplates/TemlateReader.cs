using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

public class TemlateReader
{
    class TemplateValues
    {
        public string name;
        public int fieldsCount;
        public List<Dictionary<string,string>> data = new List<Dictionary<string, string>>();
    }

    static string generatedCSPath = Application.dataPath + "/Scripts/Templates";
    static string serializedDataPath = "Assets/Resources/TemplateData/";
    static string serializedDataPathFull = Application.dataPath + "/Resources/TemplateData/";
    static Dictionary<string,TemplateValues> allData = new Dictionary<string,TemplateValues>();

    //[MenuItem("工具/正则测试")]
    //static void TEST()
    //{
    //    DelectDir(serializedDataPathFull);
    //}

    [MenuItem("工具/序列化所有模板表数据")]
    static void SerializeAllTemplateData()
    {
       // DelectDir(serializedDataPathFull);
        if (allData.Count == 0)
        {
            EditorUtility.DisplayDialog("错误", "错误", "确定");
            return;
        }
        foreach (KeyValuePair<string, TemplateValues> kv in allData)
        {
            SaveAsset(kv.Key, kv.Value);
        }
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("成功","序列化模板表完成", "确定");
    }

    [MenuItem("工具/生成所有模板表类")]
    static void GenerateAllTemplateTbs()
    {
        string classTemplate = @"/**
----------------------------------------
                星际战争模板表
----------------------------------------
**/
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using System.Reflection;  
            
public class {0}SerializedData : ScriptableObject
$(
    {1}
    
#if UNITY_EDITOR
     public void Put(List<System.Object> data)
     $(
        for(int j = 0; j < data.Count; j++)
        $(
            {2}Template ht  = ({3}Template)data[j];
            dataList.Add(ht);
        )$
    )$
#endif
)$
            
[System.Serializable]
public class {4}Template
$(
    {5}
)$
        ";

        string fieldTemplate = @"public {0} {1};";
        string fieldListTemplate = @"public List<{0}> {1} = new List<{2}>();";
        //string fieldListObjTemplate = @"public List<System.Object> dataList = new List<System.Object>();";

        DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath);
        string tplPath = dirInfo.Parent + "/GameData/模板表-excel/";
        string[] tpls = Directory.GetFiles(tplPath,"*.txt");
        //DelectDir(generatedCSPath);
        allData.Clear();
        for (int i = 0; i < tpls.Length; i++)
        {
            var filePath = tpls[i];
            if (File.Exists(filePath))
            {
                var className = GetFileName(filePath);
                //var serDataFieldList = fieldListObjTemplate;
                var serDataFieldList = string.Format(fieldListTemplate,className+"Template","dataList",className+"Template");

                TemplateValues tplValue = new TemplateValues();
                tplValue.name = className;
                allData.Add(tplValue.name, tplValue);

                using (StreamReader reader = new StreamReader(tpls[i], System.Text.Encoding.UTF8))
                {
                    int lineIndex = 1;
                    string[] fieldNames = null;
                    string[] fieldTypes = null;
                    string[] fieldComments = null;
                    List<string> values = new List<string>();

                    string classTplBody = "";

                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        //Debug.Log("=========:" + line);
                        string[] strings = line.Split('\t');
                        if (lineIndex == 1)
                        {
                            fieldNames = strings;
                        }
                        else if (lineIndex == 2)
                        {
                            fieldTypes = strings;
                        }
                        else if (lineIndex == 3)
                        {
                            fieldComments = strings;
                        }
                        else
                        {
                            values.Add(line);
                        }

                        lineIndex++;
                        line = reader.ReadLine();
                    }
                    if (fieldTypes == null || fieldNames == null)
                    {
                        EditorUtility.DisplayDialog("内部错误", "发生了一些意料之外的错误,请重新操作", "确定");
                        Debug.LogError("出错了！！");
                        return;
                    }
                    else
                    {
                        for (int j = 0; j < fieldTypes.Length; j++)
                        {
                            string fType = fieldTypes[j];
                            string fName = fieldNames[j];
                            string rt = j == fieldTypes.Length - 1 ? "" : "\n\t";

                            Regex reg = new Regex("^\\d");
                            if(reg.IsMatch(fName))
                            {
                                Debug.LogError(string.Format("表中{0}非法字段{1},数字开头", className, fName));
                                EditorUtility.DisplayDialog("错误", string.Format("表中{0}非法字段{1},数字开头", className, fName), "确定");
                                return;
                            }
                            reg = new Regex("^[a-zA-Z0-9_]+$");
                            if(!reg.IsMatch(fName))
                            {
                                Debug.LogError(string.Format("表中{0}非法字段{1},字段只能包含字母[a-zA-Z]，数字[0-9]和下划线_",className, fName));
                                EditorUtility.DisplayDialog("错误", string.Format("表中{0}非法字段{ 1},字段只能包含字母[a - zA - Z]，数字[0 - 9]和下划线_", className, fName), "确定");
                                return;
                            }

                            if(fType != "int" && fType != "string" && fType != "float")
                            {
                                Debug.LogError(string.Format("表中{0}非法字段类型{1},字段类型只能是int/string/float", className, fName));
                                EditorUtility.DisplayDialog("错误", string.Format("表中{0}非法字段类型{1},字段类型只能是int/string/float", className, fName), "确定");
                                return;
                            }

                            if (fType.Contains("list<"))
                            {
                                fType = fType.Substring(5, fType.Length - 6);
                                classTplBody += string.Format(fieldListTemplate, fType, fName, fType) + rt;
                            }
                            else
                            {
                                classTplBody += string.Format(fieldTemplate, fType, fName) + rt;
                            }
                        }

                        tplValue.fieldsCount = fieldNames.Length;
                        foreach (string ln in values)
                        {
                            string[] ss = ln.Split('\t');
                            Dictionary<string,string> dic = new Dictionary<string, string>();
                            for (int k = 0; k < ss.Length; k++)
                            {
                                   dic.Add(fieldNames[k], ss[k]);
                            }
                            tplValue.data.Add(dic);
                        }

                        string classText = string.Format(classTemplate,className,serDataFieldList,className,className,className,classTplBody);
                        WriteToFile(classText, className);
                    }
                }
            }
        }
        AssetDatabase.Refresh();
    }

    static void WriteToFile( string content, string className )
    {
        content = content.Replace("$(", "{").Replace(")$", "}");
        var filePath = generatedCSPath + "/" + className + "SerializedData.cs";
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
        {
            sw.Write(content);
        }
    }

    static string GetFileName( string filePath )
    {
        int lastIndex1 = filePath.LastIndexOf('/');
        int lastIndex2 = filePath.LastIndexOf('.');
        string fileName = filePath.Substring(lastIndex1 + 1, lastIndex2 - lastIndex1 - 1);
        //Debug.Log("=========: " + fileName);
        return fileName;
    }

    static void SaveAsset( string className, TemplateValues values )
    {
        var serData = ScriptableObject.CreateInstance(className + "SerializedData");
        Type t = serData.GetType();
        FieldInfo[] fInfo = t.GetFields();
        MethodInfo mInfo = t.GetMethod("Put");

        List<object> list = new List<object>();
        for (int j = 0; j < values.data.Count; j ++)
        {
            Dictionary<string,string> fieldsMap = values.data[j];
            object obj = t.Assembly.CreateInstance(className + "Template");
            Type objT = obj.GetType();
            FieldInfo[] objFInfos = objT.GetFields();

            foreach (KeyValuePair<string, string> kv in fieldsMap)
            {
                for (int i = 0; i < objFInfos.Length; i++)
                {
                    FieldInfo p = objFInfos[i];
                    if (p.Name.ToLower() == kv.Key.ToLower())
                    {
                        object v = Convert.ChangeType(kv.Value, p.FieldType);
                        p.SetValue(obj, v);
                    }
                }
            }
            list.Add(obj);
        }
        mInfo.Invoke(serData,new object[]{ list});
        AssetDatabase.CreateAsset(serData, serializedDataPath + className + "SerializedData.asset");
    }

    static void DelectDir( string srcPath )
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)            //判断是否文件夹
                {
                    DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                    subdir.Delete(true);          //删除子目录和文件
                }
                else
                {
                    File.Delete(i.FullName);      //删除指定文件
                }
            }
        }
        catch (Exception e)
        {
            throw;
        }
        //AssetDatabase.Refresh();
    }
}
