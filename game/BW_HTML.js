function GetButton(r, c) {
    return document.getElementById("R" + r + "C" + c);
}

function ChangeColor(r, c) {
    var originalClass = GetButton(r, c).className;
    if (originalClass == "GW") {
        GetButton(r, c).className = "GB";
    }
    else {
        GetButton(r, c).className = "GW";
    }
}

function SetColor(r, c, value) {
    if (value) {
        GetButton(r, c).className = "GB";
    }
    else {
        GetButton(r, c).className = "GW";
    }
}

function initHTML(size) {
    var inGame = "";
    for (ir = 0; ir < size; ir++) {
        inGame = inGame + initRow(ir, size);
    }
    document.getElementById("Game").innerHTML = inGame;
}

function initButton(r, c) {
    return '<td><button class="GW" onclick="buttonClick(' + r + ',' + c + ')" id="R' + r + 'C' + c + '"></button></td>\n';
}

function initRow(r, size) {
    var inRow = ""
    for (ic = 0; ic < size; ic++) {
        inRow = inRow + initButton(r, ic);
    }
    return '<tr>\n' + inRow + '</tr>\n'
}

function init() {
    let params = new URLSearchParams(location.search);
    if (params.has("size")) {
        var size = params.get("size");
    }
    else {
        size = NaN
    }
    size = parseInt(size);
    if (!(size >= 3)) {
        location.search = "size=" + prompt("请输入游戏尺寸 (整数)\n例: 3x3 -> 3")
    }
    initHTML(size);
    initGame(size);
}