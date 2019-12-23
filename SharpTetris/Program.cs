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

        public Tetramino(int type)
        {

            this.type = type;
            this.rotation = 0;

        }

        public void Rotate(int direction)
        {

            if (direction == 1)
                this.rotation = (this.rotation + 1) % 4;
            else
                this.rotation = (this.rotation - 1 < 0 ? 3 : this.rotation - 1);

        }

        public int[] GetRenderSize()
        {

            int[] result = new int[2];

            switch (this.type)
            {

                case 0:

                    if (this.rotation % 2 == 0) 
                    { 
                        result[0] = 4;
                        result[1] = 1;
                    } 
                    else 
                    {
                        result[0] = 1;
                        result[1] = 4;
                    }

                    break;
                case 1:
                    result[0] = 2;
                    result[1] = 2;
                    break;
                case 2:

                    if (this.rotation % 2 == 0)
                    {
                        result[0] = 2;
                        result[1] = 3;
                    }
                    else
                    {
                        result[0] = 3;
                        result[1] = 2;
                    }

                    break;
                case 3:

                    if (this.rotation % 2 == 0)
                    {
                        result[0] = 3;
                        result[1] = 2;
                    }
                    else
                    {
                        result[0] = 2;
                        result[1] = 3;
                    }

                    break;
                case 4:

                    if (this.rotation % 2 == 0)
                    {
                        result[0] = 3;
                        result[1] = 2;
                    }
                    else
                    {
                        result[0] = 2;
                        result[1] = 3;
                    }

                    break;
                case 5:

                    if (this.rotation % 2 == 0)
                    {
                        result[0] = 3;
                        result[1] = 2;
                    }
                    else
                    {
                        result[0] = 2;
                        result[1] = 3;
                    }

                    break;
                case 6:

                    if (this.rotation % 2 == 0)
                    {
                        result[0] = 3;
                        result[1] = 2;
                    }
                    else
                    {
                        result[0] = 2;
                        result[1] = 3;
                    }

                    break;

            }

            return result;

        }

        public char[,] Render()
        {

            

        }

    }
    class Program
    {

        static bool gameover = false;

        static void LifeCycle()
        {

            Matrix matrix = new Matrix(10, 20);

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
