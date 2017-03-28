using UnityEngine;
using System.Collections;
using System;
using System.Xml;
//XML解析中心
public class XMLParserCenter
{

    public delegate void ParserFin();
    public ParserFin OnParserFin;

    private OneWeekWeatherInfo m_DataClass = null;
    /// <summary>
    /// 實體化xml解析中心
    /// </summary>
    public XMLParserCenter()
    {
        
    }

    public void SetDataClass(OneWeekWeatherInfo _DataClass)
    {
        m_DataClass = _DataClass;
    }

    public void ParserXML(XmlDocument _XMLData)
    {
        XmlNodeList NodeLists = _XMLData.GetElementsByTagName("cwbopendata");
        foreach ( XmlNode OneNode in NodeLists[0].ChildNodes )
        {
            #if DEBUG_LOG
            Debug.LogWarning("Node Name = " + OneNode.Name);
            #endif
            if (OneNode.Name == "sent")
            {
                this.ParserSent(OneNode);
            }
            else if (OneNode.Name == "dataset")
            {
                this.ParserDataset(OneNode);
            }
        }
    }

    // dataset / datasetInfo / location

    /// <summary>
    /// 解析 sent.
    /// </summary>
    private void ParserSent(XmlNode _Node)
    {
        string _sContext = _Node.InnerText;
        m_DataClass.m_sUpdateTime = _sContext;

        #if DEBUG_LOG || DATA_LOG
        Debug.Log("中央氣象局資料更新時間："+ _sContext);
        #endif
    }
    /// <summary>
    /// 解析 dataset.
    /// </summary>
    private void ParserDataset(XmlNode _Node)
    {
        XmlNodeList NodeList = _Node.ChildNodes;
        int _iTime = 0; 
        foreach(XmlNode OneNode in NodeList )
        {
            #if DEBUG_LOG
            Debug.LogWarning( "DataSet Child Node = " + OneNode.Name );
            #endif
            if (OneNode.Name == "datasetInfo")
            {
                ParserDatasetInfo(OneNode);
            }
            else if (OneNode.Name == "location")
            {
                this.ParserLocation( OneNode );
            }
            _iTime ++;
        }
        Debug.LogError("Time = " + _iTime);
        if ( _iTime == NodeList.Count )
        {
            if(OnParserFin != null)
            {
                OnParserFin();
            }
        }
    }
    /// <summary>
    /// 解析 dataset info.
    /// </summary>
    private void ParserDatasetInfo(XmlNode _Node)
    {
        XmlNodeList NodeList = _Node.ChildNodes;
        foreach(XmlNode OneNode in NodeList )
        {
            #if DEBUG_LOG
            Debug.LogWarning( "Dataset Child Node = " + OneNode.Name );
            #endif
            if (OneNode.Name == "datasetDescription")
            {
                m_DataClass.m_sTitle = OneNode.InnerText;

                #if DEBUG_LOG || DATA_LOG
                Debug.Log("類別：" + OneNode.InnerText);
                #endif
            }
        }
    }

    private void ParserLocation(XmlNode _Node)
    {
        XmlNodeList NodeList = _Node.ChildNodes;
        foreach(XmlNode OneNode in NodeList )
        {
            #if DEBUG_LOG 
            Debug.LogWarning( "Location Child Node = " + OneNode.Name );
            #endif
            if (OneNode.Name == "locationName")
            {
                //實體化一個地區天氣資料class，並將之放進總data class
                LocationWeatherInfo _LocationWeatherInfo = new LocationWeatherInfo();
                m_DataClass.m_LocationsWeatherInfoList.Add( _LocationWeatherInfo );
                //設定地區名稱
                _LocationWeatherInfo.m_sLoactionName = OneNode.InnerText;

                #if DEBUG_LOG || DATA_LOG
                Debug.Log("地區名："+OneNode.InnerText);
                #endif
            }
            else if (OneNode.Name == "weatherElement")
            {
                this.ParserWeatherElement(OneNode);
            }
        }
    }

