using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//地區天氣概況資料Class
public class LocationWeatherInfo
{
    public class WeatherData
    {
        public string   m_StartTime         = string.Empty; //開始時間
        public string   m_EndTime           = string.Empty; //結束時間
        public string   m_WeatherType       = string.Empty; //天氣類型
        public string      m_iMaxTemperature   = string.Empty;            //最高溫度
        public string      m_iMinTemperature   = string.Empty;            //最低溫度
    }
    public string       m_sLoactionName     = string.Empty; //地名名稱
    public WeatherData[] m_WeatherDataAry   = null;
    public LocationWeatherInfo()
    {
        m_WeatherDataAry = new WeatherData[7];
        for (int i = 0 ; i < m_WeatherDataAry.Length ; i++)
        {
            m_WeatherDataAry[i] = new WeatherData();
        }
    }
}

public class OneWeekWeatherInfo
{
    public string       m_sTitle            = string.Empty;
    public string       m_sUpdateTime       = string.Empty;
    public List<LocationWeatherInfo>    m_LocationsWeatherInfoList   = null;

    public OneWeekWeatherInfo()
    {
        m_LocationsWeatherInfoList = new List<LocationWeatherInfo>();
    }

}