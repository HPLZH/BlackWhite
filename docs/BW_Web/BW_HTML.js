function GetButton(r,c) {
    return document.getElementById("R"+r+"C"+c);
}

function ChangeColor(r,c){
    var originalClass = GetButton(r,c).className;
    //alert(originalClass == "GW")
    if(originalClass == "GW"){
        GetButton(r,c).className="GB";
        //alert(GetButton(r,c).className)
    }
    else{
        GetButton(r,c).className="GW";
    }
}

function SetColor(r,c,value){
    if(value){
        GetButton(r,c).className="GB";
    }
    else{
        GetButton(r,c).className="GW";
    }
}

function initHTML(size){
    var inGame="";
    //game.innerHTML="";
    for(ir=0;ir<size;ir++){
        inGame=inGame+initRow(ir,size);
    }
    //alert(inGame);
    document.getElementById("Game").innerHTML=inGame;
}

function initButton(r,c){
    return '<td><button class="GW" onclick="buttonClick('+r+','+c+')" id="R'+ r + 'C' + c +'"></button></td>\n';
}

function initRow(r,size){
    var inRow=""
    for(ic=0;ic<size;ic++){
        inRow=inRow+initButton(r,ic);
    }
    return '<tr>\n' + inRow + '</tr>\n'
}

function init(){
    var size=location.search.substring(1);
    //document.writeln(size);
    size=parseInt(size);
    //document.writeln(size);
    if(size<3 || isNaN(size)){
        size=3;
    }
    initHTML(size);
    initGame(size);
}