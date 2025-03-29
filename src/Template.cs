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
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Contains an enumerated list of template types.
    /// </summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum TemplateContentType
    {
        /// <summary>
        /// Message Content Type is Text
        /// </summary>
        Text,

        /// <summary>
        /// Message Content Type is HTML
        /// </summary>
        Html,

        /// <summary>
        /// Message Content Type is JSON
        /// </summary>
        JSON,

        /// <summary>
        /// Message Content Type is XML
        /// </summary>
        XML,

        /// <summary>
        /// Message Content Type is Markdown
        /// </summary>
        Markdown,

        /// <summary>
        /// Message Content Type is Other
        /// </summary>
        Other
    }

    /// <summary>
    /// This class represents a content template within the application.
    /// </summary>
    public class Template
    {
        /// <summary>
        /// Gets or sets the related language code for the template.
        /// </summary>
        public string LanguageCode { get; set; } = TemplateConstants.DefaultLanguageCode; // Default to English (en) if not specified

        /// <summary>
        /// Gets or sets the content type of the template. This indicates the type of content being used.
        /// </summary>
        public string ContentType { get; set; } = TemplateConstants.TextContentType;

        /// <summary>
        /// Gets the template content type based on the ContentType property.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public TemplateContentType TemplateType => TemplateConstants.ContentTypeTemplateTypes.ContainsKey(this.ContentType) ? TemplateConstants.ContentTypeTemplateTypes[this.ContentType] : TemplateContentType.Other;

        /// <summary>
        /// Gets or sets the image binary data.
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}