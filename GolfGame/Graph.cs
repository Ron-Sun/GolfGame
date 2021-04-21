using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GolfGame
{
    class Graph
    {
        private int driver    =  1;         // Just for code readability.   
        public int menuline   =  2;
        public int menucolumn = 50;
        /// <summary>
        /// User Menu
        /// </summary>
        /// <param name="Sw"></param>
        public void Menu(Swing Sw) 
        {
            int line = menuline;
            int col = menucolumn;
            WriteAt(@"      Nuvarande golfklubba [        ]                              ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"      [ S ] Styrka på slag.    [    ]  1 - 100 styrka.             ", col, line++);
            WriteAt(@"      [ V ] Vinkel på slag.    [    ]  0 -  90 grader.             ", col, line++);
            WriteAt(@"      [ B ] Byt golfklubba.                                        ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"      [ H ] Hjälp.             Du är ... meter från flaggan.       ", col, line++);
            WriteAt(@"                               Du har gjort .. försök.             ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"      [ ? ]                    Tryck Enter för att slå dit slag.   ", col, line++);
 
            ShowStengthValue(Sw.Strengt);
            ShowAngleValue(Sw.Angle );
            ShowDistanceToCup(Sw.DistanceToCup);
            ShowSwingAttempts(Sw.SwingNr);
            ShowSwingDirection(Sw.SwingDirection);
            FlagDistanceAway(Sw.DistanceToCup);
            GolfClub(Sw.Club);
        }
        /// <summary>
        /// Place cursor at question mark.
        /// </summary>
        public void PlaceMenuCursor()
        {
            int col = menucolumn + 8; int line = menuline + 9;
            Console.SetCursorPosition(col, line);
        }
        /// <summary>
        /// Place cursor and write Ex. strenght or angle.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="line"></param>
        /// <param name="nr"></param>
        public void PositionCursorAndWrite(int col, int line, int nr)
        {
            string s = nr.ToString();
            WriteAt(s, col - s.Length, line);
            Console.SetCursorPosition(col - 1, line);
        }

        /// <summary>
        /// Show witch way player swing the ball. Green vs Red.
        /// </summary>
        /// <param name="swingDirection"></param>
        public void ShowSwingDirection(int swingDirection)
        {
            ConsoleColor tmp = Console.ForegroundColor;
            int col = menucolumn + 31; int line = menuline + 5;
            if (swingDirection < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteAt("Flaggan är öster ut.  ", col, line);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                WriteAt("Flaggan är väster ut. ", col, line);
            }
            Console.ForegroundColor = tmp;
        }

        /// <summary>
        /// Distance to cup.
        /// </summary>
        /// <param name="distanceToCup"></param>
        public void ShowDistanceToCup(double distanceToCup)
        {
            int col = menucolumn + 31; int line = menuline + 6;
            if (distanceToCup > 30)
                WriteAt("Du är " + Convert.ToInt32(distanceToCup).ToString() + " meter från flaggan.    ", col, line);
            else
                WriteAt("Du är " + distanceToCup.ToString("n2") + " meter från flaggan.    ", col, line);
        }
        /// <summary>
        /// Number of attempts to get ball in the cup.
        /// </summary>
        /// <param name="swing"></param>
        public void ShowSwingAttempts(int swing)
        {
            int col = menucolumn + 31; int line = menuline + 7;
            WriteAt("Du har gjort " + swing.ToString() + " försök.    ", col, line);
        }

        /// <summary>
        /// Strength
        /// </summary>
        /// <param name="strengt"></param>
        public void ShowStengthValue(int strengt)
        {
            int col = menucolumn + 36; int line = menuline + 2;
            PositionCursorAndWrite(col, line, strengt);
        }
        /// <summary>
        /// Angle
        /// </summary>
        /// <param name="angle"></param>
        public void ShowAngleValue(int angle)
        {
            int col = menucolumn + 36; int line = menuline + 3;
            PositionCursorAndWrite(col, line, angle);
        }

        /// <summary>
        /// Show club choice.
        /// </summary>
        /// <param name="club"></param>
        public void GolfClub(int club)
        {
            int line = menuline;
            int col = menucolumn;
            if (club > 0)
            {
                WriteAt(@"      Nuvarande golfklubba [ Driver ]                              ", col, line);
            }
            else
            {
                WriteAt(@"      Nuvarande golfklubba [ Putter ]                              ", col, line);
            }
        }

        /// <summary>
        /// Help text.
        /// </summary>
        /// <param name="Sw"></param>
        public void HelpText(Swing Sw)
        {
            int line = menuline;
            int col = menucolumn;
            string s = "Ditt uppdrag är att få bollen i koppen " + Sw.DistanceToCup.ToString("n0") + " meter bort.          ";

            WriteAt(s, col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"Du har 2 klubbor att välja mellan. Driver samt putter.             ", col, line++);
            WriteAt(@"Driver slår bollen 10 gånger längre än puttern.                    ", col, line++);
            WriteAt(@"Du anger styrka samt vinkel på slaget och trycker Enter.           ", col, line++);
            WriteAt(@"Därefter tar naturkrafterna över.   Lycka till.                    ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"Ps. Slå inte långt över green, det finns vattnet i närheten.       ", col, line++);
            s =      "Det är ett par " +  Sw.Par.ToString() + " hål. Du får max " + Sw.MaxSwingNr.ToString() + " försök.          ";
            WriteAt(s, col, line++);
            WriteAt(@"         Tryck Enter när du läst klart.                            ", col, line++);

            // WriteAt(@"                                                                   ", col, line++);
            Console.ReadKey();    
        }

        /// <summary>
        /// Remove text on screen.
        /// </summary>
        public void RemoveText()
        {
            int line = menuline;
            int col = menucolumn;
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
            WriteAt(@"                                                                   ", col, line++);
        }

        /// <summary>
        /// Golfer
        /// </summary>
        public void Golfer()
        {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            int line = 2;
            int col = 5;
            WriteAt(@" _..			    ", col, line++);
            WriteAt(@"( _,\			    ", col, line++);
            WriteAt(@" `  \\			", col, line++);
            WriteAt(@"     \\			", col, line++);
            WriteAt(@"      \\		    ", col, line++);
            WriteAt(@"    __ \\		    ", col, line++);
            WriteAt(@"  .'   `\\		", col, line++);
            WriteAt(@" / '     \\		", col, line++);
            WriteAt(@"|_/`\____,\\_	    ", col, line++);
            WriteAt(@"(   -   - \\\)	", col, line++);
            WriteAt(@" \     >  /( \	", col, line++);
            WriteAt(@"  ;. _=_.' / /\	", col, line++);
            WriteAt(@" /       \/ / \)	", col, line++);
            WriteAt(@"|   '-._.' /		", col, line++);
            WriteAt(@";._    \ .'		", col, line++);
            WriteAt(@"|  `'--'`|		", col, line++);
            Console.ForegroundColor = tmp;
        }

        /// <summary>
        /// Dangerous swing.
        /// </summary>
        public void Fore()
        {
            int line = menuline;
            int col = menucolumn;
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            WriteAt(@"  █████▒▒█████   ██▀███  ▓█████  ▐██▌ ", col, line++);
            WriteAt(@"▓██   ▒▒██▒  ██▒▓██ ▒ ██▒▓█   ▀  ▐██▌ ", col, line++);
            WriteAt(@"▒████ ░▒██░  ██▒▓██ ░▄█ ▒▒███    ▐██▌ ", col, line++);
            WriteAt(@"░▓█▒  ░▒██   ██░▒██▀▀█▄  ▒▓█  ▄  ▓██▒ ", col, line++);
            WriteAt(@"░▒█░   ░ ████▓▒░░██▓ ▒██▒░▒████▒ ▒▄▄  ", col, line++);
            WriteAt(@" ▒ ░   ░ ▒░▒░▒░ ░ ▒▓ ░▒▓░░░ ▒░ ░ ░▀▀▒ ", col, line++);
            WriteAt(@" ░       ░ ▒ ▒░   ░▒ ░ ▒░ ░ ░  ░ ░  ░ ", col, line++);
            WriteAt(@" ░ ░   ░ ░ ░ ▒    ░░   ░    ░       ░ ", col, line++);
            WriteAt(@"           ░ ░     ░        ░  ░ ░    ", col, line++);

            Console.ForegroundColor = tmp;
        }

        /// <summary>
        /// Simple but fun trajectory routine. use sinwave to simulate angle and strength.
        /// </summary>
        /// <param name="Sw"></param>
        /// <param name="dist"></param>
        public void BallTrajectory(Swing Sw,double dist)        // Not pretty ... But it works ;-)
        {
            double x = 16; double y;
            int  oldx = 1; int oldy = 1;
            double sin = 3.2; double add = 0.033;
            double str = (double)Sw.Strengt; 
            if (Sw.Club != driver) str /= 10;   
            str /= 100;                         // Fit trajectory
            Console.CursorVisible = false;

            for (int i = 0; i <100; i++)
            {
                x += str;   
                y = Math.Sin(sin) * Sw.Angle / 4;
                sin += add; // 0.033;

                WriteAt(" ",oldx, oldy);
                WriteAt("*", Convert.ToInt32(x),  43 + Convert.ToInt32(y));
                oldx = Convert.ToInt32(x); oldy = 43 + Convert.ToInt32(y);


                if (Sw.DistanceToCup -dist < -50)   // Shout... Fore!
                {

                    Fore();
                    Thread.Sleep(10);
                    RemoveText();
                    Thread.Sleep(10);
                }
                else  Thread.Sleep(20);

            }
            WriteAt(" ", oldx, oldy);
        }

        /// <summary>
        /// Move flag closer depending on distance.
        /// </summary>
        /// <param name="distanceToFlag"></param>
        public void FlagDistanceAway(double distanceToFlag)
        {
            int fcol = 130;
            int line = 40;            

            if (distanceToFlag > 400)
            {
                line += 5;
                WriteAt(@"|¨        ", fcol, line++);
                return;
            }
            if (distanceToFlag > 300)
            {
                line += 4;
                WriteAt(@"|¨'        ", fcol, line++);
                WriteAt(@"|          ", fcol, line++);
                return;
            }
            if (distanceToFlag > 200)
            {
                line += 3;
                WriteAt(@"|>:>      ", fcol-15, line++);
                WriteAt(@"|         ", fcol-15, line++);
                WriteAt(@"|         ", fcol-15, line++);
                return;
            }
            if (distanceToFlag > 100)
            {
                line += 2;
                WriteAt(@"|>xx>     ", fcol-30, line++);
                WriteAt(@"|         ", fcol-30, line++);
                WriteAt(@"|         ", fcol-30, line++);
                WriteAt(@"|         ", fcol-30, line++);
                return;
            }
            if (distanceToFlag > 50)
            {
                line += 1;
                WriteAt(@"|>XX>     ", fcol-60, line++);
                WriteAt(@"|         ", fcol-60, line++);
                WriteAt(@"|         ", fcol-60, line++);
                WriteAt(@"|         ", fcol-60, line++);
                WriteAt(@"|         ", fcol-60, line++);
                return;
            }

            WriteAt(@"|>18>>    ", 35 + Convert.ToInt32(distanceToFlag), line++);
            WriteAt(@"|         ", 35 + Convert.ToInt32(distanceToFlag), line++);
            WriteAt(@"|         ", 35 + Convert.ToInt32(distanceToFlag), line++);
            WriteAt(@"|         ", 35 + Convert.ToInt32(distanceToFlag), line++);
            WriteAt(@"|         ", 35 + Convert.ToInt32(distanceToFlag), line++);
            WriteAt(@"|         ", 35 + Convert.ToInt32(distanceToFlag), line++);
        }

        /// <summary>
        /// Repaint golf course
        /// </summary>
        /// <param name="distanceToCup"></param>
        public void RestorePlayerGroundAndFlag(double distanceToCup)
        {
            Golfer();
            int line = 40;
            int pcol  = 10;
            PaintGround(pcol - 5, line);

            line = 40;
            WriteAt(@"            ", pcol, line++);// 4
            WriteAt(@"            ", pcol, line++);
            WriteAt(@"     O>     ", pcol, line++);
            WriteAt(@"     \      ", pcol, line++);
            WriteAt(@"     |\     ", pcol, line++);
            WriteAt(@"    /|/     ", pcol, line++);
            FlagDistanceAway(distanceToCup);
        }

        /// <summary>
        /// Paint ground
        /// </summary>
        /// <param name="col"></param>
        /// <param name="line"></param>
        public void PaintGround(int col, int line)
        {

            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            WriteAt(@"                                                                                                                                            ", col, line++);
            WriteAt(@"                                                                                                                                            ", col, line++);
            WriteAt(@"                                                                                                                                            ", col, line++);
            WriteAt(@"                                                                                                                                            ", col, line++);
            WriteAt(@"                                                                                                                                            ", col, line++);
            WriteAt(@"                                                                                                                                            ", col, line++);
            WriteAt(@"   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^'^^^^'^^^^^^^^^'^^^^^^^'^^^^^^'''^^^^^^^^^'^^^^^^^^^^^^^'^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^", col, line++);
            WriteAt(@"  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^.^.^.^^.^^'^^^^.^^.^^.^^.^^^'^^^^.^^^^.^^.^^^^^.^^^^'^^.^^^.^^.^^'^^.^^^^,^^^.^.^.^^,^.^^^.^^.^^^^^ ", col, line++);
            WriteAt(@" ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^.^.^^.^.^^^^.^^.^.^.^^^'^^^.^^.^^.^^.^^'^^.^^^..^.^^.^^^'^.^^.^.^.^^.^^^.^^^.^'^^.^^.^.^.^^.^^.^^^^^^^^  ", col, line++);
            WriteAt(@"^^^^^^^^^^^^^^^^^^^^^^^^^^^^^.^^^^^^^^^^.^^^^^^^^^^^^^^..^^^^^^'^^'^^^^^^.^..^^^^^.^^^^^^^'^^^.^.^^^^^''^^^^'^^^^^.^^^^^^.^^^^^^^^^^^^^^^   ", col, line++);
            Console.ForegroundColor = tmp;
        }

        /// <summary>
        /// Player animation
        /// </summary>
        /// <param name="Sw"></param>
        public void PlayerAnimation(Swing Sw) {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            if (Sw.Club == 1) DriverPlayerAnimation();
            else PutterPlayerAnimation();
            Console.ForegroundColor = tmp;
        }

        /// <summary>
        /// Driver animation
        /// </summary>
        public void DriverPlayerAnimation()
        {
            //return;
            Console.CursorVisible = false;
            int wait = 200;
            int line = 40;
            int pcol = 10;

            // Thread.Sleep(wait);
            PaintGround(pcol - 5, line );

            WriteAt(@"       /`   ", pcol, line++);// 1
            WriteAt(@"      /     ", pcol, line++);
            WriteAt(@"     O>     ", pcol, line++);
            WriteAt(@"      \     ", pcol, line++);
            WriteAt(@"      /\    ", pcol, line++);
            WriteAt(@"     / /    ", pcol, line++);
            Thread.Sleep(wait);
            line = 40;
            WriteAt(@" '\         ", pcol, line++);// 2
            WriteAt(@"   \        ", pcol, line++);
            WriteAt(@"     O>     ", pcol, line++);
            WriteAt(@"     \      ", pcol, line++);
            WriteAt(@"     /\     ", pcol, line++);
            WriteAt(@"    / /     ", pcol, line++);
            Thread.Sleep(wait);
            line = 40;
            WriteAt(@"            ", pcol, line++);// 3
            WriteAt(@"            ", pcol, line++);
            WriteAt(@",____O>     ", pcol, line++);
            WriteAt(@"     \      ", pcol, line++);
            WriteAt(@"     |\     ", pcol, line++);
            WriteAt(@"    / /     ", pcol, line++);
            Thread.Sleep(wait);
            line = 40;
            WriteAt(@"            ", pcol, line++);// 4
            WriteAt(@"            ", pcol, line++);
            WriteAt(@"     O>     ", pcol, line++);
            WriteAt(@"     \      ", pcol, line++);
            WriteAt(@"     |\     ", pcol, line++);
            WriteAt(@"    /|/     ", pcol, line++);
            Thread.Sleep(wait);
            line = 40;
            WriteAt(@"            ", pcol, line++);// 5
            WriteAt(@"            ", pcol, line++);
            WriteAt(@"     O>     ", pcol, line++);
            WriteAt(@"     \\____,", pcol, line++);
            WriteAt(@"     |\     ", pcol, line++);
            WriteAt(@"    / /     ", pcol, line++);
            Thread.Sleep(wait);
            line = 40;
            WriteAt(@"    '\      ", pcol, line++);// 6
            WriteAt(@"      \     ", pcol, line++);
            WriteAt(@"     O>     ", pcol, line++);
            WriteAt(@"      \     ", pcol, line++);
            WriteAt(@"      /\    ", pcol, line++);
            WriteAt(@"     / /    ", pcol, line++);
            Thread.Sleep(wait);
            line = 40;
            WriteAt(@"            ", pcol, line++);// 5
            WriteAt(@"            ", pcol, line++);
            WriteAt(@"     O>     ", pcol, line++);
            WriteAt(@"     \\____,", pcol, line++);
            WriteAt(@"     |\     ", pcol, line++);
            WriteAt(@"    / /     ", pcol, line++);
            Thread.Sleep(wait);
            line = 40;
            WriteAt(@"            ", pcol, line++);// 4
            WriteAt(@"            ", pcol, line++);
            WriteAt(@"     O>     ", pcol, line++);
            WriteAt(@"     \      ", pcol, line++);
            WriteAt(@"     |\     ", pcol, line++);
            WriteAt(@"    /|/     ", pcol, line++);
       
        }

        /// <summary>
        /// Putter animation
        /// </summary>
        public void PutterPlayerAnimation()
        {
            Console.CursorVisible = false;
            int wait = 200;
            int line = 40;
            int pcol = 10;

            Thread.Sleep(wait);
            PaintGround(pcol - 5, line);

  
            WriteAt(@"            ", pcol, line++);// 3
            WriteAt(@"            ", pcol, line++);
            WriteAt(@",____O`     ", pcol, line++);
            WriteAt(@"     \      ", pcol, line++);
            WriteAt(@"     |\     ", pcol, line++);
            WriteAt(@"    / /     ", pcol, line++);
            Thread.Sleep(wait);
            line = 40;
            WriteAt(@"            ", pcol, line++);// 4
            WriteAt(@"            ", pcol, line++);
            WriteAt(@"     O`     ", pcol, line++);
            WriteAt(@"     \      ", pcol, line++);
            WriteAt(@"     |\     ", pcol, line++);
            WriteAt(@"    /|/     ", pcol, line++);
            Thread.Sleep(wait);
            line = 40;
            WriteAt(@"            ", pcol, line++);// 5
            WriteAt(@"            ", pcol, line++);
            WriteAt(@"     O`     ", pcol, line++);
            WriteAt(@"     \\____,", pcol, line++);
            WriteAt(@"     |\     ", pcol, line++);
            WriteAt(@"    / /     ", pcol, line++);
            Thread.Sleep(wait);
            line = 40;
            WriteAt(@"            ", pcol, line++);// 4
            WriteAt(@"            ", pcol, line++);
            WriteAt(@"     O`     ", pcol, line++);
            WriteAt(@"     \      ", pcol, line++);
            WriteAt(@"     |\     ", pcol, line++);
            WriteAt(@"    /|/     ", pcol, line++);
        }

        /// <summary>
        /// Player lost
        /// </summary>
        /// <param name="s"></param>
        public void Looser(string s)
        {
            Console.Clear();
            ConsoleColor tmp = Console.ForegroundColor;
            int wait = 250;
            for (int i = 0; i <= 5; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Lost();
                Thread.Sleep(wait);
                Console.ForegroundColor = ConsoleColor.Red;
                Lost();
                Thread.Sleep(wait);
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Lost();
            Thread.Sleep(1000);
            Console.ForegroundColor = tmp;

            WriteAt(s, 25, 13);

        }
        public void Lost()
        {
            int line = 2;

            WriteAt(@" /$$     /$$ /$$$$$$  /$$   /$$       /$$        /$$$$$$   /$$$$$$  /$$$$$$$$ ", 25, line++);
            WriteAt(@"|  $$   /$$//$$__  $$| $$  | $$      | $$       /$$__  $$ /$$__  $$|__  $$__/ ", 25, line++);
            WriteAt(@" \  $$ /$$/| $$  \ $$| $$  | $$      | $$      | $$  \ $$| $$  \__/   | $$    ", 25, line++);
            WriteAt(@"  \  $$$$/ | $$  | $$| $$  | $$      | $$      | $$  | $$|  $$$$$$    | $$    ", 25, line++);
            WriteAt(@"   \  $$/  | $$  | $$| $$  | $$      | $$      | $$  | $$ \____  $$   | $$    ", 25, line++);
            WriteAt(@"    | $$   | $$  | $$| $$  | $$      | $$      | $$  | $$ /$$  \ $$   | $$    ", 25, line++);
            WriteAt(@"    | $$   |  $$$$$$/|  $$$$$$/      | $$$$$$$$|  $$$$$$/|  $$$$$$/   | $$    ", 25, line++);
            WriteAt(@"    |__/    \______/  \______/       |________/ \______/  \______/    |__/    ", 25, line++);
        }

        /// <summary>
        /// Player win
        /// </summary>
        /// <param name="s"></param>
        public void Winner(string s)
        {
            Console.Clear();
            ConsoleColor tmp = Console.ForegroundColor;
            int wait = 250;
            for (int i = 0; i <= 5; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Win();
                Thread.Sleep(wait);
                Console.ForegroundColor = ConsoleColor.Green;
                Win();
                Thread.Sleep(wait);
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Win();
            Thread.Sleep(1000);
            Console.ForegroundColor = tmp;

            WriteAt(s, 25, 13);

        }

        public void Win()
        {
            int line = 2;

            WriteAt(@" /$$     /$$ /$$$$$$  /$$   /$$       /$$      /$$ /$$$$$$ /$$   /$$       /$$ ", 25, line++);
            WriteAt(@"|  $$   /$$//$$__  $$| $$  | $$      | $$  /$ | $$|_  $$_/| $$$ | $$      | $$ ", 25, line++);
            WriteAt(@" \  $$ /$$/| $$  \ $$| $$  | $$      | $$ /$$$| $$  | $$  | $$$$| $$      | $$ ", 25, line++);
            WriteAt(@"  \  $$$$/ | $$  | $$| $$  | $$      | $$/$$ $$ $$  | $$  | $$ $$ $$      | $$ ", 25, line++);
            WriteAt(@"   \  $$/  | $$  | $$| $$  | $$      | $$$$_  $$$$  | $$  | $$  $$$$      |__/ ", 25, line++);
            WriteAt(@"    | $$   | $$  | $$| $$  | $$      | $$$/ \  $$$  | $$  | $$\  $$$           ", 25, line++);
            WriteAt(@"    | $$   |  $$$$$$/|  $$$$$$/      | $$/   \  $$ /$$$$$$| $$ \  $$       /$$ ", 25, line++);
            WriteAt(@"    |__/    \______/  \______/       |__/     \__/|______/|__/  \__/      |__/ ", 25, line++);

        }

        /// <summary>
        /// Player want to quit.
        /// </summary>
        /// <returns></returns>
        public bool QuitGameQuestion()
        {
            int line = 10;
            int col  = 50;
            Console.Clear();
            WriteAt("Vill du verkligen avsluta spelet?    Ja / Nej [?]                   ", col +5, line);
            Console.SetCursorPosition(col + 52, line);
            Console.CursorVisible = true;
            char key = Console.ReadKey().KeyChar;
            key = char.ToUpper(key);
            if (key == 'J') return true;       // Quit           
            return false;
        }
  
        /// <summary>
        /// Player log while playing. Only last 6 swing.
        /// </summary>
        /// <param name="Sw"></param>
        public void DisplayLog(Swing Sw)
        {
            int line = 52;
            int col = 20;
            int lineEnd = Sw.Log.Count -1;
            int i = Sw.Log.Count - 6;
            if (i < 0) i = 0;
            for (; i <= lineEnd; i++)
            {
                WriteAt(Sw.Log.ElementAt(i), col, line++);
            }
        }

        /// <summary>
        /// Clean selected line
        /// </summary>
        /// <param name="col"></param>
        /// <param name="line"></param>
        public void CleanLineAt(int col, int line)
        {
            // 150 wide. Full screen.
            string s = "                                                                                                                                                      ";
            int w = Console.WindowWidth -1;
            WriteAt(s.Substring(0, w - col),col,line);
        }

        /// <summary>
        /// Place and write "s" at selected "x,y" position on screen.
        /// If outside screen. Clear and reset window size.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch
            {
                Console.Clear();
                Console.SetWindowSize(150, 60);
            }
        }
    }
}
