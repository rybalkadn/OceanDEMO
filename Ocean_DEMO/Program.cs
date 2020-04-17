using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(width: 90, height: 40);

            #region Поле 1

            Ocean Ocean1 = new Ocean(cols: 30, rows: 30);
           
            Ocean1.Initialize(prey: 100, predators: 5, obstacles: 20);
     
            ConsoleViewer Viewer1 = new ConsoleViewer(view: Ocean1, color: ConsoleColor.White, offsetLeft: 1, offsetTop: 1);
            Viewer1.Draw();
            Viewer1.DisplayStatistics();


            Ocean Ocean2 = new Ocean(cols: 30, rows: 30);

            Ocean2.Initialize(prey: 100, predators: 5, obstacles: 20);

            ConsoleViewer Viewer2 = new ConsoleViewer(view: Ocean2, color: ConsoleColor.White, offsetLeft: 40, offsetTop: 1);
            Viewer2.Draw();
            Viewer2.DisplayStatistics();

            #endregion


            Console.CursorVisible = false;

            System.Threading.Thread.Sleep(1000);
   

            int iterationCount = 100;
            for (int i = 0; i < iterationCount; i++)
            {
                if (i % 1 == 0)
                {
                    #region Поле 1

                    Viewer1.DisplayStatistics();

                    Viewer2.DisplayStatistics();

                    #endregion


                    Console.SetCursorPosition(0, Console.WindowHeight - 1);
                    Console.Write("Iteration: {0}", i);
                }

                #region Поле 1

                Ocean1.Run();
                Viewer1.Draw();

                Ocean2.Run();
                Viewer2.Draw();

                #endregion

                System.Threading.Thread.Sleep(100);
            
            }

          


            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Iteration: {0}", iterationCount);

            Console.ReadKey();
        }
    }
}
