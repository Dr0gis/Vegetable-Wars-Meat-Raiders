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
    public List<GameObject> Landscapes;
    public int ScoreForTwoStars;
    public int ScoreForThreeStars;
    public string MusicTitle;
    public Vector2 CatapultPosition;

    public int MaxVegetables { get; set; }
    
    public Level()
    {
        Blocks = new List<BlockClass>();
        Meats = new List<MeatClass>();
        Landscapes = new List<GameObject>();
    }
    
    public void AddBlock(BlockClass block, Vector2 Position, Quaternion Rotation)
    {
        block.Position = Position;
        block.Rotation = Rotation;
        Blocks.Add(block);
    }
    public void AddMeat(MeatClass Meat, Vector2 Position)
    {
        Meat.Position = Position;
        Meats.Add(Meat);
    }
    public void AddLandscape (string title)
    {
        Landscapes.Add((GameObject) Resources.Load("Levels/" + title));
    }

    public void SetCatapultPosition(Vector2 Position)
    {
        CatapultPosition = Position;   
    }
}
