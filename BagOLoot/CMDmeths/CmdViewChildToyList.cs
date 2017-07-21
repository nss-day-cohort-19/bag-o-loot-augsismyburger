using System;
using System.Collections.Generic;
using System.Linq;

namespace BagOLoot
{
    public class ViewToys
    {
        public void CmdViewChildToys(ChildRegister registry, ToyBag toyBag, int choice)
        {
            Dictionary<int, string> children = registry.GetChildren();
            List<Child> ChildList = new List<Child>();
            int i = 1;
            Console.WriteLine("*******************");
            Console.WriteLine("Select Which Child To View Toy List");
            foreach(KeyValuePair <int, string> kvp in children)
            {
                ChildList.Add(new Child(i, kvp.Key, kvp.Value));
                Console.WriteLine($"{i}. {kvp.Value}");
                i++;
            }
            Console.WriteLine ("");
            Console.Write ("> ");
            Int32.TryParse (Console.ReadLine(), out choice);
            int selectedChildId = -1;
            foreach(Child child in ChildList)
            {
                if(child.iter == choice)
                {
                    selectedChildId = child.childId;
                }
            }
            Dictionary<int, string> toys = toyBag.GetChildToys(selectedChildId);
            Console.WriteLine("*******************");
            i = 1;
            Console.WriteLine($"{children[selectedChildId]}'s Toy List");
            foreach(KeyValuePair <int, string> kvp in toys)
            {
                Console.WriteLine($"    {i}. {kvp.Value}");
                i++;
            }
            Console.WriteLine("*******************");
        }
    }
}