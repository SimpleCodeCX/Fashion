//自己实现的一些跟登录有关的函数或变量
//注意：1.引用本js需要引用jquery包


//用户已经登录，显示用户已登录信息块(block)，并且显示用户头像， 并且隐藏注册和登录的信息块(block)
//参数：loginBlock用户登录信息块的id，当用户登录成功时显示
//             unLoginBlock用户未登录信息块的id，当用户未登录时显示
//             img_touXiangId用户头像元素的Id
//             img_touXiangIdUrl用户头像的url地址
//用于页面：_Layout.cshtml  top.cshtml

function LoginYesDisplay(loginBlock, unLoginBlock) {   
        document.getElementById(loginBlock).style.display = "block";
        document.getElementById(unLoginBlock).style.display = "none";
        //$(img_touXiang).attr('src', img_touXiangIdUrl);//显示个人头像
}



//用户未登录，显示注册和登录的信息块(block) ，隐藏用户已登录信息块(block)
//参数：loginBlock用户登录信息块的id，当用户登录成功时显示
//             unLoginBlock用户未登录信息块的id，当用户未登录时显示
//用于页面：_Layout.cshtml  top.cshtml
function UnLoginDisplay( loginBlock, unLoginBlock) {
    document.getElementById(unLoginBlock).style.display = "block";
    document.getElementById(loginBlock).style.display = "none";
}

///结合以上LoginYesDisplay和UnLoginDisplay的函数
function IsLoginDisplay(isLogin, loginBlock, unLoginBlock) {
    if (isLogin == "1") {
        document.getElementById(loginBlock).style.display = "block";
        document.getElementById(unLoginBlock).style.display = "none";
    }
    else {
        document.getElementById(unLoginBlock).style.display = "block";
        document.getElementById(loginBlock).style.display = "none";
    }
    
}

