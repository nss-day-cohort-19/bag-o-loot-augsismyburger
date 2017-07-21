using System;
using System.Collections.Generic;
using System.Linq;

namespace BagOLoot
{
    public class AddToyToChild
    {
        public void CmdAddToyToChild(ChildRegister registry, ToyBag toyBag, int choice)
        {
            int i = 1;
            List<Child> ChildList = new List<Child>();
            var deliveredList = registry.GetDeleveryStatus();
            Dictionary<int, string> children = registry.GetChildren();
            Console.WriteLine("*******************");
            Console.WriteLine("Select Which Child To Give A Toy To");
            foreach(KeyValuePair <int, string> kvp in children)
            {
                if (deliveredList.ContainsKey(kvp.Key) && deliveredList[kvp.Key] == false)
                {
                    ChildList.Add(new Child(i, kvp.Key, kvp.Value));
                    Console.WriteLine($"{i}. {kvp.Value}");
                    i++;
                }
            }
            Console.WriteLine ("");
            Console.Write ("> ");
            Int32.TryParse (Console.ReadLine(), out choice);
            Console.WriteLine("*******************");
            Console.WriteLine("Enter A Toy To Give");
            Console.Write ("> ");
            string toy = Console.ReadLine();
            int selectedChildId = -1;
            foreach(Child child in ChildList)
            {
                if(child.iter == choice)
                {
                    selectedChildId = child.childId;
                }
            }
            toyBag.AddToyToBag(selectedChildId, toy);
            Console.WriteLine("Nice, very generous of you.");
        }
    }
}