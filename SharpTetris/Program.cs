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
        private int[] tetraminoPosition;
        private int[,] matrix, activeMatrix;

        public Matrix(int width, int height)
        {

            this.width = width;
            this.height = height;

            matrix = new int[height, width];
            activeMatrix = new int[height, width];

        }

        public void Render()
        {

            RenderHorizontalBorder();

            for (int i = 0; i < height; i++)
            {

                Console.Write('#');

                for (int j = 0; j < width; j++)
                    Console.Write(activeMatrix[i, j] == 1 ? '*' : ' ');

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

        public bool ImposeTetramino(Tetramino tetramino)
        {

            tetraminoPosition = tetramino.GetPositon();
            bool canImpose = true;
            int[] size = tetramino.GetRenderSize();
            int[,] render = tetramino.Render();

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    activeMatrix[i, j] = matrix[i, j];

            for (int i = 0; i < size[0]; i++)
                for (int j = 0; j < size[1]; j++)
                    if ((matrix[tetraminoPosition[0] + i, tetraminoPosition[1] + j] == 1) && (render[i, j] == 1))
                        canImpose = false;

            if (canImpose)
                for (int i = 0; i < size[0]; i++)
                    for (int j = 0; j < size[1]; j++)
                        activeMatrix[tetraminoPosition[0] + i, tetraminoPosition[1] + j] = render[i, j] | activeMatrix[tetraminoPosition[0] + i, tetraminoPosition[1] + j];

            return canImpose;

        }

        public void InsertTetramino(Tetramino tetramino)
        {

            tetraminoPosition = tetramino.GetPositon();
            int[] size = tetramino.GetRenderSize();
            int[,] render = tetramino.Render();

            for (int i = 0; i < size[0]; i++)
                for (int j = 0; j < size[1]; j++)
                    matrix[tetraminoPosition[0] + i, tetraminoPosition[1] + j] = render[i, j] | matrix[tetraminoPosition[0] + i, tetraminoPosition[1] + j];

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
        private int[] positon;

        public Tetramino(int type)
        {

            this.type = type;
            this.rotation = 0;
            this.positon = new[] { 0, 4 };

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

        public int[,] Render()
        {

            int[] size = this.GetRenderSize();
            int[,] result = new int[size[0], size[1]];

            for (int i = 0; i < size[0]; i++)
                for (int j = 0; j < size[1]; j++)
                    result[i, j] = 0;

            switch (this.type)
            {

                case 0:

                    if (this.rotation % 2 == 0)
                        for (int i = 0; i < 4; i++)
                            result[i, 0] = 1;
                    else
                        for (int i = 0; i < 4; i++)
                            result[0, i] = 1;
                    
                    break;
                case 1:

                    for (int i = 0; i < 2; i++)
                        for (int j = 0; j < 2; j++)
                            result[i, j] = 1;

                    break;
                case 2:

                    switch (this.rotation)
                    {

                        case 0:

                            result[0, 1] = 1;
                            for (int i = 0; i < 3; i++)
                                result[1, i] = 1;

                            break;
                        case 1:

                            result[1, 1] = 1;
                            for (int i = 0; i < 3; i++)
                                result[i, 0] = 1;

                            break;
                        case 2:

                            result[1, 1] = 1;
                            for (int i = 0; i < 3; i++)
                                result[0, i] = 1;

                            break;
                        case 3:

                            result[1, 0] = 1;
                            for (int i = 0; i < 3; i++)
                                result[i, 1] = 1;

                            break;

                    }

                    break;
                case 3:

                    switch (this.rotation)
                    {

                        case 0:

                            result[2, 1] = 1;
                            for (int i = 0; i < 3; i++)
                                result[i, 0] = 1;

                            break;
                        case 1:

                            result[1, 0] = 1;
                            for (int i = 0; i < 3; i++)
                                result[0, i] = 1;

                            break;
                        case 2:

                            result[0, 0] = 1;
                            for (int i = 0; i < 3; i++)
                                result[i, 1] = 1;

                            break;
                        case 3:

                            result[0, 2] = 1;
                            for (int i = 0; i < 3; i++)
                                result[1, i] = 1;

                            break;

                    }

                    break;
                case 4:

                    switch (this.rotation)
                    {

                        case 0:

                            result[2, 0] = 1;
                            for (int i = 0; i < 3; i++)
                                result[i, 1] = 1;

                            break;
                        case 1:

                            result[0, 0] = 1;
                            for (int i = 0; i < 3; i++)
                                result[1, i] = 1;

                            break;
                        case 2:

                            result[0, 1] = 1;
                            for (int i = 0; i < 3; i++)
                                result[i, 0] = 1;

                            break;
                        case 3:

                            result[1, 2] = 1;
                            for (int i = 0; i < 3; i++)
                                result[0, i] = 1;

                            break;

                    }

                    break;
                case 5:

                    if (this.rotation % 2 == 0)
                    {
                        result[1, 0] = 1;
                        result[2, 0] = 1;
                        result[0, 1] = 1;
                        result[1, 1] = 1;
                    } 
                    else
                    {
                        result[0, 0] = 1;
                        result[0, 1] = 1;
                        result[1, 1] = 1;
                        result[1, 2] = 1;
                    }

                    break;
                case 6:

                    if (this.rotation % 2 == 0)
                    {
                        result[0, 0] = 1;
                        result[1, 0] = 1;
                        result[1, 1] = 1;
                        result[2, 1] = 1;
                    }
                    else
                    {
                        result[0, 1] = 1;
                        result[0, 2] = 1;
                        result[1, 0] = 1;
                        result[1, 1] = 1;
                    }

                    break;

            }

            return result;

        }

        public void GoDown()
        {
            positon[0]++;
        }
        public void GoBack()
        {
            positon[0]--;
        }
        public void GoLeft()
        {
            positon[1]--;
        }
        public void GoRight()
        {
            positon[1]++;
        }

        public int[] GetPositon()
        {
            return this.positon;
        }

    }
    class Program
    {

        static bool gameover = false;
        const int WIDTH = 10, HEIGHT = 20;

        static void LifeCycle()
        {

            bool hasTetramino = true;
            Random random = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
            Matrix matrix = new Matrix(WIDTH, HEIGHT);
            Tetramino tetramino = new Tetramino(random.Next(7));

            while (!gameover)
            {

                int[] position = tetramino.GetPositon();
                int[] renderSize = tetramino.GetRenderSize();

                if (!hasTetramino)
                {

                    tetramino = new Tetramino(/*random.Next(7)*/6);
                    hasTetramino = true;

                }

                if (position[0] + renderSize[0] <= HEIGHT)
                {

                    if (matrix.ImposeTetramino(tetramino))
                        tetramino.GoDown();
                    else {

                        tetramino.GoBack();
                        matrix.InsertTetramino(tetramino);
                        hasTetramino = false;

                    }

                } 
                else
                {

                    tetramino.GoBack();
                    matrix.InsertTetramino(tetramino);
                    hasTetramino = false;

                }

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
