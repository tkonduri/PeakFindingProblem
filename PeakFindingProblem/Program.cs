/*
 * Name: Tejaswi Konduri
 * UNCC Niner #: 800965883
 * ADS - Programming Assignment 1
 */

using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PeakFindingProblem
{
    /// <summary>
    /// Class that contains all the operations to solve the peak finding problem, 
    /// by writing all the peaks in a 2D array into a file...
    /// </summary>
    class PeakFindingProblem
    {
        #region GLOBAL_VARIABLES
        /// <summary>
        /// szOutFilePath is the output file path, and its in the same directory as the executable
        /// </summary>
        const string OUT_FILE_PATH = "localPeaks_Output.txt";
        int iRows = 0, iCol = 0;
        #endregion

        #region GETTING_INPUT
        /// <summary>
        /// Counts the number of rows and coloumns
        /// </summary>
        /// <param name="szInputString">Contents of the input file</param>
        private void countNoOfRowsAndCols(string szInputString)
        {
            try
            {
                bool bBreak = false;

                ///Finding number of rows
                for (int i = 0; i < szInputString.Length; i++)
                {
                    if (Char.Equals(szInputString[i], '['))
                    {
                        this.iRows++;
                    }

                    ///Finding number of colomns                
                    if ((Char.Equals(szInputString[i], ',')) && (!bBreak))
                    {
                        iCol++;
                    }
                    else if ((Char.Equals(szInputString[i], ']')) && (!bBreak))
                    {
                        bBreak = true;
                    }
                }

                ///-- For the initial [
                --(this.iRows);
                Console.WriteLine("Number of Rows = " + this.iRows);

                if (this.iRows != 0)
                {
                    ++(this.iCol);
                }

                Console.WriteLine("Number of Colomns = " + this.iCol);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.Message);
            }            
        }

        /// <summary>
        /// Create a 2D array and Inserting numbers in 2D array
        /// </summary>
        /// <param name="iOneDArr">Input 1D array</param>
        /// <returns></returns>
        private int[,] enterNumberin2DArray(int[] iOneDArr)
        {
            int[,] iArray = new int[this.iRows, this.iCol];

            try
            {
                int iCnt = 0;
                

                for (int i = 0; i < this.iRows; i++)
                {
                    for (int j = 0; j < this.iCol; j++)
                    {
                        iArray[i, j] = iOneDArr[iCnt++];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.Message);
            }
            return iArray;
        }
        
        /// <summary>
        /// Process the input string to create an integer array
        /// </summary>
        /// <param name="szInputString">input string</param>
        private int[,] processIpStrArr(string szInputString)
        {
            int[,] iArray = null;

            try
            {
                // Split on one or more non-digit characters.
                string[] szNumbers = Regex.Split(szInputString, @"\D+");
                int[] iOneDArr = new int[this.iRows * this.iCol];
                int iCnt = 0;

                foreach (string value in szNumbers)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        iOneDArr[iCnt++] = int.Parse(value);
                    }
                }

                iArray = enterNumberin2DArray(iOneDArr);                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.Message);
            }

            return iArray;
        }

        /// <summary>
        /// Fetches the input from the file
        /// </summary>
        /// <param name="szInpFilePath">Input file</param>
        private int[,] fetchIpArr(string szInpFilePath)
        {
            int[,] iArray = null;

            try
            {
                Console.WriteLine("Fetching input array from the file...");

                string szInputString = File.ReadAllText(szInpFilePath);

                countNoOfRowsAndCols(szInputString);

                iArray = processIpStrArr(szInputString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.Message);
            }

            return iArray;
        }
        #endregion

        #region 2D_OPERATIONS
        /// <summary>
        /// Displays the 2D array
        /// </summary>
        /// <param name="iArray">input 2D Array</param>
        private void display2DArr(int[,] iArray)
        {
            ///Displaying
            Console.WriteLine("Printing...");
            for (int i = 0; i < this.iRows; i++)
            {
                for (int j = 0; j < this.iCol; j++)
                {
                    Console.Write(iArray[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Scan the 2D array row wise to find all the peaks
        /// </summary>
        /// <param name="iArray">Input 2D array</param>
        private void scanRowWise(int[,] iArray)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(OUT_FILE_PATH))
                {
                    for (int i = 1; i < (this.iRows - 1); i++)
                    {
                        for (int j = 1; j < (this.iCol - 1); j++)
                        {
                            ///Business logic
                            if ((iArray[i, j] >= iArray[(i - 1), j]) && (iArray[i, j] >= iArray[i, (j - 1)])
                             && (iArray[i, j] >= iArray[(i + 1), j]) && (iArray[i, j] >= iArray[i, (j + 1)]))
                            {
                                ///Writing the peak value and the row number and coloumn number to the output file.
                                writer.WriteLine("Peak = " + iArray[i, j] + " => Row = {0}, Coloumn = {1}", (i+1), (j+1));
                                Console.WriteLine("Peak = " + iArray[i, j] + " => Row = {0}, Coloumn = {1}", (i+1), (j+1));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.Message);
            }
        }

        /// <summary>
        /// Finds the all the local peaks
        /// </summary>
        /// <param name="iArray">Input 2D array</param>
        private void findAllLocalPeaks(int[,] iArray)
        {
            try
            {
                Console.WriteLine("\nFinding local Peak...");
                if ((this.iRows < 3) || (this.iCol < 3))
                {
                    Console.WriteLine("\nNo local Peak found...");
                }

                scanRowWise(iArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Main entry point of the program
        /// </summary>
        /// <param name="args">Input args</param>
        static void Main(string[] args)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                
                int[,] iArray = null;

                Console.WriteLine("Peak Finding Problem:\n");
                string szInpFilePath = @args[0];
                
                if (szInpFilePath != "")
                {
                    PeakFindingProblem obPFP = new PeakFindingProblem();
                    if (File.Exists(szInpFilePath))
                    {
                        iArray = obPFP.fetchIpArr(szInpFilePath);
                        obPFP.display2DArr(iArray);
                        obPFP.findAllLocalPeaks(iArray);
                    }
                    else
                    {
                        Console.WriteLine("Invalid file path...");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid input parameter...");
                }
                
                watch.Stop();
                Console.WriteLine("Time required to execute the program (in Milliseconds) = " + watch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.Message);
            }
        }
    }
}