using Clients;

Client client = new();
List<Client> ClientList = [];

bool repeat = true;
FileLoad(ClientList);

Console.Clear();
while(repeat) 
{
  try 
  {
    displayMainMenu();
    string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
    if(mainMenuChoice == "L")
      displayAllClients();
    if(mainMenuChoice == "F")
      findClient();
    if(mainMenuChoice == "A")
      AddClientToList(ClientList);
    if(mainMenuChoice == "E")
      EditClient();
    if(mainMenuChoice == "D")
     DeleteClient();
    if(mainMenuChoice == "S")
      showClientBmiInfo();
    if (mainMenuChoice == "Q") 
    {
      SaveClients();
			repeat = false;
			throw new Exception("Bye, hope to see you again.");

    }

  }  
  catch (Exception ex) 
  {
    Console.WriteLine(ex.Message);
  } 
}

void displayMainMenu() 
{
  Console.WriteLine($"\n MENU OPTIONS");  
  Console.WriteLine($"(L)ist All Clients");
  Console.WriteLine($"(F)ind Client Information");
  Console.WriteLine($"(A)dd New Client Record");
  Console.WriteLine($"(E)dit Client Information");
  Console.WriteLine($"(D)elete Client Record");
  Console.WriteLine($"(S)how Client BMI Information");
  Console.WriteLine($"(Q)uit");
}

void displayEditMenu() 
{
  Console.WriteLine($"(F)irst Name");
  Console.WriteLine($"(L)ast Name");
  Console.WriteLine($"(H)eight");
  Console.WriteLine($"(W)eight");
  Console.WriteLine($"(R)eturn to Main Menu");
}

