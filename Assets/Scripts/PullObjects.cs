using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PullObjects : MonoBehaviour
{
    public List<VegetableClass> Vegetables;
    public List<BlockClass> Blocks;
    public List<MeatClass> Meats;
    public List<Level> Levels;

    public void Initiate()
    {
        Vegetables = new List<VegetableClass>
        {
            new PotatoClass(4, 1, 500, 1, "Potato", null, 0),
            new TomatoClass(1, 3, 500, 0.75f, "Tomato", null, 10),
            new CabbageClass(10, 5, 500, 0.5f, "Cabbage", null, 20)
        };

        Blocks = new List<BlockClass>
        {
            new DishClass(5, PhysicsConstants.DefaultDamage, 5, "Dish", null, "DishSound"),
            new CookieClass(1, PhysicsConstants.DefaultDamage, 5, "Cookie", null, "CookieSound"),
            new BreadClass(3, PhysicsConstants.DefaultDamage, 5, "Bread", null, "BreadSound")
        };

        Meats = new List<MeatClass>
        {
            new MeatClass(1, 1, 50, "MeatSmall", null, false),
            new MeatClass(2, 2, 60, "MeatMiddle", null, false),
            new MeatClass(3, 3, 70, "MeatBig", null, false)
        };

        InitiateLevels();
    }
    public void InitiateLevels()
    {
        Levels = new List<Level>();
        Level tempLevel = new Level();

        #region Level 1
        //Blocks
        tempLevel.AddBlock(Blocks[0].Clone(), new Vector2(6, -7.5f));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(8, -7.5f));
        tempLevel.AddBlock(Blocks[2].Clone(), new Vector2(11, -7.5f));

        //Music
        tempLevel.MusicTitle = "Dutty";

        //Scores
        tempLevel.ScoreForTwoStars = 1180;
        tempLevel.ScoreForThreeStars = 1680;

        //Meat
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(15, -9));
        tempLevel.AddMeat(Meats[1].Clone(), new Vector2(17, -8.7f));
        tempLevel.AddMeat(Meats[2].Clone(), new Vector2(19, -8.5f));

        //Max Vegetables
        tempLevel.MaxVegetables = 6;

        Levels.Add(tempLevel);
        #endregion

        #region Level 2
        tempLevel = new Level();
        
        //Blocks
        tempLevel.AddBlock(Blocks[0].Clone(), new Vector2(7, -7.5f));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(10, -7.5f));
        tempLevel.AddBlock(Blocks[2].Clone(), new Vector2(15, -7.5f));

        //Music
        tempLevel.MusicTitle = "";

        //Scores
        tempLevel.ScoreForTwoStars = 1180;
        tempLevel.ScoreForThreeStars = 1680;

        //Meat
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(8.5f, -7.5f));
        tempLevel.AddMeat(Meats[1].Clone(), new Vector2(13, -7.5f));
        tempLevel.AddMeat(Meats[2].Clone(), new Vector2(17, -8.5f));

        //Max Vegetables
        tempLevel.MaxVegetables = 6;

        Levels.Add(tempLevel);
        #endregion
    }
}
