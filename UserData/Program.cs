using UserData.Data;
using UserData.Entitys;
using UserData.Repositories;

var userRepository = new SqlRepository<User>(new UserDataDbContext());

userRepository.ItemAdded += userRepositoryOnItemAdded;
userRepository.ItemRemove += userRepositoryOnItemRemove;

static void userRepositoryOnItemAdded(object? sender, User e)
{

    if (e.Administrator == true)
    {
        string administrator = $"Administrator \"{e.NickName}\" added from class \"{sender?.GetType().Name}\", Date: {DateTime.Now}";
        Console.WriteLine(administrator);
        using (var writer = File.AppendText("History.txt"))
        {
            writer.WriteLine(administrator);
        }
    }
    else if (e.Moderator == true)
    {
        string moderator = $"Moderator  \"{e.NickName}\" added from class \"{sender?.GetType().Name}\", Date: {DateTime.Now}";
        Console.WriteLine(moderator);
        using (var writer = File.AppendText("History.txt"))
        {
            writer.WriteLine(moderator);
        }
    }
    else
    {
        string user = $"User \"{e.NickName}\" added from class \"{sender?.GetType().Name}\", Date:  {DateTime.Now}";
        Console.WriteLine(user);
        using (var writer = File.AppendText("History.txt"))
        {
            writer.WriteLine(user);
        }
    }
}
static void userRepositoryOnItemRemove(object? sender, User e)
{
    if (e.Administrator == true)
    {
        string administrator = $"Administrator \"{e.NickName}\" remove from class \"{sender?.GetType().Name}\", Date:  {DateTime.Now}";
        Console.WriteLine(administrator);
        using (var writer = File.AppendText("History.txt"))
        {
            writer.WriteLine(administrator);
        }
    }
    else if (e.Moderator == true)
    {
        string moderator = $"Moderator  \"{e.NickName}\" remove from class \"{sender?.GetType().Name}\", Date:   {DateTime.Now}";
        Console.WriteLine(moderator);
        using (var writer = File.AppendText("History.txt"))
        {
            writer.WriteLine(moderator);
        }
    }
    else
    {
        string user = $"User \"{e.NickName}\" remove from class \"{sender?.GetType().Name}\", Date:   {DateTime.Now}";
        Console.WriteLine(user);
        using (var writer = File.AppendText("History.txt"))
        {
            writer.WriteLine(user);
        }
    }
}

do
{
    Menu();

    string input = Console.ReadLine();
    if (input == "q" || input == "Q")
        break;

    switch (input)
    {
        case "1":
            WriteAllToConsole(userRepository);
            break;
        case "2":
            AddUser(userRepository);
            break;
        case "3":
            RemoveUser(userRepository);
            break;
        case "4":
            ClearDb(userRepository);
            break;
        case "c":
        case "C":
            Console.Clear();
            break;
        default:
            Console.WriteLine("wrong option");
            break;
    }
} while (true);

static void Menu()
{
    Console.WriteLine("|----------------------MENU----------------------|");
    Console.WriteLine("1 - show the user table");
    Console.WriteLine("2 - register the user");
    Console.WriteLine("3 - delete the selected user");
    Console.WriteLine("4 - clear data base");
    Console.WriteLine("c - clear console");
    Console.WriteLine("q - exit the program");
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();

    if (items.Any())
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
    else
    {
        Console.WriteLine("elements not found");
    }
    Console.ReadKey();//waiting
}

static void AddUser(IRepository<User> userRepository)
{
    Console.WriteLine("Indicate user status (adm = administrator, mdr = moderator)");

    bool adminStatus = false;
    bool moderatorStatus = false;
    string status = Console.ReadLine();

    if (status == "Adm" || status == "adm" || status == "ADM" || status == "adM" || status == "aDm")
    {
        adminStatus = true;
    }
    else if (status == "Mdr" || status == "mdr" || status == "MDR" || status == "mdR" || status == "mDr")
    {
        moderatorStatus = true;
    }

    Console.WriteLine("Please provide the following information in this order:\n1. Nickname\n2. Login\n3. Password");

    userRepository.Add(new User { NickName = Console.ReadLine(), Password = Console.ReadLine(), Login = Console.ReadLine(), Administrator = adminStatus, Moderator = moderatorStatus });
    userRepository.Save();
}

static void RemoveUser(IRepository<User> repository)
{
    Console.WriteLine("Enter the Id of user you want to delete");
    try
    {
        repository.Remove(repository.GetById(int.Parse(Console.ReadLine())));
        repository.Save();
    }
    catch
    {
        Console.WriteLine("wrong option");
    }
}

static void ClearDb(IRepository<User> repository)
{
    var items = repository.GetAll();

    if (items.Any())
    {
        foreach (var item in items)
        {
            repository.Remove(item);
        }
        repository.Save();

        Console.WriteLine("Db is claen!");
        Console.ReadKey();
    }
    else
    {
        Console.WriteLine("Db is null");
    }
}

