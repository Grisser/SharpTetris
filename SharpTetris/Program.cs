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
    class Program
    {
        static void Main(string[] args)
        {

            Matrix matrix = new Matrix(10, 20);
            matrix.Render();

            Console.ReadKey();

        }

    }

}
