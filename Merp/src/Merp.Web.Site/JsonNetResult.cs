﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Merp.Web.Mvc
{
    /// <summary>
    /// Represents a class that is used to send JSON-formatted content to the response.
    /// </summary>
    public class JsonNetResult : ActionResult
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MvcMate.Web.Mvc.JsonpResult class.
        /// </summary>
        /// <param name="data">The object to be serialized.</param>
        public JsonNetResult(object data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Data = data;
        }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the System.Web.Mvc.ActionResult class.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        /// <exception cref="System.ArgumentNullException">The context parameter is null.</exception>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var serializer = new JsonSerializer();
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, this.Data);
                var json = writer.ToString();
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.Write(json);
            }   
        }

        /// <summary>
        /// Creates a System.Web.Mvc.JsonResult object that serializes the specified object to JavaScript Object Notation (JSON).
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        /// <returns>
        /// The JSONP result object that serializes the specified object to JSONP format. 
        /// The result object that is prepared by this method is written to the response 
        /// by the ASP.NET MVC framework when the object is executed.
        /// </returns>
        public static JsonNetResult JsonNet(object data)
        {
            return new JsonNetResult(data);
        }
    }
}
