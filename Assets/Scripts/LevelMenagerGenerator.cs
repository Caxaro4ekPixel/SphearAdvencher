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
        }

        RenderSettings.skybox = skyBox;
        lightDay.intensity = 1;


        int partsCountLevel = level * 20;

        GeneratorLevel generator = new GeneratorLevel();
        bool is_defoult = true;
        for (int part = 0; part < partsCountLevel; part++)
        {
            generator.GeneratePart(gameObjectsGround, gameObjectsHouse, is_defoult);
            is_defoult = false;
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

        private int[,] PartPatternGenerator(bool is_defoult)
        {
            int[,] arr = new int[25, 50];
            System.Random random = new System.Random();

            if (!is_defoult)
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
            int start_pos = -1;
            int end_pos = -1;
            for (int i = 0; i < arr.GetLength(1); i++)
                if (patternGround[2, i] == 2)
                {
                    if (start_pos < 0)
                        start_pos = i;
                    end_pos = i;
                }

            if (start_pos > 0 && end_pos > 0)
                if (random.Next(0, 101) % 2 == 0)
                    arr[2, (start_pos + ((end_pos - start_pos + 1) / 2))] = 3;
                else
                    arr[2, start_pos + 1] = 4;

            //Генератор короблей
            start_pos = -1;
            end_pos = -1;
            for (int i = 0; i < arr.GetLength(1); i++)
                if (patternGround[15, i] == 2)
                {
                    if (start_pos < 0)
                        start_pos = i;
                    end_pos = i;
                }

            if (start_pos > 0 && end_pos > 0)
                if (random.Next(0, 101) % 2 == 1)
                    arr[15, (start_pos + ((end_pos - start_pos + 1) / 2))] = 2;

            return arr;
        }

        public void GeneratePart(GameObject[] groundObj, GameObject[] gameObjectsHouse, bool is_defoultPattern)
        {
            int[,] arr_ground = PartPatternGenerator(is_defoultPattern);
            int[,] arr_obj = GeneratePatternObjects(arr_ground);

            float x,y,z = 0;

            for (int i = 0; i < arr_ground.GetLength(0); i++)
            {
                this.startX = this.defoultStartX;
                for (int j = 0; j < arr_ground.GetLength(1); j++)
                {
                    x = groundObj[arr_ground[i, j]].transform.localScale.x;
                    y = groundObj[arr_ground[i, j]].transform.localScale.y;
                    z = groundObj[arr_ground[i, j]].transform.localScale.z;

                    Instantiate(groundObj[arr_ground[i, j]], new Vector3(this.startX, this.startY, this.startZ), Quaternion.identity);

                    if (arr_obj[i,j] > 0)
                        Instantiate(gameObjectsHouse[arr_obj[i, j]], new Vector3(this.startX, (this.startY + gameObjectsHouse[Math.Abs(arr_obj[i, j])].transform.position.y), this.startZ), gameObjectsHouse[arr_obj[i, j]].transform.rotation);

                    this.startX += x;
                }
                this.startZ += z;
            }
            this.defoultStartX = this.startX;
            this.startZ = this.defoultStartZ;
        }
    }
}
