using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Xml;

public class CSaveFile
{
    static private CSaveFile mInstance = null;

    string strFileName = "SavedData.xml";
    string strFilePath = "";



    public CSettingUI setting = null;

    private CSaveFile()
    {

    }

    public static CSaveFile GetInstance()
    {
        if(mInstance == null)
        {
            mInstance = new CSaveFile();
        }
        return mInstance;
    }

    public void SaveFile()
    {
        XmlDocument xmlDoc = new XmlDocument();

#if UNITY_EDITOR
        strFilePath = Application.dataPath + "/" + strFileName;
#elif UNITY_ANDROID
        strFilePath = Application.persistentDataPath + "/" + strFileName;
#endif

        if (!System.IO.File.Exists(strFilePath))
        {
            CreateFile();
        }
        else
        {
            xmlDoc.Load(strFilePath);

            XmlNodeList ElementList = xmlDoc.SelectNodes("Status");

            foreach (XmlElement NodeElement in ElementList)
            {
                NodeElement.SelectSingleNode("Stage").InnerText = SgtGameData.GetInstance().Stage.ToString();
                NodeElement.SelectSingleNode("Char").InnerText = SgtGameData.GetInstance().CharIndex.ToString();
                NodeElement.SelectSingleNode("Score").InnerText = SgtGameData.GetInstance().Get_Best_Score().ToString();
                NodeElement.SelectSingleNode("SoundBGM").InnerText = CSoundMgr.Getinstance().MusicVolumeLevel.ToString();
                NodeElement.SelectSingleNode("SoundEffect").InnerText = CSoundMgr.Getinstance().EffectVolume.ToString();
                NodeElement.SelectSingleNode("GameSpeed").InnerText = SgtGameData.GetInstance().GameSpeed.ToString();
            }

            xmlDoc.Save(strFilePath);
        }
    }

    public void CreateFile()
    {
#if UNITY_EDITOR
        strFilePath = Application.dataPath + "/" + strFileName;
#elif UNITY_ANDROID
        strFilePath = Application.persistentDataPath + "/" + strFileName;
#endif

        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "Status", string.Empty);
        xmlDoc.AppendChild(root);

        XmlNode stage = xmlDoc.CreateElement("Stage", string.Empty);
        stage.InnerText = SgtGameData.GetInstance().Stage.ToString();

        root.AppendChild(stage); 

        XmlNode character = xmlDoc.CreateElement("Char", string.Empty);
        character.InnerText = SgtGameData.GetInstance().CharIndex.ToString();

        root.AppendChild(character);

        XmlNode score = xmlDoc.CreateElement("Score", string.Empty);
        score.InnerText = SgtGameData.GetInstance().Get_Best_Score().ToString();

        root.AppendChild(score);

        XmlNode SoundBGM = xmlDoc.CreateElement("SoundBGM", string.Empty);
        SoundBGM.InnerText = CSoundMgr.Getinstance().MusicVolumeLevel.ToString();

        root.AppendChild(SoundBGM);

        XmlNode SoundEffect = xmlDoc.CreateElement("SoundEffect", string.Empty);
        SoundEffect.InnerText = CSoundMgr.Getinstance().EffectVolume.ToString();

        root.AppendChild(SoundEffect);

        XmlNode GameSpeed = xmlDoc.CreateElement("GameSpeed", string.Empty);
        GameSpeed.InnerText = SgtGameData.GetInstance().GameSpeed.ToString();

        root.AppendChild(GameSpeed);

        xmlDoc.Save(strFilePath);
    }

    public void LoadFile()
    {
        XmlDocument xmlDoc = new XmlDocument();

#if UNITY_EDITOR
        strFilePath = Application.dataPath + "/" + strFileName;
#elif UNITY_ANDROID
        strFilePath = Application.persistentDataPath + "/" + strFileName;
#endif

        if (!System.IO.File.Exists(strFilePath))
        {
            SaveFile();
        }
        else
        {
            xmlDoc.Load(strFilePath);

            XmlNodeList ElementList = xmlDoc.SelectNodes("Status");

            foreach (XmlElement NodeElement in ElementList)
            {
                SgtGameData.GetInstance().Stage = System.Convert.ToInt32(NodeElement.SelectSingleNode("Stage").InnerText);
                SgtGameData.GetInstance().CharIndex = System.Convert.ToInt32(NodeElement.SelectSingleNode("Char").InnerText);
                SgtGameData.GetInstance().Save_Score(System.Convert.ToInt32(NodeElement.SelectSingleNode("Score").InnerText),0);
                CSoundMgr.Getinstance().MusicVolumeLevel = float.Parse(NodeElement.SelectSingleNode("SoundBGM").InnerText);
                CSoundMgr.Getinstance().EffectVolume = float.Parse(NodeElement.SelectSingleNode("SoundEffect").InnerText);
                SgtGameData.GetInstance().GameSpeed = float.Parse(NodeElement.SelectSingleNode("GameSpeed").InnerText);
            }
        }
    }
}
