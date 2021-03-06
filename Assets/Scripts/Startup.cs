using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Startup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        cleanPersistentDataPath();

        // Set up variables in other files
        WFDSManager.persistentDataPath = Application.persistentDataPath;
        WFDSManager.streamingAssetsPath = Application.streamingAssetsPath;
    }

    /// <summary>
    /// Clean the persistent data path.
    /// Deletes all folders and files in the persistent data path.
    /// </summary>
    private void cleanPersistentDataPath()
    {
        DirectoryInfo persistentDataPath = new DirectoryInfo(Application.persistentDataPath);
        foreach (FileInfo file in persistentDataPath.GetFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in persistentDataPath.GetDirectories())
        {
            dir.Delete(true);
        }
    }
}
