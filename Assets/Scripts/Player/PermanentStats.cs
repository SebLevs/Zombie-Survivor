using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class PermanentStats : MonoBehaviour
{
    public int permanentGold;
    public void GivePermaGold(int goldToGive)
    {
        permanentGold += goldToGive;
        SerializePermaGold();
    }

    private void Start()
    {
        DeserializePermaGold();
    }

    private void SerializePermaGold()
    {
        FileStream fs = new FileStream("DataFile.dat", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, permanentGold);
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
        FileStream fs = new FileStream("DataFile.dat", FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            permanentGold = (int) formatter.Deserialize(fs);
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
