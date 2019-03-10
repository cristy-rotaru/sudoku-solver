using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Proiect
{
    class SudokuPlate : Button
    {
        public int VectorIndex { get; set; }
        public bool Locked { get; set; }

        public int GetNumber()
        {
            int number = 0;

            try
            {
                string s = this.Content.ToString();
                number = int.Parse(s);
            }
            catch
            {
                number = -1;
            }

            return number;
        }
    }
}
