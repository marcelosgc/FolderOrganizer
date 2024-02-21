namespace FolderOrganizer.Jobs;

public class CleanDesktopJob
{
    public static void Execute()
    {
        Console.WriteLine("Executing the method CleanDesktopJob...");
        Console.WriteLine(string.Empty);

        var documentTypes = new List<string>
        {
            ".txt",
            ".doc",
            ".docx",
            ".pdf",
            ".xls",
            ".xlsx",
            ".ppt",
            ".pptx"
        };

        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        foreach (var type in documentTypes)
        {
            var files = Directory.GetFiles(desktopPath, "*" + type);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                File.Move(file, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + fileName);
            }
        }

        Console.WriteLine("All documents was moved to the documents folder.");
        Console.WriteLine("Do you want to organize the documents folder? (y/n)");
        var organizeDocuments = Console.ReadLine();

        if (organizeDocuments == "y")
        {
            OrganizeFolderByTypeJob.Execute(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            Console.WriteLine("Documents folder organized!");
            Console.WriteLine(string.Empty);
        }

        Console.WriteLine("Finished the method CleanDesktopJob.");
        Console.WriteLine(string.Empty);
    }
}