using System;
using System.Collections.Generic;
using System.Linq;

namespace BagOLoot
{
    public class DeliverToys
    {
        public void CmdDeliverChildToys(ChildRegister registry, int choice)
        {
            Dictionary<int, string> children = registry.GetChildren();
            List<Child> ChildList = new List<Child>();
            var deliveredList = registry.GetDeleveryStatus();
            Console.WriteLine("*******************");
            Console.WriteLine("Select Which Child's Toys Should be Delivered");
            Console.WriteLine("");
            int i = 1;
            foreach(KeyValuePair <int, string> kvp in children)
            {
                if (deliveredList.ContainsKey(kvp.Key) && deliveredList[kvp.Key] == false)
                {
                    ChildList.Add(new Child(i, kvp.Key, kvp.Value));
                    Console.WriteLine($"{i}. {kvp.Value}");
                    i++;
                }else
                {
                    Console.WriteLine($"{kvp.Value}'s Toys have already been delivered");
                }
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
            registry.DeliverChildToys(selectedChildId);
            Console.WriteLine("Toys have been delivered");
        }
    }
}