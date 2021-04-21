using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfGame
{
    class Swing
    {      
        public int  MaxSwingNr
        {
            get { return _maxSwingNr; }
            set { _maxSwingNr = value; }
        }
        public int Par
        {
            get { return _par; }
            set { _par = value; }
        }
        public int SwingDirection
        {
            get { return _swingDirection; }
            set { _swingDirection = value; }
        }
        public double DistanceToCup
        {
            get { return _distanceToCup; }
            set { _distanceToCup = value; }
        }
        public int Strengt
        {
            get { return _strengt; }
            set { _strengt = value; }
        }
        public int Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }
        public int Club
        {
            get { return _club; }
            set { _club = value; }
        }
        public int SwingNr
        {
            get { return _swingnr; }
            set { _swingnr = value; }
        }
        public bool GameOwer
        {
            get { return _gameower; }
            set { _gameower = value; }
        }
        public bool GameWin
        {
            get { return _gamewin; }
            set { _gamewin = value; }
        }

        public List<string> Log = new List<string>();  // Log

        const   double GRAVITY = 9.8;
        private double _distanceToCup;              // in meter 

        const   int _driver = 1;                    // Just for code readability. 
        private int _swingDirection = 1;
        private int _strengt = 45;
        private int _angle = 45;
        private int _club = _driver;                // 1 = driver, -1 = putter
        private int _maxSwingNr;
        private int _par;
        private int _swingnr = 0;

        private bool _gameower = false;             // Exit flag.
        private bool _gamewin = false;

        private string log;
        public Graph Gr = new Graph();

        /// <summary>
        /// Swing the club. 
        /// </summary>
        public void SwingClub()
        {
            double str = _strengt; 
            if (_club != _driver) str /= 5;         // Putter
            double dist = Math.Pow(str, 2) / (double)GRAVITY * Math.Sin(2 * (Math.PI / 180) * (double)_angle);

            Gr.PlayerAnimation(this);
            Gr.BallTrajectory(this,dist);  

            _swingnr++;

            // Build log.
            log = "Swing Nr: " + _swingnr.ToString() +
                  " av " + _maxSwingNr + ".  Du slog " + dist.ToString("n2") + " meter.  Stryrkan var " +
                  _strengt.ToString() + " och vinkeln " + _angle; 
            
            string l = CheckStatus(dist);      

            log += " Det är " + _distanceToCup.ToString("n2") + " kvar. " + l;
            Log.Add(log);
            Gr.DisplayLog(this);
        }

        /// <summary>
        /// Check swing status. 
        /// </summary>
        /// <param name="dist"></param>
        /// <returns></returns>
        public string CheckStatus(double dist)
        {
            try
            {
                LostBallExeption(dist);

                if (Math.Abs(_distanceToCup) < 0.3)       // Ball in the cup ?
                {
                    if (_swingnr == 1) Gr.Winner("Du lyckades med Hole in one.. JoHoo... Winner!");
                    else Gr.Winner("Du lyckades JoHoo... Winner!");

                    _gamewin = true;
                    return " Status: Du lyckades JoHoo. Du är en Winnare !\n\n";
                }
            }

            catch (ArgumentException e)
            {
                Gr.Looser(e.GetType().Name);
                _gameower = true;
                return " Status: Du hamnade i vattnet. Game Ower!\n\n";
            }
            return " Status OK. \n";
        }

        /// <summary>
        /// Exception. Ball to far away from green.
        /// </summary>
        /// <param name="dist"></param>
        public void LostBallExeption(double dist)
        {
            double test = _distanceToCup - dist;
            if (_distanceToCup - dist < -50)                             // To far away.. In the water.
            {
                test = -test;
                throw new ArgumentException(String.Format(" Din boll är i vattnet, " + test.ToString("N2") + " meter från flaggan. Spelet är slut"));
            }

            _distanceToCup -= dist;
            if (_distanceToCup < 0)
            {
                _swingDirection = -_swingDirection;     // Alter direction.
                _distanceToCup = -_distanceToCup;       // Allvays positive distance.
            }
            return;
        }

        /// <summary>
        /// Golfer try new strength
        /// </summary>
        public void ReadNewStrengthValue()
        {
            int col = Gr.menucolumn; int line = Gr.menuline + 2;
            string s = "     Ange Styrka på ditt slag. [    ]  1 - 100 styrka.             ";
            _strengt = ReadIntNumber(s, 100, 1, col, line);
            Gr.Menu(this);
        }

        /// <summary>
        /// Golfer try new angle
        /// </summary>
        public void ReadNewAngleValue()
        {
            int col = Gr.menucolumn; int line = Gr.menuline + 3;
            string s = "     Ange Vinkel på ditt slag. [    ]  0 - 90 grader.              ";
            _angle = ReadIntNumber(s, 90, 0, col, line);
            Gr.Menu(this);
        }

        /// <summary>
        /// Failsafe input routine for strength and angle value.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="col"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public int ReadIntNumber(string msg, int max, int min, int col, int line)
        {
            int nr = 0;
            string s;
            do
            {
                Gr.RemoveText();

                WriteAt(msg, col, line);
                Console.SetCursorPosition(col + 33, line);
                s = Console.ReadLine();

                {
                    try
                    {
                        nr = TryParseInt(s, max, min);
                        return nr;
                    }
                    catch (ArgumentException e)
                    {
                        string[] exeption = (e.GetType().Name + " : " + e.Message).Split('\n');
                        WriteAt(exeption[0], col + 5, line + 1);

                        if (exeption.Length > 0)
                            WriteAt(exeption[1], col + 5, line + 2);

                        WriteAt("Tryck Enter.", col + 5, line + 3);

                        Console.ReadKey();

                        Gr.CleanLineAt(col, line + 1);
                        Gr.CleanLineAt(col, line + 2);
                        Gr.CleanLineAt(col, line + 3);
                    }
                }
            } while (true);
        }

        /// <summary>
        /// Exeption routine for failed number.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public int TryParseInt(string s, int max, int min)
        {
            if (int.TryParse(s, out int nr))
            {
                if (nr < max + 1 && nr > min - 1) return nr;
                else throw new ArgumentException(String.Format("{0} Är utanför (max {1} - min {2}) värdet.", nr, max, min), "int nr"); //, "int nr");
            }
            else throw new ArgumentException(String.Format("{0} Kan inte konverteras till ett heltal.", s), "string s"); //, "string s");
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
