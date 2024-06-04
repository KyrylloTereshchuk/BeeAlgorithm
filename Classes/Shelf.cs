using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Shelf
{
    public int MaxWeight { get; set; }
    public int MaxVolume { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();

    public int CurrentWeight => Items.Sum(item => item.Weight);
    public int CurrentVolume => Items.Sum(item => item.Volume);
    public int CurrentValue => Items.Sum(item => item.Value);
}
