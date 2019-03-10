using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    class SudokuSolver
    {
        /// <summary>
        /// Generates a sudoku puzzle and stores into the vector passed as argument.
        /// The positions that should remain empty will be written with negative values.
        /// </summary>
        /// <param name="vector">
        /// The new puzzle will be stord in this variable.
        /// The vector needs to be already allocated with at least 81 positions.
        /// </param>
        public static void Generate(int[] vector)
        {
            if (vector.Length < 81)
            {
                throw new Exception("Vector too short!");
            }

            List<int> indexes = new List<int>();
            for (int i = 0; i < 81; ++i)
            {
                vector[i] = -1;
                indexes.Add(i);
            }

            SolveWithForwardChecking(vector, 0);

            indexes = ShuffleList(indexes);
            Random rnd = new Random();
            int numbersToRemove = rnd.Next(40, 45);
            for (int i = 0; i < numbersToRemove; ++i)
            {
                vector[indexes[i]] = -1; // removes a few numbers from the board
            }
        }

        /// <summary>
        /// Solves the puzzle passed as parameter.
        /// </summary>
        /// <param name="vector">
        /// This variable should contain the puzzle.
        /// </param>
        /// <returns>
        /// Returns a boolean value that represents weather the puzzle has a solution or not.
        /// </returns>
        public static bool Solve(int[] vector)
        {
            if (vector.Length < 81)
            {
                throw new Exception("The vector is not a valid sudoku puzzle!");
            }

            for (int i = 0; i < 81; ++i)
            {
                if ((vector[i] == 0) || (vector[i] > 9))
                {
                    throw new Exception("The vector is not a valid sudoku puzzle!");
                }
            }

            if (CheckConstraints(vector) == false)
            {
                return false;
            }

            return SolveWithForwardChecking(vector, 0);
        }

        /// <summary>
        /// Checks if the puzzle has been solved correctly
        /// </summary>
        /// <param name="vector">
        /// This variable should contain the puzzle.
        /// </param>
        /// <returns>
        /// Returns null if the solution is valid. Else it returns a vector with the positions that are detected as wrong;
        /// </returns>
        public static int[] Check(int[] vector)
        {
            if (vector.Length < 81)
            {
                throw new Exception("The vector is not a valid sudoku puzzle!");
            }

            for (int i = 0; i < 81; ++i)
            {
                if (vector[i] < 0)
                {
                    int[] result = new int[1];
                    result[0] = i;
                    return result;
                }

                if ((vector[i] == 0) || (vector[i] > 9))
                {
                    throw new Exception("The vector is not a valid sudoku puzzle!");
                }

                int[] neighbours = GetAllNeighbours(i);
                /* A neighbour of cell 'i' is considered another cell that is situated
                 * on the same line, column or block with said cell 'i'             */

                foreach (int j in neighbours)
                {
                    if (vector[i] == vector[j])
                    {
                        int[] result = new int[2];
                        result[0] = i;
                        result[1] = j;

                        return result;
                    }
                }
            }

            return null;
        }

        private static int[] GetAllNeighbours(int index)
        {
            if ((index < 0) || (index > 80))
            {
                return null;
            }

            int[] neighbours = new int[20]; // each cell has 20 neighbours

            int row = index / 9;
            int column = index % 9;
            int currentWritingPosition = 0;

            int blockRowStart = (row / 3) * 3;
            int blockColumnStart = (column / 3) * 3;

            for (int r = blockRowStart; r < blockRowStart + 3; ++r)
            {
                for (int c = blockColumnStart; c < blockColumnStart + 3; ++c)
                {
                    int n = r * 9 + c;
                    if (n != index)
                    {
                        neighbours[currentWritingPosition++] = n;
                    }
                }
            }

            for (int r = 0; r < blockRowStart; ++r)
            {
                neighbours[currentWritingPosition++] = r * 9 + column;
            }
            for (int r = blockRowStart + 3; r < 9; ++r)
            {
                neighbours[currentWritingPosition++] = r * 9 + column;
            }

            for (int c = 0; c < blockColumnStart; ++c)
            {
                neighbours[currentWritingPosition++] = row * 9 + c;
            }
            for (int c = blockColumnStart + 3; c < 9; ++c)
            {
                neighbours[currentWritingPosition++] = row * 9 + c;
            }

            return neighbours;
        }

        private static List<int> GetAllValidOptions(int[] vector, int position)
        {
            List<int> options = new List<int>();
            int[] neighbours = GetAllNeighbours(position);

            for (int i = 1; i <= 9; ++i)
            {
                bool valid = true;

                foreach (int neighbour in neighbours)
                {
                    if (i == vector[neighbour])
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    options.Add(i);
                }
            }

            return options;
        }

        private static List<T> ShuffleList<T>(List<T> list)
        {
            List<T> randomizedList = new List<T>();

            Random rnd = new Random();
            while (list.Count > 0)
            {
                int nextIndex = rnd.Next(0, list.Count);
                randomizedList.Add(list[nextIndex]);
                list.RemoveAt(nextIndex);
            }

            return randomizedList;
        }

        private static bool CheckConstraints(int[] vector)
        {
            for (int i = 0; i < 81; ++i)
            {
                if (vector[i] < 0)
                {
                    continue;
                }

                int[] neighbours = GetAllNeighbours(i);
                foreach (int neighbour in neighbours)
                {
                    if (vector[i] == vector[neighbour])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool SolveWithForwardChecking(int[] vector, int position)
        {
            if (position > 81)
            {
                throw new Exception("Index outside of solvable table!");
            }

            if (position == 81)
            {
                return true;
            }

            // if the cell already has a value
            if (vector[position] > 0)
            {
                return SolveWithForwardChecking(vector, position + 1);
            }

            // the list is shuffled because this method is also used for generating puzzles
            List<int> possibleOptions = ShuffleList(GetAllValidOptions(vector, position));
            foreach (int value in possibleOptions)
            {
                vector[position] = value;
                if (SolveWithForwardChecking(vector, position + 1))
                {
                    return true;
                }
            }

            vector[position] = -1;
            return false;
        }
    }
}
