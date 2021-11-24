using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace kmeans_algoritm
{
    public partial class Form1 : Form
    {
        /// Основные входные данные:
        ///1) Количество векторов - vecCount
        ///2) Числовой диапазон векторов - vecRange
        ///3) Количество кластеров - clusterCount
        ///4) Эпсилон(нужно для расчета J, по сути сколько итераций будет произведено) - Eps

        ///Общий алгоритм:
        ///1) Генерация векторов
        ///2) Выбор центров
        ///3) Распределение по кластерам
        ///4) Проверка критерия J.Если разница между нынешним и предыдущим меньше, чем Эпсилон - заканчиваем алгоритм/ иначе повторяем пункты 2-3

        ///Основные входные данные:
        ///1) Количество векторов
        ///2) Размерность векторов
        ///3) Количество кластеров
        ///4) Эпсилон(нужно для расчета J, по сути сколько итераций будет произведено)

        ///1 задача:
        ///Генерация векторов как говорил Коган
        ///Входные данные: 1,2 + диапазон рандома, который входит в 2
        ///Выходные данные: сгенерированные векторы (одномерные массивы)



        //ИСХОДНЫЕ ДАННЫЕ//

        int vecCount = 0;//количество векторов
        int vecRange = 0;//размерность векторов
        public static int clusterCount = 0; //количество кластеров
        int Eps = 0;//дельта для генерации 
        public static int iteration = 0; //количество итераций
        public static double correspondence = 0;//число совпадений
        public static int countIterationVar = 0;//макс. количество итераций
        public static double minEqualVar = 0;//минимальная совместимость
        double bestPartitionJ0;//массив центроидов кластера J0 (что-то, что Коган назван Эпсилон)
        double bestPartitionJ1;//массив центроидов кластера J1 (что-то, что Коган назван Эпсилон)

        int[,] vectors; //[vecCount, vecRange];
        int[,] centroids; //[vecCount, vecRange];
        int[,] matrixToRead;//массив для чтения из файла
        int[] pointCoord; //выбор вектора для центра кластера (рандом)
        double[,] sumPoint; //расстояние от каждого вектора до центра (первая ячейка - центр, вторая - расстояние от вектора)
        public int[,] centroidsCluster; //координата центра кластера
        public static int[,] vectorCoord; //двумерный массив с координатами векторов
        public static double[] bestPartition;//массив центроидов кластеров по всем итерациям (что-то, что Коган назван Эпсилон)
        int[] minimum;//кластер, до которого минимальное расстояние от вектора
        //double[] bestPartitionJ0;//массив центроидов кластера J0 (что-то, что Коган назван Эпсилон)
        //double[] bestPartitionJ1;//массив центроидов кластера J1 (что-то, что Коган назван Эпсилон)
        //сравнение по центрам, т.к. если вектора не будут менять кластеры, то центры не будут меняться
        int[,] check_1; //предыдущая итерация
        int[,] check_2; //текущая итерация
        List<int[,]> listOfVectors = new List<int[,]>();
        string stringForSaveIntoFile = String.Empty;//строка для сохранения в файл массива точек
        public static int[,,] clasteringAllInfo; //массив, который хранит информацию о всех итерациях [итерация|номер вектора|номер кластера, которому принадлежит вектор]
        int[,] clasteringInfo; //массив принадлежности вектора кластеру в одной итерации [номер вектора|номер кластера, которому принадлежит вектор] (оставлено для отладки)
        public static bool nextClasterization; //продолжаем кластеризацию, меняя J
        public static int countAllVec;//количество всех векторов

        public static int[,,] centroidsClusterAll;//массив координат цетров по всем итерациям
        public static int graphCount=0;
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        // кнопка запуска генерации векторов с рандомным набором значений 
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            lblError.Text = String.Empty;
            //запускает генератор только если входные данные корректны
            if (checkCheckBoxes())
            {
                lblError.Text = String.Format("вошли в генератор");//для дебага
                generateCentroids();
                generateClasterPoints();

                //использовала в качестве дебага, но может пригодиться
                //метод загружает в текст бокс векторы
                loadVectorsIntoTextBox();
                btnClusterization.Enabled = true;
                btnSaveFile.Enabled = true;
            }
            else lblError.Text += String.Format("\n" + "Ошибка входных данных");

        }

        //метод для генерации начальных центроидов
        private void generateCentroids()
        {
            Random random = new Random();
            //начальные точки, от которых стартую рандом
            int x1 = random.Next(50);
            int y1 = random.Next(50);
            centroids = new int[vecCount, clusterCount];
            //для каждого кластера начинаем генерить вектора
            //координаты х
            for (int i = 0; i < clusterCount; i++)
            {
                //координаты у
                for (int j = 0; j < vecRange; j++)
                {
                    centroids[i, j] = random.Next(-450 +i*20 + x1, 450 - i * 20 + y1);
                }
            }

        }

        //метод для генерации точек для кластера
        private void generateClasterPoints()
        {
            txt_Result.Text = String.Empty;
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
                        if (j == 0) vectors[i, j] = random.Next(zx - Eps, zx + Eps);
                        else vectors[i, j] = random.Next(zy - Eps, zy + Eps);
                        
                        //vectorCoord[i, j] = vectors[i, j];
                    }

                }
                listOfVectors.Add(vectors);
                vectors = null;
            }
            fillVectorFromList();
        }

        private void fillVectorFromList()
        {
            int vector;
            vectorCoord = new int[vecCount*clusterCount, vecRange];
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

        }

        //использовала для дебага, выводит массив веторов в текст бокс
        private void loadVectorsIntoTextBox()
        {
            txt_Result.Text = String.Empty;
            string vector = String.Empty;

            //выводим цетроиды
            for (int i = 0; i < clusterCount; i++)
            {
                for (int j = 0; j < vecRange; j++)
                {
                    vector += centroids[i, j] + " ";
                }
                txt_Result.Text += "Z[" + i.ToString() + "] = { " + vector + "}\n";
                vector = String.Empty;
            }

            int c = 0;
            //выводим кластеры
            foreach (var item in listOfVectors)
            {
                for (int k = 0; k < item.GetLength(0); k++)
                {
                    vector += item[k, 0] + ", " + item[k, 1] + "\n";
                }
                txt_Result.Text += "Cluster[" + c + "] = { " + vector + "}\n";
                stringForSaveIntoFile += vector;
                vector = String.Empty;
                c++;
            }
        }

        //производим проверку на корректность введенных данных
        private bool checkCheckBoxes()
        {
            bool check1 = false;
            bool check2 = false;
            bool check3 = false;
            bool check5 = false;
            bool check6 = false;
            bool check7 = false;

            try
            {
                countIterationVar = Convert.ToInt32(txt_countIteration.Text);
                check3 = (countIterationVar > 0) ? true : false;
            }
            catch
            {
                lblError.Text = String.Format("Ошибка данных в поле <Количество итераций>");
                return false;
            }
            try
            {
                minEqualVar = Convert.ToDouble(txt_minEqual.Text);
                check7 = (minEqualVar > 0) ? true : false;
            }
            catch
            {
                lblError.Text = String.Format("Ошибка данных в поле <Эпсилон");
                return false;
            }
            //проверка 1 поля - количество векторов
            try
            {
                vecCount = Convert.ToInt32(txt_vecCount.Text);
                check1 = (vecCount > 0) ? true : false;
            }
            catch
            {
                lblError.Text = String.Format("Ошибка данных в поле <Количество векторов>");
                return false;
            }

            //проверка 2 поля - количество элементов в векторе
            try
            {
                vecRange = 2;//Convert.ToInt32(txt_vecRange.Text);
                check2 = (vecRange > 0) ? true : false;
            }
            catch
            {
                lblError.Text = String.Format("Ошибка данных в поле <Размерность>");
                return false;
            }

            ////проверка 3 и 4 поля - диапазон генератора чисел
            //try
            //{
            //   // rangeMin = Convert.ToInt32(txt_rangeMin.Text);
            //  //  rangeMax = Convert.ToInt32(txt_rangeMax.Text);
            //    check3 = (rangeMax >= rangeMin) ? true : false;
            //    if(!check3)
            //        lblError.Text = String.Format("Минимально значение должно быть меньше максимального!");
            //}
            //catch
            //{
            //    lblError.Text = String.Format("Ошибка данных в поле <Диапазон>");
            //    return false;
            //}

            //проверка 5 поля - количество кластеров
            try
            {
                clusterCount = Convert.ToInt32(txt_clusterCount.Text);
                check5 = (clusterCount > 0) ? true : false;
            }
            catch
            {
                lblError.Text = String.Format("Ошибка данных в поле <Количество векторов>");
                return false;
            }

            //проверка 6 поля - эпсилон
            try
            {
                Eps = Convert.ToInt32(txt_Epsilon.Text);
                check6 = (Eps > 0) ? true : false;
            }
            catch
            {
                lblError.Text = String.Format("Ошибка данных в поле <Количество векторов>");
                return false;
            }
            return check1 && check2 && check3 && check5 && check6 && check7;
        }

        //Разрешаем вводить только числа в текстовые поля
        //Буквы не введем
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }
        void clasteringMetod()
        {
            txt_Result.Text += "Начало кластеризации\n";
            variableInitialization();//инициализация переменных
            if (radioButton1.Checked==true)
            {
                calculationCentroidsClaster();//выбор центра кластеров случайным образом из списка векторов
            }
            if (radioButton2.Checked == true)
            {
                calculationCentroidsClaster111();
            }
            if (radioButton3.Checked == true)
            {
                calculationCentroidsClaster210();
            }
            if (radioButton4.Checked == true)
            {
                calculationCentroidsClaster300();
            }
            calculationOfDistance();//рассчет минимального расстояния
            bestPartitionJ0Calculate();//расчет J0
            bestPartition[0] = bestPartitionJ1;
            while (true)
            {
                newCenterCoord();//пересчет центра кластеров
                calculationOfDistance();//рассчет минимального расстояния
                bestPartitionCalculate();//расчет J1
                correspondence = Math.Abs(bestPartitionJ1 - bestPartitionJ0);
                bestPartition[iteration] = bestPartitionJ1;
                iteration++;
                if (iteration + 1 > countIterationVar - 1)
                {
                    break;
                }
                else if (correspondence == 0)
                {
                    break;
                }
                else if (correspondence < minEqualVar)
                {
                    //Form3 form3 = new Form3(correspondence, nextClasterization);
                    Form3 form3 = new Form3();
                    form3.ShowDialog();
                    form3.Dispose();
                    if(nextClasterization==false)
                    {
                        break;
                    }
                }
            }
            txt_Result.Text += "Кластеризация выполнена. Количество итераций:" + iteration + "\n";
            txt_Result.Text += "J = " + correspondence + ", количество итераций = " + iteration + "\n";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            disablebuttons();

        }
        private void disablebuttons()
        {
            btnClusterization.Enabled = false;
            btnSaveFile.Enabled = false;
            btnPaint.Enabled = false;
        }
        void variableInitialization()
        {
            countAllVec = vecCount * clusterCount;
            centroidsClusterAll = new int[countIterationVar, clusterCount, vecRange];
            clasteringAllInfo = new int[countIterationVar, countAllVec, clusterCount];
            minimum = new int[countAllVec];
            clasteringInfo = new int[countAllVec, vecRange];
            pointCoord = new int[clusterCount];
            centroidsCluster = new int[clusterCount, vecRange];
            check_1 = new int[clusterCount, vecRange];
            check_2 = new int[clusterCount, vecRange];
            bestPartition = new double[countIterationVar];
        }
        void calculationCentroidsClaster()
        {
            //объявим вычисляемые центры векторов, записывать их будем в один массив
            Random random = new Random();
            //количество итераций зависит от количества точек для расчета
            for (int c = 0; c < clusterCount; c++)
            {
                pointCoord[c] = random.Next(countAllVec - clusterCount);
            }
            //убираем повторяющиеся значения
            //счетчик итераций
            int cc = 0;
            while (true)
            {
                for (int k = 1; k < clusterCount; k++)
                {
                    if (pointCoord[k] == pointCoord[k - 1])
                    {
                        pointCoord[k]++;
                    }
                }
                if (cc == clusterCount)
                {
                    break;
                }
                cc++;
            }
            //заполнение координатами, 0 и 1 - размерность вектора, если она >2, нужно будет добавить цикл на перебор
            for (int c = 0; c < clusterCount; c++)
            {
                for (int r = 0; r < vecRange; r++)
                {
                    centroidsCluster[c, r] = vectorCoord[pointCoord[c], r];
                    check_2[c, r] = centroidsCluster[c, r];

                    centroidsClusterAll[iteration, c, r] = centroidsCluster[c, r];
                }
                //txt_Result.Text += "Центром кластера " + c + " выбрана точка с кординатами " + check_2[c, 0] + ", " + check_2[c, 1] + "\n";
            }
            //txt_Result.Text += "\n";
        }
        void calculationCentroidsClaster111()
        {
            Random random = new Random();
            int clust = random.Next(clusterCount);
            switch (clusterCount)
            {
                case 1:
                    pointCoord[0] = random.Next(countAllVec - clusterCount);
                    break;
                case 2:
                    pointCoord[0] = random.Next(0, countAllVec / 2 - clusterCount);
                    pointCoord[1] = random.Next(countAllVec / 2, countAllVec - clusterCount);
                    break;
                case 3:
                    pointCoord[0] = random.Next(0, countAllVec / 3 - clusterCount);
                    pointCoord[1] = random.Next(countAllVec / 3 - clusterCount, (countAllVec * 2 / 3) - clusterCount);
                    pointCoord[2] = random.Next((countAllVec * 2 / 3) - clusterCount, countAllVec - clusterCount);
                    break;
                case 4:
                    pointCoord[0] = random.Next(0, countAllVec / 4 - clusterCount);
                    pointCoord[1] = random.Next(countAllVec / 4 - clusterCount, (countAllVec * 2 / 4) - clusterCount);
                    pointCoord[2] = random.Next(countAllVec * 2 / 4 - clusterCount, (countAllVec * 3 / 4) - clusterCount);
                    pointCoord[3] = random.Next((countAllVec * 3 / 4) - clusterCount, countAllVec - clusterCount);
                    break;
                case 5:
                    pointCoord[0] = random.Next(0, countAllVec / 4 - clusterCount);
                    pointCoord[1] = random.Next(countAllVec / 5 - clusterCount, (countAllVec * 2 / 5) - clusterCount);
                    pointCoord[2] = random.Next(countAllVec * 2 / 5 - clusterCount, (countAllVec * 3 / 5) - clusterCount);
                    pointCoord[3] = random.Next(countAllVec * 3 / 5 - clusterCount, (countAllVec * 4 / 5) - clusterCount);
                    pointCoord[4] = random.Next((countAllVec * 4 / 5) - clusterCount, countAllVec - clusterCount);
                    break;
                case 6:
                    pointCoord[0] = random.Next(0, countAllVec / 6 - clusterCount);
                    pointCoord[1] = random.Next(countAllVec / 6 - clusterCount, (countAllVec * 2 / 6) - clusterCount);
                    pointCoord[2] = random.Next(countAllVec * 2 / 6 - clusterCount, (countAllVec * 3 / 6) - clusterCount);
                    pointCoord[3] = random.Next(countAllVec * 3 / 6 - clusterCount, (countAllVec * 4 / 6) - clusterCount);
                    pointCoord[4] = random.Next(countAllVec * 4 / 6 - clusterCount, (countAllVec * 5 / 6) - clusterCount);
                    pointCoord[5] = random.Next((countAllVec * 5 / 6) - clusterCount, countAllVec - clusterCount);
                    break;
                case 7:
                    pointCoord[0] = random.Next(0, countAllVec / 7 - clusterCount);
                    pointCoord[1] = random.Next(countAllVec / 7 - clusterCount, (countAllVec * 2 / 7) - clusterCount);
                    pointCoord[2] = random.Next(countAllVec * 2 / 7 - clusterCount, (countAllVec * 3 / 7) - clusterCount);
                    pointCoord[3] = random.Next(countAllVec * 3 / 7 - clusterCount, (countAllVec * 4 / 7) - clusterCount);
                    pointCoord[4] = random.Next(countAllVec * 4 / 7 - clusterCount, (countAllVec * 5 / 7) - clusterCount);
                    pointCoord[5] = random.Next(countAllVec * 5 / 7 - clusterCount, (countAllVec * 6 / 7) - clusterCount);
                    pointCoord[6] = random.Next((countAllVec * 6 / 7) - clusterCount, countAllVec - clusterCount);
                    break;
                case 8:
                    pointCoord[0] = random.Next(0, countAllVec / 8 - clusterCount);
                    pointCoord[1] = random.Next(countAllVec / 8 - clusterCount, (countAllVec * 2 / 8) - clusterCount);
                    pointCoord[2] = random.Next(countAllVec * 2 / 8 - clusterCount, (countAllVec * 3 / 8) - clusterCount);
                    pointCoord[3] = random.Next(countAllVec * 3 / 8 - clusterCount, (countAllVec * 4 / 8) - clusterCount);
                    pointCoord[4] = random.Next(countAllVec * 4 / 8 - clusterCount, (countAllVec * 5 / 8) - clusterCount);
                    pointCoord[5] = random.Next(countAllVec * 5 / 8 - clusterCount, (countAllVec * 6 / 8) - clusterCount);
                    pointCoord[6] = random.Next(countAllVec * 6 / 8 - clusterCount, (countAllVec * 7 / 8) - clusterCount);
                    pointCoord[7] = random.Next((countAllVec * 7 / 8) - clusterCount, countAllVec - clusterCount);
                    break;
                case 9:
                    pointCoord[0] = random.Next(0, countAllVec / 9 - clusterCount);
                    pointCoord[1] = random.Next(countAllVec / 9 - clusterCount, (countAllVec * 2 / 9) - clusterCount);
                    pointCoord[2] = random.Next(countAllVec * 2 / 9 - clusterCount, (countAllVec * 3 / 9) - clusterCount);
                    pointCoord[3] = random.Next(countAllVec * 3 / 9 - clusterCount, (countAllVec * 4 / 9) - clusterCount);
                    pointCoord[4] = random.Next(countAllVec * 4 / 9 - clusterCount, (countAllVec * 5 / 9) - clusterCount);
                    pointCoord[5] = random.Next(countAllVec * 5 / 9 - clusterCount, (countAllVec * 6 / 9) - clusterCount);
                    pointCoord[6] = random.Next(countAllVec * 6 / 9 - clusterCount, (countAllVec * 7 / 9) - clusterCount);
                    pointCoord[7] = random.Next(countAllVec * 7 / 9 - clusterCount, (countAllVec * 8 / 9) - clusterCount);
                    pointCoord[8] = random.Next((countAllVec * 8 / 9) - clusterCount, countAllVec - clusterCount);
                    break;
                case 10:
                    pointCoord[0] = random.Next(0, countAllVec / 10 - clusterCount);
                    pointCoord[1] = random.Next(countAllVec / 10 - clusterCount, (countAllVec * 2 / 10) - clusterCount);
                    pointCoord[2] = random.Next(countAllVec * 2 / 10 - clusterCount, (countAllVec * 3 / 10) - clusterCount);
                    pointCoord[3] = random.Next(countAllVec * 3 / 10 - clusterCount, (countAllVec * 4 / 10) - clusterCount);
                    pointCoord[4] = random.Next(countAllVec * 4 / 10 - clusterCount, (countAllVec * 5 / 10) - clusterCount);
                    pointCoord[5] = random.Next(countAllVec * 5 / 10 - clusterCount, (countAllVec * 6 / 10) - clusterCount);
                    pointCoord[6] = random.Next(countAllVec * 6 / 10 - clusterCount, (countAllVec * 7 / 10) - clusterCount);
                    pointCoord[7] = random.Next(countAllVec * 7 / 10 - clusterCount, (countAllVec * 8 / 10) - clusterCount);
                    pointCoord[8] = random.Next(countAllVec * 8 / 10 - clusterCount, (countAllVec * 9 / 10) - clusterCount);
                    pointCoord[9] = random.Next((countAllVec * 9 / 10) - clusterCount, countAllVec - clusterCount);
                    break;
                default:
                    break;
            }

            int cc = 0;
            while (true)
            {
                for (int k = 1; k < clusterCount; k++)
                {
                    if (pointCoord[k] == pointCoord[k - 1])
                    {
                        pointCoord[k]++;
                    }
                }
                if (cc == clusterCount)
                {
                    break;
                }
                cc++;
            }
            for (int c = 0; c < clusterCount; c++)
            {
                for (int r = 0; r < vecRange; r++)
                {
                    centroidsCluster[c, r] = vectorCoord[pointCoord[c], r];
                    check_2[c, r] = centroidsCluster[c, r];

                    centroidsClusterAll[iteration, c, r] = centroidsCluster[c, r];
                }
            }
        }
        void calculationCentroidsClaster210()
        {
            Random random = new Random();
            int clust = random.Next(clusterCount);
            pointCoord[0] = random.Next(0, countAllVec / 3 - clusterCount);
            pointCoord[1] = random.Next(countAllVec / 3 - clusterCount, (countAllVec * 2 / 3) - clusterCount);
            pointCoord[2] = random.Next(0, countAllVec / 3 - clusterCount);
            int cc = 0;
            while (true)
            {
                for (int k = 1; k < clusterCount; k++)
                {
                    if (pointCoord[k] == pointCoord[k - 1])
                    {
                        pointCoord[k]++;
                    }
                }
                if (cc == clusterCount)
                {
                    break;
                }
                cc++;
            }
            for (int c = 0; c < clusterCount; c++)
            {
                for (int r = 0; r < vecRange; r++)
                {
                    centroidsCluster[c, r] = vectorCoord[pointCoord[c], r];
                    check_2[c, r] = centroidsCluster[c, r];

                    centroidsClusterAll[iteration, c, r] = centroidsCluster[c, r];
                }
            }
        }
        void calculationCentroidsClaster300()
        {
            Random random = new Random();
            int clust = random.Next(clusterCount);
            switch (clust)
            {
                case 1:
                    for (int c = 0; c < clusterCount; c++)
                    {
                        pointCoord[c] = random.Next(countAllVec - clusterCount);
                    }
                    break;
                case 2:
                    if (clust==1)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(0, countAllVec/2 - clusterCount);
                        }

                    }
                    else
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec/2,countAllVec - clusterCount);
                        }

                    }
                    break;
                case 3:
                    if (clust == 1)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(0, countAllVec/3 - clusterCount);
                        }

                    }
                    else if (clust == 2)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec / 3 - clusterCount, (countAllVec*2/3) - clusterCount);
                        }

                    }
                    else
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next((countAllVec * 2 / 3) - clusterCount, countAllVec - clusterCount);
                        }
                    }
                    break;
                case 4:
                    if (clust == 1)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(0, countAllVec / 4 - clusterCount);
                        }

                    }
                    else if (clust == 2)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec / 4 - clusterCount, (countAllVec * 2 / 4) - clusterCount);
                        }

                    }
                    else if (clust == 3)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec*2 / 4 - clusterCount, (countAllVec * 3 / 4) - clusterCount);
                        }

                    }
                    else
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next((countAllVec * 3 / 4) - clusterCount, countAllVec - clusterCount);
                        }
                    }
                    break;
                case 5:
                    if (clust == 1)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(0, countAllVec / 4 - clusterCount);
                        }

                    }
                    else if (clust == 2)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec / 5 - clusterCount, (countAllVec * 2 /5) - clusterCount);
                        }

                    }
                    else if (clust == 3)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 2 / 5 - clusterCount, (countAllVec * 3 / 5) - clusterCount);
                        }

                    }
                    else if (clust == 4)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 3 / 5- clusterCount, (countAllVec * 4 / 5) - clusterCount);
                        }

                    }
                    else
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next((countAllVec * 4 / 5) - clusterCount, countAllVec - clusterCount);
                        }
                    }
                    break;
                case 6:
                    if (clust == 1)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(0, countAllVec / 6 - clusterCount);
                        }

                    }
                    else if (clust == 2)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec / 6 - clusterCount, (countAllVec * 2 / 6) - clusterCount);
                        }

                    }
                    else if (clust == 3)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 2 / 6 - clusterCount, (countAllVec * 3 / 6) - clusterCount);
                        }

                    }
                    else if (clust == 4)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 3 / 6 - clusterCount, (countAllVec * 4 / 6) - clusterCount);
                        }

                    }
                    else if (clust == 5)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 4 / 6- clusterCount, (countAllVec * 5 / 6) - clusterCount);
                        }

                    }
                    else
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next((countAllVec * 5 / 6) - clusterCount, countAllVec - clusterCount);
                        }
                    }
                    break;
                case 7:
                    if (clust == 1)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(0, countAllVec / 7 - clusterCount);
                        }

                    }
                    else if (clust == 2)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec / 7 - clusterCount, (countAllVec * 2 / 7) - clusterCount);
                        }

                    }
                    else if (clust == 3)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 2 / 7- clusterCount, (countAllVec * 3 / 7) - clusterCount);
                        }

                    }
                    else if (clust == 4)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 3 / 7- clusterCount, (countAllVec * 4 / 7) - clusterCount);
                        }

                    }
                    else if (clust == 5)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 4 / 7 - clusterCount, (countAllVec * 5 / 7) - clusterCount);
                        }

                    }
                    else if (clust == 6)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 5/ 7 - clusterCount, (countAllVec * 6 / 7) - clusterCount);
                        }

                    }
                    else
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next((countAllVec * 6 / 7) - clusterCount, countAllVec - clusterCount);
                        }
                    }
                    break;
                case 8:
                    if (clust == 1)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(0, countAllVec / 8 - clusterCount);
                        }

                    }
                    else if (clust == 2)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec / 8 - clusterCount, (countAllVec * 2 / 8) - clusterCount);
                        }

                    }
                    else if (clust == 3)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 2 / 8 - clusterCount, (countAllVec * 3 / 8) - clusterCount);
                        }

                    }
                    else if (clust == 4)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 3 / 8 - clusterCount, (countAllVec * 4 / 8) - clusterCount);
                        }

                    }
                    else if (clust == 5)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 4 / 8 - clusterCount, (countAllVec * 5 / 8) - clusterCount);
                        }

                    }
                    else if (clust == 6)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 5 / 8 - clusterCount, (countAllVec * 6 / 8) - clusterCount);
                        }

                    }
                    else if (clust == 7)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 6 / 8 - clusterCount, (countAllVec * 7 / 8) - clusterCount);
                        }

                    }
                    else
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next((countAllVec * 7 / 8) - clusterCount, countAllVec - clusterCount);
                        }
                    }
                    break;
                case 9:
                    if (clust == 1)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(0, countAllVec / 9 - clusterCount);
                        }

                    }
                    else if (clust == 2)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec / 9 - clusterCount, (countAllVec * 2 / 9) - clusterCount);
                        }

                    }
                    else if (clust == 3)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 2 / 9 - clusterCount, (countAllVec * 3 / 9) - clusterCount);
                        }

                    }
                    else if (clust == 4)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 3 / 9- clusterCount, (countAllVec * 4 / 9) - clusterCount);
                        }

                    }
                    else if (clust == 5)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 4 / 9 - clusterCount, (countAllVec * 5 / 9) - clusterCount);
                        }

                    }
                    else if (clust == 6)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 5 / 9- clusterCount, (countAllVec * 6 / 9) - clusterCount);
                        }

                    }
                    else if (clust == 7)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 6 / 9 - clusterCount, (countAllVec * 7 / 9) - clusterCount);
                        }

                    }
                    else if (clust == 8)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 7 / 9 - clusterCount, (countAllVec * 8 / 9) - clusterCount);
                        }

                    }
                    else
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next((countAllVec * 8 / 9) - clusterCount, countAllVec - clusterCount);
                        }
                    }
                    break;
                case 10:
                    if (clust == 1)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(0, countAllVec / 10 - clusterCount);
                        }

                    }
                    else if (clust == 2)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec / 10 - clusterCount, (countAllVec * 2 / 10) - clusterCount);
                        }

                    }
                    else if (clust == 3)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 2 / 10 - clusterCount, (countAllVec * 3 / 10) - clusterCount);
                        }

                    }
                    else if (clust == 4)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 3 / 10- clusterCount, (countAllVec * 4 / 10) - clusterCount);
                        }

                    }
                    else if (clust == 5)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 4 / 10 - clusterCount, (countAllVec * 5 / 10) - clusterCount);
                        }

                    }
                    else if (clust == 6)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 5 / 10 - clusterCount, (countAllVec * 6 / 10) - clusterCount);
                        }

                    }
                    else if (clust == 7)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 6 /10 - clusterCount, (countAllVec * 7 / 10) - clusterCount);
                        }

                    }
                    else if (clust == 8)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 7 / 10 - clusterCount, (countAllVec * 8 / 10) - clusterCount);
                        }

                    }
                    else if (clust == 9)
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next(countAllVec * 8 / 10 - clusterCount, (countAllVec * 9 / 10) - clusterCount);
                        }

                    }
                    else
                    {
                        for (int c = 0; c < clusterCount; c++)
                        {
                            pointCoord[c] = random.Next((countAllVec * 9 / 10) - clusterCount, countAllVec - clusterCount);
                        }
                    }
                    break;
                default:
                    break;
            }

            int cc = 0;
            while (true)
            {
                for (int k = 1; k < clusterCount; k++)
                {
                    if (pointCoord[k] == pointCoord[k - 1])
                    {
                        pointCoord[k]++;
                    }
                }
                if (cc == clusterCount)
                {
                    break;
                }
                cc++;
            }
            for (int c = 0; c < clusterCount; c++)
            {
                for (int r = 0; r < vecRange; r++)
                {
                    centroidsCluster[c, r] = vectorCoord[pointCoord[c], r];
                    check_2[c, r] = centroidsCluster[c, r];

                    centroidsClusterAll[iteration, c, r] = centroidsCluster[c, r];
                }
            }
        }
        void bestPartitionJ0Calculate()
        {
            //функция для расчета J (РАБОТАЕТ ТОЛЬКО ДЛЯ ДВУМЕРНОГО ВЕКТОРА, ДЛЯ ВЕКТОРОВ БОЛЬШЕЙ РАЗМЕРНОСТИ НУЖНО ДОБАВИТЬ ЕЩЕ ОДИН ЦИКЛ)
            //Ш.1 Находим разность между вектором V и координатами центра кластера, которому он принадлежит (разность между коорд. векторов)
            //Ш.2 Возводим разность в квадрат
            //Ш.3 Суммируем все вычисления для всех векторов
            //Ш.4 Делим на количество векторов
            //это мы определили новый центроид для кластера c

            int sum = 0;
            for (int v = 0; v < countAllVec; v++)
            {
                //Ш.1 Находим разность между вектором V и координатами центра (разность между коорд. векторов)
                //Ш.2 Возводим разность в квадрат
                sum += Math.Abs((vectorCoord[v, 0] - centroidsCluster[minimum[v], 0]) * (vectorCoord[v, 0] - centroidsCluster[minimum[v], 0])) + Math.Abs((vectorCoord[v, 1] - centroidsCluster[minimum[v], 1]) * (vectorCoord[v, 1] - centroidsCluster[minimum[v], 1]));
            }
            //Ш.3 Суммируем все вычисления для всех векторов
            //Ш.4 Делим на количество векторов
            sum = sum / 2;
            //это мы определили новый центроид для кластера c
            bestPartitionJ1 = sum;
        }
        void bestPartitionCalculate()
        {
            bestPartitionJ0 = bestPartitionJ1;
            int sum = 0;
            for (int v = 0; v < countAllVec; v++)
            {
                sum += Math.Abs((vectorCoord[v, 0] - centroidsCluster[minimum[v], 0]) * (vectorCoord[v, 0] - centroidsCluster[minimum[v], 0])) + Math.Abs((vectorCoord[v, 1] - centroidsCluster[minimum[v], 1]) * (vectorCoord[v, 1] - centroidsCluster[minimum[v], 1]));
            }
            sum = sum / 2;
            bestPartitionJ1 = sum;
        }
        private void calculationOfDistance()
        {
            //txt_Result.Text += "Расчет минимального расстояния\n";
            sumPoint = new double[clusterCount, countAllVec]; //расстояние записываем сюда

            //расчет Евклидовой нормы
            for (int c = 0; c < clusterCount; c++)
            {
                for (int k = 0; k < countAllVec; k++)
                {
                    double sumX = (vectorCoord[k, 0] - centroidsCluster[c, 0]) * (vectorCoord[k, 0] - centroidsCluster[c, 0]);
                    double sumY = (vectorCoord[k, 1] - centroidsCluster[c, 1]) * (vectorCoord[k, 1] - centroidsCluster[c, 1]);
                    sumPoint[c, k] = Math.Sqrt(sumX + sumY);
                    //txt_Result.Text += "Расстояние от вектора "+ k + " до центра кластера "+ c + " равно "+ sumPoint[c, k] + "\n";
                }
            }
            //txt_Result.Text += "\n";
            double min_dist = 1000;
            for (int v = 0; v < countAllVec; v++)
            {
                //у нас вектор v
                //мы смотрим по каждому центру (pointCoord) расстояние sumPoint[v,i] от вектора (vectorCoord[v])
                //находим минимальное расстояние и в minimum фиксируем номер центра для дальнейшего пересчета.
                min_dist = 1000;
                for (int i = 0; i < clusterCount; i++)
                {
                    if (min_dist > sumPoint[i, v])
                    {
                        min_dist = sumPoint[i, v];
                        minimum[v] = i;
                    }
                }

                //массив принадлежности вектора кластеру в одной итерации
                clasteringInfo[v, 0] = v;
                clasteringInfo[v, 1] = minimum[v];

                //массив принадлежности вектора во всех итерациях
                for (int c = 0; c < clusterCount; c++)
                {
                    clasteringAllInfo[iteration, v, c] = minimum[v];
                }
            }
        }
        void newCenterCoord()
        {
            //заполнение координатами, 0 и 1 - размерность вектора, если она >2, нужно будет добавить цикл на перебор
            for (int c = 0; c < clusterCount; c++)
            {
                check_1[c, 0] = check_2[c, 0];
                check_1[c, 1] = check_2[c, 1];
            }
            bestPartitionJ0 = bestPartitionJ1;

            for (int c = 0; c < clusterCount; c++)
            {
                double coordXForCenter = 0;
                int coubtXForCenter = 0;
                double coordYForCenter = 0;
                int coubtYForCenter = 0;
                for (int v = 0; v < countAllVec; v++)
                {
                    if (minimum[v] == c)
                    {
                        //считаем Х
                        coordXForCenter += vectorCoord[v, 0];
                        coubtXForCenter += 1;
                        //считаем У
                        coordYForCenter += vectorCoord[v, 1];
                        coubtYForCenter += 1;
                    }
                }
                centroidsCluster[c, 0] = Convert.ToInt32(coordXForCenter / coubtXForCenter);
                centroidsCluster[c, 1] = Convert.ToInt32(coordYForCenter / coubtYForCenter);

                for (int r = 0; r < vecRange; r++)
                {
                    check_2[c, r] = centroidsCluster[c, r];
                    centroidsClusterAll[iteration + 1, c, r] = centroidsCluster[c, r];
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void countIteration_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Epsilon_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        //кнопка кластеризации
        private void button2_Click(object sender, EventArgs e)
        {
            graphCount++;
            clasteringMetod();
            btnPaint.Enabled = true;
        }
        string stringOfVector = String.Empty;
        // кнопка читать из файла
        private void button3_Click(object sender, EventArgs e)
        {

            var fileContent = string.Empty;
            var filePath = string.Empty;
            string line = String.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "D:\\Code";
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileStream = openFileDialog.OpenFile();
                    int counter = 0;
                    int i = 0;
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            txt_Result.Text += "counter = " + counter + "\n";
                            line.Replace("\n", "");
                            line.Trim();
                            if (counter == 0)
                            {
                                vecCount = Convert.ToInt32(line);
                                counter++;
                            }
                            else if (counter == 1)
                            {
                                vecRange = Convert.ToInt32(line);
                                counter++;

                            }
                            else if (counter == 2)
                            {
                                clusterCount = Convert.ToInt32(line);
                                counter++;
                                matrixToRead = new int[vecCount * clusterCount, vecRange];
                            }
                            else if (counter == 3)
                            {
                                minEqualVar = Convert.ToDouble(line);
                                counter++;
                            }

                            else if (counter > 3)
                            {
                                txt_Result.Text += "i = " + i + "\n";
                                // stringOfVector = reader.ReadToEnd();                                
                                //parseStringToVector(stringOfVector);
                                //for (int i = 0; i < vecCount; i++)
                                //{
                                if (i < vecCount * clusterCount)
                                {
                                    for (int j = 0; j < vecRange; j++)
                                    {
                                        try
                                        {
                                            txt_Result.Text += "0 = " + Convert.ToInt32(line.Substring(0, line.IndexOf(','))) + "\n";
                                            matrixToRead[i, j] = Convert.ToInt32(line.Substring(0, line.IndexOf(',')));
                                            line = line.Substring(line.IndexOf(',') + 1);
                                        }
                                        catch
                                        {
                                            txt_Result.Text += "1 = " + Convert.ToInt32(line.Substring(0)) + "\n";
                                            matrixToRead[i, j] = Convert.ToInt32(line.Substring(0));
                                        }

                                    }

                                }
                                i++;
                                //}
                            }

                        }
                    }
                }
                vectorCoord = matrixToRead;
                fillTextBoxes();
            }
            btnClusterization.Enabled = true;
        }


        private void fillTextBoxes()
        {
            txt_vecCount.Text = vecCount.ToString();
            txt_vecRange.Text = vecRange.ToString();
            txt_clusterCount.Text = clusterCount.ToString();
            txt_minEqual.Text = minEqualVar.ToString();
        }

        //Сохранить в файл
        private void button5_Click(object sender, EventArgs e)
        {
            safeIntoFile();
            txt_Result.Text += "Файл сохранен.\n";
        }

        private void safeIntoFile()
        {
            try
            {
                string writePath = String.Empty;
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = "D:\\Code";
                    saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                    saveFileDialog.FilterIndex = 2;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(vecCount);
                            sw.WriteLine(vecRange);
                            sw.WriteLine(clusterCount);
                            sw.WriteLine(minEqualVar);
                            sw.WriteLine(stringForSaveIntoFile);
                        }
                    }
                }
            }
            catch
            {
                txt_Result.Text += "Ошибка записи в файл \n";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            vecCount = 0;//количество векторов
            vecRange = 0;//размерность векторов
            clusterCount = 0; //количество кластеров
            vectors = null; //[vecCount, vecRange];
            centroids = null; //[vecCount, vecRange];
            Eps = 0;//дельта для генерации 
            iteration = 0; //количество итераций
            correspondence = 0;//число совпадений
            countIterationVar = 0;//макс. количество итераций
            minEqualVar = 0;//минимальная совместимость


            bestPartitionJ0 =0;//массив центроидов кластера J0 (что-то, что Коган назван Эпсилон)
            bestPartitionJ1 =0;//массив центроидов кластера J1 (что-то, что Коган назван Эпсилон)
            pointCoord = null; //выбор вектора для центра кластера (рандом)
            sumPoint = null; //расстояние от каждого вектора до центра (первая ячейка - центр, вторая - расстояние от вектора)
            centroidsCluster = null; //координата центра кластера
            vectorCoord = null; //двумерный массив с координатами векторов
            bestPartition = null;//массив центроидов кластеров по всем итерациям (что-то, что Коган назван Эпсилон)
            minimum = null;//кластер, до которого минимальное расстояние от вектора
            check_1 = null; //предыдущая итерация
            check_2 = null; //текущая итерация
            clasteringAllInfo = null; //массив, который хранит информацию о всех итерациях [итерация|номер вектора|номер кластера, которому принадлежит вектор]
            clasteringInfo = null; //массив принадлежности вектора кластеру в одной итерации [номер вектора|номер кластера, которому принадлежит вектор] (оставлено для отладки)
            nextClasterization=true; //продолжаем кластеризацию, меняя J
            countAllVec = 0;

            disablebuttons();
        }

        private void txt_Result_TextChanged(object sender, EventArgs e)
        {
            txt_Result.SelectionStart = txt_Result.Text.Length;
            txt_Result.ScrollToCaret();
        }


        private void txt_clusterCount_TextChanged(object sender, EventArgs e)
        {
            if (txt_clusterCount.Text == "3")
            {
                radioButton3.Enabled = true;
            }
            else
            {
                radioButton3.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
