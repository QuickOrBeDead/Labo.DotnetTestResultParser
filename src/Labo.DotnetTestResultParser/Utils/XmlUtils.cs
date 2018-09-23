namespace Labo.DotnetTestResultParser.Utils
{
    using System;
    using System.Xml.Linq;

    using Labo.DotnetTestResultParser.Exceptions;

    /// <summary>
    /// The xml utils class.
    /// </summary>
    public static class XmlUtils
    {
        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="xElement">The x element.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="TestResultParserException"></exception>
        public static string GetAttributeValue(XElement xElement, string name)
        {
            if (xElement == null) throw new ArgumentNullException(nameof(xElement));
            if (name == null) throw new ArgumentNullException(nameof(name));

            XAttribute attribute = xElement.Attribute(name);
            if (attribute == null)
            {
                throw new TestResultParserException($"Attribute '{name}' could not be found for the xml element '{xElement.Name}'.");
            }

            return attribute.Value;
        }
    }
}
