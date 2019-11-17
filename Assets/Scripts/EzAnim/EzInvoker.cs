using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzInvoker : MonoBehaviour
{
    [SerializeField] private EzPlayer player;
    [SerializeField] private EzEvent ezEvent;
    [SerializeField] private bool waitForEnd = true;

    private void OnValidate()
    {
        if (player is null) { GetComponent<EzPlayer>(); }
    }

    public void Invoke()
    {
        if (waitForEnd && player.enabled) { return; }
        player.Play(ezEvent);
    }
}
