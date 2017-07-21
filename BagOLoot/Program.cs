using System;
using System.Collections.Generic;
using System.Linq;

namespace BagOLoot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = new DatabaseInterface();
            db.CheckForChildTable();
            db.CheckForToyTable();
            AddChild add = new AddChild();
            AddToyToChild addToy = new AddToyToChild();
            RevokeToy revokeToy = new RevokeToy();
            ViewToys viewToys = new ViewToys();
            DeliverToys deliverToys = new DeliverToys();
            YuleTideReport yuleTideReport = new YuleTideReport();
            ChildRegister registry = new ChildRegister();
            ToyBag toyBag = new ToyBag();
            CmdLineInterface menu = new CmdLineInterface();
            int choice;

			
            do
            {
                choice = menu.writeMenu();
                switch (choice)
                {
                    case 1:
                        add.CmdAddChild(registry);
                        break;
                    case 2:
                        addToy.CmdAddToyToChild(registry, toyBag, choice);
                        break;
                    case 3:
                        revokeToy.CmdRevokeToy(registry, toyBag, choice);
                        break;
                    case 4:
                        viewToys.CmdViewChildToys(registry, toyBag, choice);
                        break;
                    case 5:
                        deliverToys.CmdDeliverChildToys(registry, choice);
                        break;
                    case 6:
                        yuleTideReport.CmdGetYuleTideReport(registry, toyBag, choice);
                        break;
                    case 7:
                        break;
                    default:
                        Console.Beep();
                        Console.WriteLine("Invalid Key Entered");
                        break;
                }
            } while (choice != 7);
        }
    }
}
