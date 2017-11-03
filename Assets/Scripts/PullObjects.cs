using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObjects : MonoBehaviour
{
    // public List<VegatableClass> Vegatables;
    // public List<BlockClass> Blocks;
    public List<MeatClass> Meats;

    void Start()
    {
        // Vegatables = new List<VegatableClass>();

        // Vegatables.Add(new PotatoClass());
        // Vegatables.Add(new PomidorClass());
        // Vegatables.Add(new KapustaClass());

        // Blocks = new List<BlockClass>();

        // Blocks.Add(new TarelkaClass());
        // Blocks.Add(new CrekerClass());
        // Blocks.Add(new HlebClass());

        Meats = new List<MeatClass>();

        Meats.Add(new MeatClass(1, "MeatSmall", null, false));
        Meats.Add(new MeatClass(2, "MeatMiddle", null, false));
        Meats.Add(new MeatClass(3, "MeatBig", null, false));
    }
}
