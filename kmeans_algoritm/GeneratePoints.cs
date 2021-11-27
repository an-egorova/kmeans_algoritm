using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kmeans_algoritm
{
    class GeneratePoints
    {
        int[,] centroids;
        int vecCount;
        int clusterCount;
        int vecRange;
        int[,] vectors;
        int delta;
        int[,] vectorCoord;
        List<int[,]> centers = new List<int[,]>();

        List<int[,]> listOfVectors = new List<int[,]>();
        public int[,] Centroids { get; set; }
        public List<int[,]> ListOfVectors { get; set; }
        public int[,] VectorCoord { get; set; }
        //метод для генерации начальных центроидов
        public void generateCentroids(int vecCount, int vecRange, int clusterCount, int delta)
        {
            thisInitializer(vecCount, vecRange, clusterCount, delta);
            Random random = new Random();
            //начальные точки, от которых стартую рандом
            int x1 = random.Next(50);
            int y1 = random.Next(50);
            var lasty = 0;
            var lastx = 0;
            var center = 0;
            centroids = new int[this.clusterCount, this.vecRange];
            do {
                //для каждого кластера начинаем генерить вектора
                //координаты х
                for (int i = 0; i < this.clusterCount; i++)
                {
                    //координаты у
                    for (int j = 0; j < this.vecRange; j++)
                    {
                        center = random.Next(-450 + x1, 450 + y1);
                        
                            centroids[i, j] = center;
                            lasty = center;
                        
                    }
                }
            } while (!checkCeneters());
            Centroids = centroids;
            generateClasterPoints();
        }

        private bool checkCeneters()
        {
            List<int> colx = new List<int>();
            for (int j = 0; j < centroids.GetLength(1); j++)
            {
                for (int i = 0; i < centroids.GetLength(0); i++)
                {
                    colx.Add(centroids[i, j]);
                }

                for (int k = 0; k < colx.Count(); k++)
                {
                    try
                    {
                        if (Math.Abs(colx[k] - colx[k + 1]) < delta * 2 + 10)
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        if (Math.Abs(colx[k] - colx[0]) < delta * 2 + 10)
                        {
                            return false;//regenerate
                        }
                    }
                }
            }
            return true;
        }
        private void thisInitializer(int vecCount, int vecRange, int clusterCount, int delta) 
        {
            this.vecRange = vecRange;
            this.vecCount = vecCount;
            this.clusterCount = clusterCount;
            this.delta = delta;
        }
        //метод для генерации точек для кластера
        private void generateClasterPoints()
        {
            //txt_Result.Text = String.Empty;
            Random random = new Random();
            //vectorCoord = new int[vecCount*clusterCount, vecRange];
            //для каждого кластера начинаем генерить вектора
            for (int clusterNum = 0; clusterNum < clusterCount; clusterNum++)
            {
                vectors = new int[vecCount, vecRange];
                var zx = centroids[clusterNum, 0];
                var zy = centroids[clusterNum, 1];
                //координаты х
                for (int i = 0; i < vecCount; i++)
                {
                    //координаты у
                    for (int j = 0; j < vecRange; j++)
                    {
                        if (j == 0) vectors[i, j] = random.Next(zx - this.delta, zx + this.delta);
                        else vectors[i, j] = random.Next(zy - this.delta, zy + this.delta);

                        //vectorCoord[i, j] = vectors[i, j];
                    }

                }
                listOfVectors.Add(vectors);
                vectors = null;
            }
            fillVectorFromList();
            ListOfVectors = listOfVectors;
        }

        private void fillVectorFromList()
        {
            int vector;
            vectorCoord = new int[vecCount * clusterCount, vecRange];
            //выводим кластеры
            int h = 0;//<clusterCount*vecCount
            int d = 0; //<vecRange
            foreach (var item in listOfVectors)
            {
                for (int k = 0; k < item.GetLength(0); k++)
                {
                    d = 0;
                    for (int g = 0; g < item.GetLength(1); g++)
                    {
                        //vector = item[k, g];

                        vectorCoord[h, d] = item[k, g];
                        d++;
                    }
                    h++;
                }

            }
            VectorCoord = vectorCoord;
        }

        public void fillDataFromFile()
        { 
        
        }
    }
}
