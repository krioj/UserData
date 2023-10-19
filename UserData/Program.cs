using UserData.Data;
using UserData.Entitys;
using UserData.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Options;
using UserData.Repositories.Extensions;

var userRepository = new SqlRepository<User>(new UserDataDbContext());
userRepository.ItemAdded += UserRepository_ItemAdded;

static void UserRepository_ItemAdded(object? sender, User user)
{
    if (user.Administrator == true)
    {
        Console.WriteLine($"Administrator \"{user.NickName}\" added from class \"{sender?.GetType().Name}\"");
    }
    else if(user.Moderator == true)
    {
        Console.WriteLine($"Moderator  \"{user.NickName}\" added from class \"{sender?.GetType().Name}\"");
    }
    else
    {
        Console.WriteLine($"User \"{user.NickName}\" added from class \"{sender?.GetType().Name}\"");
    }
}

do
{
    Menu();
    string selection = Console.ReadLine();
    if (selection == "q")
        break;
    switch (selection)
    {
        case "1":
            WriteAllToConsole(userRepository);
            Console.ReadLine();
            break;
        case "2":
            AddUser(userRepository);
            break;
        case "3":
            RemoveUser(userRepository);
            break;
        case "c":
        case "C":
            Console.Clear();
            break;
        default:
            Console.WriteLine("this choice does not exist");
            continue;
    }
} while (true);

static void AddUser(IRepository<User> userRepository)
{
    Console.WriteLine("indicate user status (adm = administrator, mdr = moderator)");

    bool adminStatus = false;
    bool moderatorStatus = false;

    do
    {
        string status = Console.ReadLine();
        if (status == "Adm" || status == "adm" || status == "ADM" || status == "adM" || status == "aDm")
        {
            adminStatus = true;
            Console.WriteLine("user is an administrator");
            break;
        }
        else if (status == "Mdr" || status == "mdr" || status == "MDR" || status == "mdR" || status == "mDr")
        {
            moderatorStatus = true;
            Console.WriteLine("user is an moderator");
            break;
        }
        else
        {
            Console.WriteLine("adding a default user");
            break;
        }

        /*
        string status = Console.ReadLine();
        switch (status)
        {
            case "adm":
            case "ADM":
            case "Adm":
            case "aDm":
            case "adM":
                adminStatus = true;
                Console.WriteLine("user is an administrator");
                break;
            case "mdr":
            case "MDR":
            case "Mdr":
            case "mDr":
            case "mdR":
                moderatorStatus = true;
                Console.WriteLine("user is an moderator");
                break;
            default:
                Console.WriteLine("adding a default user");
                break;
        }
        if (adminStatus == true || moderatorStatus == true)
        {
            break;
        }
        else if (adminStatus == false && moderatorStatus == false)
        {
            break;
        }*/
    } while (true);
    Console.WriteLine("please provide the following information in this order:\n1. nickname\n2. login\n3. password");
    userRepository.Add(new User { NickName = Console.ReadLine(), Login = Console.ReadLine(), Password = Console.ReadLine(), Administrator = adminStatus, Moderator = moderatorStatus });
    userRepository.Save();
 
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();

    if (!items.Any())
    {
        Console.WriteLine("no data found");
    }
    else
    {
        foreach (var item in items)
        {

            Console.WriteLine(item);
        }
    }
}

static void RemoveUser(IRepository<User> repository)
{
    Console.WriteLine("Enter the Id of user you want to delete");
    try
    {
        repository.Remove(repository.GetById(int.Parse(Console.ReadLine())));      // exeption
        repository.Save();
    }
    catch
    {
        Console.WriteLine("Wrong option");
    }
}

static void Menu()
{
    //Console.Clear();
    Console.WriteLine("+------------------------------------------+");
    Console.WriteLine("|                   MENU                   |");
    Console.WriteLine("+------------------------------------------+");
    Console.WriteLine(" 1 - show the user table");
    Console.WriteLine(" 2 - register the user");
    Console.WriteLine(" 3 - delete the selected user");
    Console.WriteLine(" c - clear console");
    Console.WriteLine(" q - exit the program");
}






