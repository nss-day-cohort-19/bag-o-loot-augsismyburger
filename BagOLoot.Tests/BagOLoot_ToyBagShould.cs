using System;
using System.Collections.Generic;
using Xunit;

namespace BagOLoot.Tests
{
    public class ToyBagShould
    {
        private ToyBag _toyBag;
        public ToyBagShould()
        {
            _toyBag = new ToyBag();
        }
        [Fact]
        public void AddToyToChildBag()
        {
            string toyname = "Fucking Firetruck";
            int ChildId = 715;
            bool result = _toyBag.AddToyToBag(ChildId, toyname);
            List<int> toys = _toyBag.GetChildToys(ChildId);

            Assert.True(result);
            Assert.IsType<List<int>>(toys);
        }
        [Fact]
        public void RevokeToyShould()
        {
            int ChildId = 217;
            int ToyId = 77;
            bool result = _toyBag.RevokeToyFromBag(ChildId, ToyId);
            List<int> toys = _toyBag.GetChildToys(ChildId);

            Assert.True(result);
            Assert.IsType<List<int>>(toys);
        }
        [Fact]
        public void ListChildrenWithToysShould()
        {
        //Given

        //When
            List<string> goodChildren = _toyBag.ListChildrenWithToys();
        //Then
            Assert.IsType<List<string>>(goodChildren);
        }
    }
}