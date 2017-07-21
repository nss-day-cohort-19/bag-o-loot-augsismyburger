using System;
using System.Collections.Generic;
using System.Linq;

namespace BagOLoot
{
    public class RevokeToy
    {
        public void CmdRevokeToy(ChildRegister registry, ToyBag toyBag, int choice)
        {
            int i = 1;
            List<Child>ChildList = new List<Child>();
            List<Child> ToyList = new List<Child>();
            Dictionary<int, string> children = registry.GetChildren();
            Console.WriteLine("*******************");
            Console.WriteLine("Select Which Child To Revoke A Toy From");
            Console.WriteLine("");
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
            var toys = toyBag.GetChildToys(selectedChildId);
            int selectedToyId = -1;
            i = 1;
            foreach(KeyValuePair <int, string> kvp in toys)
            {
                ToyList.Add(new Child(i, kvp.Key, kvp.Value));
                Console.WriteLine("*******************");
                Console.WriteLine("Select Which Toy To Revoke");
                Console.WriteLine($"{i}. {kvp.Value}");
            }
            Int32.TryParse (Console.ReadLine(), out choice);
            foreach(Child t in ToyList)
            {
                if(t.iter == choice)
                {
                    selectedToyId = t.childId;
                }
            }
            toyBag.RevokeToyFromBag(selectedToyId
            );
            Console.WriteLine("Nice Job Scrooge");
        }
    }
}