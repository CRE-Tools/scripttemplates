> [!TIP]
> <a href="/Documentation~/UserManual.pdf">View this manual as PDF</a>

<h1 align="center">Script Templates - User Manual</h1>
<p align="right">v2.2.0</p>

## Table of Contents
1. [Introduction](#introduction)
   - [Overview](#overview)
   - [Features](#features)
   - [Requirements](#requirements)
2. [Installation](#installation)
   - [Via Package Manager](#via-package-manager)
   - [Manual Installation](#manual-installation)
3. [Getting Started](#getting-started)
   - [Accessing Script Templates](#accessing-script-templates)
4. [Template Reference](#template-reference)
   - [Available Templates](#available-templates)
   - [Template Syntax](#template-syntax)
5. [Advanced Usage](#advanced-usage)
   - [MultiScriptCreator](#multiscriptcreator)
6. [Troubleshooting](#troubleshooting)
   - [Common Issues](#common-issues)
   - [FAQ](#faq)

---
## Introduction
### Overview

Script Templates is a Unity package designed to streamline your development workflow by providing a set of predefined script templates. These templates help maintain consistency across your codebase and save time when creating common script types.

### Features

- **Multiple Template Types**: Includes templates for common script types like MonoBehaviours, ScriptableObjects, Singletons, and more.
- **Context Menu Integration**: Easily accessible through Unity's right-click context menu in the Project window.
- **Custom Templates**: Create and use your own custom templates.
- **MultiScriptCreator**: Create multiple scripts at once with consistent naming.
- **Seamless Unity Integration**: Works with Unity's existing workflow and project structure.

### Requirements

- Unity 2019.3 or later
- Basic knowledge of C# and Unity scripting

## Installation

### Via Package Manager (Recommended)

1. Open your Unity project
2. Go to `Window > Package Manager`
3. Click the `+` button in the top-left corner
4. Select `Add package from git URL...`
5. Enter: `https://github.com/CRE-Tools/scripttemplates.git`
6. Click `Add`

### Manual Installation

1. Clone or download this repository
2. Copy the contents to your project's `Packages` folder
3. The package will be automatically imported into your Unity project

## Getting Started

### Accessing Script Templates

1. In the Unity Editor, right-click in the Project window
2. Navigate to `Create Script`
3. Select the desired template from the menu

## Template Reference

### Available Templates

- **Empty**: Empty class with no heritage
- **Enum**: Template for C# enums
- **Interface**: Template for creating C# interfaces
- **ScriptableObject**: Template for ScriptableObject classes
- **Singleton**: Template for singleton pattern implementation
- **Struct**: Template for C# structs

### Template Syntax

Templates use a simple syntax for dynamic content:

- `#SCRIPTNAME#`: Will be replaced with the script's name
- `#NOTRIM#`: Prevents Unity from stripping empty lines inside a template. It's essential for preserving spacing and formatting in your generated scripts.
- `#SCRIPTNAME_LOWER#`: Replaced with a lowercase version of the script's name. Useful for naming conventions where you need the lowercase version of the script name.

#### Example

Here's an example of a template file and how it would be processed:

**Template file (ExampleTemplate.txt):**
```csharp
// Example from ExampleTemplate.txt
public class #SCRIPTNAME#
{
    private string example = "#SCRIPTNAME_LOWER#";
#NOTRIM#

    public void ExampleMethod()
    {
#NOTRIM#
    }
}
```

**Generated script (using "ScriptExample" as script name):**
```csharp
public class ScriptExample
{
    private string example = "scriptexample";

    public void ExampleMethod()
    {

    }
}
```

> **Note:** The `#NOTRIM#` tag is essential for preserving spacing and formatting in your generated scripts. Spaces and empty lines without it will be removed during script generation.

## Advanced Usage

### MultiScriptCreator

The MultiScriptCreator allows you to create multiple scripts at once with consistent naming, making it easy to create related scripts with different inheritance and interfaces. Here's how to use it:

1. Right-click in the Project window
2. Select `Create Script > MultiScriptCreator`
   - This will create a folder `Assets/MultiScriptCreator` if it doesn't exist
3. Create template files in the `Assets/MultiScriptCreator` folder with the following structure:

   **Example 1: Interface Template (IExampleInterface.txt)**
   ```csharp
   public interface I#SCRIPTNAME#
   {
   #NOTRIM#
   }
   ```

   **Example 2: Implementation Template (ExampleClass.txt)**
   ```csharp
   public class #SCRIPTNAME# : I#SCRIPTNAME#
   {
   #NOTRIM#
   }
   ```

4. Right-click and select `Create Script > MultiScriptCreator` again
5. In the dialog, enter the base name for your scripts
6. Click "Create" to generate all scripts with consistent naming

**Resulting generated files (using "ScriptExample" as script name):**

**IScriptExample.cs**
```csharp
public interface IScriptExample
{
    
}
```

**ScriptExample.cs**
```csharp
public class ScriptExample : IScriptExample
{
    
}
```

## Troubleshooting

### Common Issues

#### MultiScriptCreator creates files with compilation errors
- Ensure the template files have differences around `#SCRIPTNAME#` because the MultiScriptCreator will use the same ScriptName for all files. See the [MultiScriptCreator](#multiscriptcreator) section for correct template examples.
- Make sure each template file in the `Assets/MultiScriptCreator` folder has a unique name and content structure to avoid conflicts.

#### Script compilation errors
- Verify that all required namespaces are included
- Check for syntax errors in MultiScriptCreator
- Ensure the template uses valid C# syntax
- Ensure files has unique names

### FAQ

**Q: Can I modify the default templates?**
A: Yes, you can find the default templates in the `Templates` folder and modify them as needed.

**Q: How do I update the package?**
A: Use the Package Manager to update to the latest version, or download the latest release from GitHub.

**Q: Can I use this in a commercial project?**
A: Yes, this package is open-source and free for commercial use under the MIT License.

**Q: How do I report a bug or request a feature?**
A: Please open an issue on the [GitHub repository](https://github.com/CRE-Tools/scripttemplates/issues).
