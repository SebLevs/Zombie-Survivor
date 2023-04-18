using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class PermanentStats : MonoBehaviour
{
    public int permanentSmallGold;
    public int permanentBigGold;
    public const string FilePath = "Assets/DataFile.dat";
    public void GivePermaGold(int goldToGive)
    {
        permanentSmallGold += goldToGive;
        SerializePermaGold();
    }

    private void OnApplicationQuit()
    {
        SerializePermaGold();
    }

    private void Start()
    {
        DeserializePermaGold();
    }

    private void SerializePermaGold()
    {
        FileStream fs = new FileStream(FilePath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, permanentSmallGold);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

    private void DeserializePermaGold()
    {
        FileStream fs = new FileStream(FilePath, FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            permanentSmallGold = (int) formatter.Deserialize(fs);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }
}
