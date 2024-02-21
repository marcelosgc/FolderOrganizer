using Humanizer;

namespace FolderOrganizer.Jobs;

public static class OrganizeFolderByTypeJob
{
    public static void Execute(string? newFolderPath)
    {
        Console.WriteLine("Executing the method OrganizeFolderByTypeJob for the folder " + newFolderPath + ".");
        Console.WriteLine(string.Empty);

        //get the download folder path
        var folder = newFolderPath;

        //get all files in the download folder
        var originalFiles = Directory.GetFiles(folder);
        var files = new List<string?>();

        foreach (var file in originalFiles)
        {
            var fileName = Path.GetFileName(file);
            var extension = Path.GetExtension(file);
            if (extension != string.Empty)
            {
                files.Add(file);
            }
            else
            {
                //create a generic folder and move the file to it   
                Directory.CreateDirectory(folder + "\\NonType");
                File.Move(file, folder + "\\NonType\\" + fileName);
                Console.WriteLine("Moved the file " + fileName + " to the folder NonType.");
                Console.WriteLine(string.Empty);
            }
        }


        //get the types of files
        var types = files.Select(x => x.Split('.').Last().Pascalize()).Distinct().ToHashSet();

        if (!types.Any())
        {
            Console.WriteLine("No files to organize.");
            Console.WriteLine(string.Empty);
            return;
        }


        //create folders to types of files
        foreach (var type in types) Directory.CreateDirectory(folder + "\\" + type);

        //move files to folders
        foreach (var file in files)
        {
            var type = file.Split('.').Last();
            if (File.Exists(folder + "\\" + type + "\\" + Path.GetFileName(file)))
            {
                File.Move(file, folder + "\\" + type + "\\" + DateTime.Now.ToString("yy-MM-dd") + "_"+ Path.GetFileName(file));
            }
            else
            {
                File.Move(file, folder + "\\" + type + "\\" + Path.GetFileName(file));    
            }
            
        }

        Console.WriteLine("Finished the method OrganizeFolderByTypeJob.");
        Console.WriteLine(string.Empty);
    }
}