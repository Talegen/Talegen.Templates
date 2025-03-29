/*
 * Talegen Fancy Template Library
 * (c) Copyright Talegen, LLC.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/
namespace Talegen.Templates
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains constant dictionaries for content type lookups.
    /// </summary>
    public static class TemplateConstants
    {
        public const string DefaultThemeName = "default";
        public const string DefaultLanguageCode = "en";
        public const string TextContentType = "text/plain";
        public const string HtmlContentType = "text/html";
        public const string JsonContentType = "application/json";
        public const string XmlContentType = "application/xml";
        public const string MarkdownContentType = "text/markdown";
        public const string OtherContentType = "other";
        public const string TextExtension = ".txt";
        public const string HtmlExtension = ".html";
        public const string JsonExtension = ".json";
        public const string XmlExtension = ".xml";
        public const string MarkdownExtension = ".md";

        /// <summary>
        /// A static dictionary to map content type strings to the corresponding TemplateContentType enum values.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, TemplateContentType> ContentTypeTemplateTypes = new Dictionary<string, TemplateContentType>
        {
            { TextContentType, TemplateContentType.Text },
            { HtmlContentType, TemplateContentType.Html },
            { JsonContentType, TemplateContentType.JSON },
            { XmlContentType, TemplateContentType.XML },
            { MarkdownContentType, TemplateContentType.Markdown },
            { OtherContentType, TemplateContentType.Other }
        };

        /// <summary>
        /// A static dictionary to map template content types to a file extension.
        /// </summary>
        public static readonly IReadOnlyDictionary<TemplateContentType, string> TemplateTypeExtensions = new Dictionary<TemplateContentType, string>
        {
            { TemplateContentType.Text, TextExtension },
            { TemplateContentType.Html, HtmlExtension },
            { TemplateContentType.JSON, JsonExtension },
            { TemplateContentType.XML, XmlExtension },
            { TemplateContentType.Markdown, MarkdownExtension },
            { TemplateContentType.Other, TextExtension } // Default to .txt for other types
        };

        /// <summary>
        /// A static dictionary to map content type strings to their corresponding file extensions.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, string> ContentTypeExtensions = new Dictionary<string, string>
        {
            { TextContentType, TextExtension },
            { HtmlContentType, HtmlExtension },
            { JsonContentType, JsonExtension },
            { XmlContentType, XmlExtension },
            { MarkdownContentType, MarkdownExtension },
            { OtherContentType, TextExtension } // Default to .txt for other types
        };

        /// <summary>
        /// A static dictionary to map file extensions to their corresponding content types.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, string> ExtensionContentTypes = new Dictionary<string, string>
        {
            { TextExtension, TextContentType },
            { HtmlExtension, HtmlContentType },
            { JsonExtension, JsonContentType },
            { XmlExtension, XmlContentType },
            { MarkdownExtension, MarkdownContentType }
        };


        /// <summary>
        /// A static dictionary to map template file extension to template content type.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, TemplateContentType> ExtensionTemplateType = new Dictionary<string, TemplateContentType>
        {
            { TextExtension, TemplateContentType.Text},
            { HtmlExtension, TemplateContentType.Html },
            { JsonExtension, TemplateContentType.JSON },
            { XmlExtension, TemplateContentType.XML },
            { MarkdownExtension, TemplateContentType.Markdown }
        };

    }
}
