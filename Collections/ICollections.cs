using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeSpace
{
    public interface ICollections
    {
        List<Item> Items { get; }
        List<Shelf> Shelves { get; }
        void ClearCollections();
        void GenerateRandomCollections(int shelvesCount, int itemsCount);
        void FillCollectionsFromConsole();
        void FillCollectionsFromFile(string filePath);
        void PrintCollections();
    }
}
