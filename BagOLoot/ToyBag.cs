using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace BagOLoot
{
    public class ToyBag
    {
        // private string _newToy;

        public bool AddToyToBag(int ChildId, string toyName)
        {
            return true;
        }
        public List<int> GetChildToys(int ChildId)
        {
            return new List<int>() {7, 9, 13};
        }
        public bool RevokeToyFromBag(int ChildId, int ToyId)
        {
            return true;
        }
        public List<string> ListChildrenWithToys()
        {
            return new List<string>() {"Gary", "Sarah"};
        }
    }
}