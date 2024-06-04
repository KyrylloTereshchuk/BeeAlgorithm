using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeSpace

{
    public class MenuDictionary
    {
        Command _Commands = new();

        public Dictionary<Enums.CommandsNames, Action> MenuCreate()
        {
            var menuChoices = new Dictionary<Enums.CommandsNames, Action>()
               {
                    {Enums.CommandsNames.Exit, () => _Commands.Exit()},
                    {Enums.CommandsNames.GreedyAlgorithm, () => _Commands.Command1()},
                    {Enums.CommandsNames.BeeAlgorithm, () => _Commands.Command2()},
                    {Enums.CommandsNames.PrintData, () => _Commands.Command3()},
                    {Enums.CommandsNames.GenerateData, () => _Commands.Command4()},
                    {Enums.CommandsNames.DataFromConsole, () => _Commands.Command5()},
                    {Enums.CommandsNames.DataFromFile, () => _Commands.Command6()},
                    {Enums.CommandsNames.IterationsResearch, () => _Commands.Command7()},
                    {Enums.CommandsNames.ParametrResearch, () => _Commands.Command8()},
                    {Enums.CommandsNames.Compasion, () => _Commands.Command9()},

               };
            return menuChoices;
        }
    }
}
