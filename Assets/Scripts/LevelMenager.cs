using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenager : MonoBehaviour
{
    [SerializeField] private int levelNum;
    private string nextLevel;
    [SerializeField] private int levelPartCount = 20;

    [SerializeField] protected GameObject[] gameObjectsGround;
    [SerializeField] protected GameObject[] gameObjectsObjects;

    [SerializeField] private Material[] skyBoxesDay;
    [SerializeField] private Material[] skyBoxesNight;
    [SerializeField] private Light lightDay;
    [SerializeField] private GameObject stars;

    [SerializeField] private SphereCollider player;
    [SerializeField] private Text text;

    private MeshCollider[] coins;
    private int coin—ounter;
    private int allCoin—ounter;

    private GameObject coinTemp;

    private GameObject[] temp;

    private Vector3 positionCenter;
    private float diameter;
    private System.Random random;
    private Material skyBox;

    private void Start()
    {
        random = new System.Random();

        bool stateDay;

        if (random.Next(0, 101) % 2 == 0)
        {
            skyBox = skyBoxesDay[random.Next(0, skyBoxesDay.Length)];
            stateDay = true;
        }
        else
        {
            skyBox = skyBoxesNight[random.Next(0, skyBoxesNight.Length)];
            stateDay = false;
            lightDay.color = new Color(r: 0f, g: 0f, b: 0f, a: 255f);
            stars.SetActive(true);
        }

        GameObject[] gameObjectLigth = GameObject.FindGameObjectsWithTag("LampLight");
        if (stateDay)
            foreach (GameObject obj in gameObjectLigth)
                obj.SetActive(false);

        RenderSettings.skybox = skyBox;
        lightDay.intensity = 1;

        coin—ounter = 0;
        temp = GameObject.FindGameObjectsWithTag("Coin");
        MeshCollider[] tempCollidersCoin = new MeshCollider[temp.Length];

        for (int i = 0; i < temp.Length; i++)
            tempCollidersCoin[i] = temp[i].GetComponent<MeshCollider>();

        coins = tempCollidersCoin;

        allCoin—ounter = coins.Length;
        coin—ounter = 0;

        nextLevel = "Level_" + (levelNum + 1);

        //«‡‰ÂÎ Ì‡ ·Û‰Û˘ÂÂ :)
        //GenerateLevel(levelNum * levelPartCount, 25, 50);
    }

    private void FixedUpdate()
    {
        positionCenter = player.transform.position + player.center;
        diameter = player.radius * 2;

        coin—ounter = allCoin—ounter - GameObject.FindGameObjectsWithTag("Coin").Length;


        Collider[] coins = Physics.OverlapSphere(positionCenter, diameter);
        foreach (Collider coin in coins)
        {
            if (coin.CompareTag("Coin"))
            {
                coin.gameObject.GetComponent<Animator>().SetBool("Take", true);
            }
        }

        text.text = $" ÓÎ-‚Ó ÏÓÌÂÚÓÍ: {coin—ounter}/{allCoin—ounter}";

        if (coin—ounter == allCoin—ounter)
            SceneManager.LoadScene(nextLevel);
    }

    //class ObjectLevel
    //{
    //    public ObjectLevel(GameObject gameObject, float x, float y, float z, float rx, float ry, float rz)
    //    {
    //        this.gameObject = gameObject;
    //        this.x = x;
    //        this.y = y;
    //        this.z = z;
    //        this.rx = rx;
    //        this.ry = ry;
    //        this.rz = rz;
    //    }

    //    public ObjectLevel(GameObject gameObject, float x, float y, float z)
    //    {
    //        this.gameObject = gameObject;
    //        this.x = x;
    //        this.y = y;
    //        this.z = z;
    //    }

    //    public GameObject gameObject { get; }
    //    public float x { get; }
    //    public float y { get; }
    //    public float z { get; }
    //    public float rx { get; }
    //    public float ry { get; }
    //    public float rz { get; }
    //}

    //class PartLevel
    //{
    //    private ObjectLevel[,,] objects;
    //    private GameObject[] gameObjectsGround;
    //    private GameObject[] gameObjectsObjects;

    //    public PartLevel(int objectsX, int objectsY, GameObject[] gameObjectsGround, GameObject[] gameObjectsObjects)
    //    {
    //        this.objects = new ObjectLevel[objectsX, objectsY, 3];
    //        this.gameObjectsGround = gameObjectsGround;
    //        this.gameObjectsObjects = gameObjectsObjects;
    //    }

    //    public float[] GeneratePattern(float[] lastCoord)
    //    {
    //        System.Random random = new System.Random();
    //        float x = lastCoord[0];
    //        float y = lastCoord[1];
    //        float z = lastCoord[2];

    //        //√ÂÌÂ‡ˆËˇ ‰ÓÓ„Ë
    //        for (int objectsX = 0; objectsX < 5; objectsX++)
    //        {
    //            x = lastCoord[0];
    //            for (int objectsY = 0; objectsY < this.objects.GetLength(1); objectsY++)
    //            {
    //                objects[objectsX, objectsY, 0] = new ObjectLevel(this.gameObjectsGround[0], x, y, z);
    //                x += this.gameObjectsGround[0].transform.localScale.x;
    //            }
    //            z += this.gameObjectsGround[0].transform.localScale.z;
    //        }
    //        //√ÂÌÂ‡ˆËˇ ÁÂÏÎË
    //        for (int objectsX = 5; objectsX < this.objects.GetLength(0); objectsX++)
    //        {
    //            x = lastCoord[0];
    //            for (int objectsY = 0; objectsY < this.objects.GetLength(1); objectsY++)
    //            {
    //                objects[objectsX, objectsY, 0] = new ObjectLevel(this.gameObjectsGround[1], x, y, z);
    //                x += this.gameObjectsGround[1].transform.localScale.x;
    //            }
    //            z += this.gameObjectsGround[0].transform.localScale.z;
    //        }
    //        lastCoord[0] = x;

    //        //√ÂÌÂ‡ˆËˇ –ÂÍË
    //        if (random.Next(0,101) > 90)
    //        {

    //        }

    //        return lastCoord;
    //    }

    //    public void CreatePattern()
    //    {
    //        for (int objectsX = 0; objectsX < this.objects.GetLength(0); objectsX++)
    //        {
    //            for (int objectsY = 0; objectsY < this.objects.GetLength(1); objectsY++)
    //            {
    //                //for (int objectsZ = 0; objectsX < this.objects.GetLength(2); objectsZ++)
    //                //{
    //                Instantiate(this.objects[objectsX, objectsY, 0].gameObject,
    //                    new Vector3(
    //                        this.objects[objectsX, objectsY, 0].x,
    //                        this.objects[objectsX, objectsY, 0].y,
    //                        this.objects[objectsX, objectsY, 0].z),
    //                    Quaternion.identity
    //                    );
    //                //}
    //            }
    //        }
    //    }
    //}

    //private PartLevel[] partsLevel;
    //public void GenerateLevel(int partCount, int partCountX, int partCountY)
    //{
    //    partsLevel = new PartLevel[partCount];
    //    float[] lastCoord = new float[] { -500f, -300f, 490f };
    //    for (int part = 0; part < partsLevel.Length; part++)
    //    {
    //        partsLevel[part] = new PartLevel(partCountX, partCountY, gameObjectsGround, gameObjectsObjects);
    //        lastCoord = partsLevel[part].GeneratePattern(lastCoord);
    //    }

    //    for (int part = 0; part < partsLevel.Length; part++)
    //    {
    //        partsLevel[part].CreatePattern();
    //    }
    //}
}
