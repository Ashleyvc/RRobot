using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRobot
{
    class User
    {
       
        public List<int> Position { get; set; }
        public int BoundaryY { get; set; }
        public int BoundaryX { get; set; }

        public User(int FactorySizeX, int FactorySizeY, List<int> UserPosition)
        {
            this.BoundaryX = FactorySizeX - 1;
            this.BoundaryY = FactorySizeY - 1;
            this.Position = UserPosition;
        }

        // Bepaal de nieuwe positie van de User na het toepassen van de opgegeven richting
        public void ChangePosition(char Move)
        { 
            List<int> OldPosition = this.Position;

            if (Move == 'a') { OldPosition[0]--; }
            else if (Move == 's') { OldPosition[1]++; }
            else if (Move == 'd') { OldPosition[0]++; }
            else if (Move == 'w') { OldPosition[1]--; }

            // Als er door de opgegeven richting tegen de muur wordt aangelopen,
            // zal de User op dezelfde plek blijven staan.
            for (int i=0; i<2; i++) {
                if (OldPosition[i] < 0) { OldPosition[i] = 0; }
            }
            if (OldPosition[0] > BoundaryX) { OldPosition[0] = BoundaryX; }
            if (OldPosition[1] > BoundaryY) { OldPosition[1] = BoundaryY; }
            Position = OldPosition;
        }

    }
}
