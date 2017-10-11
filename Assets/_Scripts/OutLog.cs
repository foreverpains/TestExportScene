using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using UnityEngine.Networking;

public class OutLog : MonoBehaviour
{
    static List<string> mLines = new List<string>();
    static List<string> mWriteTxt = new List<string>();
    private static string m_outPath;
    private static string m_fileName;
    private static string m_pathName;

    public static string OutPath
    {
        get
        {
            return m_outPath;
        }
    }

    public static string FileName
    {
        get
        {
            return m_fileName;
        }
    }

    void Awake()
    {
        mLines.Clear();
        mWriteTxt.Clear();
        //Application.persistentDataPath Unity中只有这个路径是既可以读也可以写的。
        m_fileName = "outLog_"+ System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".txt";
        m_outPath = Application.persistentDataPath;
        m_pathName = m_outPath +"/"+ m_fileName;
        //每次启动客户端删除之前保存的Log
        if (System.IO.File.Exists(m_pathName))
        {
            File.Delete(m_pathName);
        }
        //在这里做一个Log的监听
        Application.logMessageReceived += HandleLog;
        //一个输出
        Debug.Log("###############Start Game################");
       
    }

    void Update()
    {
        //因为写入文件的操作必须在主线程中完成，所以在Update中哦给你写入文件。
        if (mWriteTxt.Count > 0)
        {
            string[] temp = mWriteTxt.ToArray();
            foreach (string t in temp)
            {
                using (StreamWriter writer = new StreamWriter(m_pathName, true, Encoding.UTF8))
                {
                    writer.WriteLine(t);
                }
                mWriteTxt.Remove(t);
            }
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        mWriteTxt.Add(logString);
        if (type == LogType.Error || type == LogType.Exception)
        {
            Log(logString);
            Log(stackTrace);
        }
    }

    //这里我把错误的信息保存起来，用来输出在手机屏幕上
    static public void Log(params object[] objs)
    {
        string text = "";
        for (int i = 0; i < objs.Length; ++i)
        {
            if (i == 0)
            {
                text += objs[i].ToString();
            }
            else
            {
                text += ", " + objs[i].ToString();
            }
        }
        if (Application.isPlaying)
        {
            if (mLines.Count > 20)
            {
                mLines.RemoveAt(0);
            }
            mLines.Add(text);

        }
    }

    void OnGUI()
    {
        //GUI.color = Color.red;
        //for (int i = 0, imax = mLines.Count; i < imax; ++i)
        //{
        //    GUILayout.Label(mLines[i]);
        //}
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }
}