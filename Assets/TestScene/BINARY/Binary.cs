using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
public class Binary : MonoBehaviour
{

    void Start()
    {
        string filepath = Application.dataPath + @"/StreamingAssets/binary.txt";

        if (File.Exists(filepath))
        {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            int index = 0;
            //将二进制字节流全部读取在这个byte数组当中
            //ReadBytes传递的参数是一个长度，也就是流的长度
            byte[] tempall = br.ReadBytes((int)fs.Length);

            //开始解析这个字节数组
            while (true)
            {
                //当超过流长度，跳出循环
                if (index >= tempall.Length)
                {
                    break;
                }

                //得到第一个byte 也就是得到字符串的长度
                int scenelength = tempall[index];
                byte[] sceneName = new byte[scenelength];
                index += 1;
                //根据长度拷贝出对应长度的字节数组
                System.Array.Copy(tempall, index, sceneName, 0, sceneName.Length);
                //然后把字节数组对应转换成字符串
                string sname = System.Text.Encoding.Default.GetString(sceneName);

                int objectLength = tempall[index + sceneName.Length];
                byte[] objectName = new byte[objectLength];

                index += sceneName.Length + 1;
                System.Array.Copy(tempall, index, objectName, 0, objectName.Length);
                string oname = System.Text.Encoding.Default.GetString(objectName);


                //pos
                index += objectName.Length;
                byte[] posx = new byte[4];
                System.Array.Copy(tempall, index, posx, 0, posx.Length);
                float x = System.BitConverter.ToInt32(posx, 0) / 100.0f;

                index += posx.Length;
                byte[] posy = new byte[4];
                System.Array.Copy(tempall, index, posy, 0, posy.Length);
                float y = System.BitConverter.ToInt32(posy, 0) / 100.0f;

                index += posy.Length;
                byte[] posz = new byte[4];
                System.Array.Copy(tempall, index, posz, 0, posz.Length);
                float z = System.BitConverter.ToInt32(posz, 0) / 100.0f;

                //rotate
                index += posz.Length;
                byte[] rotx = new byte[4];
                System.Array.Copy(tempall, index, rotx, 0, rotx.Length);
                float rx = System.BitConverter.ToInt32(rotx, 0) / 100.0f;

                index += rotx.Length;
                byte[] roty = new byte[4];
                System.Array.Copy(tempall, index, roty, 0, roty.Length);
                float ry = System.BitConverter.ToInt32(roty, 0) / 100.0f;

                index += roty.Length;
                byte[] rotz = new byte[4];
                System.Array.Copy(tempall, index, rotz, 0, rotz.Length);
                float rz = System.BitConverter.ToInt32(rotz, 0) / 100.0f;

                //scale
                index += rotz.Length;
                byte[] scax = new byte[2];
                System.Array.Copy(tempall, index, scax, 0, scax.Length);
                float sx = System.BitConverter.ToInt16(scax, 0) / 100.0f;

                index += scax.Length;
                byte[] scay = new byte[2];
                System.Array.Copy(tempall, index, scay, 0, scay.Length);
                float sy = System.BitConverter.ToInt16(scay, 0) / 100.0f;

                index += scay.Length;
                byte[] scaz = new byte[2];
                System.Array.Copy(tempall, index, scaz, 0, scaz.Length);
                float sz = System.BitConverter.ToInt16(scaz, 0) / 100.0f;


                //center
                index += scaz.Length;
                byte[] bcCenterX = new byte[2];
                System.Array.Copy(tempall, index, bcCenterX, 0, bcCenterX.Length);
                float bccx = System.BitConverter.ToInt16(bcCenterX, 0) / 100.0f;

                index += bcCenterX.Length;
                byte[] bcCenterY = new byte[2];
                System.Array.Copy(tempall, index, bcCenterY, 0, bcCenterY.Length);
                float bccy = System.BitConverter.ToInt16(bcCenterY, 0) / 100.0f;

                index += bcCenterY.Length;
                byte[] bcCenterZ = new byte[2];
                System.Array.Copy(tempall, index, bcCenterZ, 0, bcCenterZ.Length);
                float bccz = System.BitConverter.ToInt16(bcCenterZ, 0) / 100.0f;


                //size
                index += bcCenterZ.Length;
                byte[] bcSizeX = new byte[2];
                System.Array.Copy(tempall, index, bcSizeX, 0, bcSizeX.Length);
                float bcsx = System.BitConverter.ToInt16(bcSizeX, 0) / 100.0f;

                index += bcSizeX.Length;
                byte[] bcSizeY = new byte[2];
                System.Array.Copy(tempall, index, bcSizeY, 0, bcSizeY.Length);
                float bcsy = System.BitConverter.ToInt16(bcSizeY, 0) / 100.0f;

                index += bcSizeY.Length;
                byte[] bcSizeZ = new byte[2];
                System.Array.Copy(tempall, index, bcSizeZ, 0, bcSizeZ.Length);
                float bcsz = System.BitConverter.ToInt16(bcSizeZ, 0) / 100.0f;

                index += bcSizeZ.Length;

                if (sname.Equals("Assets/AngryBots.unity"))
                {
                    //string asset = "Prefab/" + oname;
                    Vector3 pos = new Vector3(x, y, z);             
                    Vector3 rot = new Vector3(rx, ry, rz);
                    Vector3 sca = new Vector3(sx, sy, sz);
                    //(GameObject)Instantiate(Resources.Load(asset), pos, Quaternion.Euler(rot));
                    GameObject ob = new GameObject(oname);
                    BoxCollider bc = ob.AddComponent<BoxCollider>();
                    ob.transform.position = pos;
                    ob.transform.rotation = Quaternion.Euler(rot);
                    ob.transform.localScale = sca;
                    
                    Vector3 bcCenter = new Vector3(bccx, bccy, bccz);
                    Vector3 bcSize = new Vector3(bcsx, bcsy, bcsz);
                    bc.center = bcCenter;
                    bc.size = bcSize;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}