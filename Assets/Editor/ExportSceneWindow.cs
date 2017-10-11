using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ExportSceneWindow : EditorWindow
{
   
    public static string[] CustomTypeArr;
    ExportSceneConfig exportConfig;
    private string assetPath;

    [MenuItem("Assets/ExportSceneWindow")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ExportSceneWindow));
    }

    void OnEnable()
    {
        assetPath = Application.dataPath + "/StreamingAssets/Config/ExportSceneConfig.json";
        if (File.Exists(assetPath))
        {
            string jsonString = File.ReadAllText(assetPath);
            exportConfig = new ExportSceneConfig(jsonString);
        }
        else
        {
            exportConfig = new ExportSceneConfig();
        }
        
    }
    void OnGUI()
    {
        ExportSceneConfig.IsExportXml = EditorGUILayout.Toggle("XML:", ExportSceneConfig.IsExportXml);
        ExportSceneConfig.IsExportJson = EditorGUILayout.Toggle("JSON:", ExportSceneConfig.IsExportJson);
        ExportSceneConfig.IsExportBinary = EditorGUILayout.Toggle("BINARY:", ExportSceneConfig.IsExportBinary);
        EditorGUILayout.Space();

        ExportSceneConfig.CustomTypeStr = EditorGUILayout.TextField("自定义碰撞类型:", ExportSceneConfig.CustomTypeStr);
        EditorGUILayout.Space();

        if (GUILayout.Button("导出数据"))
        {
            if (ExportSceneConfig.CustomTypeStr != "")
            {
                CustomTypeArr = ExportSceneConfig.CustomTypeStr.Split(',');
            }

            if (ExportSceneConfig.IsExportXml) ExportSceneEditor.ExportXML();
            if (ExportSceneConfig.IsExportJson) ExportSceneEditor.ExportJSON();
            if (ExportSceneConfig.IsExportBinary) ExportSceneEditor.ExportBINARY();

            File.WriteAllText(assetPath, exportConfig.ToString());
        }
    }
}