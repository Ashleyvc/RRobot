using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRobot
{

    class Robot
    {

        public List<int> Position { get; set; }
        public int BoundaryY { get; set; }
        public int BoundaryX { get; set; }

        public Robot(int FactorySizeX, int FactorySizeY) {
            this.BoundaryX = FactorySizeX - 1;
            this.BoundaryY = FactorySizeY - 1;
        }

        // Bepaal een random richting voor een robot
        public void RandomMove(Random rand) {
            List<char> TypeOfMoves = new List<char> { 's', 'd', 'w', 'a' };
             
            // Als de robot naast een muur staat, kan de robot niet in de richting van de muur lopen
            int PossibleMovements = 4; 
            if (Position[0] == 0) {
                TypeOfMoves.Remove('a');
                PossibleMovements--;
            }
            if (Position[0] == BoundaryX)
            {
                TypeOfMoves.Remove('d');
                PossibleMovements--;
            }
            if (Position[1] == 0)
            {
                TypeOfMoves.Remove('w');
                PossibleMovements--;
            }
            if (Position[1] == BoundaryY)
            {
                TypeOfMoves.Remove('s');
                PossibleMovements--;
            }

            // Bepaal een random richting uit alle mogelijke richtingen
            int NumberMove = rand.Next(PossibleMovements);
            char Move = TypeOfMoves[NumberMove];

            // Verander de positie van de robot
            ChangePosition(Move);

        }

        public void ChangePosition(char Move)
        {
            // Verander de positie van de robot aan de hand van de richting
            if (Move == 'a') { Position[0]--; }
            if (Move == 's') { Position[1]++; }
            if (Move == 'd') { Position[0]++; }
            if (Move == 'w') { Position[1]--; }
        }

        // Bepaal een random start positie
        public void StartPosition(Random rand)
        {
            List<int> RobotPosition = new List<int>();

             // Bepaal een random positie tussen 0 en de grootte van de fabriek
            int NumberMove1 = rand.Next(BoundaryX+1);
            RobotPosition.Add(NumberMove1);
            int NumberMove2 = rand.Next(BoundaryY + 1);
            RobotPosition.Add(NumberMove2); 

            Position = RobotPosition;
        }

        // Controleer of robots niet op dezelfde plek staan
        public void CheckRobotPosition(List<Robot> Allrobots, Random rand, int Start)
        {
            bool Check;

           // Als er robots op dezelfde plek staan, zal er een nieuwe positie voor de huidige robot moeten worden
           // bepaald. Vervolgens zal er nog een keer worden gecontroleerd of er geen robots op dezelfde plek 
           // staan. Dit process herhaalt zich totdat er geen robots meer op dezelfde plek staan.
            do
            {
                Check = false;

                // Kijk voor elke robot
                foreach (Robot rbt in Allrobots)
                {
                    // Als rbt niet de huidige robot is en de positie van rbt dezelfde positie is als van de 
                    // huidige robot
                    if (rbt != this && Position.SequenceEqual(rbt.Position))
                    {
                        // In het geval van het bepalen van een startpositie zal er opnieuw een random 
                        // startpositie voor de huidige robot worden bepaald.
                        if (Start == 1)
                        {
                            StartPosition(rand);
                        }
                        // Als de User al een beweging heeft opgegeven, zal er een nieuwe richting voor de
                        // huidige robot worden bepaald
                        else
                        {
                            RandomMove(rand);
                        }

                        Check = true;
                    }
                }

            } while (Check == true);
        }

    }
}
