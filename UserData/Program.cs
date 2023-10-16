using UserData.Data;
using UserData.Entitys;
using UserData.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Options;

var userRepository = new SqlRepository<User>(new MotoAppDbContext());
var use = userRepository.GetAll();

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
            Console.ReadKey();
            break;
        case "2":
            AddUser(userRepository);
            break;
        case "3":

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

static void RemuveUser(IReadRepository<User> repository)
{
    var item = repository.GetAll();
    try
    {
        //repository.Remove(repository.GetById(int.Parse(Console.ReadLine())));
    }
    catch
    {
        Console.WriteLine("Wrong option");
    }
}

static void Menu()
{
    Console.Clear();
    Console.WriteLine("1 - show the user table\n" +
        "2 - register the user\n" +
        "3 - delete the selected user\n" +
        "q - exit the program");
}






