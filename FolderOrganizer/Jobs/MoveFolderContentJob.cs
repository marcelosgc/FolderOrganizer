using Spectre.Console;

namespace FolderOrganizer.Jobs;

public class MoveFolderContentJob
{
    public static void Execute(string? folder)
    {
        AnsiConsole.MarkupLine("[underline yellow]Executing the method to Move Folder Content[/]");

        //get all files in the download folder
        if (!Directory.Exists(folder))
        {
            AnsiConsole.MarkupLine("[red bold]Finished the method to Move Folder Content | Folder doesn't exist[/]");
            return;
        }

        var folders = Directory.GetDirectories(folder);

        if (folder.Length > 0)
        {
            AnsiConsole.MarkupLine("Folders being analyzed: ");
            var rows = FillRowsWithFolders(folders);
            AnsiConsole.Write(new Rows(rows));
        }
        else
        {
            AnsiConsole.MarkupLine(
                "[dodgerblue2 bold]Finished the method to Move Folder Content | Empty folder specified[/]");
            return;
        }


        var newDir = AnsiConsole.Ask<string>("[bold red]What's the new directory?[/]");

        AnsiConsole.Status()
            .AutoRefresh(true)
            .Spinner(Spinner.Known.Star)
            .SpinnerStyle(Style.Parse("green bold"))
            .Start("Moving some folders and files...", ctx =>
            {
                foreach (var f in folders)
                {
                    ctx.Refresh();
                    var folderName = f.Split("\\").LastOrDefault();
                    var existingFolder = GetExistingFolder(f, folderName, newDir);
                    if (Directory.Exists(existingFolder))
                    {
                        var files = Directory.GetFiles(f);
                        MoveFiles(files, existingFolder);
                    }
                    else
                    {
                        Directory.Move(f, Path.Combine(newDir, folderName));
                    }

                    ctx.Refresh();
                }

                DeleteEmptyFolders(folders, folder);
            });


        AnsiConsole.MarkupLine("[dodgerblue2 bold]Finished the method to Move Folder Content[/]");
    }

    private static List<Text> FillRowsWithFolders(string[] folders)
    {
        var l = new List<Text>();
        foreach (var folder in folders)
        {
            var t = new Text(folder, new Style(Color.DarkOrange));
            l.Add(t);
        }

        return l;
    }


    private static void DeleteEmptyFolders(string[] folders, string mainFolder)
    {
        var deleteRoot = true;
        foreach (var f in folders)
            if (Directory.GetFiles("f").Length != 0)
                deleteRoot = false;

        if (deleteRoot) Directory.Delete(mainFolder);
    }

    private static string GetExistingFolder(string f, string? folderName, string newDir)
    {
        var existingFolder = Path.Combine(newDir, folderName);
        return existingFolder;
    }

    private static void MoveFiles(string[] files, string existingFolder)
    {
        foreach (var content in files)
        {
            var fileName = Path.GetFileName(content);
            var destFileName = Path.Combine(existingFolder, fileName);

            File.Move(content, destFileName, true);
        }
    }
}