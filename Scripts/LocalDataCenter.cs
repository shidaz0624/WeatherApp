using UnityEngine;
using System.Collections;

public class LocalDataCenter 
{
    private static LocalDataCenter _Instance = new LocalDataCenter();
    public  static LocalDataCenter Instance{ get{ return _Instance; } }

    public OneWeekWeatherInfo m_OneWeekWeatherInfo = null;

}
