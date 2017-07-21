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
        [Theory]
        [InlineData(1, "Rock")]
        [InlineData(2, "Cape")]
        [InlineData(3, "To be pretty")]
        public void AddToyToChildBag(int ChildId, string toyname)
        {
            // string toyname = "Fucking Firetruck";
            // int ChildId = 715;
            bool result = _toyBag.AddToyToBag(ChildId, toyname);
            Dictionary<int, string> toys = _toyBag.GetChildToys(ChildId);

            Assert.True(result);
            Assert.IsType<Dictionary<int, string>>(toys);
        }

        [Fact]
        public void RevokeToyShould()
        {
            int ChildId = 1;
            int ToyId = 2;
            bool result = _toyBag.RevokeToyFromBag(ToyId);
            Dictionary<int, string> toys = _toyBag.GetChildToys(ChildId);

            Assert.True(result);
            Assert.IsType<Dictionary<int, string>>(toys);
        }
        [Fact]
        public void ListChildrenWithToysShould()
        {
            var goodChildren = _toyBag.ListChildrenWithToys();
        //Then
            Assert.IsType<List<(string, string)>>(goodChildren);
        }
    }
}