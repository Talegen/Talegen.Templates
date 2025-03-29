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
    /// This interface defines a contract for providing templates based on a unique key and language code.
    /// </summary>
    public interface ITemplateProvider
    {
        /// <summary>
        /// This method is used to return template contents with token values replaced with values provided.
        /// </summary>
        /// <param name="templateKey">The unique key for the template.</param>
        /// <param name="tokenValueDictionary">Contains a dictionary of token values.</param>
        /// <param name="themeName">Contains the optional theme name for a template to be encased within.</param>
        /// <param name="contentType">Contains the optional content type to define the type of theme content to return.</param>
        /// <param name="languageCode">The language code for the template.</param>
        /// <returns>Returns the message with token values replaced.</returns>
        public string GetMessage(string templateKey, Dictionary<string, string> tokenValueDictionary, string themeName = "default", TemplateContentType contentType = TemplateContentType.Text, string? languageCode = default);

        /// <summary>
        /// Gets the template by its unique key and language code.
        /// </summary>
        /// <param name="templateKey">The unique key for the template.</param>
        /// <param name="themeName">Contains the optional theme name for a template to be encased within.</param>
        /// <param name="contentType">Contains the optional content type to define the type of theme content to return.</param>
        /// <param name="languageCode">The language code for the template.</param>
        /// <returns>The template content as a string, or null if not found.</returns>
        string GetTemplate(string templateKey, string themeName = "default", TemplateContentType contentType = TemplateContentType.Text, string? languageCode = default);


    }
}
