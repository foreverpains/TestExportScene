using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExportSceneConfig
{

    public static bool IsExportXml, IsExportJson, IsExportBinary;
    public static string CustomTypeStr = "";

    public ExportSceneConfig()
    {

    }

    public ExportSceneConfig(string jsonStr)
    {
        JsonData obj = JsonMapper.ToObject(jsonStr);
        IDictionary dic = (IDictionary)obj;
        if (dic.Contains("IsExportXml"))
        {
            IsExportXml = (bool)obj["IsExportXml"];
        }
        if (dic.Contains("IsExportJson"))
        {
            IsExportJson = (bool)obj["IsExportJson"];
        }
        if (dic.Contains("IsExportBinary"))
        {
            IsExportBinary = (bool)obj["IsExportBinary"];
        }
        if (dic.Contains("CustomTypeStr"))
        {
            CustomTypeStr = (string)obj["CustomTypeStr"];
        }
    }

    public override string ToString()
    {
        JsonData obj = new JsonData();
        obj["IsExportXml"] = IsExportXml;
        obj["IsExportJson"] = IsExportJson;
        obj["IsExportBinary"] = IsExportBinary;
        obj["CustomTypeStr"] = CustomTypeStr;

        return obj.ToJson();
    }
}
