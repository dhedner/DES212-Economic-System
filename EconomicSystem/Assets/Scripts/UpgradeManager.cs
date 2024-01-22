using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Currency geneticMaterial;
    public Currency DNA;
    public Currency genome;

    public Button GenerateUpgradeButton;

    private int[] _upgradeCost = { 10, 30, 70, 150, 280 };
    private int _upgradeLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (GenerateUpgradeButton != null)
        {
            GenerateUpgradeButton.onClick.AddListener(ApplyUpgrade);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("GameplayCanvas/Upgrades/Phase1UpgradeButton/UpgradeLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "LV: " + _upgradeLevel.ToString() + "/5";
    }

    void ApplyUpgrade()
    {
        if (geneticMaterial.amount < _upgradeCost[_upgradeLevel])
        {
            Debug.Log("Not enough Genetic Material to upgrade generation");
            return;
        }

        geneticMaterial.amount -= _upgradeCost[_upgradeLevel];
        _upgradeLevel += 1;
        Debug.Log("Upgrade Purchased (LV: " + _upgradeLevel + ")");
        //geneticMaterial.rate += 1;
    }
}
