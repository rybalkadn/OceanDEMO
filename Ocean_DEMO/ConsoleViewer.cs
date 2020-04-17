using Ocean_DEMO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    class ConsoleViewer
    {
        public ConsoleViewer(IOceanView view, ConsoleColor color, int offsetLeft , int offsetTop )
        {
            _view = view;
            _offsetLeft = offsetLeft;
            _offsetTop = offsetTop;
            _color = color;
        }

        public void Draw()
        {
            DrawBorder(_offsetLeft, _offsetTop, _view.Width, _view.Height, _color);
            foreach (ICell item in _view)
            {
                Console.SetCursorPosition(_offsetLeft + item.Position.X, _offsetTop + item.Position.Y);
                Console.Write(item.AnObject);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Статистика
        /// </summary>
        public void DisplayStatistics()
        {
            int[] cellCount = { 0, 0, 0, 0 };

            foreach (ICell item in _view)
            {
                switch (item.AnObject)
                {
                    case (char)ECharObject.AnObject :
                        ++cellCount[0];
                        break;

                    case (char)ECharObject.NotPredatorFish:
                        ++cellCount[1];
                        break;

                    case (char)ECharObject.PredatorShark:
                        ++cellCount[2];
                        break;

                    case (char)ECharObject.PredatorTuna:
                        ++cellCount[3];
                        break;
                    default:
                        break;
                }
            }

            Console.SetCursorPosition(_offsetLeft, _offsetTop + _view.Height + 1);
            Console.Write("Obstacles: {0,5}", cellCount[0]);

            Console.SetCursorPosition(_offsetLeft, _offsetTop + _view.Height + 2);
            Console.Write("Fishes   : {0,5}", cellCount[1]);

            Console.SetCursorPosition(_offsetLeft, _offsetTop + _view.Height + 3);
            Console.Write("Sharks   : {0,5}", cellCount[2]);

            Console.SetCursorPosition(_offsetLeft, _offsetTop + _view.Height + 4);
            Console.Write("Tuna     : {0,5}", cellCount[3]);
        }

        /// <summary>
        /// Рисовать границу
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderColor"></param>
        private static void DrawBorder(int left, int top, int width, int height, ConsoleColor borderColor)
        {
            int padWidth = 1 + width;
            string up = "\u2554".PadRight(padWidth, '\u2550') + '\u2557';
            string middle = "\u2551".PadRight(padWidth) + '\u2551';
            string bottom = "\u255A".PadRight(padWidth, '\u2550') + '\u255D';

            Console.ForegroundColor = borderColor;

            Console.SetCursorPosition(left - 1, top - 1);
            Console.WriteLine(up);

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(left - 1, Console.CursorTop);
                Console.WriteLine(middle);
            }

            Console.SetCursorPosition(left - 1, Console.CursorTop);
            Console.WriteLine(bottom);
        }

        private int _offsetLeft;
        private int _offsetTop;
        private IOceanView _view;
        ConsoleColor _color;
    }
}
