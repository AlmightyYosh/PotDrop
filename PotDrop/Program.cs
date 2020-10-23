using System;
using System.Collections.Generic;

namespace PotDrop
{
    class Program
    {

        //creates pots 
        static double pot = 0;
        static double r = 0;

        static bool BotInPlay;


        static void Main(string[] args)
        {
            //double[] betActive = { 10, 10, 10, 10, 10, 0.50, 30, 0.00, 200, 60.33};
            //int[] id = { 5, 3, 2, 1, 4, 7, 6, 9, 10, 8};


            Reset();

            int[] id = UserInput();
            


            string input = "";

            while (input != "*")
            {
                double[] betActive = bets(id);

                //this allow the bets in the pot to be shuffled testing this implementation incase cheater manage to find an exploit in knowing thier position in the pot
                //Shuffle(id, betActive);  


                Console.WriteLine("");
                Console.WriteLine("Ticket Number");
                r = Result();
                Console.WriteLine(r);
                Console.WriteLine("");



                double[] m = Game(betActive, id);
                Console.WriteLine("");


                Console.WriteLine("Winner:");
                Console.WriteLine("User {0}", Winner(m, id, r));
                Console.WriteLine("");

                Console.WriteLine("Please Enter to restart round. Or * to exit. ");
                input = Console.ReadLine();

            }
            


        }
         

        public static int botActive(bool bot)
        {
            if (BotInPlay == true)
            {
                return 1;
            }

            return 0;
        }

        public static void Shuffle(int[] id, double[] bet)
        {
            Random rand = new Random();
            //add length-1 when bot active
            for (int i = 0; i < id.Length-botActive(BotInPlay); i++)
            {
                int j = rand.Next(i, id.Length-botActive(BotInPlay));

                
                int temp = id[i];
                double temp2 = bet[i];

                id[i] = id[j];
                bet[i] = bet[j];

                id[j] = temp;
                bet[j] = temp2;
            }

       

        }

        public static int[] UserInput()
        {
            List<int> id = new List<int>();
            string input = "";
            
            string playBot = "";
            int hold = 1;

            Console.WriteLine("Welcome to PotDrop 2.8 Bata");
            Console.WriteLine(" ");

            //need to work on this
            while (playBot != "*")
            {
                Console.WriteLine("Do you want to play with a bot(Adds 10% )? for yes press * or just press enter to continue");
                playBot = Console.ReadLine();
                if (playBot == "*")
                {
                    BotInPlay = true;
                }
                if(playBot != "*")
                {
                    BotInPlay = false;
                    playBot = "*";
                }
            }


            while (input != "*")
            {

                
                Console.WriteLine("Please enter a UserName:");
                string userName = Console.ReadLine();
                Console.WriteLine(" ");
                



                
                if (userName != null)
                {
                    
                    id.Add(hold);
                    Console.WriteLine("{0}, your Id is {1}", userName, hold);
                    hold += 1;
                    
                }

                Console.WriteLine("");
                Console.WriteLine("Press * to sart the game or press enter to sign into the next user.");
                input = Console.ReadLine();
                
            }

            //add this in comments if you want bot off.
            if (BotInPlay == true)
            {
                id.Add(RobotId(id.ToArray()));
            }
            return id.ToArray();

            
        }

        public static double[] bets(int[] id)
        {
            List<double> bet = new List<double>();
            //add length-1 when bot active
            for (int i = 0; i < id.Length-botActive(BotInPlay); i++)
            {
                Console.WriteLine("");
                Console.WriteLine("user Id {0} can you please type in your bet amount below:",id[i]);

                double input = 0;

                while (!double.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Please Enter a valid numerical value!");

                }

                double num = Convert.ToDouble(input);
                bet.Add(num);
            }

            //add this in comments if you want bot off.
            if (BotInPlay == true)
            {
                bet.Add(RobotBet(bet.ToArray()));
            }
            return bet.ToArray();
        }
    
        //Robot
        public static int RobotId(int[] id)
        {
            int botId = 999;

            for(int i = 0; i < id.Length; i++)
            {
                int hold = 0;

                if(id[i] > hold)
                {
                    botId = id[i];
                }

            }
            return botId + 1;


            
        }

        public static double RobotBet(double[] bet)
        {
            double p = 0;

            for (int i = 0; i < bet.Length; i++)
            {
                p += bet[i];

            }

            //10%
            return p * 0.1;

        }

        //result
        public static double Result()
        {
            Random randObj = new Random();
            int range = 100;
            double rDouble = randObj.NextDouble() * range;
            

            return Math.Round(rDouble, 2);



        }


        public static double[] Game(double[] bet, int[] id)
        {
            pot = 0;

            for(int i = 0; i < bet.Length; i++)
            {
                pot += bet[i];

            }
            Console.WriteLine("Pot Amount:");
            Console.WriteLine(pot);
            Console.WriteLine(" ");



            //returns array of percents 
            static double[] Percent(double pot, double[] bet, int[] id)
            {
                List<double> percent = new List<double>();
                

                foreach (double i in bet)
                {
                    double round = (i / pot) * 100;
                    double z = Math.Round(round, 2);

                    percent.Add(z);
                }

                //display
                for(int i = 0; i < percent.Count; i++)
                {
                    for(int j = 0; j < id.Length; j++)
                    {
                        for(int x = 0; x < bet.Length; x++)
                        {
                            if (i == j && i == x)
                                Console.WriteLine("User {0}: Bet Amount: {1} - {2}%", id[j], bet[x], percent[i]);

                        }
                        
                        
                    }
                }

            

                return percent.ToArray();
            }


            return Percent(pot, bet, id);

        }

        public static int Winner(double[] per, int[] id, double result)
        {
            double winner = 0;
            for(int i = 0; i < per.Length; i++)
            {
                winner += per[i];
                
                if(winner == result || winner > result)
                {
                    return id[i];
                }
            }

            return -1;
        }


        public static void Reset()
        {
            pot = 0;
            r = 0;
        }
    }
}
