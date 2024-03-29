using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Etch UK Ltd.",
    Category = "Design",
    Dependencies = new[] { "OrchardCore.ContentFields", "OrchardCore.CustomSettings" },
    Description = "Customise visual appearance of site.",
    Name = "Theme Settings",
    Version = "$(VersionNumber)",
    Website = "https://etchuk.com"
)]