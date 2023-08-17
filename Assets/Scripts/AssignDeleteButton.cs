using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class AssignDeleteButton : MonoBehaviour
{
    [SerializeField]
    public List<Transform> buttonTransforms = new ();

    [SerializeField]
    public List<Transform> deleteButtonTransforms = new ();

    [SerializeField]
    public int fileDeLoadNumber;

    public Button finalDeleteButton;

    public SaveLoad saveLoad;


    //public Button myButton; // Reference to your Unity Button
    public string itemName; // The text you want to set
    public TMP_InputField inputField;

    public bool letDelete= false;



    private void Start()
    {
        for (int i = 0; i < buttonTransforms.Count; i++)
        {
            Button button = buttonTransforms[i].GetComponent<Button>();
            if (button != null)
            {
                int buttonIndex = i; // Capture the index in a local variable
                button.onClick.AddListener(() => AssignButton(buttonIndex));
            }
            
        }
        for (int i = 0; i < deleteButtonTransforms.Count; i++)
        {
           
            Button button = deleteButtonTransforms[i].GetComponent<Button>();
            if (button != null)
            {
                int deleteButtonIndex = i; // Capture the index in a local variable
                button.onClick.AddListener(() => AssignConfirmDelete(deleteButtonIndex));
                
            }
        }
        List<int> fileNumbers = SaveSystem.GetFileNumbers();
        List<string> itemNames = SaveSystem.ReadItemNames();
        if(itemNames.Count != 0)
        {
             
            for (int i = 0; i < fileNumbers.Count; i++)
            {
                Button button = buttonTransforms[i].GetComponent<Button>();
                if (button != null)
                {
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                    if (buttonText != null)
                    {
                        buttonText.text = itemNames[i];
                    }
                    else
                    {
                        Debug.LogError("TextMeshProUGUI component not found on the button.");
                    }
                }
                else
                {
                    Debug.LogError("Button component not found on the button transform.");
                }
            }
        } 
    }
    
    public int AssignConfirmDelete(int deleteButtonIndex)   
    {
        List<int> fileNumbers = SaveSystem.GetFileNumbers();

        if (deleteButtonIndex >= 0 && deleteButtonIndex < fileNumbers.Count)
        {
            fileDeLoadNumber = fileNumbers[deleteButtonIndex];
            Debug.Log("got deload num: " + fileDeLoadNumber);
            return fileDeLoadNumber;
            
        }
        else
        {
            Debug.Log("zzzz");
            return -1;
            
        }
    }
 
    public void DeleteFile()
    {
        int x = fileDeLoadNumber;
        SaveSystem.loadNumber = fileDeLoadNumber; 
        Debug.Log("Deleted file number: " + x);
        SaveSystem.Delete();
    }

    public void GetItemName()
    {
        itemName = inputField.text;
        Debug.Log("Got name : " + itemName);
    }

    public void AssignButton(int buttonIndex)
    {
        List<int> fileNumbers = SaveSystem.GetFileNumbers();

        if (buttonIndex >= 0 && buttonIndex < fileNumbers.Count)
        {
            int fileLoadNumber = fileNumbers[buttonIndex];
            Debug.Log("Number at index " + buttonIndex + ": " + fileLoadNumber);

            OpenFile(fileLoadNumber);
        }
        else
        {
            Debug.LogError("Index out of range.");
        }
    }
    
   
    public void OpenFile(int fileLoadNumber)
    {
        saveLoad.loadInt = fileLoadNumber;
        saveLoad.Load();
    }

    /* public void GetIDNumber(int buttonIndex)
     {
         Transform selectedButon = buttonTransforms[buttonIndex].transform;
         Debug.Log(selectedButon.name);
     }*/
}
