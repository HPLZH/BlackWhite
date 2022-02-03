var blocks = null;
var gameSize;

function initGame(size){
    blocks=[]
    gameSize=size;
    for(ir=0;ir<size;ir++){
        blocks[ir]=[];
        for(ic=0;ic<size;ic++){
            blocks[ir][ic]=false;
        }
    }
}

function buttonClick(r,c){
    //alert(gameSize);
    tryChange(r,c);
    tryChange(r+1,c);
    tryChange(r-1,c);
    tryChange(r,c+1);
    tryChange(r,c-1);
    MakeHash();
}

function tryChange(r,c){
    //alert("R"+r+"C"+c)
    if(r>=0 && c>=0 && r<gameSize && c<gameSize){
        //alert("tryOK");
        ChangeColor(r,c);
        blocks[r][c]= !blocks[r][c];
    }
}

function setBlock(r,c,value){
    SetColor(r,c,value);
    blocks[r][c]=value;
}