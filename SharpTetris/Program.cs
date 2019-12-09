using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpTetris
{

    class Matrix
    {

        private int width, height;
        private int[,] matrix;

        public Matrix(int width, int height)
        {

            this.width = width;
            this.height = height;

            matrix = new int[height, width];

        }

        public void Render()
        {

            RenderHorizontalBorder();

            for (int i = 0; i < height; i++)
            {

                Console.Write('#');

                for (int j = 0; j < width; j++)
                    Console.Write(matrix[i, j] == 1 ? '*' : ' ');

                Console.WriteLine('#');

            }

            RenderHorizontalBorder();

        }

        private void RenderHorizontalBorder()
        {

            for (int i = 0; i < width + 2; i++)
                Console.Write('#');
            Console.WriteLine();

        }

    }

    class Tetramino
    {

        /*
         * 
         * ТИПЫ ТЕТРОМИНО:
         * 
         * 0 - I-тетрамино
         * 1 - O-тетрамино
         * 2 - T-тетрамино
         * 3 - L-тетрамино
         * 4 - J-тетрамино
         * 5 - Z-тетрамино
         * 6 - S-тетрамино
         * 
         */

        private int type, rotation;
        private Dictionary<char, int> position;

        public Tetramino(int type)
        {

            this.type = type;
            this.rotation = 0;

        }

    }
    class Program
    {

        static void LifeCycle()
        {

            Matrix matrix = new Matrix(10, 20);
            bool gameover = false;

            while (!gameover)
            {

                Console.Clear();
                matrix.Render();
                Thread.Sleep(500);

            }

        }
        static void Main(string[] args)
        {

            Thread gamecycle = new Thread(LifeCycle);
            gamecycle.Start();

        }

    }

}
