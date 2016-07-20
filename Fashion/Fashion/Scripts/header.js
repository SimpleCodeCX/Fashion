
//点击导航栏导航栏切换
function nav_switch(x) {
    document.getElementById("headerNav1").className = "none";
    document.getElementById("headerNav2").className = "none";
    document.getElementById("headerNav3").className = "none";
    document.getElementById("headerNav4").className = "none";
    document.getElementById(x).className = "navli_active";
    if (x == "headerNav1") {
        document.getElementById("home-nav-topjiao").style.display = "block";
        document.getElementById("secondHeader").style.height = "35px";
    }
    else {
        document.getElementById("home-nav-topjiao").style.display = "none";
        document.getElementById("secondHeader").style.height = "6px";
        document.getElementById("secondHeader").style.borderBottom = "1px solid black";

    }
}

/**
 * 鼠标划过就展开子菜单
 */
function dropdownOpen() {
    document.getElementById("userUl").style.display = "block";
    document.getElementById("triangle_symbol").className = "triangle_symbol_open";
}
/**
 * 鼠标移开就关闭子菜单
 */
function dropdownClose() {
    document.getElementById("triangle_symbol").className = "triangle_symbol_close";
    document.getElementById("userUl").style.display = "none";
}
//点击二级导航二级栏导航栏切换
function secondNav_switch(x) {
    document.getElementById("secondNav1").className = "none";
    document.getElementById("secondNav2").className = "none";
    document.getElementById("secondNav3").className = "none";
    document.getElementById("secondNav4").className = "none";
    document.getElementById("secondNav5").className = "none";
    document.getElementById("secondNav6").className = "none";
    document.getElementById(x).className = "secondHeadernNav-active";
}

/*------透明层JS样式----------*/
function question_sousuo() {
    var val = document.getElementById("question_text").value;
    if (val.length <= 0) {
        document.getElementById("xg_question").innerHTML = "";
        document.getElementById("xg_question").className = "";

    }
    else {
        document.getElementById("xg_question").className = "xg_question";
        if (val.indexOf("l") >= 0) {
            document.getElementById("xg_question").innerHTML = "";
            var h6_div = document.createElement('h6');//创建h6
            h6_div.innerHTML = "你想问的问题是不是:";
            document.getElementById("xg_question").appendChild(h6_div);
            var p1 = document.createElement('p');//创建h6
            p1.innerHTML = "<font>女生有LO（LOLITA）装，男士有相对的洋装吗？</font>20个回答";
            document.getElementById("xg_question").appendChild(p1);
            var p2 = document.createElement('p');//创建h6
            p2.innerHTML = "<font>应该如何选购 JK 制服和 LO 装？</font>20个回答";
            document.getElementById("xg_question").appendChild(p2);
            var p3 = document.createElement('p');//创建h6
            p3.innerHTML = "<font>觉得lo装超美，结婚的时候也想穿lo，求各位推荐适合的款或牌子！？国牌日牌都可！</font>20个回答";
            document.getElementById("xg_question").appendChild(p3);
            var p4 = document.createElement('p');//创建h6
            p4.innerHTML = "<font>Lob 发型对脸型有什么要求？想剪 Lob 发型要注意哪些问题？</font>20个回答";
            document.getElementById("xg_question").appendChild(p4);
            var div_new = document.createElement('div');//创建div
            div_new.innerHTML = "不是，我要提一个新问题>>";
            div_new.className = "div_new"
            div_new.onclick = function () { window.location.href = '../../Topic/Post' };
            document.getElementById("xg_question").appendChild(div_new);
        }
        else if (val.indexOf("发") >= 0) {
            document.getElementById("xg_question").innerHTML = "";
            var h6_div = document.createElement('h6');//创建h6
            h6_div.innerHTML = "你想问的问题是不是:";
            document.getElementById("xg_question").appendChild(h6_div);
            var p1 = document.createElement('p');//创建h6
            p1.innerHTML = "<font>圆脸女生适合什么发型？</font>20个回答";
            document.getElementById("xg_question").appendChild(p1);
            var p2 = document.createElement('p');//创建h6
            p2.innerHTML = "<font>男生发型求推荐</font>20个回答";
            document.getElementById("xg_question").appendChild(p2);
            var p3 = document.createElement('p');//创建h6
            p3.innerHTML = "<font>脸大适合什么发型？</font>20个回答";
            document.getElementById("xg_question").appendChild(p3);
            var div_new = document.createElement('div');//创建div
            div_new.innerHTML = "不是，我要提一个新问题>>";
            div_new.className = "div_new";
            div_new.onclick = function () { window.location.href = '../../Topic/Post' };
            document.getElementById("xg_question").appendChild(div_new);
        }
        else {
            document.getElementById("xg_question").innerHTML = "";
            var div_new = document.createElement('div');//创建div
            div_new.innerHTML = "没有找到相关问题，马上提问>>";
            div_new.className = "div_new";
            div_new.id = "div_new_id";
            div_new.onclick = function () { window.location.href = '../../Topic/Post' };
            document.getElementById("xg_question").appendChild(div_new);
        }
    }
}

try {     //关于显示隐藏滚动条js
    var isStyle = document.getElementById("hsScroll").type; //ask_div
}
catch (err) {     //关于显示隐藏滚动条js
    document.write('<style id="ahsScroll" type="text/css">.hScroll{overflow:hidden;} .sScroll{}</style>');
}

function close_ask() {             //关闭提问层JS
    //document.documentElement.className = "sScroll";
    document.getElementById("touming_fugai_div").style.display = "none";
    document.getElementById("ask_div").style.display = "none";
}
function xianshi_ask_div() {       //点击显示提问层js
    //document.documentElement.className = "hScroll";
    document.getElementById("touming_fugai_div").style.display = "block";
    document.getElementById("ask_div").style.display = "block";
    var Y = $(document).scrollTop() + "px";  //获取滚动条到顶部的垂直高度
    document.getElementById("ask_div").style.marginTop = Y;
}