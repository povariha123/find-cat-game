using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using YG;

public class Levels : MonoBehaviour
{
    [SerializeField] private GameObject catbox;
    [SerializeField] private Image levelImage;
    [SerializeField] private Text levelCounter, coinsCounter;
    [SerializeField] private Timer levelTimer;
    [SerializeField] private HelpPanel helpPanel;
    [SerializeField] private DevCatSet devCatSet;
    [SerializeField] private Sprite loading;

    public int level;
    public int coins;
    private int loop;
    private Sprite loadedImage;
    private const int imgCount = 555;

    IEnumerator LoadImage(int id)
    {
        if (loadedImage)
        {
            SetImage(id);
        }
        else
        {
            levelImage.sprite = loading;
            yield return StartCoroutine(GetNextImage(id - 1));

            SetImage(id);
        }

        yield break;
    }

    private void SetImage(int id)
    {
        levelImage.sprite = loadedImage;
        loadedImage = null;
        StartCoroutine(GetNextImage(id));
    }

    private IEnumerator GetNextImage(int requestedID)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture($"https://s3.eponesh.com/games/files/15866/img%20({requestedID + 1}).jpg");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            loadedImage = sprite;
        }
    }
    private void OnEnable()
    {
        YandexGame.GetDataEvent += LoadSave;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= LoadSave;
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    private void Start()
    {
        LoadSave();

    }

    private void LoadSave()
    {
        if (YandexGame.SDKEnabled == true)
        {
            level = YandexGame.savesData.lvl;
            coins = YandexGame.savesData.coins;
            loop = YandexGame.savesData.loop;
        }

        catbox.transform.localPosition = devCatSet.saveCords.points[level - 1];
        StartCoroutine(LoadImage(level));
        coinsCounter.text = coins.ToString();
        levelCounter.text = $"Уровень {LoopedLevel()}";
    }

    public void SkipLevel()
    {
        YandexGame.RewVideoShow(0);
    }

    void Rewarded(int id)
    {
        CatHit();
    }

    private int LoopedLevel()
    {
        return level + loop * imgCount;
    }
    
    private void OverrideLevel()
    {
        level = 1;
        YandexGame.savesData.lvl = 1;
        loop++;
        YandexGame.savesData.loop = loop;
    }

    public void CatHit()
    {
        GetComponent<AudioSource>().Play();
        level++;

        if (level > imgCount)
            OverrideLevel();

        coins += Random.Range(1,3);
        coinsCounter.text = coins.ToString();

        YandexGame.FullscreenShow();

        catbox.transform.localPosition = devCatSet.saveCords.points[level - 1];
        StartCoroutine(LoadImage(level));
        levelCounter.text = $"Уровень {LoopedLevel()}";
        levelTimer.UpdateTimer();
        helpPanel.HideHelp();
        SaveData();
    }

    public void SaveData()
    {
        YandexGame.NewLeaderboardScores("TopLevels", LoopedLevel());
        YandexGame.savesData.lvl = level;
        YandexGame.savesData.coins = coins;
        YandexGame.SaveProgress();
    }

    public bool CatIsOnRight()
    {
        if (devCatSet.saveCords.points[level - 1].x >= 0)
            return true;
        else
            return false;
    }    

    public void UpdateCoins()
    {
        coinsCounter.text = coins.ToString();
    }
}
