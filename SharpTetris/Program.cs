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

        public int CheckRows()
        {

            int count = 0;

            for (int i = 1; i < height; i++)
            {

                int sum = 0;

                for (int j = 0; j < width; j++)
                    sum += matrix[i, j];

                if (sum == width)
                {

                    count++;

                    for (int j = 0; i < width; j++)
                        matrix[i, j] = 0;

                    for (int j = i - 1; j >= 0; j--)
                        for (int k = 0; k < width; k++)
                        {

                            matrix[j + 1, k] = matrix[j, k];
                            matrix[j, k] = 0;

                        }

                }

            }

            return count;

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
        static int controllerPosition = 0, rotateIntention = 0;
        const int WIDTH = 10, HEIGHT = 20, LEFT = -1, RIGHT = 1, CLOCKWISE = 1, COUNTER_CLOCKWISE = -1;

        static void Controller()
        {

            while(!gameover)
            {

                controllerPosition = 0;
                rotateIntention = 0;

                switch (Console.ReadKey(true).Key)
                {

                    case ConsoleKey.A:
                        controllerPosition = LEFT;
                        break;
                    case ConsoleKey.D:
                        controllerPosition = RIGHT;
                        break;
                    case ConsoleKey.LeftArrow:
                        controllerPosition = LEFT;
                        break;
                    case ConsoleKey.RightArrow:
                        controllerPosition = RIGHT;
                        break;
                    default:
                        controllerPosition = 0;
                        break;

                }

                switch (Console.ReadKey(true).Key)
                {

                    case ConsoleKey.Q:
                        rotateIntention = COUNTER_CLOCKWISE;
                        break;
                    case ConsoleKey.E:
                        rotateIntention = CLOCKWISE;
                        break;
                    default:
                        rotateIntention = 0;
                        break;

                }

                Thread.Sleep(50);

            }

        }

        static void LifeCycle()
        {

            bool hasTetramino = true;
            int scores = 0;
            
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            Matrix matrix = new Matrix(WIDTH, HEIGHT);
            Tetramino tetramino = new Tetramino(random.Next(7));
            Thread controller = new Thread(Controller);

            for (int i = 0; i < random.Next(4); i++)
                tetramino.Rotate(1);

            controller.Start();

            while (!gameover)
            {

                if (!hasTetramino)
                {

                    tetramino = new Tetramino(random.Next(7));
                    hasTetramino = true;

                    for (int i = 0; i < random.Next(4); i++)
                        tetramino.Rotate(1);

                }

                int[] position = tetramino.GetPositon();
                int[] renderSize = tetramino.GetRenderSize();

                switch (rotateIntention)
                {

                    case CLOCKWISE:
                        break;
                    case COUNTER_CLOCKWISE:
                        break;

                }

                switch (controllerPosition)
                {

                    case LEFT:
                        if (position[1] > 0)
                        {

                            tetramino.GoLeft();

                            if (!matrix.ImposeTetramino(tetramino))
                                tetramino.GoRight();

                        }

                        controllerPosition = 0;
                        
                        break;
                    case RIGHT:
                        if (position[1] + renderSize[1] < WIDTH)
                        {

                            tetramino.GoRight();

                            if (!matrix.ImposeTetramino(tetramino))
                                tetramino.GoLeft();

                        }

                        break;
                    default:
                        if (position[0] + renderSize[0] <= HEIGHT)
                        {

                            if (matrix.ImposeTetramino(tetramino))
                                tetramino.GoDown();
                            else if (position[0] == 0)
                            {

                                gameover = true;

                            }
                            else
                            {

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

                        break;

                }

                Console.Clear();
                scores += 100 * matrix.CheckRows();
                Console.WriteLine($"Scores: {scores}\n");
                matrix.Render();
                Thread.Sleep(300);

            }

            controller.Abort();
            Console.Clear();
            Console.WriteLine("GAME OVER!\n\nPress any key to continue...");
            Console.ReadKey();

        }
        static void Main(string[] args)
        {

            Thread gamecycle = new Thread(LifeCycle);
            gamecycle.Start();
            

        }

    }

}
