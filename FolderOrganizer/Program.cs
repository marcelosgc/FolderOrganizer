// See https://aka.ms/new-console-template for more information

using FolderOrganizer.Jobs;
using Spectre.Console;
using FolderOrganizer.Models;


int option;
var defaultFolder = "D:\\Downloads";

StartMenu();

ExecuteOptionAndKeepAlive();

bool IsExit(int i)
{
    JobsEnum.TryParse("Exit", out JobsEnum value);
    return Convert.ToInt32(value) == i;
}

void WaitingState()
{
    AnsiConsole.MarkupLine("[grey69]--------- || ---------[/]");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[springgreen3] Ergh.. What do you want, again? ¬_¬[/]");
    var sOption = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .AddChoices(Enum.GetNames(typeof(JobsEnum)).ToList()));
    option = Convert.ToInt32(Enum.Parse<JobsEnum>(sOption));
}

void StartMenu()
{
    AnsiConsole.Write(
        new FigletText("Welcome!")
            .LeftJustified()
            .Color(Color.Green));

    AnsiConsole.MarkupLine("What do you want?");
    var sOption = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .AddChoices(Enum.GetNames(typeof(JobsEnum)).ToList()));
    option = Convert.ToInt32(Enum.Parse<JobsEnum>(sOption));
}

void ExecuteOptionAndKeepAlive()
{
    do
    {
        try
        {
            switch (option)
            {
                case 1:

                    OrganizeFolderByTypeJob.Execute(SetFolder());

                    break;
                case 2:

                    OrganizeFolderByCreationDate.Execute(SetFolder());

                    break;
                case 3:

                    PascalizeFoldersJob.Execute(SetFolder());
                    break;

                case 4:

                    CleanDesktopJob.Execute();
                    break;

                case 5:

                    MoveFolderContentJob.Execute(SetFolder());
                    break;
            }
        }
        catch (CustomException e)
        {
            AnsiConsole.MarkupLine("[bold red] 0 on directory input is to cancel the operation. Operation Canceled![/]");
            AnsiConsole.WriteLine();
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            AnsiConsole.WriteLine();
        }

        if (!IsExit(option))
        {
            WaitingState();
        }
    } while (!IsExit(option));
    
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("Bye!");
}

string SetFolder()
{
    string? newDir;
    newDir = AnsiConsole.Ask<string>("Set the folder to organize. Press enter to use the default folder");
    if (newDir == "0")
    {
        throw new CustomException("0 as folder name to cancel the job");
    }

    var chosenFolder = string.IsNullOrWhiteSpace(newDir) ? defaultFolder : newDir;
    return chosenFolder;
}