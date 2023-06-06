using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClockManager : GameManager
{
    public class OnTickEventArgs : EventArgs
    {
        public int gameHour; //this class hold an int and receives the int when the events are invoked. It takes and event argument
    }                           //

    public static event EventHandler<OnTickEventArgs> OnTick;
    public static event EventHandler<OnTickEventArgs> OnTick_6;
    private const float HoursInADay = 24f;
    private const float RealSecondsPerGameHour = 1f;
    [SerializeField] private float minutes;
    private int gameHour;
    private int daysInGame;
    public static bool isMorning { get; private set; }
    public static bool isDay { get; private set; }
    public static bool isEvening { get; private set; }
    public static bool isNight { get; private set; }


    private void Awake()
    {
        gameHour = 0;
    }
    private void Update()
    {
        minutes += Time.deltaTime;

        if(minutes >= RealSecondsPerGameHour)
        {
            minutes -= RealSecondsPerGameHour;
            gameHour++;
            if (OnTick != null) OnTick(this, new OnTickEventArgs { gameHour = gameHour });
            Debug.Log(gameHour);
            if(gameHour % 6 ==0)//this is fancy syntax for if game hour is a mulitple of 6 i.e. 12, 18, 24
            {
                if (OnTick_6 != null) OnTick_6(this, new OnTickEventArgs { gameHour = gameHour });
            }            
            if (gameHour >= HoursInADay)
            {
                daysInGame++;
                gameHour = 0;
            }
            //more logic can go here and events to create various time related events
            //CHECK STAMINA SCRIPT OF EXAMPLE OF HOW TO SUBSCRIBE

        }
    }


}
