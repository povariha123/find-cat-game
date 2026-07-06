using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DevCatSet : MonoBehaviour
{
    public SaveCords saveCords;
    [SerializeField] private GameObject catbox;
    [SerializeField] private Levels levels;

    private void Start()
    {
        if (Application.isEditor)
            Load();
        else
            StartCoroutine(LoadCoordinatesFromJson());
    }

    IEnumerator LoadCoordinatesFromJson()
    {
        string jsonPath = Path.Combine(Application.streamingAssetsPath, "CatPos.json");
        UnityWebRequest www = UnityWebRequest.Get(jsonPath);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            saveCords = JsonUtility.FromJson<SaveCords>(jsonString);
            catbox.transform.localPosition = saveCords.points[levels.level - 1];
        }
    }

    public void ApplyCord()
    {
        saveCords.points[levels.level - 1] = catbox.transform.localPosition;
        levels.CatHit();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/CatPos.json", JsonUtility.ToJson(saveCords));
    }

    [ContextMenu("Load")]
    public void Load()
    {
        saveCords = JsonUtility.FromJson<SaveCords>(File.ReadAllText(Application.streamingAssetsPath + "/CatPos.json"));
    }

    [ContextMenu("Delete all")]
    public void Delete()
    {
        saveCords.points = null;
        Save();
        Load();
    }
}