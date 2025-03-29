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
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// This class represents the options for the <see cref="FileTemplateProvider"/>.
    /// </summary>
    public class FileTemplateProviderOptions
    {
        /// <summary>
        /// Gets the default culture information based on the default language code provided during initialization.
        /// </summary>
        public CultureInfo DefaultCultureInfo { get; set; } = new CultureInfo(TemplateConstants.DefaultLanguageCode);

        /// <summary>
        /// Gets or sets the template root path.
        /// </summary>
        public string TemplatePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets the template directory where the template files are stored.
        /// </summary>
        public DirectoryInfo TemplateDirectory => new DirectoryInfo(this.TemplatePath);
    }
}
