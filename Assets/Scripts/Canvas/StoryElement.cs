using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryElement : MonoBehaviour
{
    public Dialog dialogs;

    private void Start()
    {
        TriggerStory();
    }

    public void TriggerStory()
    {
        DialogManager.Instance.PlayStory(dialogs);
    }
}
