using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRobot
{
    
    class Program
    {
        static void Main(string[] args)
        {
            // Aanmaken van de fabriek
            int SizeFactoryX = 10;
            int SizeFactoryY = 5;
            Factory F1 = new Factory(SizeFactoryX, SizeFactoryY);

            // Aanmaken van onderhoudsmonteur
            List<int> UserPosition = new List<int> {0, 0};
            User UserofGame = new User(F1.SizeX, F1.SizeY, UserPosition);
           
            // Aanmaken van de robots
            Robot Robot1 = new Robot(F1.SizeX, F1.SizeY);
            Robot Robot2 = new Robot(F1.SizeX, F1.SizeY);
            Robot Robot3 = new Robot(F1.SizeX, F1.SizeY);
            Robot Robot4 = new Robot(F1.SizeX, F1.SizeY);

            // Robots samenvoegen in een lijst
            List<Robot> AllRobots = new List<Robot>();
            AllRobots.Add(Robot1);
            AllRobots.Add(Robot2);
            AllRobots.Add(Robot3);
            AllRobots.Add(Robot4);

            // Bepaal een randomstartpositie voor elke robot
            Random rand = new Random();
            foreach (Robot rbt in AllRobots)
            {
                rbt.StartPosition(rand);
            }
            
            // Check of de robots niet op dezelfde plek staan            
            foreach (Robot rbt in AllRobots) {
                rbt.CheckRobotPosition(AllRobots, rand, 0);
            }

            // Sla de posities van de onderhoudsmonteur en de robots op in een lijst.
            List<List<int>> AllPositions = new List<List<int>> { UserPosition, Robot1.Position,
                                                                Robot2.Position, Robot3.Position, Robot4.Position };

            // De oliekan heeft een grootte van 60
            int Oilcan = 60;
            int indexKilledRobot;
            string Movement;
            string TypesOfMovements = "adsw";

            // Ga door met het spel tot de oliekan op is of als de onderhoudsmonteur alle robots heeft gesmeerd
            do
            {
                // Intro voor de gebruiker
                Console.WriteLine("RAMPANT ROBOTS\n");
                Console.WriteLine("Probeer alle robots in de fabriek te smeren voordat de oliekan leeg is");
                Console.WriteLine("Gebruik de WASD-toetsen om de onderhoudsmonteur te laten bewegen");
                Console.WriteLine("Succes!\n");

                // Weergeven van de grootte van de oliekan
                Console.WriteLine("Oliekan: {0}", Oilcan);
                F1.FactoryBuilding(AllPositions);

                // Vraag de User om één of meerdere bewegingen op te geven
                Console.Write("Geef één of meerdere bewegingen op:");
                Movement = Console.ReadLine();

                // Als de opgegeven beweging niet bestaat uit de richtingen WASD
                // gebeurt er niks
                if (Movement.Contains('a') || Movement.Contains('s')
                 || Movement.Contains('d') || Movement.Contains('w')) { 

                    //Voor elke richting uit de totale beweging 
                    foreach (char Move in Movement)
                    {
                        // Als de richting niet bestaat uit een WASD richting
                        // zal dit character worden overgeslagen
                        if (TypesOfMovements.Contains(Move))
                        {
                            // Zet één stap in de gekozen richting (Move)
                            UserofGame.ChangePosition(Move);
                            
                            // Elke robot zet ook één stap
                            for (int i = 0; i < AllRobots.Count; i++)
                            {
                                // Bepaal per robot een random richting
                                AllRobots[i].RandomMove(rand);

                                // Controleren of robots niet op dezelfde positie staan
                                AllRobots[i].CheckRobotPosition(AllRobots, rand, 0);
                            }
                        }
                    }
                    // Verklein de grootte van de oliekan
                    Oilcan--;
                }

                // Controleer of er een Robot is gesmeerd
                indexKilledRobot = F1.KillRobot(AllPositions);

                // Als er een robot is gesmeerd, moet deze robot worden verwijderd uit de lijst met alle posities
                // en de lijst met alle robots
                if (indexKilledRobot != -1)
                {
                    AllPositions.Remove(AllPositions[indexKilledRobot]);
                    AllRobots.Remove(AllRobots[indexKilledRobot - 1]);
                }

                // Het scherm wordt schoongemaakt om de volgende situatie te visualiseren.
                Console.Clear();

            } while (Oilcan!=0 && AllPositions.Count !=1);
            
            // Als alle robots zijn gesmeerd, wordt dit duidelijk gemaakt aan de gebruiker
            F1.FactoryBuilding(AllPositions);
            if (AllPositions.Count == 1)
            {
                Console.WriteLine("\nGefeliciteerd, alle robots zijn gesmeerd!");
            }
            // Als niet alle robots zijn gesmeerd, maar de oliekan is wel op, zal hierover een melding volgen
            else {Console.WriteLine("\nHelaas, het is u niet gelukt om alle robots op tijd te smeren"); }

            Console.WriteLine("\nDe game wordt volledig afgesloten als u op 'enter' klikt");
            Console.ReadKey();    
        }
    }
}
