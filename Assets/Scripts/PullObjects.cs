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
            new TomatoClass(20, 3, 500, 0.75f, "Tomato", null, 10),
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

        //Landscape
        tempLevel.AddLandscape("Level1");

        //Catapult Position
        tempLevel.SetCatapultPosition(new Vector2(-15.79f, -1.75f));

        //Blocks
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(10.21593f, -2.892341f), Quaternion.Euler(0, 0, 90.26701f));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(-0.1401955f, -6.457932f), Quaternion.Euler(0, 0, 0.041f));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(12.9698f, -6.392479f), Quaternion.Euler(0, 0, 0.155f));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(2.149655f, -2.937033f), Quaternion.Euler(0, 0, 90.269f));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(7.500449f, -6.418904f), Quaternion.Euler(0, 0, 0.09100001f));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(4.529908f, -6.435939f), Quaternion.Euler(0, 0, 0.056f));

        //Music
        tempLevel.MusicTitle = "Dutty";

        //Scores
        tempLevel.ScoreForTwoStars = 1180;
        tempLevel.ScoreForThreeStars = 1680;

        //Meat
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(2.22f, -8.89f));
        tempLevel.AddMeat(Meats[1].Clone(), new Vector2(9.278996f, -8.77f));
        tempLevel.AddMeat(Meats[2].Clone(), new Vector2(10.33f, -1.57f));

        //Max Vegetables
        tempLevel.MaxVegetables = 6;

        Levels.Add(tempLevel);
        #endregion

        #region Level 2
        tempLevel = new Level();
        //Landscape
        tempLevel.AddLandscape("Level2");

        //Catapult Position
        tempLevel.SetCatapultPosition(new Vector2(-15.79f, -1.75f));

        //Blocks
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(3.793869f, -3.62f), Quaternion.Euler(0, 0, 0));
        tempLevel.AddBlock(Blocks[2].Clone(), new Vector2(12.51774f, 1.16f), Quaternion.Euler(0, 0, 0));
        tempLevel.AddBlock(Blocks[0].Clone(), new Vector2(20.29699f, 5.72f), Quaternion.Euler(0, 0, 0));

        //Music
        tempLevel.MusicTitle = "Dutty";

        //Scores
        tempLevel.ScoreForTwoStars = 1180;
        tempLevel.ScoreForThreeStars = 1680;

        //Meat
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(5.139043f, -6.091519f));
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(7.699126f, -6.091519f));
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(6.405476f, -6.091519f));
        tempLevel.AddMeat(Meats[1].Clone(), new Vector2(13.56967f, -0.9713382f));
        tempLevel.AddMeat(Meats[1].Clone(), new Vector2(15.12935f, -1.01f)); 
        tempLevel.AddMeat(Meats[2].Clone(), new Vector2(22.2418f, 3.772467f));

        //Max Vegetables
        tempLevel.MaxVegetables = 6;

        Levels.Add(tempLevel);
        #endregion

        #region Level 3
        tempLevel = new Level();
        //Landscape
        tempLevel.AddLandscape("Level3");

        //Catapult Position
        tempLevel.SetCatapultPosition(new Vector2(-15.79f, -1.75f));

        //Blocks
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(9.692039f, 3.819306f), Quaternion.Euler(0, 0, 80.50301f));
        tempLevel.AddBlock(Blocks[2].Clone(), new Vector2(-4.62f, 7.05f), Quaternion.Euler(0, 0, -102.658f));

        //Music
        tempLevel.MusicTitle = "Dutty";

        //Scores
        tempLevel.ScoreForTwoStars = 1180;
        tempLevel.ScoreForThreeStars = 1680;
        
        //Meat
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(-5.89189f, 8.407306f));
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(-4.7099f, 8.111808f));
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(-3.593577f, 7.947642f));
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(-2.345921f, 7.652145f));
        tempLevel.AddMeat(Meats[1].Clone(), new Vector2(10.82321f, 4.816171f));
        tempLevel.AddMeat(Meats[2].Clone(), new Vector2(12.08969f, 4.881282f));

        //Max Vegetables
        tempLevel.MaxVegetables = 6;

        Levels.Add(tempLevel);
        #endregion

        #region Level 4
        tempLevel = new Level();
        //Landscape
        tempLevel.AddLandscape("Level4");

        //Catapult Position
        tempLevel.SetCatapultPosition(new Vector2(-15.79f, -1.75f));
        //Blocks
        tempLevel.AddBlock(Blocks[0].Clone(), new Vector2(18.38663f, 5.199508f), Quaternion.Euler(0, 0, -0.476f));
        tempLevel.AddBlock(Blocks[0].Clone(), new Vector2(13.26352f, 3.227602f), Quaternion.Euler(0, 0, 0.7820001f));
        tempLevel.AddBlock(Blocks[0].Clone(), new Vector2(1.819896f, 3.539904f), Quaternion.Euler(0, 0, 91.31001f));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(12.52407f, 6.404523f), Quaternion.Euler(0, 0, -2.081f));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(15.94847f, 7.884973f), Quaternion.Euler(0, 0, -68.382f));
        tempLevel.AddBlock(Blocks[2].Clone(), new Vector2(-6.999947f, -0.2677824f), Quaternion.Euler(0, 0, -0.029f));
        tempLevel.AddBlock(Blocks[2].Clone(), new Vector2(-0.2620025f, -0.02361375f), Quaternion.Euler(0, 0, 1.303f));
        tempLevel.AddBlock(Blocks[2].Clone(), new Vector2(3.830903f, 0.06998272f), Quaternion.Euler(0, 0, 1.32f));

        //Music
        tempLevel.MusicTitle = "Dutty";

        //Scores
        tempLevel.ScoreForTwoStars = 1180;
        tempLevel.ScoreForThreeStars = 1680;

        //Meat
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(2.310153f, -2.369045f));
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(0.85f, -2.369045f));
        tempLevel.AddMeat(Meats[0].Clone(), new Vector2(16.87f, 3.9f));
        tempLevel.AddMeat(Meats[1].Clone(), new Vector2(-3.691193f, -4.565026f));
        tempLevel.AddMeat(Meats[2].Clone(), new Vector2(15.1f, 4.3f));

        //Max Vegetables
        tempLevel.MaxVegetables = 6;

        Levels.Add(tempLevel);
        #endregion

        #region Level 5
        tempLevel = new Level();
        //Landscape
        tempLevel.AddLandscape("Level5");

        //Catapult Position
        tempLevel.SetCatapultPosition(new Vector2(-15.79f, -1.75f));

        //Blocks
        tempLevel.AddBlock(Blocks[0].Clone(), new Vector2(3.822095f, 2.73f), Quaternion.Euler(0, 0, 0));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(4.561045f, 2.75f), Quaternion.Euler(0, 0, 0));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(16.7f, -7.1f), Quaternion.Euler(0, 0, 0));
        tempLevel.AddBlock(Blocks[2].Clone(), new Vector2(5.3f, 2.73f), Quaternion.Euler(0, 0, 0));

        //Music
        tempLevel.MusicTitle = "Dutty";

        //Scores
        tempLevel.ScoreForTwoStars = 1180;
        tempLevel.ScoreForThreeStars = 1680;

        //Meat
        tempLevel.AddMeat(Meats[1].Clone(), new Vector2(18.55424f, -0.5403616f));
        tempLevel.AddMeat(Meats[1].Clone(), new Vector2(18.92f, -8.62f));
        tempLevel.AddMeat(Meats[2].Clone(), new Vector2(20.98222f, -8.4f));

        //Max Vegetables
        tempLevel.MaxVegetables = 6;

        Levels.Add(tempLevel);
        #endregion

        #region Level 6
        tempLevel = new Level();
        //Landscape
        tempLevel.AddLandscape("Level6");

        //Catapult Position
        tempLevel.SetCatapultPosition(new Vector2(-15.79f, -1.75f));

        //Blocks
        tempLevel.AddBlock(Blocks[0].Clone(), new Vector2(-0.8759069f, -2.033174f), Quaternion.Euler(0, 0, 1.426f));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(8.283579f, -0.7703575f), Quaternion.Euler(0, 0, -1.76f));
        tempLevel.AddBlock(Blocks[1].Clone(), new Vector2(15.42211f, -3.155972f), Quaternion.Euler(0, 0, 89.954f));

        //Music
        tempLevel.MusicTitle = "Dutty";

        //Scores
        tempLevel.ScoreForTwoStars = 1180;
        tempLevel.ScoreForThreeStars = 1680;

        //Meat
        tempLevel.AddMeat(Meats[1].Clone(), new Vector2(1.54935f, -4.505739f));
        tempLevel.AddMeat(Meats[2].Clone(), new Vector2(8.087934f, -7.874353f));

        //Max Vegetables
        tempLevel.MaxVegetables = 6;

        Levels.Add(tempLevel);
        #endregion
    }
}
