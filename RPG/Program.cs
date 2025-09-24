// See https://aka.ms/new-console-template for more information
using RPG;
using System.Security.Cryptography;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Hello, RPG!");
string m = "\u25D9"; 
Hero mage = new Mage();
mage.AddAgility(1);
mage.AddStrength(2);
//mage.SetUp();
Console.WriteLine($"My mage has Health = {mage.Health}");
Console.WriteLine($"My mage has Mana = {mage.Mana}");
Console.WriteLine($"My mage has Damage = {mage.Damage}");
Console.WriteLine($"My mage has Range = {mage.Range}");
Console.WriteLine($"My mage has Symbol = {mage.Symbol}");

for(int i=0; i<10; i++)
{
    for(int j=0; j<10; j++)
    {
        Console.Write("▒");
    }
    Console.WriteLine();
}
    
