<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cabinets.aspx.cs" Inherits="Cabinets" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Шкафы</title>
    <style type="text/css">
        .Note
        {
            background-color:Silver;
            font-size:x-small;
            font-weight:bold;
            color:Black;
            text-align:center;
        }
        .Kol
        {
            color:chocolate;
            text-align:right;
        }
        .cl_red
        {
            color:blue;
        }
        .cl_green
        {
            color:green;
        }
        table
        {
            border-style:solid;
            border-width:thin;
            width: 500px;
            height:400px;          
        }
        td {
            border-style: solid;
            border-width: thin;
            text-align: left;
            vertical-align: top;
            background-color: gray;
            cursor: pointer;
            font-weight: bold;
            color: White;
        }
        .tabs { width: 528px; padding: 0px; margin: 0 auto; }
        .tabs>input { display: none; }
        .tabs>div {
            display: none;
            padding: 12px;
            border: 1px solid #C0C0C0;
            background: #FFFFFF;
        }
        .tabs>label {
            display: inline-block;
            padding: 7px;
            margin: 0 -5px -1px 0;
            text-align: center;
            color: #666666;
            border: 1px solid #C0C0C0;
            background: #E0E0E0;
            cursor: pointer;
        }
        .tabs>input:checked + label {
            color: #000000;
            border: 1px solid #C0C0C0;
            border-bottom: 1px solid #FFFFFF;
            background: #FFFFFF;
        }
        #tab_1:checked ~ #txt_1,
        #tab_2:checked ~ #txt_2,
        #tab_3:checked ~ #txt_3,
        #tab_4:checked ~ #txt_4 { display: block; }
    </style>
    <style type="text/css">
    body {
      height: 2000px;
      /* добавим body прокрутку, подсказка должна работать и с прокруткой */
    }

    .tooltip {
      position: fixed;
      padding: 10px 20px;
      border: 1px solid #b3c9ce;
      border-radius: 4px;
      text-align: right;
      font: italic 14px/1.3 sans-serif;
      color: #333;
      background: #fff;
      box-shadow: 3px 3px 3px rgba(0, 0, 0, .3);
    }
  </style>
    <script type="text/javascript">
        //document.write("Шкаф средств защиты");
        var dict = {};
        var color = "black";
        var bufer = "black";
        var ibuf = 0;
        var param = '-1';
        function CC(elem) {
            if (elem.style.backgroundColor != "red") {
                color = elem.style.backgroundColor;
                elem.style.backgroundColor = "pink";
                //document.getElementById("Label").innerText = elem.id;
            }
        }
        function CCB(elem) {
            if (elem.style.backgroundColor != "red") {
                elem.style.backgroundColor = color;
            }
        }
        function Sel(elem) {
            if (color != "red" && elem != ibuf) {
                if (ibuf == 0) {
                    ReSel(elem);
                }
                else {
                    ibuf.style.backgroundColor = bufer;
                    ReSel(elem);
                }
            }
        }
        function ReSel(elem) {
            if (param < 1) { 
            bufer = color;
            ibuf = elem;
            elem.style.backgroundColor = "red"; color = "red";
            GetRadWindow().Close();
            GetRadWindow().BrowserWindow.GetParam(elem.id.slice(2));
        }
        }
        function Initialize() {
            var cells = document.querySelectorAll("td");
            let idd = "0";
            let str_note ="";
            for (let i = 0; i < cells.length; i++) {
            str_note = "";
                let id_cell = document.getElementById(cells[i].id);
                if (id_cell != null)
                    idd = id_cell.id.slice(2);
                    if(idd==48){ str_note = "<div class=\"Note\">Сел.РЭС</div>";}
                    if(idd==49){ str_note = "<div class=\"Note\">Лун.РЭС</div>";}
                    if(idd==50){ str_note = "<div class=\"Note\">Дрог.РЭС</div>";}
                    if(idd==51){ str_note = "<div class=\"Note\">Гор.РЭС</div>";}
                    if(idd==58){ str_note = "<div class=\"Note\">Иван.РЭС</div>";}
                    if(idd==59){ str_note = "<div class=\"Note\">службы</div>";}
                    if(idd==60){ str_note = "<div class=\"Note\">Стол.РЭС</div>";}
                    if(idd==61){ str_note = "<div class=\"Note\">ВысВ.РЭС</div>";}
                id_cell.innerHTML = str_note +"<span>" + idd + "</span><br /><div class=\"Kol\"><a id=\"kol" + idd + "\" >&nbsp;</a></div>";
            }
            var arr = new Array();
            var arr_dict = new Array();
            arr = document.getElementById("HiddenField_Content").value.split(";");
            for (let i = 0; i < arr.length - 1; i++) {
                arr_dict = arr[i].split(":");
                let elem_td = document.getElementById("Td" + arr_dict[0]);
                elem_td.style.backgroundColor = "blue";
                let elem_a = document.getElementById("kol" + arr_dict[0]);
                elem_a.innerText += arr_dict[2];
                if (elem_a.getAttribute('data-tooltip') == null) {
                    elem_a.setAttribute('data-tooltip', arr_dict[1]);
                } else {
                    elem_a.setAttribute('data-tooltip', elem_a.getAttribute('data-tooltip') + "<br />" + arr_dict[1] );
                }
                //document.getElementById("Label_Cell").innerText = document.getElementById("Td5").getAttribute('data-tooltip');
            }
            param = document.getElementById("HiddenField_Cell").value;
            if (param > 0) {
                document.getElementById("Button3").value = "   ЗАКРЫТЬ   ";
                let elem = document.getElementById("Td" + param);
                elem.style.backgroundColor = "red";
                let radioBtn2 = document.getElementById("tab_2");
                if (param >= 30) {
                    radioBtn2.checked = true;
                }
            }
        }
        
    </script>
  <script>
      let tooltipElem;

      document.onmouseover = function (event) {
          let target = event.target;

          // если у нас есть подсказка...
          let tooltipHtml = target.dataset.tooltip;
          if (!tooltipHtml) return;

          // ...создадим элемент для подсказки

          tooltipElem = document.createElement('div');
          tooltipElem.className = 'tooltip';
          tooltipElem.innerHTML = tooltipHtml;
          document.body.append(tooltipElem);

          // спозиционируем его сверху от аннотируемого элемента (top-center)
          let coords = target.getBoundingClientRect();

          let left = coords.left + (target.offsetWidth - tooltipElem.offsetWidth) / 2;
          if (left > document.documentElement.clientWidth - tooltipElem.offsetWidth) left = document.documentElement.clientWidth - tooltipElem.offsetWidth - 5;
          if (left < 0) left = 0; // не заезжать за левый край окна

          let top = coords.top - tooltipElem.offsetHeight - 5;
          if (top < 0) { // если подсказка не помещается сверху, то отображать её снизу
              top = coords.top + target.offsetHeight + 5;
          }

          tooltipElem.style.left = left + 'px';
          tooltipElem.style.top = top + 'px';
      };

      document.onmouseout = function (e) {

          if (tooltipElem) {
              tooltipElem.remove();
              tooltipElem = null;
          }

      };
  </script>
