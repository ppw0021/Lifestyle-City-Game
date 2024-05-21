using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlotMachine : MonoBehaviour
{
    public Image[] slots;
    public Sprite[] symbols;
    public float spinTime = 1.0f; // Time for each spin
    private bool spinning;
    public GameObject slotsWindow;
    [SerializeField]

    private void Start()
    {
        // Make the slotsWindow inactive on game start
        slotsWindow.SetActive(false);
        spinning = false;
    }

    

    public void Spin()
    {
        if (!spinning)
        {
            int currentCoins = InterfaceAPI.getCoins();
            if (currentCoins >= 100) // Check if the player has enough coins
            {
                InterfaceAPI.setCoins(currentCoins - 100); // Subtract 100 coins
                StartCoroutine(SpinReels());
            }
            else
            {
                Debug.Log("Not enough coins to spin.");
            }
        }
    }

    private IEnumerator SpinReels()
    {
        spinning = true;
        float timer = 0;
        while (timer < spinTime)
        {
            foreach (var slot in slots)
            {
                slot.sprite = symbols[Random.Range(0, symbols.Length)];
            }
            timer += Time.deltaTime;
            yield return null;
        }
        spinning = false;

        // Check for win conditions
        int coinsWon = CheckWinConditions();
        if (coinsWon > 0)
        {
            int currentCoins = InterfaceAPI.getCoins();
            InterfaceAPI.setCoins(currentCoins + coinsWon); // Add coins based on win condition
            Debug.Log("You won $" + coinsWon);
        }
    }

    private int CheckWinConditions()
    {
        int coinsWon = 0;

        if (slots.Length < 3)
        {
            Debug.LogError("Not enough slots to check win conditions.");
            return coinsWon;
        }

        // Check if all three slots show the same symbol
        if (slots[0].sprite == slots[1].sprite && slots[1].sprite == slots[2].sprite)
        {
            coinsWon += 1000; // Win condition: All three slots are the same
        }
        // Check if any two slots show the same symbol
        else if (slots[0].sprite == slots[1].sprite || slots[0].sprite == slots[2].sprite || slots[1].sprite == slots[2].sprite)
        {
            coinsWon += 150; // Win condition: Any two slots are the same
        }

        return coinsWon;
    }
}
