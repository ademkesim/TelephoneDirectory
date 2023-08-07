using System;
using System.Collections.Generic;
using DirectoryService.Api.Core.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Services.UnitTest
{

    [TestClass]
    public class ValidateEnumAttributeTests
    {
        private class SampleModel
        {
            public SampleEnum EnumProperty { get; set; }
        }

        private enum SampleEnum
        {
            Value1,
            Value2,
            Value3
        }

        private class TestController : Controller
        {
            [ValidateEnum(typeof(SampleEnum))]
            public IActionResult TestAction([FromBody] SampleModel model)
            {
                return Ok();
            }
        }

        [TestMethod]
        public void EnumValueIsValid_ReturnsOkResult()
        {
            // Arrange
            var attribute = new ValidateEnumAttribute(typeof(SampleEnum));
            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor());
            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object> { ["model"] = new SampleModel { EnumProperty = SampleEnum.Value1 } },
                new TestController());

            // Act
            attribute.OnActionExecuting(context);

            // Assert
            Assert.IsNull(context.Result);
        }

        [TestMethod]
        public void EnumValueIsInvalid_ReturnsBadRequest()
        {
            // Arrange
            var attribute = new ValidateEnumAttribute(typeof(SampleEnum));
            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor());
            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object> { ["model"] = new SampleModel { EnumProperty = (SampleEnum)99 } },
                new TestController());

            // Act
            attribute.OnActionExecuting(context);

            // Assert
            Assert.IsNotNull(context.Result);
            Assert.IsInstanceOfType(context.Result, typeof(BadRequestObjectResult));
            var resultValue = (BadRequestObjectResult)context.Result;
            Assert.AreEqual("Invalid value for model.EnumProperty.", resultValue.Value);
        }
    }

}

