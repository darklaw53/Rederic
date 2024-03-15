using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveManager : MonoBehaviour
{
    public PatchworkSave activeSave;

    public static SaveManager instance;

    public bool hasLoaded;

    private void Awake()
    {
        instance = this;

        Load();
    }

    public void Save()
    {
        //HoldmyShit.arrowColectedLvl1A = ArrowCollected.arrowColectedLvl1.ToArray();
        //HoldmyShit.arrowColectedLvl2A = ArrowCollected.arrowColectedLvl2.ToArray();
        //HoldmyShit.arrowColectedLvl3A = ArrowCollected.arrowColectedLvl3.ToArray();
        //HoldmyShit.arrowColectedLvl4A = ArrowCollected.arrowColectedLvl4.ToArray();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveData.cum");

        PatchworkSave data = new PatchworkSave();

        data.keepScore = HoldmyShit.keepScore;
        data.keepScore2 = HoldmyShit.keepScore2;
        data.keepScore3 = HoldmyShit.keepScore3;
        data.keepScore4 = HoldmyShit.keepScore4;
        data.keepScore5 = HoldmyShit.keepScore5;
        data.keepHealth = HoldmyShit.keepHealth;
        data.keepHealth2 = HoldmyShit.keepHealth2;
        data.keepHealth3 = HoldmyShit.keepHealth3;
        data.keepHealth4 = HoldmyShit.keepHealth4;
        data.keepHealth5 = HoldmyShit.keepHealth5;
        data.arrows = HoldmyShit.arrows;
        data.arrows2 = HoldmyShit.arrows2;
        data.arrows3 = HoldmyShit.arrows3;
        data.arrows4 = HoldmyShit.arrows4;
        data.arrows5 = HoldmyShit.arrows5;
        data.volume = HoldmyShit.volume;
        data.lvl1 = HoldmyShit.lvl1;
        data.lvl2 = HoldmyShit.lvl2;
        data.lvl3 = HoldmyShit.lvl3;
        data.lvl4 = HoldmyShit.lvl4;
        data.lvl5 = HoldmyShit.lvl5;
        data.lvl1R = HoldmyShit.lvl1R;
        data.lvl2R = HoldmyShit.lvl2R;
        data.lvl3R = HoldmyShit.lvl3R;
        data.lvl4R = HoldmyShit.lvl4R;
        data.lvl5R = HoldmyShit.lvl5R;
        data.BountyCompleteR = HoldmyShit.BountyCompleteR;
        data.JugglingR = HoldmyShit.JugglingR;
        data.HoarderR = HoldmyShit.HoarderR;
        data.FrugalR = HoldmyShit.FrugalR;
        data.UnloadQuiverR = HoldmyShit.UnloadQuiverR;
        data.DoNoEvilR = HoldmyShit.DoNoEvilR;
        data.hasMadePurchase = HoldmyShit.hasMadePurchase;
        data.hasMadePurchaseStep = HoldmyShit.hasMadePurchaseStep;
        //data.gotAllArrows1 = HoldmyShit.gotAllArrows1;
        //data.gotAllArrows2 = HoldmyShit.gotAllArrows2;
        //data.gotAllArrows3 = HoldmyShit.gotAllArrows3;
        //data.gotAllArrows4 = HoldmyShit.gotAllArrows4;
        data.hasDoneAKill = HoldmyShit.hasDoneAKill;
        data.totalArrowKills = HoldmyShit.totalArrowKills;
        //data.arrowColectedLvl2A = HoldmyShit.arrowColectedLvl2A;
        //data.arrowColectedLvl3A = HoldmyShit.arrowColectedLvl3A;
        //data.arrowColectedLvl4A = HoldmyShit.arrowColectedLvl4A;

        bf.Serialize(file,data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData.cum"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.cum", FileMode.Open);

            PatchworkSave data = (PatchworkSave)bf.Deserialize(file);
            file.Close();

            HoldmyShit.keepScore = data.keepScore;
            HoldmyShit.keepScore2 = data.keepScore2;
            HoldmyShit.keepScore3 = data.keepScore3;
            HoldmyShit.keepScore4 = data.keepScore4;
            HoldmyShit.keepScore5 = data.keepScore5;
            HoldmyShit.keepHealth = data.keepHealth;
            HoldmyShit.keepHealth2 = data.keepHealth2;
            HoldmyShit.keepHealth3 = data.keepHealth3;
            HoldmyShit.keepHealth4 = data.keepHealth4;
            HoldmyShit.keepHealth5 = data.keepHealth5;
            HoldmyShit.arrows = data.arrows;
            HoldmyShit.arrows2 = data.arrows2;
            HoldmyShit.arrows3 = data.arrows3;
            HoldmyShit.arrows4 = data.arrows4;
            HoldmyShit.arrows5 = data.arrows5;
            HoldmyShit.volume = data.volume;
            HoldmyShit.lvl1 = data.lvl1;
            HoldmyShit.lvl2 = data.lvl2;
            HoldmyShit.lvl3 = data.lvl3;
            HoldmyShit.lvl4 = data.lvl4;
            HoldmyShit.lvl5 = data.lvl5;
            HoldmyShit.lvl1R = data.lvl1R;
            HoldmyShit.lvl2R = data.lvl2R;
            HoldmyShit.lvl3R = data.lvl3R;
            HoldmyShit.lvl4R = data.lvl4R;
            HoldmyShit.lvl5R = data.lvl5R;
            HoldmyShit.BountyCompleteR = data.BountyCompleteR;
            HoldmyShit.JugglingR = data.JugglingR;
            HoldmyShit.HoarderR = data.HoarderR;
            HoldmyShit.FrugalR = data.FrugalR;
            HoldmyShit.UnloadQuiverR = data.UnloadQuiverR;
            HoldmyShit.DoNoEvilR = data.DoNoEvilR;
            HoldmyShit.hasMadePurchase = data.hasMadePurchase;
            HoldmyShit.hasMadePurchaseStep = data.hasMadePurchaseStep;
            //HoldmyShit.gotAllArrows1 = data.gotAllArrows1;
            //HoldmyShit.gotAllArrows2 = data.gotAllArrows2;
            //HoldmyShit.gotAllArrows3 = data.gotAllArrows3;
            //HoldmyShit.gotAllArrows4 = data.gotAllArrows4;
            HoldmyShit.hasDoneAKill = data.hasDoneAKill;
            HoldmyShit.totalArrowKills = data.totalArrowKills;
            //HoldmyShit.arrowColectedLvl2A = data.arrowColectedLvl2A;
            //HoldmyShit.arrowColectedLvl3A = data.arrowColectedLvl3A;
            //HoldmyShit.arrowColectedLvl4A = data.arrowColectedLvl4A;

            hasLoaded = true;

            //ArrowCollected.arrowColectedLvl1.AddRange(HoldmyShit.arrowColectedLvl1A);
            //ArrowCollected.arrowColectedLvl2.AddRange(HoldmyShit.arrowColectedLvl2A);
            //ArrowCollected.arrowColectedLvl3.AddRange(HoldmyShit.arrowColectedLvl3A);
            //ArrowCollected.arrowColectedLvl4.AddRange(HoldmyShit.arrowColectedLvl4A);
            /*
            Debug.Log("keepScore " + HoldmyShit.keepScore);
            Debug.Log("keepScore2 " + HoldmyShit.keepScore2);
            Debug.Log("keepScore3 " + HoldmyShit.keepScore3);
            Debug.Log("keepScore4 " + HoldmyShit.keepScore4);
            Debug.Log("keepScore5 " + HoldmyShit.keepScore5);
            Debug.Log("keepHealth " + HoldmyShit.keepHealth);
            Debug.Log("keepHealth2 " + HoldmyShit.keepHealth2);
            Debug.Log("keepHealth3 " + HoldmyShit.keepHealth3);
            Debug.Log("keepHealth4 " + HoldmyShit.keepHealth4);
            Debug.Log("keepHealth5 " + HoldmyShit.keepHealth5);
            Debug.Log("arrows " + HoldmyShit.arrows);
            Debug.Log("arrows2 " + HoldmyShit.arrows2);
            Debug.Log("arrows3 " + HoldmyShit.arrows3);
            Debug.Log("arrows4 " + HoldmyShit.arrows4);
            Debug.Log("arrows5 " + HoldmyShit.arrows5);
            Debug.Log("volume " + HoldmyShit.volume);
            Debug.Log("lvl1 " + HoldmyShit.lvl1);
            Debug.Log("lvl2 " + HoldmyShit.lvl2);
            Debug.Log("lvl3 " + HoldmyShit.lvl3);
            Debug.Log("lvl4 " + HoldmyShit.lvl4);
            Debug.Log("lvl5 " + HoldmyShit.lvl5);
            Debug.Log("lvl1R " + HoldmyShit.lvl1R);
            Debug.Log("lvl2R " + HoldmyShit.lvl2R);
            Debug.Log("lvl3R " + HoldmyShit.lvl3R);
            Debug.Log("lvl4R " + HoldmyShit.lvl4R);
            Debug.Log("lvl5R " + HoldmyShit.lvl5R);
            Debug.Log("BountyCompleteR " + HoldmyShit.BountyCompleteR);
            Debug.Log("JugglingR " + HoldmyShit.JugglingR);
            Debug.Log("HoarderR " + HoldmyShit.HoarderR);
            Debug.Log("FrugalR " + HoldmyShit.FrugalR);
            Debug.Log("UnloadQuiverR " + HoldmyShit.UnloadQuiverR);
            Debug.Log("DoNoEvilR " + HoldmyShit.DoNoEvilR);
            Debug.Log("hasMadePurchase " + HoldmyShit.hasMadePurchase);
            Debug.Log("hasMadePurchaseStep " + HoldmyShit.hasMadePurchaseStep);
            //Debug.Log("gotAllArrows1 " + HoldmyShit.gotAllArrows1);
            //Debug.Log("gotAllArrows2 " + HoldmyShit.gotAllArrows2);
            //Debug.Log("gotAllArrows3 " + HoldmyShit.gotAllArrows3);
            //Debug.Log("gotAllArrows4 " + HoldmyShit.gotAllArrows4);
            Debug.Log("hasDoneAKill " + HoldmyShit.hasDoneAKill);
            Debug.Log("totalArrowKills " + HoldmyShit.totalArrowKills);
            //Debug.Log("arrowColectedLvl2A " + HoldmyShit.arrowColectedLvl2A);
            //Debug.Log("arrowColectedLvl3A " + HoldmyShit.arrowColectedLvl3A);
            //Debug.Log("arrowColectedLvl4A " + HoldmyShit.arrowColectedLvl4A);
            */
        }
    }
}

[System.Serializable]
public class PatchworkSave
{
    public int keepScore;
    public int keepScore2;
    public int keepScore3;
    public int keepScore4;
    public int keepScore5;
    public int keepHealth;
    public int keepHealth2;
    public int keepHealth3;
    public int keepHealth4;
    public int keepHealth5;
    public int arrows;
    public int arrows2;
    public int arrows3;
    public int arrows4;
    public int arrows5;
    public float volume;
    public bool lvl1;
    public bool lvl2;
    public bool lvl3;
    public bool lvl4;
    public bool lvl5;
    public bool lvl1R;
    public bool lvl2R;
    public bool lvl3R;
    public bool lvl4R;
    public bool lvl5R;
    public bool BountyCompleteR, JugglingR, HoarderR, FrugalR, UnloadQuiverR, DoNoEvilR;
    public bool hasMadePurchase;
    public bool hasMadePurchaseStep;
    public bool hasDoneAKill;

    public int totalArrowKills;
}
