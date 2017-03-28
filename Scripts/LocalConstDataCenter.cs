using UnityEngine;
using System.Collections;

public class LocalConstDataCenter
{
    /// <summary>
    /// 實體化本機資料中心
    /// </summary>
    public LocalConstDataCenter()
    {}

    //檔案資料類別
    public enum DataType
    {
        F_C0032_003,    //台灣區各地區一個禮拜的天氣預報
        F_C0032_005,
    }

    //資料類別
    public enum ElementType
    {
        NONE,
        Wx,     //天氣概況
        MinT,   //最低溫
        MaxT,   //最高溫
    }

    //檔案資料類別轉換
    public string GetDataType(DataType _Type)
    {
        string _sType = (_Type.ToString()).Replace("_","-");
        return _sType;
    }

    //中央氣象局api網址
    private string s_sWeatherAPIUrl = "http://opendata.cwb.gov.tw/opendataapi?dataid={0}&authorizationkey={1}";
    public string WeatherAPIUrl{ get{ return s_sWeatherAPIUrl; } }
    //中央氣象局會員key
    private string s_sKey = "CWB-B8C2CDB1-9832-4C8C-B2A9-D6D2BC6AFF2F";
    public string Key{ get{ return s_sKey; } }


}
