using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRobot
{
    class Factory
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public Factory(int FactorSizeX, int FactorSizeY) {
            this.SizeX = FactorSizeX;
            this.SizeY = FactorSizeY;
        }

        // Maak de fabriek aan
        public void FactoryBuilding(List<List<int>> Positions) {
            
            for (int i = 0; i < SizeY; i++)
            {   
                // Leg de vloer
                List<string> row = new List<string>();
                for (int Punt = 0; Punt < SizeX; Punt++) {
                    row.Add(".");
                }

                // Visualiseer de User en de robots in de fabriek
                for (int j = 0; j < Positions.Count; j++)
                {
                    if (Positions[j][1] == i)
                    {
                        if (j == 0)
                        {
                            row[Positions[j][0]] = "M";
                        }
                        else { row[Positions[j][0]] = "R"; }
                    }
                }
                Console.WriteLine(String.Join("  ", row));            
            }
        }

        // Controleer of er een robot is gesmeerd
        public int KillRobot(List<List<int>> AllPositions)
        {
            List<int> UserPosition = AllPositions[0];

            // Als er geen robot is gesmeerd zal indexKilledRobot gelijk blijven aan -1
            // Anders krijgt indexKilledRobot de index van de robot uit de lijst AllPositions.
            int indexKilledRobot = -1;
            for (int i = 1; i < AllPositions.Count; i++)
            {
                if (AllPositions[i].SequenceEqual(UserPosition)) {
                    indexKilledRobot = i;
                }
            }
            return indexKilledRobot;
        }
    }
}