string Prompt(string Choice)
{
	string myString = "";
	while (true)
	{
		try
		{
		Console.Write(Choice);
		myString = Console.ReadLine().Trim();
		if(string.IsNullOrEmpty(myString))
			throw new Exception($"Empty Input: Please enter something.");
		break;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
	return myString;
}


double seconfinput(String msg, double min)
{
	double num = 0;
	while (true)
	{
		try
		{
			Console.Write($"{msg} between {min}: ");
			num = double.Parse(Console.ReadLine());
			if (num < min)
				throw new Exception($"Must be greater than {min:n2}");
			break;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Invalid: {ex.Message}");
		}
	}
	return num;
}

void FileLoad(List<Client> ClientList)
{
  while(true)
  {
    try
    {
      //string fileName = Prompt("Enter file name including .csv or .txt: ");
			string fileName = "client.csv";
			string filePath = $"./data/{fileName}";
			if (!File.Exists(filePath))
				throw new Exception($"The file {fileName} does not exist.");
			string[] csvFileInput = File.ReadAllLines(filePath);
			for(int i = 0; i < csvFileInput.Length; i++)
			{
				//Console.WriteLine($"lineIndex: {i}; line: {csvFileInput[i]}");
				string[] items = csvFileInput[i].Split(',');
				for(int j = 0; j < items.Length; j++)
				{
					//Console.WriteLine($"itemIndex: {j}; item: {items[j]}");
				}
				Client client = new(items[0], items[1], double.Parse(items[2]), double.Parse(items[3]));
        ClientList.Add(client);
			}
			Console.WriteLine($"Load complete. {fileName} has {ClientList.Count} data entries");
			break;
    }
    catch(Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
}

void displayAllClients()
{
  try 
  {
    if(ClientList.Count <= 0)
      throw new Exception($"No data has been loaded");
    
    foreach(Client client in ClientList) 
      showClientInfo(client);

  } catch (Exception ex) {
    Console.WriteLine($"Error: {ex.Message}");
  }
}

void findClient()
{
  displayAllClients();
  string clientName = Prompt("Enter client's Firstname: ");
  List<Client> filteredClient = ClientList.Where(c => c.Firstname.Contains(clientName)).ToList();

  Client selectedClient = filteredClient.FirstOrDefault();
  Console.WriteLine($"\n{selectedClient.ToString()}");
  Console.WriteLine($"Client's BMI Score     :\t{selectedClient.Bminumber:n2}");
	Console.WriteLine($"Client's BMI Status    :\t{selectedClient.BmiStatus}");

}

void SaveClients() {
      string fileName = "client.csv";
      string filePath = $"./data/{fileName}";
      List<String> ClientRecords = [];
      foreach(Client data in ClientList) {
        ClientRecords.Add($"{data.Firstname}, {data.Lastname}, {data.Weight}, {data.Height}");
      }
      File.WriteAllLines(filePath, ClientRecords);
    }

void showClientBmiInfo()
{
  
  string clientName = Prompt("\nEnter client's Firstname: ");
  List<Client> filteredClient = ClientList.Where(c => c.Firstname.Contains(clientName)).ToList();
  Client selectedClient = filteredClient.FirstOrDefault();
  Console.WriteLine($"\n{selectedClient.ToString()}");
  Console.WriteLine($"Client's BMI Score     :\t{selectedClient.Bminumber:n2}");
	Console.WriteLine($"Client's BMI Status    :\t{selectedClient.BmiStatus}");
}

void showClientInfo(Client client)
{
  if(client == null)
    throw new Exception("No Client In Memory");
  Console.WriteLine($"\n{client.ToString()}");
  Console.WriteLine($"Client's BMI Score     :\t{client.Bminumber:n2}");
	Console.WriteLine($"Client's BMI Status    :\t{client.BmiStatus}");
}

void AddClientToList(List<Client> ClientList)
{
  GetFirstname(client);
  GetLastname(client);
  GetWeight(client);
  GetHeight(client);
  ClientList.Add(client);


}


void EditClient() 
{
      
      displayAllClients();
  string clientName = Prompt("Enter client's Firstname: ");
    List<Client> filteredClient = ClientList.Where(c => c.Firstname.Contains(clientName)).ToList();
    Client selectedClient = filteredClient.FirstOrDefault();
  
      while(true) 
      {
        Console.WriteLine($"===== SELECT DATA OF TO EDIT ====="); 
        displayEditMenu();
        string editChoice = Prompt("\nEnter Edit Menu Choice: ").ToUpper();
        if(editChoice == "F") 
        {
          selectedClient.Firstname = Prompt($"Enter Client Firstname: ");
        } else if(editChoice == "L") 
        {
          selectedClient.Lastname = Prompt($"Enter Client Lastname: ");
        } else if(editChoice == "W") 
        {
          selectedClient.Weight = seconfinput($"Enter Client Weight (lbs): ", 0);
        } else if(editChoice == "H")
         {
          selectedClient.Height = seconfinput($"Enter Client Height (inches): ", 0);
        }else if(editChoice == "R") 
        {
          Console.WriteLine($"You have successfully updated details");
          break;
        } else 
        {
          throw new Exception("Invalid Edit Menu Choice. Please Try Again.");
        }
      }          
    }



void DeleteClient() 
{
      
      displayAllClients();
  string clientName = Prompt("Enter client's Firstname: ");
  List<Client> filteredClient = ClientList.Where(c => c.Firstname.Contains(clientName)).ToList();
  Client selectedClient = filteredClient.FirstOrDefault();
  
      while(true) 
      {
        string confirmation = Prompt($"You are about to delete "+ selectedClient.Firstname + "'s record. Proceed? Y/N: ").ToUpper();
        if (confirmation == "Y") 
        {
          ClientList.Remove(selectedClient);
          Console.WriteLine($"{selectedClient.Firstname}'s has been deleted.");
          break;
        } else if (confirmation == "N") 
        {
          Console.WriteLine($"Delete operation cancelled for {selectedClient.Firstname}.");
          break;
        } else 
        {
          Console.WriteLine($"Invalid confirmation input. Please enter 'Y' or 'N'.");
        }
      }
    }


void GetFirstname(Client client)
{
	string myString = Prompt($"Enter Firstname: ");
	client.Firstname = myString;
}

void GetLastname(Client client)
{
	string myString = Prompt($"Enter Lastname: ");
	client.Lastname = myString;
}

void GetWeight(Client client)
{
	double myDouble = seconfinput("Enter Weight in inches: ", 0);
	client.Weight = myDouble;
}

void GetHeight(Client client)
{
	double myDouble = seconfinput("Enter Height in inches: ", 0);
	client.Height = myDouble;
}