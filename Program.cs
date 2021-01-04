using System;
using System.Collections.Generic;

namespace CS01
{
    class Program
    {
        static void Main(string[] args)
        {
            string play = "yes";

            while(play == "yes")
            {
            
           
            Console.WriteLine("Instructions:\na - attack\ne - eat\ns - sleep\nplayers - see the list of players\nslots - see how many slots you have\nhealth - see your hp.");
            Console.WriteLine("Each player starts with 10 health points and 2 spell slots.\nGood luck!");

            Reset(1,5); 

            Console.WriteLine("Choose player:");

            string input = Console.ReadLine();
            int playerid = 0;   
            bool sucess = int.TryParse(input, out playerid);

            while (sucess == false || playerid < 1 || playerid > 6)
            {
                Console.Write("Input not valid. Please write a number from 1 to " + Wizard.wizardsList.Count + ": ");
                input = Console.ReadLine();
                sucess = int.TryParse(input, out playerid);
            }

            playerid -= 1;
            Wizard player = Wizard.wizardsList[playerid];
            int currentplayerid = playerid;
            string playerName = Wizard.namesList[playerid];

            bool gameOver = !Wizard.wizardsList.Exists(x => x.name == playerName);
        
            Console.WriteLine("You have choosen " + playerName + ".");
            Console.Write("Choose number of opponents: ");
            input = Console.ReadLine();
            int numopp = 0;

            sucess = int.TryParse(input, out numopp);

            while (sucess == false || numopp < 1 || numopp > 5)
            {
                Console.Write("Input not valid. Please write a number from 1 to 5: ");
                input = Console.ReadLine();
                sucess = int.TryParse(input, out numopp);
            }

            Reset(playerid, numopp);

            bool found = false;
            int i = 0;
            while(!found)
            {
                if(playerName == Wizard.namesList[i]) //ver aqui
                {
                    found = true;
                    playerid = i;
                }
                else
                {
                    i++;
                }

            }
            player = Wizard.wizardsList[playerid];
            currentplayerid = playerid;

            while(Wizard.wizardsList.Count > 1 && !gameOver)
            {
                Wizard currwiz = Wizard.wizardsList[currentplayerid];
                Round(currwiz,player);
                if(currentplayerid >= Wizard.wizardsList.Count-1)
                {
                    currentplayerid = 0;
                }
                else
                {
                    currentplayerid += 1;
                }
                gameOver = !Wizard.wizardsList.Exists(x => x.name == playerName);
            }
            if (gameOver)
            {
                Console.WriteLine("You lost, game over.");
            }
            else
            {
                Console.WriteLine("Congratulations, you win!");
            }

            Console.Write("Play again? (type yes or no): ");
            play = Console.ReadLine();
            while(play != "yes" && play != "no")
            {
                Console.Write("Input not valid. Type yes or no: ");
                play = Console.ReadLine();
            }
            }

            Console.WriteLine("Thanks for playing!");
            Console.ReadKey();
        }
        
