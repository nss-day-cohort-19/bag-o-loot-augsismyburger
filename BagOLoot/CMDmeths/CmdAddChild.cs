using System;
using System.Collections.Generic;
using System.Linq;

namespace BagOLoot
{
    public class AddChild
    {
        public void CmdAddChild(ChildRegister registry)
        {
            var children = registry.GetChildren();
            Console.Clear();
            Console.WriteLine ("Enter child name");
            Console.Write ("> ");
            string childName = Console.ReadLine();

            if (children.ContainsValue(childName))
            {
                Console.Beep();
                Console.WriteLine("Names been taken, try again");
        
            }else
            {
                bool childId = registry.AddChild(childName);
                Console.WriteLine(childId);
            
            }
        }
    }
}