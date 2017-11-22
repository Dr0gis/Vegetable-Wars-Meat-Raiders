using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreviewObjectManager : MonoBehaviour {

    public List<BlockClass> AvailableBlocks;
    public List<MeatClass> AvailableMeats;
    public int CurrentLevel;
    public Button ShopButton;

    public List<BlockClass> GetAvailableBlocks()
    {
        List<BlockClass> listBlocks = GetComponent<PullObjects>().Levels[CurrentLevel].Blocks;
        return listBlocks;
    }

    public List<MeatClass> GetAvailableMeats()
    {
        List<MeatClass> listMeats = GetComponent<PullObjects>().Levels[CurrentLevel].Meats;
        return listMeats;
    }

    public void Initiate()
    {
        GetComponent<PullObjects>().Initiate();
        AvailableBlocks = GetAvailableBlocks();
        foreach (var block in AvailableBlocks)
        {
            block.CurrentGameObject = Instantiate((GameObject)Resources.Load(block.Prefab), block.Position, block.Rotation);
            block.CurrentGameObject.GetComponent<BlockController>().Block = block;
        }

        AvailableMeats = GetAvailableMeats();
        foreach (var meat in AvailableMeats)
        {
            meat.CurrentGameObject = Instantiate((GameObject)Resources.Load(meat.Prefab), meat.Position, Quaternion.identity);
            meat.CurrentGameObject.GetComponent<MeatController>().Meat = meat;
        }
    }

    void Start()
    {
        Initiate();
        ShopButton.onClick.AddListener(ShopButtonListener);
    }

    private void ShopButtonListener()
    {
        //save camera size and position here
        SceneManager.LoadScene("VegetablesSelection");
    }
}
