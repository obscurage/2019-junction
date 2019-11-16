using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EzEvent", menuName = "Ez/Event", order = 0)]
public class EzEvent : ScriptableObject
{
    [SerializeField] private List<EzAction> actions = new List<EzAction>();
    public List<EzAction> Actions { get => actions; }
}
