using System;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [Header("Game")]
    public static bool AllRescued;
    public static DateTime TimePlayed;
    private DateTime _startTime;
    private DateTime _endTime;

    [Header("Nave")]
    public static bool PlayerLanded;
    public static int Lives;
    public static Vector2 CurrentCheckPoint;

    [Header("Prisioneros")]
    public static int TotalPrisoners;
    public static int PrisonersNeeded;
    public static int PrisonersRescued;
    public static int PrisonersAboard;
    public static int PrisonersLeft;
    public static int PrisonersKilled;

    [Header("Info")] 
    [SerializeField] private bool _AllRescued;
    [SerializeField] private bool _PlayerLanded;
    [SerializeField] private int _Lives;
    [SerializeField] private int _TotalPrisoners;
    [SerializeField] private int _PrisonersNeeded;
    [SerializeField] private int _PrisonersRescued;
    [SerializeField] private int _PrisonersAboard;
    [SerializeField] private int _PrisonersLeft;
    [SerializeField] private int _PrisonersKilled;

    private void Start()
    {
        _startTime = new DateTime();
        _endTime = new DateTime();
        TimePlayed = new DateTime();
        //
        TotalPrisoners = GetLevelPrisoners();
        if (TotalPrisoners < 1)
        {
            Debug.LogError("NO HAY PRISIONEROS EN ESTE NIVEL!!!");
            Debug.DebugBreak();
        }
        else
        {
            //por defecto aplico porcentaje de la mayoría, para completar el nivel
            // (WIP, derivar en modo de juego propio...)
            PrisonersNeeded = Mathf.CeilToInt((float)TotalPrisoners * 0.8f);
        }

        //
        _startTime = DateTime.Now;
        Lives = 3;
        //
        CurrentCheckPoint = Vector2.zero;
    }

    private void Update()
    {
        AllRescued = PrisonersRescued >= PrisonersNeeded;
        PrisonersLeft = TotalPrisoners - (PrisonersRescued + PrisonersKilled);

        if (Lives < 1)
        {
            GameOver(0);
        }
        else if (AllRescued)
        {
            GameOver(1);
        }
        else if (PrisonersNeeded - PrisonersRescued > PrisonersLeft)
        {
            GameOver(2);
        }
        
        //relleno info privada
        _AllRescued = AllRescued;
        _PlayerLanded = PlayerLanded;
        _Lives = Lives;
        _TotalPrisoners = TotalPrisoners;
        _PrisonersNeeded = PrisonersNeeded;
        _PrisonersRescued = PrisonersRescued;
        _PrisonersAboard = PrisonersAboard;
        _PrisonersLeft = PrisonersLeft;
        _PrisonersKilled = PrisonersKilled;
    }

    private void GameOver(int result)
    {
        _endTime = DateTime.Now;
        
        switch (result)
        {
            case 0:     // 0 - pierde todas las vidas
                Debug.Log("GAME OVER! No te quedan más vidas!");
                Debug.Break();
                break;
            case 1:     // 1 - gana la partida
                Debug.Log("YOU WIN! Level Complete!");
                Debug.Break();
                break;
            case 2:     // 2 - mueren más minions de los necesarios para completar el nivel
                Debug.Log("GAME OVER! Murieron más prisioneros de los necesarios!");
                Debug.Break();
                break;
            case 3:     // 3 - pierdes, otros motivos
                Debug.Log("GAME OVER! Otros motivos...");
                Debug.Break();
                break;
        }
    }


    private int GetLevelPrisoners()
    {
        var tempPrisoners = 0;
        var tempo = FindObjectsOfType<minion_spawnpoint>();
        foreach (var comp in tempo)
        {
            tempPrisoners += comp.amount;
        }
        return tempPrisoners;
    }
}
