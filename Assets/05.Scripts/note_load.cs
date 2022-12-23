using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class note_load : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string file_name = "tmp";
        string path = Application.dataPath + "/" + "\\tmp\\" + file_name + ".json";
        string json = File.ReadAllText(path);
        Debug.Log(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
