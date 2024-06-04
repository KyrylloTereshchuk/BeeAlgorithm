using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeSpace
{
    public class Collections : ICollections
    {

        public List<Item> Items { get; set; }
        public List<Shelf> Shelves { get; set; }

        public Collections()
        {
                Items = new List<Item>
            {
                new Item { Weight = 2, Volume = 1, Value = 3 },
                new Item { Weight = 3, Volume = 2, Value = 5 },
                new Item { Weight = 5, Volume = 2, Value = 6 },
                new Item { Weight = 4, Volume = 3, Value = 2 },
                new Item { Weight = 1, Volume = 1, Value = 4 },
                new Item { Weight = 2, Volume = 1, Value = 3 },
                new Item { Weight = 7, Volume = 2, Value = 4 },
                new Item { Weight = 12, Volume = 3, Value = 12 }
            };

                Shelves = new List<Shelf>
            {
                new Shelf { MaxWeight = 14, MaxVolume = 5 },
                new Shelf { MaxWeight = 18, MaxVolume = 7 }
            };
        }

        public void ClearCollections()
        {
            Items = new List<Item>();
            Shelves = new List<Shelf>();
        }

        public void GenerateRandomCollections(int shelvesCount, int itemsCount)
        {
            ClearCollections();
            var random = new Random();

            if (shelvesCount == 0)
            {               
                shelvesCount = random.Next(2, 5); // кількість об'єктів Shelves від 2 до 4
            }
            if (itemsCount == 0)
            {
                itemsCount = random.Next(8, 16); // кількість об'єктів Items від 8 до 15                
            }


            List<Item> items = new List<Item>();
            List<Shelf> shelves = new List<Shelf>();

            for (int i = 0; i < itemsCount; i++)
            {
                items.Add(new Item
                {
                    Weight = random.Next(1, 10),
                    Volume = random.Next(1, 5),
                    Value = random.Next(1, 15)
                });
            }

            for (int i = 0; i < shelvesCount; i++)
            {
                shelves.Add(new Shelf
                {
                    MaxWeight = random.Next(3 * shelvesCount, 2 * itemsCount),
                    MaxVolume = random.Next(2 * shelvesCount, itemsCount)
                });
            }

            while (items.Sum(item => item.Weight) <= shelves.Sum(shelf => shelf.MaxWeight) ||
                   items.Sum(item => item.Volume) <= shelves.Sum(shelf => shelf.MaxVolume))
            {
                items.Add(new Item
                {
                    Weight = random.Next(1, 10),
                    Volume = random.Next(1, 5),
                    Value = random.Next(1, 15)
                });
            }

            Items = items;
            Shelves = shelves;
        }

        public void FillCollectionsFromConsole()
        {
            ClearCollections();

            Console.Write("Enter the number of shelves: ");
            int shelfCount = int.Parse(Console.ReadLine());
            List<Shelf> shelves = new List<Shelf>();

            for (int i = 0; i < shelfCount; i++)
            {
                Console.WriteLine($"Enter attributes for shelf {i + 1}:");
                Console.Write("MaxWeight: ");
                int maxWeight = int.Parse(Console.ReadLine());
                Console.Write("MaxVolume: ");
                int maxVolume = int.Parse(Console.ReadLine());

                shelves.Add(new Shelf { MaxWeight = maxWeight, MaxVolume = maxVolume });
            }

            Console.Write("Enter the number of items: ");
            int itemCount = int.Parse(Console.ReadLine());
            List<Item> items = new List<Item>();

            for (int i = 0; i < itemCount; i++)
            {
                Console.WriteLine($"Enter attributes for item {i + 1}:");
                Console.Write("Weight: ");
                int weight = int.Parse(Console.ReadLine());
                Console.Write("Volume: ");
                int volume = int.Parse(Console.ReadLine());
                Console.Write("Value: ");
                int value = int.Parse(Console.ReadLine());

                items.Add(new Item { Weight = weight, Volume = volume, Value = value });
            }

            Items = items;
            Shelves = shelves;
        }

        public void FillCollectionsFromFile(string filePath)
        {
            ClearCollections();

            List<Shelf> shelves = new List<Shelf>();
            List<Item> items = new List<Item>();

            var lines = File.ReadAllLines($"D:\\Інформатика\\CourseWork_Bee\\BeeAlgorithm\\" + filePath);
            int lineIndex = 0;

            int shelfCount = int.Parse(lines[lineIndex++]);
            for (int i = 0; i < shelfCount; i++)
            {
                var shelfData = lines[lineIndex++].Split(' ');
                shelves.Add(new Shelf
                {
                    MaxWeight = int.Parse(shelfData[0]),
                    MaxVolume = int.Parse(shelfData[1])
                });
            }

            int itemCount = int.Parse(lines[lineIndex++]);
            for (int i = 0; i < itemCount; i++)
            {
                var itemData = lines[lineIndex++].Split(' ');
                items.Add(new Item
                {
                    Weight = int.Parse(itemData[0]),
                    Volume = int.Parse(itemData[1]),
                    Value = int.Parse(itemData[2])
                });
            }

            Items = items;
            Shelves = shelves;
        }

        public void PrintCollections()
        {
            Console.WriteLine("Current Items:");
            foreach (var item in Items)
            {
                Console.WriteLine($"Weight: {item.Weight}, Volume: {item.Volume}, Value: {item.Value}");
            }

            Console.WriteLine("\nCurrent Shelves:");
            foreach (var shelf in Shelves)
            {
                Console.WriteLine($"MaxWeight: {shelf.MaxWeight}, MaxVolume: {shelf.MaxVolume}");
            }
        }

    }

}
