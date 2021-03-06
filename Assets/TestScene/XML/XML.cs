using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
public class XML : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        //电脑和iphong上的路径是不一样的，这里用标签判断一下。
#if UNITY_EDITOR
        string filepath = Application.dataPath + "/StreamingAssets" + "/xml.xml";
#elif UNITY_IPHONE
	  string filepath = Application.dataPath +"/Raw"+"/xml.xml";
#endif
        //如果文件存在话开始解析。	
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("gameObjects").ChildNodes;
            foreach (XmlElement scene in nodeList)
            {
                if (!scene.GetAttribute("name").Equals("Assets/AngryBots.unity"))
                {
                    continue;
                }


                foreach (XmlElement gameObjects in scene.ChildNodes)
                {
                    string objectName = gameObjects.GetAttribute("name");
                    string asset = "Prefab/" + objectName;
                    Vector3 pos = Vector3.zero;
                    Vector3 rot = Vector3.zero;
                    Vector3 sca = Vector3.zero;
                    Vector3 center = Vector3.zero;
                    Vector3 size = Vector3.zero;

                    foreach (XmlElement transform in gameObjects.ChildNodes)
                    {
                        foreach (XmlElement prs in transform.ChildNodes)
                        {
                            if (prs.Name == "position")
                            {
                                foreach (XmlElement position in prs.ChildNodes)
                                {
                                    switch (position.Name)
                                    {
                                        case "x":
                                            pos.x = float.Parse(position.InnerText);
                                            break;
                                        case "y":
                                            pos.y = float.Parse(position.InnerText);
                                            break;
                                        case "z":
                                            pos.z = float.Parse(position.InnerText);
                                            break;
                                    }
                                }
                            }
                            else if (prs.Name == "rotation")
                            {
                                foreach (XmlElement rotation in prs.ChildNodes)
                                {
                                    switch (rotation.Name)
                                    {
                                        case "x":
                                            rot.x = float.Parse(rotation.InnerText);
                                            break;
                                        case "y":
                                            rot.y = float.Parse(rotation.InnerText);
                                            break;
                                        case "z":
                                            rot.z = float.Parse(rotation.InnerText);
                                            break;
                                    }
                                }
                            }
                            else if (prs.Name == "scale")
                            {
                                foreach (XmlElement scale in prs.ChildNodes)
                                {
                                    switch (scale.Name)
                                    {
                                        case "x":
                                            sca.x = float.Parse(scale.InnerText);
                                            break;
                                        case "y":
                                            sca.y = float.Parse(scale.InnerText);
                                            break;
                                        case "z":
                                            sca.z = float.Parse(scale.InnerText);
                                            break;
                                    }
                                }
                            }
                            else if (prs.Name == "bcCenter")
                            {
                                foreach (XmlElement c in prs.ChildNodes)
                                {
                                    switch (c.Name)
                                    {
                                        case "x":
                                            center.x = float.Parse(c.InnerText);
                                            break;
                                        case "y":
                                            center.y = float.Parse(c.InnerText);
                                            break;
                                        case "z":
                                            center.z = float.Parse(c.InnerText);
                                            break;
                                    }
                                }
                            }
                            else if (prs.Name == "bcSize")
                            {
                                foreach (XmlElement s in prs.ChildNodes)
                                {
                                    switch (s.Name)
                                    {
                                        case "x":
                                            size.x = float.Parse(s.InnerText);
                                            break;
                                        case "y":
                                            size.y = float.Parse(s.InnerText);
                                            break;
                                        case "z":
                                            size.z = float.Parse(s.InnerText);
                                            break;
                                    }
                                }
                            }
                        }

                        //GameObject ob = (GameObject)Instantiate(Resources.Load(asset), pos, Quaternion.Euler(rot));
                        //ob.transform.localScale = sca;

                        GameObject ob = new GameObject(objectName);
                        BoxCollider bc = ob.AddComponent<BoxCollider>();
                        ob.transform.position = pos;
                        ob.transform.rotation = Quaternion.Euler(rot);
                        ob.transform.localScale = sca;
                        bc.center = center;
                        bc.size = size;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 200), "XML WORLD"))
        {
            Application.LoadLevel("JSONScene");
        }

    }

}