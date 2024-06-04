using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeSpace
{
    public class ExecutionProcess : IExecutionProcess
    {
           MenuDictionary _dictionary = new();

        public void Process()
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Menu\n");
            foreach (Enums.CommandsNames queryName in Enum.GetValues(typeof(Enums.CommandsNames)))
            {
                Console.WriteLine($"{(int)queryName}." + queryName);
            }

               while (true)
              {
                  Console.ForegroundColor = ConsoleColor.Yellow;
                  Console.Write("\nEnter query command or 0 to exit: ");
                  Console.ResetColor();

                  string? input = Console.ReadLine();


                  var menuChoices = _dictionary.MenuCreate();

                    if (Enum.TryParse(input, out Enums.CommandsNames choice) && menuChoices.TryGetValue(choice, out var selected))
                    {
                       selected();
                    }
                      
                    else
                    {
                        Console.WriteLine("Invalid input!");
                    }

               }

        }

    }

}
