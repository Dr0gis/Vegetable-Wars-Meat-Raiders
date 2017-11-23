﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ProgressState : ScriptableObject
{
    public class SavableVegetableList : IList<VegetableClass>
    {
        private ProgressState state;
        private int levelIndx;

        public SavableVegetableList(int newLevelIndex, ProgressState newState)
        {
            levelIndx = newLevelIndex;
            state = newState;
        }

        public int Count { get { return state.vegetablesOnLevel[levelIndx].Count; } }

        public bool IsReadOnly { get { return false; } }

        public VegetableClass this[int index]
        {
            get
            {
                return state.vegetablesOnLevel[levelIndx][index];
            }

            set
            {
                state.vegetablesOnLevel[levelIndx][index] = value;
                state.saveVegetable(levelIndx, index, state.vegetablesOnLevel[levelIndx][index].Health, state.vegetablesOnLevel[levelIndx][index].Damage,
                                                state.vegetablesOnLevel[levelIndx][index].Speed, state.vegetablesOnLevel[levelIndx][index].Prefab);
            }
        }

        public int IndexOf(VegetableClass item)
        {
            return state.vegetablesOnLevel[levelIndx].IndexOf(item);
        }

        public void Insert(int index, VegetableClass item)
        {
            state.vegetablesOnLevel[levelIndx].Insert(index, item);
            state.saveVegetablesOnLevel(levelIndx);
        }

        public void RemoveAt(int index)
        {
            if (index < state.vegetablesOnLevel[levelIndx].Count)
            {
                state.forgetVegetable(levelIndx, state.vegetablesOnLevel[levelIndx].Count - 1);
            }

            state.vegetablesOnLevel[levelIndx].RemoveAt(index);

            state.saveVegetablesOnLevel(levelIndx);
        }

        public void Add(VegetableClass item)
        {
            state.vegetablesOnLevel[levelIndx].Add(item);
            state.saveVegetablesOnLevel(levelIndx);
        }

        public void Clear()
        {
            state.vegetablesOnLevel[levelIndx].Clear();
            state.forgetOnLevel(levelIndx);
        }

        public bool Contains(VegetableClass item)
        {
            return state.vegetablesOnLevel[levelIndx].Contains(item);
        }

        public void CopyTo(VegetableClass[] array, int arrayIndex)
        {
            state.vegetablesOnLevel[levelIndx].CopyTo(array, arrayIndex);
        }

        public bool Remove(VegetableClass item)
        {
            bool removed = state.vegetablesOnLevel[levelIndx].Remove(item);
            if (removed)
            {
                state.forgetVegetable(levelIndx, state.vegetablesOnLevel[levelIndx].Count - 1);
                state.saveVegetablesOnLevel(levelIndx);
            }
            return removed;
        }

        public IEnumerator<VegetableClass> GetEnumerator()
        {
            return state.vegetablesOnLevel[levelIndx].GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return state.vegetablesOnLevel[levelIndx].GetEnumerator();
        }
    }

    private int lastAvaliableLevelId;
    private int amountOfMoney;
    private List<int> starsOnLevel;
    private List<int> scoreOnLevel;
    private List<List<VegetableClass>> vegetablesOnLevel;

    public void LoadState()
    {
        lastAvaliableLevelId = PlayerPrefs.GetInt("lastAvaliableLevelId", 0);
        amountOfMoney = PlayerPrefs.GetInt("amountOfMoney", 0);
        starsOnLevel = new List<int>();
        scoreOnLevel = new List<int>();
        for (int i = 0; ; ++i)
        {
            int stars = PlayerPrefs.GetInt("starsOnLevel " + i, -1);
            if (stars != -1)
            {
                starsOnLevel.Add(stars);
            }
            else
            {
                break;
            }
        }
        for (int i = 0; ; ++i)
        {
            int score = PlayerPrefs.GetInt("scoreOnLevel " + i, -1);
            if (score != -1)
            {
                scoreOnLevel.Add(score);
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i <= lastAvaliableLevelId + 1; ++i)
        {
            for (int j = 0; ; ++j)
            {
                int health = PlayerPrefs.GetInt("onLevel " + i + " vegetable " + j + " health", -1);
                float damage = PlayerPrefs.GetFloat("onLevel " + i + " vegetable " + j + " damage", -1);
                float speed = PlayerPrefs.GetFloat("onLevel " + i + " vegetable " + j + " speed", -1);
                string prefab = PlayerPrefs.GetString("onLevel " + i + " vegetable " + j + " prefab", null);

                if (health != -1 && damage != -1 && speed != -1 && prefab != null)
                {
                    if (vegetablesOnLevel.Count == i)
                    {
                        vegetablesOnLevel.Add(new List<VegetableClass>());
                    }
                    switch (prefab)
                    {
                        case "Potato":
                            vegetablesOnLevel[i].Add(new PotatoClass(health, damage, speed, "Potato", null));
                            break;
                        case "Tomato":
                            vegetablesOnLevel[i].Add(new TomatoClass(health, damage, speed, "Potato", null));
                            break;
                        case "Cabbage":
                            vegetablesOnLevel[i].Add(new CabbageClass(health, damage, speed, "Potato", null));
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }

    private void forgetVegetable(int level, int index)
    {
        PlayerPrefs.DeleteKey("onLevel " + level + " vegetable " + index + " health");
        PlayerPrefs.DeleteKey("onLevel " + level + " vegetable " + index + " damage");
        PlayerPrefs.DeleteKey("onLevel " + level + " vegetable " + index + " speed");
        PlayerPrefs.DeleteKey("onLevel " + level + " vegetable " + index + " prefab");
    }

    private void forgetOnLevel(int level)
    {
        for (int i = 0; i < vegetablesOnLevel[level].Count; ++i)
        {
            forgetVegetable(level, i);
        }
    }

    private void saveVegetable(int level, int index, int health, float damage, float speed, string prefab)
    {
        PlayerPrefs.SetInt("onLevel " + level + " vegetable " + index + " health", health);
        PlayerPrefs.SetFloat("onLevel " + level + " vegetable " + index + " damage", damage);
        PlayerPrefs.SetFloat("onLevel " + level + " vegetable " + index + " speed", speed);
        PlayerPrefs.SetString("onLevel " + level + " vegetable " + index + " prefab", prefab);
    }

    private void saveVegetablesOnLevel(int i)
    {
        for (int j = 0; j < vegetablesOnLevel[i].Count; ++j)
        {
            saveVegetable(i, j, vegetablesOnLevel[i][j].Health, vegetablesOnLevel[i][j].Damage, vegetablesOnLevel[i][j].Speed, vegetablesOnLevel[i][j].Prefab);
        }
    }

    public void SaveVegetables()
    {
        for (int i = 0; i < vegetablesOnLevel.Count; ++i)
        {
            saveVegetablesOnLevel(i);
        }
    }

    public void SaveState()
    {
        PlayerPrefs.SetInt("lastAvaliableLevelId", lastAvaliableLevelId);
        PlayerPrefs.SetInt("amountOfMoney", amountOfMoney);

        for (int i = 0; i < starsOnLevel.Count ; ++i)
        {
            PlayerPrefs.SetInt("starsOnLevel " + i, starsOnLevel[i]);
        }
        for (int i = 0; i < scoreOnLevel.Count; ++i)
        {
            PlayerPrefs.SetInt("scoreOnLevel " + i, scoreOnLevel[i]);
        }

        SaveVegetables();
    }

    public int LastAvaliableLevelId
    {
        get
        {
            return lastAvaliableLevelId;
        }
        set
        {
            lastAvaliableLevelId = value;
            PlayerPrefs.SetInt("lastAvaliableLevelId", lastAvaliableLevelId);
            vegetablesOnLevel.Add(new List<VegetableClass>());
        }
    }

    public int AmountOfMoney
    {
        get
        {
            return amountOfMoney;
        }
        set
        {
            amountOfMoney = value;
            PlayerPrefs.SetInt("amountOfMoney", amountOfMoney);
        }
    }

    public bool IsLevelAvaliable(int index)
    {
        return index <= LastAvaliableLevelId;
    }

    public int GetStarsOnLevel(int index)
    {
        if (index >= LastAvaliableLevelId)
        {
            return 0;
        }
        return starsOnLevel[index];
    }

    public int GetScoreOnLevel(int index)
    {
        if (index >= LastAvaliableLevelId)
        {
            return 0;
        }
        return scoreOnLevel[index];
    }

    public void SetScoreOnLevel(int index, int score)
    {
        if (index == LastAvaliableLevelId)
        {
            LastAvaliableLevelId = LastAvaliableLevelId + 1;
        }
        if (index == scoreOnLevel.Count)
        {
            scoreOnLevel.Add(0);
        }
        if (index < LastAvaliableLevelId)
        {
            scoreOnLevel[index] = score;
            PlayerPrefs.SetInt("scoreOnLevel " + index, score);
        }
    }

    public void SetStarsOnLevel(int index, int stars)
    {
        if (index == LastAvaliableLevelId)
        {
            LastAvaliableLevelId = LastAvaliableLevelId + 1;
        }
        if (index == starsOnLevel.Count)
        {
            starsOnLevel.Add(0);
        }
        if (index < LastAvaliableLevelId)
        {
            starsOnLevel[index] = stars;
            PlayerPrefs.SetInt("starsOnLevel " + index, stars);
        }
    }

    public SavableVegetableList GetVegetablesOnLevel(int index)
    {
        if (index < vegetablesOnLevel.Count)
        {
            return new SavableVegetableList(index, this);
        }
        return null;
    }
}
