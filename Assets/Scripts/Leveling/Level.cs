using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Level
{
    public List<BlockClass> Blocks;
    public List<MeatClass> Meats;
    public GameObject Landscape;
    public int ScoreForTwoStars;
    public int ScoreForThreeStars;
    public int MaxVegetables { get; set; }
    
    public Level()
    {
        Blocks = new List<BlockClass>();
        Meats = new List<MeatClass>();
    }
    
    public void AddBlock(BlockClass block, Vector2 Position)
    {
        block.Position = Position;
        block.Rotation = Quaternion.identity;
        Blocks.Add(block);
    }
    public void AddMeat(MeatClass Meat, Vector2 Position)
    {
        Meat.Position = Position;
        Meats.Add(Meat);
    }
}
