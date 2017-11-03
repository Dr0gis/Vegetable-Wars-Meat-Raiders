using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PullObjects : MonoBehaviour
{
    public List<VegetableClass> Vegatables;
    public List<BlockClass> Blocks;
    public List<MeatClass> Meats;

    void Start()
    {
        Vegatables = new List<VegetableClass>
        {
            new PotatoClass(4, 1, 1, "Potato", null),
            new TomatoClass(1, 3, 0.75f, "Tomato", null),
            new CabbageClass(10, 5, 0.5f, "Cabbage", null)
        };

        Blocks = new List<BlockClass>
        {
            new DishClass(5, 30, "Dish", null),
            new CookieClass(1, 20, "Cookie", null),
            new BreadClass(3, 25, "Bread", null)
        };

        Meats = new List<MeatClass>
        {
            new MeatClass(1, 1, 10, "MeatSmall", null, false),
            new MeatClass(2, 2, 20, "MeatMiddle", null, false),
            new MeatClass(3, 3, 30, "MeatBig", null, false)
        };
    }
}