    private void ParserWeatherElement(XmlNode _Node)
    {
        XmlNodeList NodeList = _Node.ChildNodes;
        LocalConstDataCenter.ElementType _Type = LocalConstDataCenter.ElementType.NONE;
        //記錄這次的天數
        int _iDay   = 0;
        foreach(XmlNode OneNode in NodeList )
        {
            #if DEBUG_LOG
            Debug.LogWarning( "WeatherElement Child Node = " + OneNode.Name );
            #endif
            if (OneNode.Name == "elementName")
            {
                //此判斷一定會比time先進入，拿來判斷element為哪一種類型的資料
                _Type = (LocalConstDataCenter.ElementType) Enum.Parse(typeof(LocalConstDataCenter.ElementType), OneNode.InnerText);
            }
            else if (OneNode.Name == "time")
            {
                this.ParserTime( OneNode , _Type , _iDay);
                _iDay ++;
            }
        }
    }

    private void ParserTime(XmlNode _Node ,LocalConstDataCenter.ElementType _Type , int _iDay )
    {
        XmlNodeList NodeList = _Node.ChildNodes;

        //先取得此次要設定的資料index，必定為最後一個
        int _iLocationIndex = m_DataClass.m_LocationsWeatherInfoList.Count - 1;


        foreach(XmlNode OneNode in NodeList )
        {
            
            #if DEBUG_LOG
            Debug.LogWarning( "Time Child Node = " + OneNode.Name );
            #endif
            if (OneNode.Name == "startTime")
            {
                m_DataClass.m_LocationsWeatherInfoList[_iLocationIndex].m_WeatherDataAry[_iDay].m_StartTime = OneNode.InnerText;

                #if DEBUG_LOG || DATA_LOG
//                Debug.Log("開始時間："+OneNode.InnerText);
                #endif
            }
            else if (OneNode.Name == "endTime")
            {   
                m_DataClass.m_LocationsWeatherInfoList[_iLocationIndex].m_WeatherDataAry[_iDay].m_EndTime = OneNode.InnerText;
                #if DEBUG_LOG || DATA_LOG
//                Debug.Log("結束時間："+OneNode.InnerText);
                #endif
            }
            else if (OneNode.Name == "parameter")
            {
                this.ParserParameter(OneNode , _Type , _iLocationIndex , _iDay);
            }

        }
    }

    private void ParserParameter(XmlNode _Node , LocalConstDataCenter.ElementType _Type , int _iLocationIndex , int _iDay)
    {
        XmlNodeList NodeList = _Node.ChildNodes;
        foreach(XmlNode OneNode in NodeList )
        {
            #if DEBUG_LOG
            Debug.LogWarning( "Parameter Child Node = " + OneNode.Name );
            #endif
            if (OneNode.Name == "parameterName")
            {
                string _sText = string.Empty;
                switch (_Type)
                {
                case LocalConstDataCenter.ElementType.Wx:
                    _sText = "天氣概況：";
                    m_DataClass.m_LocationsWeatherInfoList[_iLocationIndex].m_WeatherDataAry[_iDay].m_WeatherType = OneNode.InnerText;
                    break;
                case LocalConstDataCenter.ElementType.MinT:
                    _sText = "最低溫：";
                    m_DataClass.m_LocationsWeatherInfoList[_iLocationIndex].m_WeatherDataAry[_iDay].m_iMinTemperature = OneNode.InnerText;
                    break;
                case LocalConstDataCenter.ElementType.MaxT:
                    _sText = "最高溫：";
                    m_DataClass.m_LocationsWeatherInfoList[_iLocationIndex].m_WeatherDataAry[_iDay].m_iMaxTemperature = OneNode.InnerText;
                    break;
                }
                #if DEBUG_LOG || DATA_LOG
                Debug.Log(_sText + OneNode.InnerText);
                #endif
            }
        }
    }

    private void ParserFinish()
    {
        Debug.LogError("===Fin");
        if (OnParserFin != null)
            OnParserFin();
    }
}
