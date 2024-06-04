using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeSpace
{
    public class GreedyAlgorithm
    {
        public List<Shelf> Optimize(List<Item> items, List<Shelf> shelves)
        {
            // Сортування товарів за вартістю по спаданню
            var sortedItems = items.OrderByDescending(i => i.Value).ToList();

            // Проходження по стелажах і заповнення їх товарами
            foreach (var shelf in shelves)
            {
                foreach (var item in sortedItems.ToList()) // Використання ToList() щоб уникнути модифікації колекції під час ітерації
                {
                    if (shelf.CurrentWeight + item.Weight <= shelf.MaxWeight && shelf.CurrentVolume + item.Volume <= shelf.MaxVolume)
                    {
                        shelf.Items.Add(item);
                        sortedItems.Remove(item);
                    }
                }
            }

            return shelves;
        }
    }
}
