namespace FolderOrganizer.Jobs;

public class OrganizeFolderByCreationDate
{
    //method to create folders by creation date
    public static void Execute(string? folder)
    {
        Console.WriteLine("Executing the method OrganizeFolderByCreationDate.");
        Console.WriteLine(string.Empty);

        //get all files in the download folder
        var files = Directory.GetFiles(folder);

        //get the creation date of the files
        var creationDates = files.Select(x => File.GetCreationTime(x).ToString("yyyy-MM-dd")).Distinct().ToHashSet();

        if (!creationDates.Any())
        {
            Console.WriteLine("No files to organize.");
            Console.WriteLine(string.Empty);
            return;
        }

        //create folders to types of files
        foreach (var creationDate in creationDates) Directory.CreateDirectory(folder + "\\" + creationDate);

        //move files to folders
        foreach (var file in files)
        {
            var creationDate = File.GetCreationTime(file).ToString("yyyy-MM-dd");
            File.Move(file, folder + "\\" + creationDate + "\\" + Path.GetFileName(file));
        }

        Console.WriteLine("Finished the method OrganizeFolderByCreationDate.");
        Console.WriteLine(string.Empty);
    }
}