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

    [Header("Prisioneros")]
    public static int TotalPrisoners;
    public static int PrisonersNeeded;
    public static int PrisonersRescued;
    public static int PrisonersAboard;
    public static int PrisonersLeft;
    public static int PrisonersKilled;

    private void Start()
    {
        _startTime = new DateTime();
        _endTime = new DateTime();
        TimePlayed = new DateTime();
        //
        _startTime = DateTime.Now;
        Lives = 3;
    }

    private void Update()
    {
        AllRescued = PrisonersRescued > PrisonersNeeded;
        
        if (Lives<1) GameOver(0);
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
}