        public static void Reset(int playerind, int oppnum) //reset wizards list
        {
            int toremove = 5-oppnum;
            
            Wizard.wizardsList.Clear();
            Wizard.namesList.Clear();
        
            Wizard joey = new Wizard("Joey","HowUDoing","H");

            Wizard.wizardsList.Add(joey);
            
            Wizard chandler = new Wizard("Chandler","Unfunny Joke","H");

            Wizard.wizardsList.Add(chandler);

            Wizard ross = new Wizard("Ross", "We were on a break", "H");
            
            Wizard.wizardsList.Add(ross);

            Wizard rachel = new Wizard("Rachel", "You're a shoe", "M");

            Wizard.wizardsList.Add(rachel);

            Wizard monica = new Wizard("Monica", "I'm not fat!", "M");

            Wizard.wizardsList.Add(monica);

            Wizard phoebe = new Wizard("Phoebe", "Smelly Cat", "M");

            Wizard.wizardsList.Add(phoebe); 

            List<int> removeList = new List<int>();

            Random rnd = new Random();

            while(removeList.Count < toremove)
            {
                
                int removedPlayer = rnd.Next(0,6);
                if(removedPlayer != playerind && !removeList.Contains(removedPlayer))
                {
                    removeList.Add(removedPlayer);
                    removeList.Sort();
                }
            }
            for (int i = removeList.Count-1; i >= 0; i--)
            {
                int removedPlayer = removeList[i];
                Wizard.wizardsList.RemoveAt(removedPlayer);
                Wizard.namesList.RemoveAt(removedPlayer);
            }
            
            //inserir aqui caso queiramos baralhar a lista dos wizards
            /*for (int i = 0; i < Wizard.wizardsList.Count; i++)
            {
                int swaping = rnd.Next(0,Wizard.wizardsList.Count);
                Wizard.wizardsList = Swap(Wizard.wizardsList, i, swaping);
            }
            for (int i = 0; i < Wizard.wizardsList.Count; i++)
            {
                Wizard.namesList[i]=Wizard.wizardsList[i].name;
            }*/

            Console.WriteLine("List of players:");
            for (int i = 0; i < Wizard.namesList.Count; i++)
            {
                Console.WriteLine(i+1 + ". " + Wizard.namesList[i]);
            }
        }

        public static void showlist()
        {
            List<Wizard> auxlist = new List<Wizard>();
            auxlist = Wizard.wizardsList;
            for (int i = 0; i < auxlist.Count; i++)
            {
                Console.WriteLine(i+1 + ". " + auxlist[i].name + " - " + auxlist[i].health + " hp.");
            }
        }

        public static List<Wizard> Swap(List<Wizard> list, int indA, int indB )
        {
            Wizard aux = list[indA];
            list[indA] = list[indB];
            list[indB] = aux;
            return list;
        }

        public static void Round(Wizard currwiz, Wizard player)

        {
            Console.WriteLine("------------");
            if(currwiz == player)
            {
                
                bool help = true;
                while(help)
                {
                    Console.Write("Choose action: ");
                    string action = Console.ReadLine();

                    switch(action)
                    {
                        case "players":
                        
                            showlist();
                        
                        break;
                        case "health":
                        
                            Console.WriteLine("You have " + player.health + " hp.");
                        
                        break;
                        case "slots":
                        
                            Console.WriteLine("You have " + player.spellslots + " spell slots.");
                        
                        break;

                        case "a":
                            Console.Write("Choose target: ");

                            string input = Console.ReadLine();
                            int ind = 0;   
                            bool sucess = int.TryParse(input, out ind);
                            while (sucess == false || ind < 1 || ind > Wizard.wizardsList.Count)
                            {
                                Console.Write("Input not valid. Please write a number from 1 to " + Wizard.wizardsList.Count + ": ");
                                input = Console.ReadLine();
                                sucess = int.TryParse(input, out ind);
                                
                            }
                            ind -= 1;
                            Wizard target = Wizard.wizardsList[ind];
                            currwiz.CastSpell(target);
                            help = false;

                        break;
                        case "e":
                    
                            currwiz.Meditate();
                            help = false;
                        
                        break;
                        
                        case "s":
                        
                            currwiz.Regen();
                            help = false;
                        
                        break;
                        default:
                            Console.WriteLine("Action not valid, please choose an action from players, health, slots, a, s or e.");
                        break;
                    }
                   
                }
                
            }
            else
            {
                if(currwiz.spellslots == 0)
                {
                    currwiz.Meditate();
                }
                else
                {
                    int action; //tenho de diminuir as probabilidades de dormir
                    Random rnd = new Random();
                    if(currwiz.spellslots == 2 && currwiz.health < 10)
                    {
                        action = rnd.Next(2,6);
                    }
                    else if (currwiz.spellslots == 2)
                    {
                        action = rnd.Next(2,5);
                    }
                    else if(currwiz.health == 10)
                    {
                        action = rnd.Next(1,5);
                    }
                    else
                    {
                        action = rnd.Next(1,6);
                    }

                    switch(action)
                    {
                        case 1: 
                    
                            currwiz.Meditate();
                    
                        break;
                        case 5:
                        
                            currwiz.Regen();
                        
                        break;
                        default:
                        
                            rnd = new Random();
                            int targetind = rnd.Next(0,Wizard.wizardsList.Count);
                            Wizard target = Wizard.wizardsList[targetind];
                            while(target == currwiz)
                            {
                                targetind = rnd.Next(0,Wizard.wizardsList.Count);
                                target = Wizard.wizardsList[targetind];
                            }

                            currwiz.CastSpell(target);
                        
                        break;
                    }
                }
            }

            Console.ReadKey();

        }

    }

