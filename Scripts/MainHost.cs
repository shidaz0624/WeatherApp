using UnityEngine;
using System.Collections;
using System.Xml;
using System;
public class MainHost : MonoBehaviour {

    private CwbDataCenter           m_CwbDataCenter             = null;
    private LocalConstDataCenter    m_LocalConstDataCenter      = null;
    private XMLParserCenter         m_XMLParserCenter           = null;
    private LocalDataCenter         m_LocalDataCenter           = null;

	#region Mono Life Cycle || Mono 生命週期
	void Start () 
    {
        this.Init();
        this.InitDelegateEvent();
        this.GetWeatherData();
	}
    #endregion

    #region Init || 初始化
    private void Init()
    {
        m_LocalConstDataCenter  = new LocalConstDataCenter();
        m_CwbDataCenter         = new CwbDataCenter();
        m_XMLParserCenter       = new XMLParserCenter();
        m_LocalDataCenter       = LocalDataCenter.Instance;
    }

    private void InitDelegateEvent()
    {
        m_CwbDataCenter.OnGetCwbData = GetData;
        m_XMLParserCenter.OnParserFin = OneWeekDataParserFinish;
    }
    #endregion
	
    #region Main Process || 主要程序
    /// <summary>
    /// 取得天氣資料
    /// </summary>
    private void GetWeatherData()
    {
        string _sUrl = FormatAPIUrl();
        StartCoroutine( m_CwbDataCenter.GetWeatherData(_sUrl) );
    }
    /// <summary>
    /// 拼湊出目標網址.
    /// </summary>
    private string FormatAPIUrl()
    {
        string _sUrl = string.Format( 
            m_LocalConstDataCenter.WeatherAPIUrl , 
            m_LocalConstDataCenter.GetDataType(LocalConstDataCenter.DataType.F_C0032_005) , 
            m_LocalConstDataCenter.Key );
        
        return _sUrl;
    }

    /// <summary>
    /// 成功取得資料後的回傳
    /// </summary>
    public void GetData(string _sData)
    {
        XmlDocument XmlDoc = new XmlDocument( );
        XmlDoc.LoadXml( _sData );

        m_LocalDataCenter.m_OneWeekWeatherInfo = new OneWeekWeatherInfo();
        m_XMLParserCenter.SetDataClass( m_LocalDataCenter.m_OneWeekWeatherInfo );

        //將資料轉拋給function
        m_XMLParserCenter.ParserXML(XmlDoc);
    }

    public void OneWeekDataParserFinish()
    {
        for (int i = 0 ; i < 25 ; i++)
        {
            string _sText = "標題：" + m_LocalDataCenter.m_OneWeekWeatherInfo.m_sTitle + "\n" +
                "更新時間：" + m_LocalDataCenter.m_OneWeekWeatherInfo.m_sUpdateTime + "\n" +
                "地區：" + m_LocalDataCenter.m_OneWeekWeatherInfo.m_LocationsWeatherInfoList[i].m_sLoactionName + "\n";

            for(int x = 0 ; x < 7 ; x ++)
            {
                string _sDay = "第 " + x.ToString() + "天"+ "\n" + 
                "   起始時間：" + m_LocalDataCenter.m_OneWeekWeatherInfo.m_LocationsWeatherInfoList[i].m_WeatherDataAry[x].m_StartTime + "\n" + 
                "   天氣概況：" + m_LocalDataCenter.m_OneWeekWeatherInfo.m_LocationsWeatherInfoList[i].m_WeatherDataAry[x].m_WeatherType + "\n" +
                "   最低溫：" + m_LocalDataCenter.m_OneWeekWeatherInfo.m_LocationsWeatherInfoList[i].m_WeatherDataAry[x].m_iMinTemperature + "\n"+
                "   最高溫：" + m_LocalDataCenter.m_OneWeekWeatherInfo.m_LocationsWeatherInfoList[i].m_WeatherDataAry[x].m_iMaxTemperature +"\n" + 
                "   結束時間：" + m_LocalDataCenter.m_OneWeekWeatherInfo.m_LocationsWeatherInfoList[i].m_WeatherDataAry[x].m_EndTime + "\n";
                _sText += _sDay;
            }
            Debug.Log(_sText);
        }
    }

    #endregion

}
