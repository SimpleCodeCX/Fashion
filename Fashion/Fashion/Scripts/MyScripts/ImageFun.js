//自己实现的一些javascript函数

//此函数用户获取图片的base64数据
//用于页面：Change_Data.cshtml
//参数，img：图片元素
//返回值：图片的去掉文件头的base64数据
function getBase64Image(img) {
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0, img.width, img.height);
    var dataURL = canvas.toDataURL("image/png");
    return dataURL.substr(dataURL.indexOf(",") + 1)//去掉base64的格式头，因为格式头不属于图片的数据，否则后台将图片的base64保存为图片时会出错
}