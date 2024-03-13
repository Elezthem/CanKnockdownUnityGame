using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager :  MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource fxSource;      
    [SerializeField] private AudioClip canHitFx, gameOverFx, gameWinFx, ballSpawnFx, ballThrowFx;  

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayFx(FxTypes fxType)
    {
        switch (fxType)                                 //switch case is used to run respective logic for respective FxType
        {
            case FxTypes.CANHIT:                        //if its CanHit
                fxSource.PlayOneShot(canHitFx);         //play canhit fx
                break;
            case FxTypes.GAMEOVER:                      //if its GAMEOVER
                fxSource.PlayOneShot(gameOverFx);       //play GAMEOVER fx
                break;
            case FxTypes.GAMEWIN:                       //if its GAMEWIN
                fxSource.PlayOneShot(gameWinFx);        //play GAMEWIN fx
                break;
            case FxTypes.BALLTHROW:
                fxSource.PlayOneShot(ballThrowFx);      
                break;
            case FxTypes.BALLSPAWN:                     
                fxSource.PlayOneShot(ballSpawnFx);      
                break;
        }
    }
}

public enum FxTypes
{
    CANHIT,
    GAMEOVER,
    GAMEWIN,
    BALLTHROW,
    BALLSPAWN
}