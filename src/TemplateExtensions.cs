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
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// This class contains extensions to support the template engine.
    /// </summary>
    public static class TemplateExtensions
    {
        /// <summary>
        /// This method is used to replace the template tokens with the specified token values.
        /// </summary>
        /// <param name="content">Contains the content within to find and replace tokens.</param>
        /// <param name="tokensList">Contains the tokens used for replacement.</param>
        /// <returns>Returns the new content with found tokens replaced.</returns>
        public static string ReplaceTokens(string content, Dictionary<string, string> tokenValueDictionary)
        {
            string result = content;

            if (tokenValueDictionary != null)
            {
                if (!tokenValueDictionary.ContainsKey(CommonTokens.DateTime))
                {
                    tokenValueDictionary.Add(CommonTokens.DateTime, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
                }

                if (!tokenValueDictionary.ContainsKey(CommonTokens.Date))
                {
                    tokenValueDictionary.Add(CommonTokens.Date, DateTime.UtcNow.ToShortDateString());
                }

                if (!tokenValueDictionary.ContainsKey(CommonTokens.Time))
                {
                    tokenValueDictionary.Add(CommonTokens.Time, DateTime.UtcNow.ToShortTimeString());
                }

                result = tokenValueDictionary.Aggregate(result, (current, item) => current.Replace("$" + item.Key.ToUpperInvariant() + "$", item.Value));
            }

            return result;
        }

    }
}