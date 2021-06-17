namespace EdBindings.Model.BindingsRaw.Tests
{
    using System;

    using Xunit;

    public class BindFileTests
    {
        [Fact]
        public void TestProcessingBindingFile()
        {
            var path = @".\Custom.4.0.binds";

            var bindingFile = BindingFile.Open(path);

            Assert.Equal("en-US", bindingFile.KeyboardLayout);

            Assert.Equal(1319, bindingFile.Bindings.Count);

        }
    }
}
