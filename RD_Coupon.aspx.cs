using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RD_Coupon : System.Web.UI.Page
{
    /*
     * _gameServerId：伺服器ID
    <option value="2001">太陽神</option>		
    <option value="2002">愛神</option>
    <option value="2003">勝利之神</option>
    <option value="2004">美之神</option>
    <option value="2005">天神</option>
    <option value="2006">女神</option>
    <option value="2007">戰神</option>
    <option value="2008">月之女神</option>
    <option value="2009">海神</option>
    <option value="2010">冥王</option>
    <option value="2011">11s 火神</option>
    */
    readonly string _gameServerId = "2008";
    //登錄序號之Web Api的URL
    readonly string _url = "http://asia.nc.com/shop/rk/tw/redeem";
    //遊戲角色名稱
    List<string> _charNames = new List<string> {
        "微熱小菊花", "短劍不夠用", "Zitra"
    }; 

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    //點擊登錄序號按鈕事件
    protected void Button_Submit_Click(object sender, EventArgs e)
    {
        //序號號碼必須輸入
        if (!string.IsNullOrEmpty(this.Input_No.Text.Trim()))
        {
            #region 產生Coupon表格

            #region 欄位名稱
            TableHeaderRow th = new TableHeaderRow();

            TableHeaderCell th_cell0 = new TableHeaderCell();
            th_cell0.Text = "編號";
            th.Cells.Add(th_cell0);

            TableHeaderCell th_cell1 = new TableHeaderCell();
            th_cell1.Text = "名稱";
            th.Cells.Add(th_cell1);

            TableHeaderCell th_cell2 = new TableHeaderCell();
            th_cell2.Text = "結果";
            th.Cells.Add(th_cell2);

            this.Table_Coupon.Rows.Add(th);
            #endregion

            for (int index = 0; index < _charNames.Count; index++)
            {
                string charName = _charNames[index];
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    //{gameServerId: "伺服器ID", charName: "遊戲角色名稱", couponKey: "登錄序號"}
                    string json_send =
                        "{\"gameServerId\":\"" + _gameServerId + "\"," +
                        "\"charName\":\"" + charName + "\"," +
                        "\"couponKey\":\"" + this.Input_No.Text.Trim() + "\"}";

                    streamWriter.Write(json_send);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string result = "";
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                /* JSON回傳字串範例如下
                 * {"ipAddress":"172.16.124.3","resultCode":-1,"resultMessage":"EXCEED_USER_REGISTRATION_LIMIT_ERROR"
                 * ,"nativeSystemDomain":null,"nativeSystemErrorCode":null,"nativeSystemErrorMessage":null,"data":null}
                 */

                //將JSON字串轉成Coupon物件
                Coupon coupon = Newtonsoft.Json.JsonConvert.DeserializeObject<Coupon>(result);

                #region 將JSON取回的結果顯示在Table
                TableRow row = new TableRow();

                TableCell cell0 = new TableCell();
                cell0.Text = (index + 1).ToString();
                row.Cells.Add(cell0);

                TableCell cell1 = new TableCell();
                cell1.Text = charName;
                row.Cells.Add(cell1);

                string resultMessage = "";
                /* 官方回傳的Json.resultMessage值分為以下幾種
                if (resultMessage == "SUCCESS") {
				    alert(messages["JS.Coupon.Delivery.Message"]);
				    window.location.reload(true);
			    } else if (resultMessage == "MAXIMUM_REGISTRABLE_COUNT_ERROR") {
				    alert(messages["JS.Coupon.Maximum.Registrable.Count.Message"]);
			    } else if (resultMessage == "INVALID_COUPON_ERROR") {
				    alert(messages["JS.Coupon.Invalid.Coupon.Message"]);
			    } else if (resultMessage == "EXCEED_USER_REGISTRATION_LIMIT_ERROR") {
				    alert(messages["JS.Coupon.Exceed.User.Registration.Limit.Message"]);	
			    } else if (resultMessage == "INVALID_EFFECTIVE_RANGE_ERROR") {
				    alert(messages["JS.Coupon.Invalid.Effective.Range.Message"]);	
			    } else if (resultMessage == "EMPTY_GAME_SERVER_ID_ERROR") {
				    alert(messages["JS.Coupon.Empty.Server.Message"]);
			    } else if (resultMessage == "EMPTY_GAME_CHARACTER_NAME_ERROR") {
				    alert(messages["JS.Coupon.Empty.Nickname.Message"]);
			    } else if (resultMessage == "NO_GAME_CHARACTER"
				    || resultMessage == "MOBILE_GAME_SERVER_CHECK_VALIDATOR_ERROR"
				    || resultMessage == "MOBILE_GAME_CHARACTER_CHECK_VALIDATOR_ERROR") {
				    alert(messages["JS.Coupon.Error.Server.Character.Message"]);
			    } else {
				    alert(messages["JS.Coupon.Error.System.Message"]);
			    }
                 */
                switch (coupon.ResultMessage)
                {
                    case "SUCCESS":
                        resultMessage = "序號兌換成功";
                        break;

                    case "INVALID_COUPON_ERROR":
                        resultMessage = "序號錯誤";
                        break;

                    case "EXCEED_USER_REGISTRATION_LIMIT_ERROR":
                        resultMessage = "已超過兌換次數";
                        break;

                    default:
                        resultMessage = "系統錯誤";
                        break;
                }

                TableCell cell2 = new TableCell();
                cell2.Text = resultMessage;
                row.Cells.Add(cell2);

                //產生表格
                this.Table_Coupon.Rows.Add(row);

                #endregion
            }

            #endregion
        }
        else
        {
            Response.Write("<script language='javascript'>alert('序號號碼必須輸入');</script>");
        }
    }

    public class Coupon
    {
        public string ResultMessage { get; set; }
    }
}