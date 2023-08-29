
# Starter App in Blazor

This project exists primarily to provide developers looking to start a new Blazor project with a useful and exemplary starting point. 

## Motivation

This project was created to in an effort to make new Blazor Apps faster and simpler to setup. 
New projects often have some boilerplate, even with Blazor. Furthermore, I didn't want a project to inlcude bootstrap automatically. (I opted for tailwindcss instead.)

The basicApp repository is a framework to get started with the actual development phase earlier, and avoid repetition.

## Getting Started

1. Clone this repository
`git clone https://github.com/CodingWithDavid/basicApp`
`cd basicApp`

2. Open in Visual Studio or Visual Studio Code
With Visual Studio Code you will need to install the C# extensions:

3. Install the required packages as documented in `basicApp.csproj`
Use `dotnet restore` or install them manually (e.g. `dotnet add package MudBlazor`)

4. Replace all text and filename occurences of `basicApp` with your actual projectname

5. Customize project-specific-colors in `static/js/twConfig.js`

6. Replace sample credentials in `static/content/credentials.json` 
This information is required by law in many countries, and the EU, which is why I included it in here.

6. Switch out the basis-legal-texts with your own ones or remove them.
Options to generate them:
[privacypolicygenerator.info](https://www.privacypolicygenerator.info/)
[freeprivacypolicy.com](https://www.freeprivacypolicy.com/free-privacy-policy-generator/)
[zryo.com](https://zyro.com/tools/privacy-policy-generator)


## Open an Issue

[Open an issue](https://github.com/DavideWiest/basicApp/issues)

## Conventions

- `tw...` Attributes refer to usage in or with tailwind-classes
- Reusable components are stored in the `Shared`-folder, Pages in `Pages`
- static content is located in `static`, not `wwwroot`
- Account-related components such as a login-page belong to `identity` (which exists in both frontend-folders)
- Frequently used components are stored in `Shared/base`, seperated into subfolders for each respective group
- Important constants, and "magic"-values are stored in the `Constants` class 
- C# Classes, like a dbManager are located in the modules-folder

## Thank you
