using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    readonly string backupExtension = ".bak";
    readonly string encryptionCodeWord = "AaIoeoQdGYGyvtDKnyXh";
    readonly string dataDirPath = "";
    readonly string dataFileName = "";
    readonly bool useEncryption;

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameSettings Load(string profileId, bool allowRestoreFromBackup = true)
    {
        if (profileId == null)
        {
            return null;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        if (!File.Exists(fullPath))
        {
            Debug.Log($"Failed to load game settings from {fullPath}");
            return null;
        }

        GameSettings loadedData = null;
        try
        {
            var dataToLoad = "";
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            if (useEncryption)
            {
                dataToLoad = EncryptDecrypt(dataToLoad);
            }

            loadedData = JsonUtility.FromJson<GameSettings>(dataToLoad);
        }
        catch (Exception e)
        {
            if (allowRestoreFromBackup)
            {
                Debug.LogWarning("Failed to load data file. Attempting to roll back.\n" + e);
                bool rollbackSuccess = AttemptRollback(fullPath);
                if (rollbackSuccess)
                {
                    loadedData = Load(profileId, false);
                }
            }
            else
            {
                Debug.LogError("Error occured when trying to load file at path: "
                               + fullPath + " and backup did not work.\n" + e);
            }
        }

        return loadedData;
    }

    public void Save(GameSettings data, string profileId)
    {
        if (profileId == null)
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        string backupFilePath = fullPath + backupExtension;
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

            GameSettings verifiedGameData = Load(profileId);
            if (verifiedGameData != null)
            {
                File.Copy(fullPath, backupFilePath, true);
            }
            else
            {
                throw new Exception("Save file could not be verified and backup could not be created.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public void Delete(string profileId)
    {
        if (profileId == null)
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        try
        {
            if (File.Exists(fullPath))
            {
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            else
            {
                Debug.LogWarning("Tried to delete profile data, but data was not found at path: " + fullPath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to delete profile data for profileId: "
                           + profileId + " at path: " + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GameSettings> LoadAllProfiles()
    {
        var profileDictionary = new Dictionary<string, GameSettings>();
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: "
                                 + profileId);
                continue;
            }

            GameSettings profileData = Load(profileId);
            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong. ProfileId: " + profileId);
            }
        }

        return profileDictionary;
    }

    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;

        Dictionary<string, GameSettings> profilesGameData = LoadAllProfiles();
        foreach (KeyValuePair<string, GameSettings> pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameSettings gameData = pair.Value;
            if (gameData == null)
            {
                continue;
            }

            if (mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            else
            {
                DateTime mostRecentDateTime =
                    DateTime.FromBinary(profilesGameData[mostRecentProfileId].LastUpdatedTime);
                DateTime newDateTime = DateTime.FromBinary(gameData.LastUpdatedTime);
                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }

        return mostRecentProfileId;
    }
    string EncryptDecrypt(string data)
    {
        var modifiedData = "";
        for (var i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }

        return modifiedData;
    }

    bool AttemptRollback(string fullPath)
    {
        var success = false;
        string backupFilePath = fullPath + backupExtension;
        try
        {
            if (File.Exists(backupFilePath))
            {
                File.Copy(backupFilePath, fullPath, true);
                success = true;
                Debug.LogWarning("Had to roll back to backup file at: " + backupFilePath);
            }
            else
            {
                throw new Exception("Tried to roll back, but no backup file exists to roll back to.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to roll back to backup file at: "
                           + backupFilePath + "\n" + e);
        }

        return success;
    }
}