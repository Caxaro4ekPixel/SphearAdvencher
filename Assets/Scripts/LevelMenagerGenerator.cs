using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenagerGenerator : MonoBehaviour
{
    [SerializeField] private int level;
    
    [SerializeField] private GameObject[] gameObjectsGround;
    [SerializeField] private GameObject[] gameObjectsHouse;
    [SerializeField] private GameObject[] gameObjectsHedge;
    [SerializeField] private GameObject[] gameObjectsInterior;

    [SerializeField] private Material[] skyBoxesDay;
    [SerializeField] private Material[] skyBoxesNight;
    [SerializeField] private Light lightDay;

    void Start()
    {
        System.Random random = new System.Random();

        Material skyBox;

        if (random.Next(0, 101) % 2 == 0)
        {
            skyBox = skyBoxesDay[random.Next(0, skyBoxesDay.Length)];
        }
        else
        {
            skyBox = skyBoxesNight[random.Next(0, skyBoxesNight.Length)];
            lightDay.color = new Color(r: 0f, g: 0f, b: 0f, a: 255f);
        }

        RenderSettings.skybox = skyBox;
        lightDay.intensity = 1;


        int partsCountLevel = level * 20;

        GeneratorLevel generator = new GeneratorLevel();
        bool isDefoult = true;
        for (int part = 0; part < partsCountLevel; part++)
        {
            generator.GeneratePart(gameObjectsGround, gameObjectsHouse, isDefoult);
            isDefoult = false;
        }
    }

    class GeneratorLevel
    {
        private float startX { get; set; }
        private float startY { get; set; }
        private float startZ { get; set; }

        private float defoultStartX = -500f;
        private float defoultStartZ = 490f;

        public GeneratorLevel(float startX = -500f, float startY = -300f, float startZ = 490f)
        {
            this.startX = startX;
            this.startY = startY;
            this.startZ = startZ;
        }

        private int[,] PartPatternGenerator(bool isDefoult)
        {
            int[,] arr = new int[25, 50];
            System.Random random = new System.Random();

            if (!isDefoult)
            {
                int countLines = random.Next(10, 13);
                int startPattern = random.Next(0, (arr.GetLength(1) - countLines - 2)); ;
                int is_pattern = random.Next(0, 101);

                if (is_pattern > 80)
                {
                    int temp = 0;
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        if (i > 4)
                            temp = startPattern + random.Next(0, 2);
                        else
                            temp = startPattern;

                        for (int j = 0; j < countLines; j++)
                        {
                            arr[i, temp] = 2;
                            temp++;
                        }
                    }
                }
            }

            for (int i = 0; i < arr.GetLength(1); i++)
                for (int j = 0; j < 5; j++)
                    if (arr[j, i] == 0)
                        arr[j, i] = 1;

            return arr;
        }

        private int[,] GeneratePatternObjects(int[,] patternGround)
        {
            int[,] arr = new int[patternGround.GetLength(0), patternGround.GetLength(1)];
            System.Random random = new System.Random();

            //Генератор мастов
            int startPos = -1;
            int endPos = -1;
            for (int i = 0; i < arr.GetLength(1); i++)
                if (patternGround[2, i] == 2)
                {
                    if (startPos < 0)
                        startPos = i;
                    endPos = i;
                }

            if (startPos > 0 && endPos > 0)
                if (random.Next(0, 101) % 2 == 0)
                    arr[2, (startPos + ((endPos - startPos + 1) / 2))] = 3;
                else
                    arr[2, startPos + 1] = 4;

            //Генератор короблей
            startPos = -1;
            endPos = -1;
            for (int i = 0; i < arr.GetLength(1); i++)
                if (patternGround[15, i] == 2)
                {
                    if (startPos < 0)
                        startPos = i;
                    endPos = i;
                }

            if (startPos > 0 && endPos > 0)
                if (random.Next(0, 101) % 2 == 1)
                    arr[15, (startPos + ((endPos - startPos + 1) / 2))] = 2;

            return arr;
        }

        public void GeneratePart(GameObject[] groundObj, GameObject[] gameObjectsHouse, bool isDefoultPattern)
        {
            int[,] arrGround = PartPatternGenerator(isDefoultPattern);
            int[,] arrObj = GeneratePatternObjects(arrGround);

            float x,y,z = 0;

            for (int i = 0; i < arrGround.GetLength(0); i++)
            {
                startX = defoultStartX;
                for (int j = 0; j < arrGround.GetLength(1); j++)
                {
                    x = groundObj[arrGround[i, j]].transform.localScale.x;
                    y = groundObj[arrGround[i, j]].transform.localScale.y;
                    z = groundObj[arrGround[i, j]].transform.localScale.z;

                    Instantiate(groundObj[arrGround[i, j]], new Vector3(startX, startY, startZ), Quaternion.identity);

                    if (arrObj[i,j] > 0)
                        Instantiate(gameObjectsHouse[arrObj[i, j]], new Vector3(startX, (startY + gameObjectsHouse[Math.Abs(arrObj[i, j])].transform.position.y), startZ), gameObjectsHouse[arrObj[i, j]].transform.rotation);

                    startX += x;
                }
                startZ += z;
            }
            defoultStartX = startX;
            startZ = defoultStartZ;
        }
    }
}
