﻿using Etch.OrchardCore.Fields.Colour.Fields;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.Admin;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.Entities;
using OrchardCore.ResourceManagement;
using OrchardCore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etch.OrchardCore.ThemeSettings.Filters
{
    public class ThemeSettingsFilter : IAsyncResultFilter
    {
        private const string CustomSettingContentTypeName = "ThemeSettings";

        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IResourceManager _resourceManager;
        private readonly ISiteService _siteService;

        private readonly string[] _compatibleFields;
        private HtmlString _stylesCache;

        public ThemeSettingsFilter(
            IContentDefinitionManager contentDefinitionManager,
            IResourceManager resourceManager,
            ISiteService siteService)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _resourceManager = resourceManager;
            _siteService = siteService;

            _compatibleFields = new string[]
            {
                typeof(ColourField).Name,
                typeof(TextField).Name
            };
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var siteSettings = await _siteService.GetSiteSettingsAsync();
            var themeSettings = siteSettings.As<ContentItem>(CustomSettingContentTypeName);

            if (themeSettings == null)
            {
                await next.Invoke();
                return;
            }

            var themeSettingsContentPart = themeSettings.ContentItem.Get<ContentPart>(CustomSettingContentTypeName);
            var themeSettingFields = _contentDefinitionManager.GetTypeDefinition(CustomSettingContentTypeName)?.Parts.SingleOrDefault(x => x.Name == CustomSettingContentTypeName)?.PartDefinition.Fields;

            if (themeSettingsContentPart == null || themeSettingFields == null)
            {
                await next.Invoke();
                return;
            }

            // Should only run on the front-end for a full view
            if ((context.Result is ViewResult || context.Result is PageResult) && !AdminAttribute.IsApplied(context.HttpContext))
            {
                var cssVariables = new Dictionary<string, string>();

                foreach (var field in themeSettingFields.Where(x => _compatibleFields.Contains(x.FieldDefinition.Name)))
                {
                    var value = GetFieldValue(themeSettingsContentPart, field);

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        cssVariables.Add(FormatName(field.Name), value);
                    }
                }

                if (_stylesCache == null && cssVariables.Any())
                {
                    _stylesCache = new HtmlString($"<style>:root {{ {Environment.NewLine} {FormatCssVariables(cssVariables)} {Environment.NewLine} }}</style>");
                }

                if (_stylesCache != null)
                {
                    _resourceManager.RegisterStyle(_stylesCache);
                }
            }

            await next.Invoke();
        }

        private string FormatCssVariables(IDictionary<string, string> cssVariables)
        {
            var builder = new StringBuilder();

            foreach (var cssVariable in cssVariables)
            {
                builder.Append($"--{cssVariable.Key}: {cssVariable.Value};{Environment.NewLine}");
            }

            return builder.ToString();
        }

        private string FormatName(string name)
        {
            return char.ToLower(name[0]) + name.Substring(1);
        }

        private string GetFieldValue(ContentPart themeSettingsContentPart, ContentPartFieldDefinition field)
        {
            if (field.FieldDefinition.Name == typeof(TextField).Name)
            {
                return themeSettingsContentPart.Get<TextField>(field.Name)?.Text;
            }

            if (field.FieldDefinition.Name == typeof(ColourField).Name)
            {
                return themeSettingsContentPart.Get<ColourField>(field.Name)?.Value;
            }

            return string.Empty;
        }
    }
}
