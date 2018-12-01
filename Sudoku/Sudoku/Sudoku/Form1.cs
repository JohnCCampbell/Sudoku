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

namespace Sudoku
{
    public partial class sudokuFrm : Form
    {
        StreamReader inputFile;
        Random randomNumbers = new Random(); 
        Button[] buttonRay; //array of buttons
        char[,] allCombos; //2D array, will hold all char from text file
        int charRayRowSize; //represents how many rows allCombos 2d char array has
        int charRayColSize; //represents how many columns allCombos 2D char arrray has
        int rand; //will hold randomly generated number
        int numberOfConfigs; // Number of configrations of the game inside the text file
        string InputFileName;

        public sudokuFrm()
        {
            InitializeComponent();
        }

        //returns a count of how many lines or rows the file has
        public int countFileRows()
        {
            int numLines = 0;
            try
            {
                inputFile = File.OpenText(InputFileName);                
                while (inputFile.ReadLine() != null)
                {
                    numLines++;
                }
                inputFile.Close();
                numberOfConfigs = numLines;
            }
            // in case the file does not open and/or reading the line does not go as expected 
            catch
            {
                MessageBox.Show("Error is I/O operation\nProgram will terminate now",
                    " === Error ===");
                this.Close();
            }
            return numLines;
        }


        //fills 2D character array with each character from text file
        private void fillCharArray()
        {
            try
            {
                inputFile = File.OpenText(InputFileName);
                string line = "";

                //populates 2D char array allCombos with each individual char in text file
                for (int row = 0; row < allCombos.GetLength(0); row++)
                {
                    line = inputFile.ReadLine();
                    for (int col = 0; col < allCombos.GetLength(1); col++)
                    {
                        allCombos[row, col] = line[col];
                    }
                }

                //close file
                inputFile.Close();
            }
            catch
            {
                MessageBox.Show("Error is I/O operation\nProgram will terminate now",
                    " === Error ===");
                this.Close();
            }
        }

        //populates all buttons with a random row from 2D char array
        private void populate()
        {
            //generate a random number that will be used as the row position for the 2D char array
            rand=randomNumbers.Next(charRayRowSize);

            //nested loops step through 2D char array
            for (int col = 0; col < allCombos.GetLength(1); col++)
            {
                //if there is a 0 in that position clears text and disables button in button array, otherwise displays number in the button
                if (allCombos[rand, col].Equals('0'))
                {
                    buttonRay[col].Text = " ";
                    buttonRay[col].Enabled = true;
                }
                else
                {
                    buttonRay[col].Text = allCombos[rand, col].ToString();
                    buttonRay[col].Enabled = false;
                }
            }            
            //sets the title of the form to the random row that is being used to fill button array
            this.Text = "Sudoku 4X4 (" + (rand+1) + " out of "+ numberOfConfigs+")";
        }

        private void checkBtn_Click(object sender, EventArgs e)
        {

        }

        private void restartBtn_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sudokuFrm_Load(object sender, EventArgs e)
        {
            InputFileName = "config.txt";
            charRayRowSize = countFileRows();
            charRayColSize = 16;
            // Fill out the allCombos array with all every possible configration
            allCombos = new char[charRayRowSize, charRayColSize];
            fillCharArray();
            // use the buttonRay to reference the 16 button objects
            buttonRay = new Button[] { loc1Btn, loc2Btn, loc3Btn, loc4Btn,
                                       loc5Btn, loc6Btn, loc7Btn, loc8Btn,
                                       loc9Btn, loc10Btn, loc11Btn, loc12Btn,
                                       loc13Btn, loc14Btn, loc15Btn, loc16Btn };
            populate();
        }

        // If the user presses any button followed by a number (1-4) that nunber
        // appears as the text on the button
        // This method is the event handler for all 16 buttons
        private void button_keyPress(object sender, KeyPressEventArgs e)
        {
            // your code goes here
        }


        // make the active button back color as light blue
        private void CellMouseClick(object sender, MouseEventArgs e)
        {
            // change every back color of every cell to rogonal color
            foreach (Button b in buttonRay)
            {
                b.BackColor = Color.WhiteSmoke;
            }
            // make the active button back color as light blue
            this.ActiveControl.BackColor = Color.LightBlue;
        }
    }
}
