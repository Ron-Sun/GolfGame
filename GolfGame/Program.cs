using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfGame
{
    class Program
    {
        static Graph Gr = new Graph();                  // Graphic
        static Swing Sw = new Swing();                  // Golf
        static bool gamequit = false;

        /// <summary>
        /// Start program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Initialize();
            RunGame();
            EndGame();
        }

        /// <summary>
        /// Initilaze game
        /// </summary>
        static void Initialize()
        {
            Console.Clear();
            Console.SetWindowSize(150, 60);
            Sw.DistanceToCup = Rnr(200, 700);           // Size of course
            Sw.Par = (int)Sw.DistanceToCup / 100;       // Par calculation
            Sw.MaxSwingNr = 10 + Sw.Par;                // Max swing calc.
        }

        /// <summary>
        /// Player user interface.
        /// </summary>
        static void RunGame()
        {
            Console.CursorVisible = false;
            Gr.Golfer();
            Gr.Menu(Sw);
            Gr.PlayerAnimation(Sw);

            do 
            {
                Gr.Menu(Sw);
                Gr.PlaceMenuCursor();
                Console.CursorVisible = true;

                char key = Console.ReadKey().KeyChar;
                key = char.ToUpper(key);

                switch (key)
                {
                    case '\u000D':                  // Enter (the swing)          
                        {
                            Sw.SwingClub();
                            if (Sw.GameOwer || Sw.GameWin) return;
                            break;
                        }

                    case 'S':                       // New strength
                        {
                            Sw.ReadNewStrengthValue();  
                            break;
                        }

                    case 'V':                       // New angle
                        {
                            Sw.ReadNewAngleValue();    
                            break;
                        }

                    case 'H':                       // Help text
                        {
                            Gr.HelpText(Sw);
                            Gr.Menu(Sw); 
                            break;
                        }

                    case 'B':                       // Toggle Driver or Putter.
                        {
                            Sw.Club = -Sw.Club;
                            break;
                        }

                    case '\u001B':                  // Escape to quit.
                        {
                            if (Gr.QuitGameQuestion())
                            {
                                gamequit = true;
                                return;
                            }
                            Gr.RestorePlayerGroundAndFlag(Sw.DistanceToCup);
                            break;
                        }
                }
                if (Sw.SwingNr >= Sw.MaxSwingNr) return;   // User Exeeded maximum try.
            } while (true);
        }  

        /// <summary>
        /// End game in 4 different ways
        /// </summary>
        static void EndGame()
        {
            Console.Clear();

            if (Sw.GameOwer){
                string s =         "     GAME OWER  \n     Din boll ligger i sjön " + Convert.ToInt32(Sw.DistanceToCup) + " meter från flaggan.\n ";
                EndGameInformation(s);
                return;
            }

            if (Sw.GameWin){
                EndGameInformation("     Du klarade uppgiften.\n     Din boll ligger i koppen mitt på green.\n ");
                return;
            }

            if (gamequit){
                EndGameInformation("     Du valde att avsluta.\n     Din boll ligger i naturen och skräpar.\n ");
                return;
            }

            EndGameInformation("     Du fick bara " + Sw.MaxSwingNr + " försök på ett par i " + Sw.Par + " hål.\n     Gå hem och träna på dina puttar ett år.\n");
            return;
        }

        /// <summary>
        /// End game header text.
        /// </summary>
        /// <param name="info"></param>
        static void EndGameInformation(string info)
        {
            Console.WriteLine();
            Console.WriteLine(info);
            Console.WriteLine();
            PlayerLog();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("     Tryck enter för att avsluta.");
            EmptyKeyboarQueue();
            Console.ReadKey();
            return;
        }

        /// <summary>
        /// Remove key hit by user while waiting.
        /// </summary>
        static void EmptyKeyboarQueue()
        {
            while (Console.KeyAvailable) Console.ReadKey();
        }

        /// <summary>
        /// Show log.
        /// </summary>
        static void PlayerLog()
        {
            foreach (var line in Sw.Log )
            {
                Console.WriteLine(line);          
            }
        }

        /// <summary>
        /// Random generator. Get a golf course.
        /// </summary>
        static readonly System.Random N_Rnd = new System.Random(System.Convert.ToInt32(System.DateTime.Now.Ticks % System.Int32.MaxValue));
        static int Rnr(int L, int H)
        {
            return N_Rnd.Next(L, H + 1);
        }
    }
}
