using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kmeans_algoritm
{
    class SaveToFile
    {
        int vecCountToSave;
        int vecRangeToSave;
        int clusterCountToSave;
        int[,] matrixToSave;//массив для чтения из файла
        double minEqualVarToSave;
        public int[,] MatrixToVector { get { return matrixToSave; } set { matrixToSave = value; } }
        public int VectorCount { get { return clusterCountToSave; } set { clusterCountToSave = value; } }
        public int VectorRange { get { return vecRangeToSave; } set { vecRangeToSave = value; } }
        public int ClusterCount { get { return clusterCountToSave; } set { clusterCountToSave = value; } }
        public double MinEqualVar { get { return minEqualVarToSave; } set { minEqualVarToSave = value; } }

        public void safeIntoFile(int vecCount, int vecRange, int clusterCount, double minEqualVar, string stringForSaveIntoFile)
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
                //txt_Result.Text += "Ошибка записи в файл \n";
            }
        }
    }
}