    class Wizard
    {
        public string name;
        public string favspell;
        public int spellslots;
        public string genero;
        public int health;
        public static int Count;
        public static List<string> namesList = new List<string>();
        public static List<Wizard> wizardsList = new List<Wizard>();
        


        public Wizard(string _name, string _favspell, string _genero)
        {
            name = _name;
            favspell = _favspell;
            genero = _genero;
            health = 10;
            spellslots = 2;

            Count ++;
            namesList.Add(_name);
        }

        public void CastSpell(Wizard _wizard)
        {
            if(spellslots < 1)
            {
                Console.WriteLine(name + " does not have enough Spells Slots.");
            }
            else
            {
            Console.WriteLine(name + " casts " + favspell + " on " + _wizard.name);
            spellslots--;
            if(name == "Joey")
            {
                if(_wizard.genero == "H")
                {
                    Console.WriteLine(_wizard.name + " is a dude. Not effective.");
                    _wizard.health -= 1;
                }
                else
                {
                    _wizard.health -= 2;
                    Console.WriteLine("It's very effective.");
                }
            }
            if(name == "Chandler")
            {
                if(_wizard.name == "Joey")
                {
                    _wizard.health -= 2;
                    Console.WriteLine("Best one Joey heard, very effective.");
                }
                else
                {
                    _wizard.health -= 1;
                    Console.WriteLine(_wizard.name + " smiled a bit.");
                }
            }
            if(name == "Ross")
            {
                if(_wizard.name == "Rachel")
                {
                    _wizard.health -= 3;
                    health -= 1;
                    Console.WriteLine("Rachel slapped Ross.");
                    Console.WriteLine("Ross now has " + health + " health.");
                }
                else if (_wizard.genero == "M")
                {
                    _wizard.health -= 1;
                    Console.WriteLine("You better shut up - said " + _wizard.name);
                }
                else
                {
                    _wizard.health -= 1;
                    Console.WriteLine("Careful dude - said " + _wizard.name);
                }
            }
            if(name == "Monica")
            {
                _wizard.health -= 1;
                Console.WriteLine(_wizard.name + " got scared.");
            }
            if(name == "Rachel")
            {
                if(_wizard.name == "Ross")
                {
                    _wizard.health -= 2;
                    Console.WriteLine("Ross is now in love and wants to marry Rachel");
                }
                else
                {
                _wizard.health -= 1;
                Console.WriteLine(_wizard.name + " got confused.");
                }
            }
            if(name == "Phoebe")
            {
                _wizard.health -= 1;
                Console.WriteLine(_wizard.name + " got an headache.");
            }

            Console.WriteLine(_wizard.name + " now has " + _wizard.health + " health.");
            
            if(_wizard.health < 1)
            {
                Console.WriteLine(_wizard.name + " lost.");
                Console.WriteLine("Remaining players:");
                wizardsList.Remove(_wizard);
                namesList.Remove(_wizard.name);
                Program.showlist();
            }
            }
            
        }

        public void Meditate()
        {
            spellslots = 2;
            Console.WriteLine(name + " is eating to regain Spellslots.");
        }

        public void Regen()
        {
            int _regen;
            if(health < 8)
            { _regen = 2;}
            else
            {_regen = 10 - health;}
            health += _regen;
            Console.WriteLine(name + " is sleeping and regains " + _regen + " health.");
        }
    }
}
