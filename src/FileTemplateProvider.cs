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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// This class implements a local file template provider.
    /// </summary>
    public class FileTemplateProvider : ITemplateProvider
    {
        /// <summary>
        /// Private field to hold the options for the template provider.
        /// </summary>
        private readonly FileTemplateProviderOptions options;

        /// <summary>
        /// A static dictionary to cache theme templates for template bodies.
        /// </summary>
        private static readonly ConcurrentDictionary<string, string> themesCache = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// A static dictionary to cache templates for performance optimization.
        /// </summary>
        private static readonly ConcurrentDictionary<string, Template> templatesCache = new ConcurrentDictionary<string, Template>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTemplateProvider"/> class.
        /// </summary>
        /// <param name="options">Contains the provider options.</param>
        /// <exception cref="ArgumentNullException">Thrown if the options parameter is null.</exception>
        public FileTemplateProvider(IOptions<FileTemplateProviderOptions> options)
        {
            // Initialize the provider with options if needed
            this.options = options.Value ?? throw new ArgumentNullException(nameof(options));
            Initialize(); // Call the Initialize method to perform any setup tasks
        }

        /// <summary>
        /// This method is used to return template contents with token values replaced with values provided.
        /// </summary>
        /// <param name="templateKey">The unique key for the template.</param>
        /// <param name="tokenValueDictionary">Contains a dictionary of token values.</param>
        /// <param name="themeName">Contains the optional theme name for a template to be encased within.</param>
        /// <param name="contentType">Contains the optional content type to define the type of theme content to return.</param>
        /// <param name="languageCode">The language code for the template.</param>
        /// <returns>Returns the message with token values replaced.</returns>
        public string GetMessage(string templateKey, Dictionary<string, string> tokenValueDictionary, string themeName = TemplateConstants.DefaultThemeName, TemplateContentType contentType = TemplateContentType.Text, string? languageCode = default)
        {
            string templateContents = this.GetTemplate(templateKey, themeName, contentType, languageCode);
            return TemplateExtensions.ReplaceTokens(templateContents, tokenValueDictionary);
        }

        /// <summary>
        /// Gets the template by its unique key and language code.
        /// </summary>
        /// <param name="templateKey">The unique key for the template.</param>
        /// <param name="themeName">Contains the optional theme name for a template to be encased within.</param>
        /// <param name="contentType">Contains the optional content type to define the type of theme content to return.</param>
        /// <param name="languageCode">The language code for the template.</param>
        /// <returns>The template content as a string, or null if not found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the template key is not found.</exception>
        public string GetTemplate(string templateKey, string themeName = TemplateConstants.DefaultThemeName, TemplateContentType contentType = TemplateContentType.Text, string? languageCode = default)
        {
            string lookupLanguageCode = languageCode != null ? languageCode : this.options.DefaultCultureInfo.TwoLetterISOLanguageName;
            string lookupKey = $"{lookupLanguageCode}:{templateKey}:{contentType}";

            if (!templatesCache.ContainsKey(lookupKey))
            {
                throw new KeyNotFoundException(string.Format(Properties.Resources.TemplateKeyNotFoundErrorText, templateKey, lookupLanguageCode, contentType, lookupKey));
            }

            // pull the template body content
            Template template = templatesCache[lookupKey];

            // build the content body of the template using passed values
            string themeKey = themeName + TemplateConstants.TemplateTypeExtensions[contentType];
            string themeContent = themesCache.ContainsKey(themeKey) ? themesCache[themeKey] : string.Empty;
            
            return string.IsNullOrWhiteSpace(themeContent) ? 
                template.Content : 
                themeContent.Replace("$BODY$", template.Content);
        }

        /// <summary>
        /// Initializes the template provider
        /// </summary>
        private void Initialize()
        {
            // This method can be used to initialize any resources or perform setup tasks
            // For example, loading templates from files or setting up connections
            if (this.options.TemplateDirectory.Exists)
            {
                if (themesCache.IsEmpty)
                {
                    // check for a themes sub-folder in template directory
                    var themesDirectory = new DirectoryInfo(Path.Combine(this.options.TemplateDirectory.FullName, "themes"));

                    if (themesDirectory.Exists)
                    {
                        var themeFiles = themesDirectory.GetFiles("*.*");

                        foreach (var themeFile in themeFiles)
                        {
                            // add the theme to the cache using the name.
                            themesCache.TryAdd(themeFile.Name, System.IO.File.ReadAllText(themeFile.FullName));
                        }
                    }
                }

                if (templatesCache.IsEmpty)
                {

                    // find all sub-folders for each language code in the template directory
                    // Assuming each sub-folder is named after the language code (e.g., "en", "fr", etc.)
                    // This will allow us to load templates for different languages if needed
                    var allCultures = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures);
                    var directories = this.options.TemplateDirectory.GetDirectories();

                    // Iterate through each directory to find language codes
                    foreach (var directory in directories)
                    {
                        string languageCode = directory.Name; // Use the directory name as the language code

                        // if directory name is not 2 letters long, or not a valid format language-culture code (en-US, fr-FR, etc.), skip it
                        if (languageCode.Length != 2 && !allCultures.Any(c => c.Name.Equals(languageCode, StringComparison.OrdinalIgnoreCase)))
                        {
                            continue; // Skip invalid language codes
                        }

                        // for each extension defined in types TemplateConfig.ExtensionContentTypes, load the templates from the directory
                        // This allows us to load templates for different content types (e.g., text, HTML, JSON, etc.)
                        // Iterate through each file in the directory to load templates for this language code
                        foreach (var extension in TemplateConstants.ExtensionContentTypes.Keys)
                        {
                            // Check if the file extension is valid for the current content type
                            var templateFilesWithExtension = directory.GetFiles($"*.{extension}"); // Get files with the specific extension
                            foreach (var file in templateFilesWithExtension)
                            {
                                string key = System.IO.Path.GetFileNameWithoutExtension(file.Name);
                                string content = System.IO.File.ReadAllText(file.FullName);
                                string contentType = TemplateConstants.ExtensionContentTypes[extension]; // Set the content type based on the extension 
                                TemplateContentType templateType = TemplateConstants.ExtensionTemplateType[extension];

                                // Create a new Template object and add it to the cache
                                var template = new Template
                                {
                                    LanguageCode = languageCode,
                                    ContentType = contentType,
                                    Content = content
                                };

                                // Add to cache with a unique key
                                templatesCache.TryAdd($"{languageCode}:{key}:{templateType}", template);
                            }
                        }
                    }
                }
            }
            else
            {
                throw new DirectoryNotFoundException(string.Format(Properties.Resources.TemplatePathNotFoundErrorText, this.options.TemplatePath));
            }
        }
    }
}
