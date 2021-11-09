using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")] // faz aparecer no menu da unity
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence;

    public List<Sentences> dialogues = new List<Sentences>();
}

[System.Serializable]
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Language sentence;
}

[System.Serializable]
public class Language
{
    public string portuguese;
    public string english;
    public string spanish;
}

// só roda dentro da unity
#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]    
public class BuilderEditor : Editor
{
    // override = reescreve metodo
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // permite modificar o inspector da unity

        DialogueSettings ds = (DialogueSettings)target;

        Language l = new Language();
        l.portuguese = ds.sentence;

        Sentences s = new Sentences();
        s.profile = ds.speakerSprite;
        s.sentence = l;

        if(GUILayout.Button("Create Dialogue"))
        {
            if(ds.sentence != "")
            {
                ds.dialogues.Add(s);
                ds.speakerSprite = null;
                ds.sentence = "";
            }
        }
    }
}

#endif 
