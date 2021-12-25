using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/Dialog", order = 1)]
public class DialogData : ScriptableObject
{
    [SerializeField] private List<Sentence> dialogSentences;


    public List<Sentence> GetSentence()
    {
        return dialogSentences;
    }
    

    public enum Speaker
    {
        Player,
        NPC
    }

    [Serializable] 
    public struct Sentence
    {
        [SerializeField] private Speaker speaker;
        [SerializeField][TextArea(3, 10)] private string sentence;
        [SerializeField] private bool sentenceWithChoice;
        [SerializeField] private int leftButtonNextDialogIndex;
        [SerializeField] private int rightButtonNextDialogIndex;
        [SerializeField] private bool isHasReaction;
        [SerializeField] private bool isAngry;
        [SerializeField] private bool isEndOfDialog;

        public string SentenceText => sentence;
        public Speaker Speaker => speaker;
        public bool SentenceWithChoice => sentenceWithChoice;
        public bool IsHasReaction => isHasReaction;
        public bool IsAngry => isAngry;
        public bool IsEndOfDialog => isEndOfDialog; //ставить на непоследних репликах, если хочешь закончить это говно

        public int LeftButtonNextDialogIndex => leftButtonNextDialogIndex;
        public int RightButtonNextDialogIndex => rightButtonNextDialogIndex; //right always should be larger then left (sorry for govnocode)

    }
    
    
    
    





}