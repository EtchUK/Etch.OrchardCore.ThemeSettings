# Etch.OrchardCore.ThemeSettings

Module for [Orchard Core](https://github.com/OrchardCMS/OrchardCore) for overriding CSS variables used within a theme.

## Build Status

[![Build Status](https://secure.travis-ci.org/etchuk/Etch.OrchardCore.ThemeSettings.png?branch=master)](http://travis-ci.org/etchuk/Etch.OrchardCore.ThemeSettings) [![NuGet](https://img.shields.io/nuget/v/Etch.OrchardCore.ThemeSettings.svg)](https://www.nuget.org/packages/Etch.OrchardCore.ThemeSettings)

## Orchard Core Reference

This module is referencing a stable build of Orchard Core ([`1.3.0`](https://www.nuget.org/packages/OrchardCore.Module.Targets/1.3.0)).

## Installing

This module is available on [NuGet](https://www.nuget.org/packages/Etch.OrchardCore.ThemeSettings). Add a reference to your Orchard Core web project via the NuGet package manager. Search for "Etch.OrchardCore.ThemeSettings", ensuring include prereleases is checked.

Alternatively you can [download the source](https://github.com/etchuk/Etch.OrchardCore.ThemeSettings/archive/master.zip) or clone the repository to your local machine. Add the project to your solution that contains an Orchard Core project and add a reference to Etch.OrchardCore.ThemeSettings.

## Usage

Firstly your theme must be using CSS variables (see our [theme boilerplate](https://github.com/EtchUK/Etch.OrchardCore.ThemeBoilerplate) for an example of this).

Enable "Theme Settings" feature within the admin dashboard. This will create a new "Theme Settings" content type that utilises custom settings and can be accessed in the admin menu under "Settings".

### Add new properties

By default the theme settings content type will only contain a field for defining the primary colour. To customise additional CSS variables within the theme, add a new [colour field](https://github.com/EtchUK/Etch.OrchardCore.Fields#colour) or text field to the theme settings ensuring that the technical name of the field matches the CSS variable name.

## Demo

![Screen recording of module functionality](https://github.com/etchuk/Etch.OrchardCore.ThemeSettings/raw/master/docs/demo-theme-settings.gif)
