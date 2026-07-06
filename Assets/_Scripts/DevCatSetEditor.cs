//using System.IO;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(DevCatSet))]
//public class DevCatSetEditor : Editor
//{

//    public override void OnInspectorGUI()
//    {
//        SaveCords saveCords = new();
//        if (GUILayout.Button("Save"))
//            File.WriteAllText(Application.streamingAssetsPath + "/CatPos.json", JsonUtility.ToJson(saveCords));
//        if (GUILayout.Button("Load"))
//            saveCords = JsonUtility.FromJson<SaveCords>(File.ReadAllText(Application.streamingAssetsPath + "/CatPos.json"));
//    }
//}