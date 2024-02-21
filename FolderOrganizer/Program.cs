// See https://aka.ms/new-console-template for more information

using FolderOrganizer.Jobs;
using Spectre.Console;
using FolderOrganizer.Models;


int option;
var defaultFolder = "D:\\Downloads";

StartMenu();

do
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

    if (!IsExit(option))
    {
        WaitingState();
    }
} while (!IsExit(option));

AnsiConsole.MarkupLine("Bye!");

bool IsExit(int i)
{
    JobsEnum.TryParse("Exit", out JobsEnum value);

    return Convert.ToInt32(value) == i;
}

void WaitingState()
{
    AnsiConsole.MarkupLine("[grey69]--------- || ---------[/]");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("Ergh.. What do you want, again?");
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

string SetFolder()
{
    string? newDir;
    newDir = AnsiConsole.Ask<string>("Set the folder to organize. Press enter to use the default folder");
    var chosenFolder = string.IsNullOrWhiteSpace(newDir) ? defaultFolder : newDir;
    return chosenFolder;
}