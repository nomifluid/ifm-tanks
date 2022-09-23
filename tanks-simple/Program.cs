//// Aplikace načte soubor tanks.txt (id; rozměry v dm). Použijte oop.

//// //1ev, gu7

//// 1. úkol
//// zjistit, jestli jsou všechna id unikátní, případně je potřeba opravit

//// 2. úkol
//// najít nádoby s největším a nejmenším objemem (vypsat ids)

//// 3.0
//// uživatel zadá počet litrů, aplikace vypíše kolik dokázala naplnit nádrží do plna.

//// 3.1
//// 3.0 + aplikace také

//// vypíše  ids naplněných nádrž

//// 3.2
//// 3.1 ale do souboru output.txt volume;count;id,id,id…
//// Aplikace soubor nepřepisuje.

//// 3.3
//// 3.2 ale snaží se naplnit co nejvíce nádob

//const fs = require("fs");
//const text = fs.readFileSync("./tanks.txt").toString();

//const tanks = [];
//const lines = text.split("\n");
//lines.map((line) => {
//const [id, sizes] = line.split(";");
//const [x, y, z] = sizes.split(",");
//const liters = x * y * z;
//// úkol 1
//if (!tanks.find((t) => t[0] === id)) tanks.push([id, liters]);
//});

//tanks.sort((a, b) => b[1] - a[1]);

//// úkol 2
//console.log(tanks[tanks.length - 1], tanks[0]);

//// úkol 3 (rovnou 3.1)

//const input = parseInt(Object.values(process.argv)[2]) || 0;
//console.log("to fill", input, "l");

//const filtered = tanks.filter(([, volume]) => volume <= input);

//const fillTanks = (availableTanks, litersToFill) => ({
//  tanks: availableTanks.filter(([, volume]) => {
//    if (volume <= litersToFill)
//    {
//        litersToFill -= volume;
//        return true;
//    }
//    return false;
//}),
//  liters: litersToFill,
//});

//const buildOutput = ({ tanks, liters }) =>
//  [`${liters}l`, tanks.length, tanks.map(([, v]) => v).join(",")].join(";");

//const mostLiters = buildOutput(fillTanks(filtered, input)); // 3.0
//const mostTanks = buildOutput(fillTanks(filtered.reverse(), input)); // 3.3

//console.log("x.x:", "volume;count;id,id,id…");
//console.log("3.0:", mostLiters);
//console.log("3.3:", mostTanks);

//fs.appendFileSync("./out.txt", `\n${ mostLiters}`);

using System;
namespace tanks_simple;


public class Program
{
    public static void Main()
    {
        string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "/";
        string file = path + "tanks.txt";

        string text = System.IO.File.ReadAllText(file);

        string[] lines = text.Split("\n");
        List<Tank> tanksList = new List<Tank>();

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            Tank tank = new Tank(line);
            Tank duplicate = tanksList.Find(t => t?.id == tank.id);
            //task 1
            if (duplicate == null)
            {
                try
                {
                    tanksList.Add(tank);
                }
                catch (Exception ex)
                {
                    if (line.Trim() == "")
                    {
                        Console.WriteLine("[!] empty instance in input");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("[!] invalid instance in input");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("[i] found duplicate for id " + tank.id + ", skipping");
            }
        }

        Tank[] tanks = tanksList.ToArray();

        Array.Sort(tanks, (a, b) => b.liters - a.liters);

        //task 2
        Console.WriteLine("[i] min: " + tanks[tanks.Length - 1].liters + "l; max: " + tanks[0].liters + "l");

        //task 3
        Console.WriteLine("> enter volume (l) to fill:");
        int input;
        try
        {
            input = int.Parse(Console.ReadLine());
        }
        catch (Exception ex)
        {
            Console.WriteLine("[!] invalid user input!");
            return;
        }

        Tank[] filtered = Array.FindAll(tanks, t => t.liters <= input);


        TankFiller mostLiters = new TankFiller(filtered, input); // 3.0
        // reverse array
        Array.Sort(filtered, (a, b) => a.liters - b.liters);
        TankFiller mostTanks = new TankFiller(filtered, input); // 3.3

        string mostLitersOut = new OutputBuilder(mostLiters.liters.ToString(), mostLiters.tanks.ToArray()).output;
        Console.WriteLine("[3.0] MOST LITERS: " + mostLitersOut);
        string mostTanksOut = new OutputBuilder(mostTanks.liters.ToString(), mostTanks.tanks.ToArray()).output;
        Console.WriteLine("[3.3] MOST TANKS: " + mostTanksOut);
    }
}
