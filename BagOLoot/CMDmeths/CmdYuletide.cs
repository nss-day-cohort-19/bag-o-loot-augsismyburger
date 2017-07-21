using System;
using System.Collections.Generic;
using System.Linq;

namespace BagOLoot
{
    public class YuleTideReport
    {
        public void CmdGetYuleTideReport(ChildRegister registry, ToyBag toyBag, int choice)
        {
            Dictionary<int, string> children = registry.GetChildren();
            Console.WriteLine("***YuleTide Report***");
            foreach(KeyValuePair <int, string> kvp in children)
            {
                Console.WriteLine($"{kvp.Value}");
                Dictionary<int, string> toys = toyBag.GetChildToys(kvp.Key);
                int i = 1;
                foreach(KeyValuePair <int, string> tvp in toys)
                {
                    if(tvp.Value != null)
                    {
                        Console.WriteLine($"    {i}. {tvp.Value}");
                        i++;
                    }else
                    {
                        Console.WriteLine($"     No Toys, Better Luck Next Year");
                    }
                }
            }
        }
    }
}