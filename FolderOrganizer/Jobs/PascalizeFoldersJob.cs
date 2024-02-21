using Humanizer;

namespace FolderOrganizer.Jobs;

public class PascalizeFoldersJob
{
    //method to rename all folders on directory
    public static void Execute(string? folder)
    {
        Console.WriteLine("Executing the method PascalizeFoldersJob.");
        Console.WriteLine(string.Empty);

        var folders = Directory.GetDirectories(folder);

        foreach (var folderName in folders)
        {
            var folderNamePascalized = folderName.Split('\\').Last().Pascalize();
            Directory.Move(folderName, folder + "\\" + folderNamePascalized);
        }

        Console.WriteLine("Finished the method PascalizeFolders.");
        Console.WriteLine(string.Empty);
    }
}