</head>
<body onload="Initialize()">
    <form runat="server">
        <script type="text/javascript">
            function CloseAndRebind(reb) {
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.GetParam("0");
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz az well)
                return oWindow;
            }
        </script>
    <asp:Label ID="InjectScript" runat="server"></asp:Label> 
    <asp:HiddenField ID="HiddenField_Cell" runat="server" Value="-1"/>
    <asp:Label ID="Label_Cell" runat="server" Text=""></asp:Label>
    <asp:HiddenField ID="HiddenField_Content" runat="server" Value=""/>
    <div class="tabs">
    <input type="radio" name="inset" value="" id="tab_1"  checked="checked"/>
    <label for="tab_1">Шкаф №1</label>
    <input type="radio" name="inset" value="" id="tab_2"/>
    <label for="tab_2">Шкаф №2</label>
        <div id="txt_1">
            <table>
                <tr>
                    <td id="Td1" style="width:25%"  onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        1</td>
                    <td id="Td2" style="width:25%" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)" >
                        2</td>
                    <td id="Td3" style="width:25%" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)" >
                        3</td>
                    <td id="Td4" style="width:25%" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)" >
                        4</td>
                </tr>
                <tr>
                    <td id="Td5" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        5</td>
                    <td id="Td6" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        6</td>
                    <td id="Td7" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        7</td>
                    <td id="Td8" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        8</td>
                </tr>
                <tr>
                    <td id="Td9" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        9</td>
                    <td id="Td10" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        10</td>
                    <td rowspan="2"  id="Td11" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        11</td>
                    <td rowspan="7" id="Td12" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        12</td>
                </tr>
                <tr>
                    <td id="Td13" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        13</td>
                    <td id="Td14" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        14</td>
                </tr>
                <tr>
                    <td id="Td15" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        15</td>
                    <td id="Td16" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        16</td>
                    <td rowspan="2" id="Td17" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        17</td>
                </tr>
                <tr>
                    <td id="Td18" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        18</td>
                    <td id="Td19" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        19</td>
                </tr>
                <tr>
                    <td id="Td20" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        20</td>
                    <td id="Td21" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        21</td>
                    <td rowspan="2" id="Td22" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        22</td>
                </tr>
                <tr>
                    <td id="Td23" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        23</td>
                    <td id="Td24" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        24</td>
                </tr>
                <tr>
                    <td id="Td25" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        25</td>
                    <td id="Td26" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        26</td>
                    <td id="Td27" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        27</td>
                </tr>
            </table>
        </div>
        <div id="txt_2">
            <table>
                <tr>
                    <td id="Td31" style="width:11%" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        31</td>
                    <td id="Td32" style="width:11%" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        32</td>
                    <td id="Td33" style="width:11%" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        33</td>
                    <td id="Td34" style="width:11%" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        34</td>
                    <td id="Td35" style="width:11%" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        35</td>
                    <td id="Td36" style="width:11%" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        36</td>
                    <td id="Td37" style="width:11%" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        37</td>
                    <td id="Td38" style="width:11%" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        38</td>
                    <td style="width:12%" rowspan="6" id="Td39" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        39</td>
                </tr>
                <tr>
                    <td id="Td40" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        40</td>
                    <td id="Td41" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        41</td>
                    <td id="Td42" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        42</td>
                    <td id="Td43" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        43</td>
                    <td id="Td44" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        44</td>
                    <td id="Td45" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        45</td>
                    <td id="Td46" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        46</td>
                    <td id="Td47" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        47</td>
                </tr>
                <tr>
                    <td rowspan="2" id="Td48" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        </td>
                    <td rowspan="2" id="Td49" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        49</td>
                    <td rowspan="2" id="Td50" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        50</td>
                    <td rowspan="2" id="Td51" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        51</td>
                    <td id="Td52" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        52</td>
                    <td id="Td53" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        53</td>
                    <td rowspan="4" id="Td54" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        54</td>
                    <td rowspan="4" id="Td55" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        55</td>
                </tr>
                <tr>
                    <td id="Td56" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        56</td>
                    <td id="Td57" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        57</td>
                </tr>
                <tr>
                    <td rowspan="2" id="Td58" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        58</td>
                    <td rowspan="2" id="Td59" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        59</td>
                    <td rowspan="2" id="Td60" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        60</td>
                    <td rowspan="2" id="Td61" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        61</td>
                    <td id="Td62" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        62</td>
                    <td id="Td63" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        63</td>
                </tr>
                <tr>
                    <td id="Td64" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        64</td>
                    <td id="Td65" onmouseover="CC(this)" onmouseout="CCB(this)" onclick="Sel(this)">
                        65</td>
                </tr>
            </table>
        </div>
    </div>
    <div style="text-align:center">
        <asp:Button id="Button3" runat="server" ForeColor="White" Font-Italic="True" Font-Bold="True"
            Text="  ОТМЕНА  " OnClientClick="CloseAndRebind(false)" BorderStyle="Outset" BackColor="DarkGray"></asp:Button>
    </div> 
</form>
</body>
</html>
