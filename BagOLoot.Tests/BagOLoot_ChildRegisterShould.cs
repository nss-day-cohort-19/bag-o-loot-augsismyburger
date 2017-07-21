using System;
using System.Collections.Generic;
using Xunit;

namespace BagOLoot.Tests
{
    public class ChildRegisterShould
    {
        private readonly ChildRegister _register;


        public ChildRegisterShould()
        {
            _register = new ChildRegister();
        }

        [Theory]
        [InlineData("Sarah")]
        [InlineData("Jamal")]
        [InlineData("Kelly")]
        public void ShouldAddChildren(string child)
        {
            var result = _register.AddChild(child);
            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnListOfChildren()
        {
            var result = _register.GetChildren();
            Assert.IsType<Dictionary<int, string>>(result);
        }

        [Fact]
        public void ShouldDeliverChildToys()
        {

            var result = _register.DeliverChildToys(1);
            Assert.Equal(result, "Toys have been delivered");
        }
    }
}
