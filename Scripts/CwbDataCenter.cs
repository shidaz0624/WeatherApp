using UnityEngine;
using System.Collections;
using System.Xml;
//中央氣象局溝通中心
public class CwbDataCenter
{
    /// <summary>
    /// 實體化中央氣象局溝通中心
    /// </summary>
    public CwbDataCenter()
    {    }

    //回傳取得資料的delegate
    public delegate void GetCwbData( string _sData );
    public GetCwbData OnGetCwbData;

    /// <summary>
    /// 向目標url抓取資料.
    /// </summary>
    public IEnumerator GetWeatherData(string _sUrl)
    {
        Debug.Log( "Start Get Weather ::\n" + _sUrl  );
        WWW _ApiData = new WWW(_sUrl);

        yield return _ApiData;

        if (_ApiData.error == null)
        {
            //資料沒問題
            Debug.Log( _ApiData.text );

            //回傳取得的資料
            if (OnGetCwbData != null)
                OnGetCwbData(_ApiData.text);
        }
        else
            //資料有誤
            Debug.LogError( _ApiData.error );
    }

}

/*
<location>
    <locationName>臺北市</locationName>
    <weatherElement>
        <elementName>Wx</elementName>
        <time>
            <startTime>2016-06-11T12:00:00+08:00</startTime>
            <endTime>2016-06-12T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>陰短暫陣雨或雷雨</parameterName>
                <parameterValue>36</parameterValue>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-12T00:00:00+08:00</startTime>
            <endTime>2016-06-13T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>多雲短暫陣雨或雷雨</parameterName>
                <parameterValue>17</parameterValue>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-13T00:00:00+08:00</startTime>
            <endTime>2016-06-14T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>多雲短暫陣雨或雷雨</parameterName>
                <parameterValue>17</parameterValue>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-14T00:00:00+08:00</startTime>
            <endTime>2016-06-15T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>多雲短暫陣雨或雷雨</parameterName>
                <parameterValue>17</parameterValue>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-15T00:00:00+08:00</startTime>
            <endTime>2016-06-16T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>多雲午後短暫雷陣雨</parameterName>
                <parameterValue>18</parameterValue>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-16T00:00:00+08:00</startTime>
            <endTime>2016-06-17T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>多雲午後短暫雷陣雨</parameterName>
                <parameterValue>18</parameterValue>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-17T00:00:00+08:00</startTime>
            <endTime>2016-06-18T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>多雲午後短暫雷陣雨</parameterName>
                <parameterValue>18</parameterValue>
            </parameter>
        </time>
    </weatherElement>
    <weatherElement>
        <elementName>MaxT</elementName>
        <time>
            <startTime>2016-06-11T12:00:00+08:00</startTime>
            <endTime>2016-06-12T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>30</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-12T00:00:00+08:00</startTime>
            <endTime>2016-06-13T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>33</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-13T00:00:00+08:00</startTime>
            <endTime>2016-06-14T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>34</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-14T00:00:00+08:00</startTime>
            <endTime>2016-06-15T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>34</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-15T00:00:00+08:00</startTime>
            <endTime>2016-06-16T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>35</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-16T00:00:00+08:00</startTime>
            <endTime>2016-06-17T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>35</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-17T00:00:00+08:00</startTime>
            <endTime>2016-06-18T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>35</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
    </weatherElement>
    <weatherElement>
        <elementName>MinT</elementName>
        <time>
            <startTime>2016-06-11T12:00:00+08:00</startTime>
            <endTime>2016-06-12T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>26</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-12T00:00:00+08:00</startTime>
            <endTime>2016-06-13T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>25</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-13T00:00:00+08:00</startTime>
            <endTime>2016-06-14T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>27</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-14T00:00:00+08:00</startTime>
            <endTime>2016-06-15T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>27</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-15T00:00:00+08:00</startTime>
            <endTime>2016-06-16T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>27</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-16T00:00:00+08:00</startTime>
            <endTime>2016-06-17T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>27</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
        <time>
            <startTime>2016-06-17T00:00:00+08:00</startTime>
            <endTime>2016-06-18T00:00:00+08:00</endTime>
            <parameter>
                <parameterName>27</parameterName>
                <parameterUnit>C</parameterUnit>
            </parameter>
        </time>
    </weatherElement>
</location>
*/