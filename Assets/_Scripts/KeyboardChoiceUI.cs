using UnityEngine;

public class KeyboardChoiceUI : MonoBehaviour
{
    public GameObject qwertyKeyboard;
    public GameObject azertyKeyboard;

    private bool p_isAxisInUse;
    private bool isQwerty;

    private void OnEnable()
    {
        if (PlayerPrefs.GetString("Keyboard") == "azerty")
        {
            azertyKeyboard.SetActive(true);
            qwertyKeyboard.SetActive(false);
            isQwerty = false;
        }
        else
        {
            azertyKeyboard.SetActive(false);
            qwertyKeyboard.SetActive(true);
            isQwerty = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (azertyHorizontal() || qwertyHorizontal())
        {
            if (!p_isAxisInUse)
            {
                switchKeyboard();
                p_isAxisInUse = true;
            }
        }
        else
            p_isAxisInUse = false;
    }

    private void OnDisable()
    {
        saveKeyboard();
    }

    public bool azertyHorizontal()
    {
        return azertyKeyboard.activeInHierarchy && Input.GetAxisRaw("Horizontal") != 0;
    }

    public bool qwertyHorizontal()
    {
        return qwertyKeyboard.activeInHierarchy && Input.GetAxisRaw("HorizontalQwerty") != 0;
    }

    public void switchKeyboard()
    {
        isQwerty = !isQwerty;
        qwertyKeyboard.SetActive(isQwerty);
        azertyKeyboard.SetActive(!isQwerty);
    }

    public void saveKeyboard()
    {
        if (isQwerty)
        {
            PlayerPrefs.SetString("Keyboard", "qwerty");
        }
        else
        {
            PlayerPrefs.SetString("Keyboard", "azerty");
        }
    }
}