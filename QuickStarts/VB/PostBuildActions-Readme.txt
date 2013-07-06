When you use the Configuration Console to change the QuickStart configuration settings, the modified configuration files for the application block are saved to the same directory as the App.config file. However, each time a project successfully builds, Visual Studio automatically copies the App.config file to the target directory and renames it to <executable>.exe.config. This means you must also copy the relevant application block configuration files to the target directory. You can either manually copy the application block configuration files to the target directory or use the Visual Studio Build Events property to have them be copied each time the project is built. 

This QuickStart uses post-build events to copy the Enterprise Library configuration files to the project's output directory when the solution is compiled. The QuickStart solution includes this project to work around a limitation in how Visual Basic handles post-build events. 

The Visual Basic compiler supports post-build events, but the Visual Basic development environment does not. Post-build events can be added to Visual Basic projects only by editing the .vbproj file in a text editor and adding the necessary attributes to the file. However, Visual Studio removes post-build events when the project file is saved using Visual Studio.

The "PostBuildEvents" project included in this QuickStart contains no code; it does contain post-build events. These build events are executed each time you compile the "PostBuildEvents" project. Because the post-build events reside in a separate project file, they are not removed if you make changes to the main QuickStart project. 

Do not make changes to the PostBuildEvents project (for example, by adding files) or use Visual Studio to save the project file; saving the project file in Visual Studio will remove the post-build events. The following instructions describe how you can recreate the post-build events:

 1. Using Notepad, open the C# version of the main QuickStart project. (The project file has the .csproj file name extenstion.)
 2. Locate the PostBuildEvent XML attribute of the Settings XML element. 
 3. Copy the line containing the PostBuildEvent attribute to the clipboard.
 4. Using Notepad, open the project file named PostBuildEvents.vbproj. 
 5. Add the PostBuildEvent attribute, copied in step 3, to the the Settings element. 
 6. Save the PostBuildEvents.vproj file..

Alternatively, you can to manually copy the application block configuration files to the project's output directory.

If you require post-build events in your own Visual Basic projects, another option you can use is the "PrePostBuildRules Add-In” which is available for download from http://www.microsoft.com/downloads/details.aspx?FamilyId=3FF9C915-30E5-430E-95B3-621DCCD25150&displaylang=en.
