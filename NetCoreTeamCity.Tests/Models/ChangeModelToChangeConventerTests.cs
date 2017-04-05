using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using NetCoreTeamCity.Models;
using NUnit.Framework;

namespace NetCoreTeamCity.Tests.Models
{
    [TestFixture]
    public class ChangeModelToChangeConventerTests
    {
        [Test]
        public void ChangeModel_Convert_AllPropertiesPopulated()
        {
            // Arrange
            var changeModel = new ChangeModel()
            {
                Id = 1,
                Version = "123",
                Username = "Test",
                Date = new DateTime(2017, 1, 1),
                Href = "test",
                WebUrl = "test",
                Comment = "test",
                User = new User(),
                Files = new Files() { File = new List<File>() { new File() { RelativeFile = "something"} } },
                VcsRootInstance = new VcsRootInstance()
            };

            // Act
            var change = changeModel.Convert();

            // Assert
            foreach (var propertyInfo in change.GetType().GetProperties())
            {
                var defaultValue = GetDefault(propertyInfo.PropertyType);
                var actualValue = propertyInfo.GetValue(change);
                actualValue.Should().NotBe(defaultValue, $"because ChangeModel::{propertyInfo.Name} should have been mapped property from ChangeModel to Change in Conventer");
            }
        }

        private object GetDefault(Type t)
        {
            return this.GetType().GetMethod("GetDefaultGeneric").MakeGenericMethod(t).Invoke(this, null);
        }

        
        public T GetDefaultGeneric<T>()
        {
            return default(T);
        }
    }
}
