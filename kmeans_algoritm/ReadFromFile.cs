using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kmeans_algoritm
{
    class ReadFromFile
    {
        int vecCountToRead;
        int vecRangeToRead;
        int clusterCountToRead;
        int[,] matrixToRead;//массив для чтения из файла
        double minEqualVarToRead;
       public int[,] MatrixToVector { get { return matrixToRead; } set { matrixToRead = value; } }
        public int VectorCount { get { return clusterCountToRead; } set { clusterCountToRead = value; } }
        public int VectorRange { get { return vecRangeToRead; } set { vecRangeToRead = value; } }
        public int ClusterCount { get { return clusterCountToRead; } set { clusterCountToRead = value; } }
        public double MinEqualVar { get {return minEqualVarToRead; } set { minEqualVarToRead = value; } }
        public int[,] readFromTXT()
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
                            //txt_Result.Text += "counter = " + counter + "\n";
                            line.Replace("\n", "");
                            line.Trim();
                            if (counter == 0)
                            {
                                vecCountToRead = Convert.ToInt32(line);
                                counter++;
                            }
                            else if (counter == 1)
                            {
                                vecRangeToRead = Convert.ToInt32(line);
                                counter++;

                            }
                            else if (counter == 2)
                            {
                                clusterCountToRead = Convert.ToInt32(line);
                                counter++;
                                matrixToRead = new int[vecCountToRead * clusterCountToRead, vecRangeToRead];
                            }
                            else if (counter == 3)
                            {
                                minEqualVarToRead = Convert.ToDouble(line);
                                counter++;
                            }

                            else if (counter > 3)
                            {
                                //txt_Result.Text += "i = " + i + "\n";
                                // stringOfVector = reader.ReadToEnd();                                
                                //parseStringToVector(stringOfVector);
                                //for (int i = 0; i < vecCount; i++)
                                //{
                                if (i < vecCountToRead * clusterCountToRead)
                                {
                                    for (int j = 0; j < vecRangeToRead; j++)
                                    {
                                        try
                                        {
                                            //txt_Result.Text += "0 = " + Convert.ToInt32(line.Substring(0, line.IndexOf(','))) + "\n";
                                            matrixToRead[i, j] = Convert.ToInt32(line.Substring(0, line.IndexOf(',')));
                                            line = line.Substring(line.IndexOf(',') + 1);
                                        }
                                        catch
                                        {
                                            //txt_Result.Text += "1 = " + Convert.ToInt32(line.Substring(0)) + "\n";
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
                return matrixToRead;
            }
           
        }
    }
}
